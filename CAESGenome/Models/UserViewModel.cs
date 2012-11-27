using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using WebMatrix.WebData;

namespace CAESGenome.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        
        public List<RechargeAccount> RechargeAccounts { get; set; }

        public IList<SelectListItem> Departments { get; set; }
        public IList<SelectListItem> Universities { get; set; }
        public bool IsPi { get; set; }
        public bool IsStaff { get; set; }

        public static UserViewModel CreateForUser(IRepositoryFactory repositoryFactory, string currentUserId, User user = null)
        {
            var viewModel = new UserViewModel()
                {
                    User = user ?? new User(),
                    RechargeAccounts = repositoryFactory.RechargeAccountRepository.Queryable.Where(a => a.User.UserName == currentUserId).ToList()
                };

            return viewModel;
        }

        public static UserViewModel CreateForPi(IRepositoryFactory repositoryFactory, User user = null)
        {
            var viewModel = new UserViewModel()
                {
                    User = user ?? new User(),
                    Universities = repositoryFactory.UniversityRepository.Queryable.OrderBy(a => a.Name)
                                        .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString() })
                                        .ToList(),
                    Departments = new List<SelectListItem>()
                };

            if (user != null)
            {
                if (user.University != null)
                {
                    viewModel.Departments =
                        repositoryFactory.DepartmentRepository.Queryable.Where(a => a.University == user.University)
                            .OrderBy(a => a.Name)
                            .Select(a => new SelectListItem() {Text = a.Name, Value = a.Id.ToString()})
                            .ToList();

                    var uni = viewModel.Universities.FirstOrDefault(a => a.Value == user.University.Id.ToString());
                    uni.Selected = true;
                }

                if (user.Department != null)
                {
                    var dept = viewModel.Departments.FirstOrDefault(a => a.Value == user.Department.Id.ToString());
                    dept.Selected = true;
                }

                viewModel.IsPi = Roles.IsUserInRole(RoleNames.PI);
                viewModel.IsStaff = Roles.IsUserInRole(RoleNames.Staff);
            }

            return viewModel;
        }
    }
}