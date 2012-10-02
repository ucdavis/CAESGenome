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

        public ActionResult Details(int id)
        {
            var uj = _repositoryFactory.UserJobRepository.GetNullableById(id);
            
            if (uj == null)
            {
                Message = "Job could not be located, please try again.";
                return RedirectToAction("Index");
            }

            return View(uj);
        }

    }
}
