using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;

namespace CAESGenome.Controllers
{
    [Authorize(Roles=RoleNames.User)]
    public class AuthorizedController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public AuthorizedController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult OpenJobs()
        //{
        //    var user = GetCurrentUser();
        //    var jobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.User.Id == user.Id);
        //    return View(jobs);
        //}

        //public ActionResult CompletedJobs()
        //{
        //    var user = GetCurrentUser();
        //    var jobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.User.Id == user.Id && !a.IsOpen);
        //    return View(jobs);
        //}

    }
}
