using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Filters;
using CAESGenome.Models;

namespace CAESGenome.Controllers
{
    [UserOnly]
    public class SequencingController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public SequencingController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Create a sequencing job submission
        /// </summary>
        /// <param name="id">Job Type Id</param>
        /// <returns></returns>
        public ActionResult Create(int? id)
        {
            JobType jobType = null;
            if (id.HasValue)
            {
                jobType = _repositoryFactory.JobTypeRepository.GetNullableById(id.Value);

                // check the job type
                if (!jobType.StandardSequencing && !jobType.CustomSequencing)
                {
                    Message = "Invalid job type specified";
                    return RedirectToAction("Create");
                }
            }

            var user = GetCurrentUser();
            var viewModel = SequencingViewModel.Create(_repositoryFactory, user, jobType);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(int? id, SequencingPostModel postModel)
        {
            JobType jobType = null;
            if (id.HasValue)
            {
                jobType = _repositoryFactory.JobTypeRepository.GetNullableById(id.Value);

                // check the job type
                if (!jobType.StandardSequencing && !jobType.CustomSequencing)
                {
                    Message = "Invalid job type specified";
                    return RedirectToAction("Create");
                }

                // all job types are assignble except dna submission
                if (jobType.Id != (int)JobTypeIds.DnaSubmission)
                {
                    postModel.JobType = jobType;
                }
            }

            var result = false;

            switch(jobType.Id)
            {
                case (int)JobTypeIds.BacterialClone: 
                    result = SaveBacterialClone(postModel);
                    break;
                case (int)JobTypeIds.DnaSubmission:
                    result = SaveDnaSubmission(postModel);
                    break;
                case (int)JobTypeIds.UserRunSequencing:
                    result = SaveUserRunSubmission(postModel);
                    break;
                case (int)JobTypeIds.Sublibrary:
                    result = SaveSublibrarySubmission(postModel);
                    break;
            }
            
            if (result)
            {
                Message = "Your job request has been successfully submitted.";
                return RedirectToAction("Index", "Authorized");
            }

            var user = GetCurrentUser();
            var viewModel = SequencingViewModel.Create(_repositoryFactory, user, jobType, postModel);
            return View(viewModel);
        }

        private bool SaveBacterialClone(SequencingPostModel postModel)
        {
            ValidateBacterialClone(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobBacterialClone = new UserJobBacterialClone();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobBacterialClone);
                userJob.UserJobBacterialClone = userJobBacterialClone;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                AddPlates(postModel.PlateNames, userJob, userJob.JobType);
                
                if (postModel.Strain != null && postModel.Strain.IsOther())
                {
                    var strain = new Strain() { Name = postModel.NewStrain, Bacteria = postModel.Bacteria, Supplied = false };
                    userJob.UserJobBacterialClone.Strain = strain;
                }

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateBacterialClone(SequencingPostModel postModel)
        {
            if (postModel.PlateType == null)
            {
                ModelState.AddModelError("PostModel.PlateType", "Plate Type is required.");
            }

            if (postModel.SequenceDirection == null)
            {
                ModelState.AddModelError("PostModel.SequenceDirection", "Sequence Direction is required.");
            }

            if (postModel.Primer1 == null)
            {
                ModelState.AddModelError("PostModel.Primer1", "Primer One is required.");
            }

            if (postModel.SequenceDirection.HasValue && postModel.SequenceDirection == SequenceDirection.Backward && postModel.Primer2 == null)
            {
                ModelState.AddModelError("PostModel.Primer2", "Primer Two is required.");
            }

            if (postModel.Vector == null)
            {
                ModelState.AddModelError("PostModel.Vector", "Vector is required.");
            }

            if (postModel.Antibiotic == null)
            {
                ModelState.AddModelError("PostModel.Antibiotic", "Antibiotic is required.");
            }

            if (postModel.Strain == null)
            {
                ModelState.AddModelError("PostModel.Strain", "Host is required.");
            }

            if (postModel.Strain != null && postModel.Strain.IsOther())
            {
                if (string.IsNullOrEmpty(postModel.NewStrain))
                {
                    ModelState.AddModelError("PostModel.NewStrain", "Name for new Host is required.");
                }

                if (postModel.Bacteria == null)
                {
                    ModelState.AddModelError("PostModel.Bacteria", "Bacteria for new Host is required.");
                }
            }

            if (postModel.NumPlates <= 0)
            {
                ModelState.AddModelError("PostModel.NumPlates", "More than one plate is required.");
            }

            if (postModel.PlateNames != null && postModel.PlateNames.Count(a => !string.IsNullOrEmpty(a)) < postModel.NumPlates)
            {
                ModelState.AddModelError("PostModel.PlateNames", "Please specify a name for each plate.");
            }
        }

        private bool SaveDnaSubmission(SequencingPostModel postModel)
        {
            ValidateDnaSubmission(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobDna = new UserJobDna();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobDna);
                userJob.UserJobDna = userJobDna;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                AddPlates(postModel.PlateNames, userJob, userJob.JobType);

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateDnaSubmission(SequencingPostModel postModel)
        {
            if (postModel.JobType == null)
            {
                ModelState.AddModelError("PostModel.JobType", "Job Type is required.");
            }

            if (postModel.PlateType == null)
            {
                ModelState.AddModelError("PostModel.PlateType", "Plate Type is required.");
            }

            if (postModel.Primer1 == null)
            {
                ModelState.AddModelError("PostModel.Primer1", "Primer One is required.");
            }

            if (postModel.NumPlates <= 0)
            {
                ModelState.AddModelError("PostModel.NumPlates", "More than one plate is required.");
            }

            if (postModel.PlateNames != null && postModel.PlateNames.Count(a => !string.IsNullOrEmpty(a)) < postModel.NumPlates)
            {
                ModelState.AddModelError("PostModel.PlateNames", "Please specify a name for each plate.");
            }
        }

        private bool SaveUserRunSubmission(SequencingPostModel postModel)
        {
            ValidateUserRunSubmission(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobUserRun = new UserJobUserRun();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobUserRun);
                userJob.UserJobUserRun = userJobUserRun;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                AddPlates(postModel.PlateNames, userJob, userJob.JobType);

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateUserRunSubmission(SequencingPostModel postModel)
        {
            if (postModel.PlateType == null)
            {
                ModelState.AddModelError("PostModel.PlateType", "Plate Type is required.");
            }

            if (postModel.Dye == null)
            {
                ModelState.AddModelError("PostModel.Dye", "Dye is required.");
            }

            if (postModel.NumPlates <= 0)
            {
                ModelState.AddModelError("PostModel.NumPlates", "More than one plate is required.");
            }

            if (postModel.PlateNames != null && postModel.PlateNames.Count(a => !string.IsNullOrEmpty(a)) < postModel.NumPlates)
            {
                ModelState.AddModelError("PostModel.PlateNames", "Please specify a name for each plate.");
            }
        }

        private bool SaveSublibrarySubmission(SequencingPostModel postModel)
        {
            ValidateSublibrarySubmission(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobSublibrary = new UserJobSublibrary();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobSublibrary);
                userJob.UserJobSublibrary = userJobSublibrary;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                AddPlates(new List<string>() {userJob.Name}, userJob, userJob.JobType);

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateSublibrarySubmission(SequencingPostModel postModel)
        {
            if (!postModel.TypeOfSample.HasValue)
            {
                ModelState.AddModelError("PostModel.TypesOfSample", "Type of Sample is required.");
            }

            if (postModel.TypeOfSample == TypeOfSamples.DNA && !postModel.Concentration.HasValue)
            {
                ModelState.AddModelError("PostModel.Concentration", "DNA Concentration is required.");
            }

            if (postModel.TypeOfSample == TypeOfSamples.BAC && postModel.Vector == null)
            {
                ModelState.AddModelError("PostModel.Vector", "Vector is required.");
            }

            if (!postModel.GenomeSize.HasValue)
            {
                ModelState.AddModelError("PostModel.GenomeSize", "Genome Size is required.");
            }

            if (!postModel.Coverage.HasValue)
            {
                ModelState.AddModelError("PostModel.Coverage", "Coverage is required.");
            }

            if (postModel.Antibiotic == null)
            {
                ModelState.AddModelError("PostModel.Antibiotic", "Antibiotic is required.");
            }
        }

        /// <summary>
        /// Adds the plates necessary for the original submission
        /// </summary>
        /// <param name="plateNames"></param>
        /// <param name="userJob"></param>
        private void AddPlates(List<string> plateNames, UserJob userJob, JobType jobType)
        {
            // figure out the stage
            Stage stage = null;

            switch(jobType.Id)
            {
                case (int)JobTypeIds.BacterialClone:
                    stage = _repositoryFactory.StageRepository.GetNullableById(StageIds.BcWebSubmittedPlates);
                    break;
                case (int)JobTypeIds.PCRProduct:
                    stage = _repositoryFactory.StageRepository.GetNullableById(StageIds.PcrWebSubmittedPlates);
                    break;
                case (int)JobTypeIds.UserRunSequencing:
                    stage = _repositoryFactory.StageRepository.GetNullableById(StageIds.UrsWebSubmittedPlates);
                    break;
                case (int)JobTypeIds.PurifiedDna:
                    stage = _repositoryFactory.StageRepository.GetNullableById(StageIds.PdWebSubmittedPlates);
                    break;
                case (int)JobTypeIds.Sublibrary:
                    stage = _repositoryFactory.StageRepository.GetNullableById(StageIds.SlWebSubmittedPlates);
                    break;
            }

            if (stage == null)
            {
                throw new ArgumentException("JobType");
            }

            foreach(var name in plateNames)
            {
                var plate = new UserJobPlate() {Name = name};
                var barcode = new Barcode() {Stage = stage};

                plate.AddBarcode(barcode);
                userJob.AddUserJobPlates(plate);
            }
        }
    }
}
