using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;

namespace CAESGenome.Controllers
{
    public class QbotController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public QbotController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public ActionResult Create(int? id)
        {
            JobType jobType = null;
            if (id.HasValue)
            {
                jobType = _repositoryFactory.JobTypeRepository.GetNullableById(id.Value);

                // check the job type
                if (!jobType.Qbot)
                {
                    Message = "Invalid job type specified";
                    return RedirectToAction("Create");
                }
            }

            var user = GetCurrentUser();
            var viewModel = QbotViewModel.Create(_repositoryFactory, user, jobType);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(int? id, QbotPostModel postModel)
        {
            JobType jobType = null;
            if (id.HasValue)
            {
                jobType = _repositoryFactory.JobTypeRepository.GetNullableById(id.Value);

                // check the job type
                if (!jobType.Qbot)
                {
                    Message = "Invalid job type specified";
                    return RedirectToAction("Create");
                }

                postModel.JobType = jobType;
            }

            var result = false;
            switch (jobType.Id)
            {
                case (int)JobTypeIds.QbotColonyPicking:
                    result = SaveColonyPicking(postModel);
                    break;
                case (int)JobTypeIds.QbotPlateReplicating:
                    result = SaveReplicating(postModel);
                    break;
                case (int)JobTypeIds.QbotGridding:
                    result = SaveGridding(postModel);
                    break;
            }
            
            if (result)
            {
                Message = "Saved!";
                return RedirectToAction("Index", "Authorized");
            }

            var user = GetCurrentUser();
            var viewModel = QbotViewModel.Create(_repositoryFactory, user, jobType, postModel);
            return View(viewModel);
        }

