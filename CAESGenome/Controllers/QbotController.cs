using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            if (jobType.Id == (int)JobTypeIds.QbotColonyPicking)
            {
                result = SaveColonyPicking(postModel);
            }

            if (result)
            {
                Message = "Saved!";
                return RedirectToAction("Index", "Authorized");
            }

            var user = GetCurrentUser();
            var viewModel = QbotViewModel.Create(_repositoryFactory, user, jobType);
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

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                return true;
            }

            return false;
        }
        private void ValidateColonyPicking(QbotPostModel postModel)
        {
            if (postModel.PlateType == null)
            {
                ModelState.AddModelError("PostModel.PlateType", "Plate Type is required.");
            }

            if (postModel.Vector == null)
            {
                ModelState.AddModelError("PostModel.Vector", "Vector is required.");
            }
            
            if(postModel.Vector != null && postModel.Vector.Name.ToLower() == "other")
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

            if (postModel.Strain == null)
            {
                ModelState.AddModelError("PostModel.Strain", "Host is required.");
            }

            if (postModel.Strain != null && postModel.Strain.Name.ToLower() == "other")
            {
                if(string.IsNullOrEmpty(postModel.NewStrain))
                {
                    ModelState.AddModelError("PostModel.NewStrain", "New Host Name is required.");
                }

                if(postModel.Bacteria != null)
                {
                    ModelState.AddModelError("PostModel.Bacteria", "Bacter is required.");
                }
            }
        }
    }
}
