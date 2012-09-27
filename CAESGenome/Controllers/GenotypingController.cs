using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;
using System.Linq;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.User)]
    public class GenotypingController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public GenotypingController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// 
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
                if (!jobType.Genotyping)
                {
                    Message = "Invalid job type specified";
                    return RedirectToAction("Create");
                }
            }

            var user = GetCurrentUser();
            var viewModel = GenotypingViewModel.Create(_repositoryFactory, user, jobType);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(int? id, GenotypingPostModel postModel)
        {
            JobType jobType = null;
            if (id.HasValue)
            {
                jobType = _repositoryFactory.JobTypeRepository.GetNullableById(id.Value);

                // check the job type
                if (!jobType.Genotyping)
                {
                    Message = "Invalid job type specified";
                    return RedirectToAction("Create");
                }

                postModel.JobType = jobType;
            }

            ValidateGenotyping(postModel);

            if (ModelState.IsValid)
            {
                var userJob = new UserJob();

                AutoMapper.Mapper.Map(postModel, userJob);
                userJob.UserJobGenotyping = new UserJobGenotyping();
                userJob.User = GetCurrentUser(true);
                userJob.RechargeAccount = postModel.RechargeAccount;

                foreach(var did in postModel.Dyes)
                {
                    userJob.UserJobGenotyping.Dyes.Add(_repositoryFactory.DyeRepository.GetById(did));
                }

                foreach (var name in postModel.PlateNames)
                {
                    userJob.AddUserJobPlates(new UserJobPlate() { Name = name });
                }

                _repositoryFactory.UserJobRepository.EnsurePersistent(userJob);

                Message = "Your job request  has been successfully submitted.";
                return RedirectToAction("Index", "Authorized");
            }

            var user = GetCurrentUser();
            var viewModel = GenotypingViewModel.Create(_repositoryFactory, user, jobType, postModel);
            return View(viewModel);
        }

        private void ValidateGenotyping(GenotypingPostModel postModel)
        {
            if (postModel.NumPlates <= 0)
            {
                ModelState.AddModelError("PostModel.NumPlates", "More than one plate is required.");
            }

            if (postModel.PlateNames != null && postModel.PlateNames.Count(a => !string.IsNullOrEmpty(a)) < postModel.NumPlates)
            {
                ModelState.AddModelError("PostModel.PlateNames", "Please specify a name for each plate.");
            }

            if (postModel.Dyes.Count <= 0)
            {
                ModelState.AddModelError("PostModel.Dyes", "At least one dye must be specified.");
            }
        }
    }
}
