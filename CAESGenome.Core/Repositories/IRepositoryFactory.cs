using CAESGenome.Core.Domain;
using UCDArch.Core.PersistanceSupport;

namespace CAESGenome.Core.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<Department> DepartmentRepository { get; set; }
        IRepository<RechargeAccount> RechargeAccountRepository { get; set; } 
        IRepository<University> UniversityRepository { get; set; } 
        IRepository<User> UserRepository { get; set; }
    }

    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<Department> DepartmentRepository { get; set; }
        public IRepository<RechargeAccount> RechargeAccountRepository { get; set; }
        public IRepository<University> UniversityRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
    }
}
