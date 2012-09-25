using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.User)]
    public class UserJobController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UserJobController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public ActionResult Index()
        {
            var user = GetCurrentUser();
            var userJobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.User.Id == user.Id);
            return View(userJobs);
        }

        /// <summary>
        /// Create a sequencing job submission
        /// </summary>
        /// <param name="id">Job Type Id</param>
        /// <returns></returns>
        public ActionResult CreateSequencing(int? id)
        {
            JobType jobType = null;
            if (id.HasValue)
            {
                jobType = _repositoryFactory.JobTypeRepository.GetNullableById(id.Value);

                // check the job type
                if (!jobType.StandardSequencing && !jobType.CustomSequencing)
                {
                    Message = "Invalid job type specified";
                    return RedirectToAction("CreateSequencing");
                }
            }

            var user = GetCurrentUser();
            var viewModel = SequencingViewModel.Create(_repositoryFactory, user, jobType);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateSequencing(int? id, SequencingViewModel postModel)
        {
            return View();
        }

        /// <summary>
        /// Create a genotyping job submission
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateGenotyping()
        {
            return View();
        }

        /// <summary>
        /// Create a qbot job submission
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateQbot()
        {
            return View();
        }

    }
}
