using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Filters;

namespace CAESGenome.Controllers
{
    [UserOnly]
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

        public ActionResult OpenJobs()
        {
            var user = GetCurrentUser();
            var jobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.User.Id == user.Id);
            return View(jobs);
        }

    }
}
