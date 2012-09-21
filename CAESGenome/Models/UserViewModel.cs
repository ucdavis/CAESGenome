using System.Collections.Generic;
using System.Linq;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;

namespace CAESGenome.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<RechargeAccount> RechargeAccounts { get; set; }

        public static UserViewModel Create(IRepositoryFactory repositoryFactory, string currentUserId, User user = null)
        {
            var viewModel = new UserViewModel()
                {
                    User = user ?? new User(),
                    RechargeAccounts = repositoryFactory.RechargeAccountRepository.Queryable.Where(a => a.User.UserName == currentUserId).ToList()
                };

            return viewModel;
        }

    }
}