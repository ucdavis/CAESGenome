﻿using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.User)]
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

                postModel.JobType = jobType;
            }

            ValidateSequencingModel(jobType, postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobBacterialClone = new UserJobBacterialClone();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobBacterialClone);
                userJob.UserJobBacterialClone = userJobBacterialClone;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                foreach(var name in postModel.PlateNames)
                {
                    userJob.AddUserJobPlates(new UserJobPlate(){Name = name});  
                }

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                Message = "Your job request has been successfully submitted.";
                return RedirectToAction("Index", "Authorized");
            }

            var user = GetCurrentUser();
            var viewModel = SequencingViewModel.Create(_repositoryFactory, user, jobType, postModel);
            return View(viewModel);
        }

        private void ValidateSequencingModel(JobType jobType, SequencingPostModel postModel)
        {
            if (jobType.Id == (int)JobTypeIds.BacterialClone)
            {
                ValidateBacterialClone(postModel);
            }
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
                ModelState.AddModelError("PostModel.Primer", "Primer is required.");
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

            if (postModel.NumPlates <= 0)
            {
                ModelState.AddModelError("PostModel.NumPlates", "More than one plate is required.");
            }

            if (postModel.PlateNames != null && postModel.PlateNames.Count(a => !string.IsNullOrEmpty(a)) < postModel.NumPlates)
            {
                ModelState.AddModelError("PostModel.PlateNames", "Please specify a name for each plate.");
            }
        }
    }
}