        private bool SaveColonyPicking(QbotPostModel postModel)
        {
            ValidateColonyPicking(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobColonyPicking = new UserJobQbotColonyPicking();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobColonyPicking);
                userJob.UserJobQbotColonyPicking = userJobColonyPicking;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                if (postModel.Strain.IsOther())
                {
                    var strain = new Strain() { Name = postModel.NewStrain, Bacteria = postModel.Bacteria, Supplied = false };
                    userJob.UserJobQbotColonyPicking.Strain = strain;
                }

                if (postModel.Vector.IsOther())
                {
                    var vector = new Vector() {Name = postModel.NewVector, VectorType = postModel.VectorType, Antibiotic1 = postModel.Antibiotic1, Antibiotic2 = postModel.Antibiotic2};
                    userJob.UserJobQbotColonyPicking.Vector = vector;
                }

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateColonyPicking(QbotPostModel postModel)
        {
            if (!postModel.PlateType.HasValue)
            {
                ModelState.AddModelError("PostModel.PlateType", "Plate Type is required.");
            }

            if (!postModel.Replication.HasValue)
            {
                ModelState.AddModelError("PostModel.Replication", "Replication value is required.");
            }

            if (!postModel.NumColonies.HasValue)
            {
                ModelState.AddModelError("PostModel.NumColonies", "Colonies expected is required.");
            }

            ValidateVector(postModel);
            ValidateStrain(postModel);
        }

        private bool SaveReplicating(QbotPostModel postModel)
        {
            ValidateReplicating(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobReplicating = new UserJobQbotReplicating();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobReplicating);
                userJob.UserJobQbotReplicating = userJobReplicating;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                if (postModel.Strain.IsOther())
                {
                    var strain = new Strain() { Name = postModel.NewStrain, Bacteria = postModel.Bacteria, Supplied = false };
                    userJob.UserJobQbotReplicating.Strain = strain;
                }

                if (postModel.Vector.IsOther())
                {
                    var vector = new Vector() { Name = postModel.NewVector, VectorType = postModel.VectorType, Antibiotic1 = postModel.Antibiotic1, Antibiotic2 = postModel.Antibiotic2 };
                    userJob.UserJobQbotReplicating.Vector = vector;
                }

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateReplicating(QbotPostModel postModel)
        {
            if (!postModel.PlateType.HasValue)
            {
                ModelState.AddModelError("PostModel.PlateType", "Plate type is required.");
            }

            if (!postModel.DestinationPlateType.HasValue)
            {
                ModelState.AddModelError("PostModel.DestinationPlateType", "Destination Plate type is required.");
            }

            if (!postModel.SourcePlates.HasValue)
            {
                ModelState.AddModelError("PostModel.SourcePlates", "Number of source plates is required.");
            }
            else if(postModel.SourcePlates.Value <= 0)
            {
                ModelState.AddModelError("PostModel.SourcePlates", "You must specify at least one source plate.");
            }

            if (!postModel.NumCopies.HasValue)
            {
                ModelState.AddModelError("PostModel.NumCopies", "Number of copies is required.");
            }
            else if (postModel.NumCopies.Value <= 0)
            {
                ModelState.AddModelError("PostModel.NumCopies", "You must specify at least one copy.");
            }

            ValidateVector(postModel);
            ValidateStrain(postModel);
        }

        private bool SaveGridding(QbotPostModel postModel)
        {
            ValidateGridding(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();
                var userJobQbotGridding = new UserJobQbotGridding();

                AutoMapper.Mapper.Map(postModel, userJob);
                AutoMapper.Mapper.Map(postModel, userJobQbotGridding);
                userJob.UserJobQbotGridding = userJobQbotGridding;
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                if (postModel.Vector.IsOther())
                {
                    var vector = new Vector() { Name = postModel.NewVector, VectorType = postModel.VectorType, Antibiotic1 = postModel.Antibiotic1, Antibiotic2 = postModel.Antibiotic2 };
                    userJob.UserJobQbotGridding.Vector = vector;
                }

                if (postModel.Strain.IsOther())
                {
                    var strain = new Strain() { Name = postModel.NewStrain, Bacteria = postModel.Bacteria, Supplied = false };
                    userJob.UserJobQbotGridding.Strain = strain;
                }

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateGridding(QbotPostModel postModel)
        {
            if (!postModel.NumPlates.HasValue)
            {
                ModelState.AddModelError("PostModel.NumPlates", "Number of plates is required.");
            }
            else if (postModel.NumPlates.Value <= 0)
            {
                ModelState.AddModelError("PostModel.NumPlates", "You must specify at least one plate.");
            }

            if (!postModel.NumMembranecopies.HasValue)
            {
                ModelState.AddModelError("PostModel.NumMembranecopies", "Number of membrane copies is required.");
            }
            else if (postModel.NumMembranecopies.Value <= 0)
            {
                ModelState.AddModelError("PostModel.NumMembranecopies", "You must specify at least one membrane copy.");
            }

            if (!postModel.GriddingPattern.HasValue)
            {
                ModelState.AddModelError("PostModel.GriddingPattern", "Gridding pattern is required.");
            }

            ValidateVector(postModel);
            ValidateStrain(postModel);
        }

        private void ValidateVector(QbotPostModel postModel)
        {
            if (postModel.Vector == null)
            {
                ModelState.AddModelError("PostModel.Vector", "Vector is required.");
            }

            if (postModel.Vector != null && postModel.Vector.IsOther())
            {
                if (string.IsNullOrEmpty(postModel.NewVector))
                {
                    ModelState.AddModelError("PostModel.NewVector", "New Vector name is required.");
                }

                if (postModel.VectorType == null)
                {
                    ModelState.AddModelError("PostModel.VectorType", "Vector Type is required.");
                }

                if (postModel.Antibiotic1 == null)
                {
                    ModelState.AddModelError("PostModel.Antibiotic1", "Antibiotic is required.");
                }

                if (postModel.Antibiotic2 == null)
                {
                    ModelState.AddModelError("PostModel.Antibiotic2", "Antibiotic is required.");
                }
            }  
        }
        private void ValidateStrain(QbotPostModel postModel)
        {
            if (postModel.Strain == null)
            {
                ModelState.AddModelError("PostModel.Strain", "Host is required.");
            }

            if (postModel.Strain != null && postModel.Strain.IsOther())
            {
                if (string.IsNullOrEmpty(postModel.NewStrain))
                {
                    ModelState.AddModelError("PostModel.NewStrain", "New Host Name is required.");
                }

                if (postModel.Bacteria == null)
                {
                    ModelState.AddModelError("PostModel.Bacteria", "Bacteria is required.");
                }
            }
        }

    }
}
