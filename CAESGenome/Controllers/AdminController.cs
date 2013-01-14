using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Helpers;
using WebMatrix.WebData;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class AdminController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public AdminController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        //
        // GET: /Admin/
        public ActionResult Users()
        {
            var role = Repository.OfType<Role>().Queryable.First(a => a.Name == RoleNames.PI);
            var pis = _repositoryFactory.UserRepository.Queryable.Where(a => a.Roles.Contains(role)).ToList();
            return View(pis);
        }

        public ActionResult CreateUser()
        {
            var viewModel = UserViewModel.CreateForPi(_repositoryFactory);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUser(User user, string password, string retypePassword, List<string> roles)
        {
            if (user.University == null)
            {
                ModelState.AddModelError("Univerrsity", "University is required");
            }

            if (user.Department == null)
            {
                ModelState.AddModelError("Department", "Department is Required");
            }

            if (roles == null || !roles.Any())
            {
                ModelState.AddModelError("Roles", "At least one role is required");
            }

            if (password != retypePassword)
            {
                ModelState.AddModelError("Password", "Passwords do not match.");
            }

            if (ModelState.IsValid)
            {
                if (!WebSecurity.UserExists(user.UserName))
                {
                    WebSecurity.CreateUserAndAccount(user.UserName, password, new { FirstName = user.FirstName, LastName = user.LastName, Title = user.Title, Phone = user.Phone, fax = user.Fax, UniversityId = user.University.Id , DepartmentId = user.Department.Id });    
                }

                foreach(var role in roles) Roles.AddUserToRole(user.UserName, role);

                Message = "User has been created.";
                return RedirectToAction("Users");
            }

            var viewModel = UserViewModel.CreateForPi(_repositoryFactory, user);
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

            var viewModel = UserViewModel.CreateForPi(_repositoryFactory, user);
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

            ModelState.Clear();
            userToEdit.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _repositoryFactory.UserRepository.EnsurePersistent(userToEdit);
                Message = "User has been updated";
                return RedirectToAction("Users");
            }

            var viewModel = UserViewModel.CreateForPi(_repositoryFactory, user);
            return View(viewModel);
        }

        public JsonNetResult LoadDepartments(int id)
        {
            var depts = _repositoryFactory.DepartmentRepository.Queryable.Where(a => a.University.Id == id);
            return new JsonNetResult (depts.Select(a => new SelectListItem(){Value = a.Id.ToString(), Text = a.Name}).ToList());
        }
    }
}
