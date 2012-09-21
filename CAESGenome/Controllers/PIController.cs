using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.PI)]
    public class PIController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public PIController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        //
        // GET: /PI/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            var currentUser = GetCurrentUser();
            var users = _repositoryFactory.UserRepository.Queryable.Where(a => a.ParentUser.Id == currentUser.Id);
            return View(users);
        }

        public ActionResult CreateUser()
        {
            var viewModel = UserViewModel.Create(_repositoryFactory, CurrentUser.Identity.Name);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _repositoryFactory.UserRepository.EnsurePersistent(user);
                Message = "User has been created.";
                return RedirectToAction("Users");
            }

            var viewModel = UserViewModel.Create(_repositoryFactory, CurrentUser.Identity.Name, user);
            return View(viewModel);
        }

        public ActionResult RechargeAccounts()
        {
            var accounts = GetCurrentUser(true).OwnedRechargeAcccounts;
            return View(accounts);
        }

    }
}
