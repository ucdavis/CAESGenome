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
            }

            ValidateSequencingModel(jobType, postModel, ModelState);

            if (ModelState.IsValid)
            {
                Message = "Was valid for saving, saved!";
            }

            var user = GetCurrentUser();
            var viewModel = SequencingViewModel.Create(_repositoryFactory, user, jobType, postModel);
            return View(viewModel);
        }

        private void ValidateSequencingModel(JobType jobType, SequencingPostModel postModel, ModelStateDictionary modelState)
        {
            if (jobType.Id == (int)JobTypeIds.BacterialClone)
            {
                ValidateBacterialClone(postModel, modelState);
            }
        }

        private void ValidateBacterialClone(SequencingPostModel postModel, ModelStateDictionary modelState)
        {
            
        }
    }
}
