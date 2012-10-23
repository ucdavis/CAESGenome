using CAESGenome.Core.Domain;
using UCDArch.Core.PersistanceSupport;

namespace CAESGenome.Core.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<Antibiotic> AntibioticRepository { get; set; } 
        IRepository<Bacteria> BacteriaRepository { get; set; }
        IRepository<Barcode> BarcodeRepository { get; set; }
        IRepository<BarcodeFile> BarcodeFileRepository { get; set; }
        IRepository<Department> DepartmentRepository { get; set; }
        IRepository<Dye> DyeRepository { get; set; }
        IRepository<Equipment> EquipmentRepository { get; set; }
        IRepository<EquipmentReservation> EquipmentReservationRepository { get; set; } 
        IRepository<JobType> JobTypeRepository { get; set; }
        IRepository<Primer> PrimerRepository { get; set; } 
        IRepository<RechargeAccount> RechargeAccountRepository { get; set; }
        IRepositoryWithTypedId<Stage, string> StageRepository { get; set; }
        IRepository<Strain> StrainRepository { get; set; } 
        IRepository<University> UniversityRepository { get; set; } 
        IRepository<User> UserRepository { get; set; }
        IRepository<UserJob> UserJobRepository { get; set; }
        IRepository<UserJobBacterialClone> UserJobBacterialCloneRepository { get; set; }
        IRepository<UserJobDna> UserJobDnaRepository { get; set; }
        IRepository<UserJobGenotyping> UserJobGenotypingRepository { get; set; }
        IRepository<UserJobPlate> UserJobPlateRepository { get; set; }
        IRepository<UserJobQbotColonyPicking> UserJobQbotColonyPickingRepository { get; set; }
        IRepository<UserJobQbotReplicating> UserJobQbotReplicatingRepository { get; set; }
        IRepository<UserJobSublibrary> UserJobSublibraryRepository { get; set; } 
        IRepository<UserJobUserRun> UserJobUserRunRepository { get; set; }
        IRepository<Vector> VectorRepository { get; set; }
        IRepository<VectorType> VectorTypeRepository { get; set; }
    }

    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<Antibiotic> AntibioticRepository { get; set; }
        public IRepository<Bacteria> BacteriaRepository { get; set; }
        public IRepository<Barcode> BarcodeRepository { get; set; }
        public IRepository<BarcodeFile> BarcodeFileRepository { get; set; }
        public IRepository<Department> DepartmentRepository { get; set; }
        public IRepository<Dye> DyeRepository { get; set; }
        public IRepository<Equipment> EquipmentRepository { get; set; }
        public IRepository<EquipmentReservation> EquipmentReservationRepository { get; set; }
        public IRepository<JobType> JobTypeRepository { get; set; }
        public IRepository<Primer> PrimerRepository { get; set; }
        public IRepository<RechargeAccount> RechargeAccountRepository { get; set; }
        public IRepositoryWithTypedId<Stage, string> StageRepository { get; set; }
        public IRepository<Strain> StrainRepository { get; set; }
        public IRepository<University> UniversityRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<UserJob> UserJobRepository { get; set; }
        public IRepository<UserJobBacterialClone> UserJobBacterialCloneRepository { get; set; }
        public IRepository<UserJobDna> UserJobDnaRepository { get; set; }
        public IRepository<UserJobGenotyping> UserJobGenotypingRepository { get; set; }
        public IRepository<UserJobPlate> UserJobPlateRepository { get; set; }
        public IRepository<UserJobQbotColonyPicking> UserJobQbotColonyPickingRepository { get; set; }
        public IRepository<UserJobQbotReplicating> UserJobQbotReplicatingRepository { get; set; }
        public IRepository<UserJobSublibrary> UserJobSublibraryRepository { get; set; }
        public IRepository<UserJobUserRun> UserJobUserRunRepository { get; set; }
        public IRepository<Vector> VectorRepository { get; set; }
        public IRepository<VectorType> VectorTypeRepository { get; set; }
    }
}
