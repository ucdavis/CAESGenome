using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Helpers;
using WebMatrix.WebData;

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
            var viewModel = UserViewModel.CreateForUser(_repositoryFactory, CurrentUser.Identity.Name);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUser(User user, string password, string retypePassword)
        {
            user.ParentUser = GetCurrentUser(true);

            if (password != retypePassword)
            {
                ModelState.AddModelError("Password", "Passwords do not match.");
            }

            if (ModelState.IsValid)
            {
                if (!WebSecurity.UserExists(user.UserName))
                {
                    // create the user
                    WebSecurity.CreateUserAndAccount(user.UserName, password, new { FirstName = user.FirstName, LastName = user.LastName, Title = user.Title, Phone = user.Phone, fax = user.Fax, parentUserId = user.ParentUser.Id });    
                }
                
                Roles.AddUserToRole(user.UserName, RoleNames.User);    

                Message = "User has been created.";
                return RedirectToAction("Users");
            }

            var viewModel = UserViewModel.CreateForUser(_repositoryFactory, CurrentUser.Identity.Name, user);
            return View(viewModel);
        }

        public ActionResult EditUser(int id)
        {
            var user = _repositoryFactory.UserRepository.GetNullableById(id);

            if (user == null)
            {
                Message = "User was not found";
                return RedirectToAction("Users");
            }

            var viewModel = UserViewModel.CreateForUser(_repositoryFactory, CurrentUser.Identity.Name, user);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditUser(int id, User user, string password, string retypePassword)
        {
            var userToEdit = _repositoryFactory.UserRepository.GetNullableById(id);

            if (userToEdit == null)
            {
                Message = "User was not found";
                return RedirectToAction("Users");
            }

            // copy the main fields
            AutoMapper.Mapper.Map(user, userToEdit);
            
            // reconcile the recharge codes
            UpdateRechargeAccounts(userToEdit.RechargeAccounts, user.RechargeAccounts);
            
            ModelState.Clear();
            userToEdit.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _repositoryFactory.UserRepository.EnsurePersistent(userToEdit);
                Message = "User has been updated";
                return RedirectToAction("Users");
            }

            var viewModel = UserViewModel.CreateForUser(_repositoryFactory, CurrentUser.Identity.Name, user);
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

        /// <summary>
        /// Reconciles the list and makes sure that the updated list is the correct one written
        /// </summary>
        /// <param name="original"></param>
        /// <param name="newList"></param>
        private void UpdateRechargeAccounts(IList<RechargeAccount> original, IList<RechargeAccount> newList) 
        {
            // to delete
            var delete = original.Where(a => !newList.Select(b => b.Id).Contains(a.Id)).ToList();
            // add add
            var add = newList.Where(a => !original.Select(b => b.Id).Contains(a.Id)).ToList();

            foreach(var ra in delete)
            {
                original.Remove(ra);
            }

            foreach(var ra in add)
            {
                original.Add(ra);
            }
        }
    }
}
