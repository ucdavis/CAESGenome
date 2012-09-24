using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Helpers;

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

        public ActionResult CreateRechargeAccount()
        {
            return View(RechargeAccountViewModel.Create());
        }

        [HttpPost]
        public ActionResult CreateRechargeAccount(RechargeAccount rechargeAccount)
        {
            rechargeAccount.User = GetCurrentUser(true);
            ModelState.Clear();
            rechargeAccount.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _repositoryFactory.RechargeAccountRepository.EnsurePersistent(rechargeAccount);
                Message = "Recharge Account has been saved.";
                return RedirectToAction("RechargeAccounts");
            }

            return View(RechargeAccountViewModel.Create(rechargeAccount));
        }

        public ActionResult EditRechargeAccount(int id)
        {
            var rechargeAccount = _repositoryFactory.RechargeAccountRepository.GetNullableById(id);

            if (rechargeAccount == null)
            {
                Message = "Recharge account could not be found.";
                return RedirectToAction("RechargeAccounts");
            }

            return View(RechargeAccountViewModel.Create(rechargeAccount));
        }

        [HttpPost]
        public ActionResult EditRechargeAccount(int id, RechargeAccount rechargeAccount)
        {
            var rechargeAccountToEdit = _repositoryFactory.RechargeAccountRepository.GetNullableById(id);

            if (rechargeAccountToEdit == null)
            {
                Message = "Recharge account could not be found.";
                return RedirectToAction("RechargeAccounts");
            }

            AutoMapper.Mapper.Map(rechargeAccount, rechargeAccountToEdit);
            ModelState.Clear();
            rechargeAccountToEdit.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _repositoryFactory.RechargeAccountRepository.EnsurePersistent(rechargeAccountToEdit);
                Message = "Recharge Account has been saved.";
                return RedirectToAction("RechargeAccounts");
            }

            return View(RechargeAccountViewModel.Create(rechargeAccountToEdit));
        }
    }
}
