using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;

namespace CAESGenome.Models
{
    public class GenotypingViewModel
    {
        public List<JobType> JobTypes { get; set; }
        public JobType JobType { get; set; }
        public GenotypingPostModel PostModel { get; set; }

        public SelectList RechargeAccounts { get; set; }
        public SelectList PlateTypes { get; set; }
        public MultiSelectList Dyes { get; set; }

        public static GenotypingViewModel Create(IRepositoryFactory repositoryFactory, User user, JobType jobType = null, GenotypingPostModel postModel = null)
        {
            var viewModel = new GenotypingViewModel()
                {
                    JobType = jobType,
                    JobTypes = jobType == null ? repositoryFactory.JobTypeRepository.Queryable.Where(a => a.Genotyping).ToList() : new List<JobType>(),
                    PostModel = postModel ?? new GenotypingPostModel() {NumPlates = 1}
                };

            if (jobType != null)
            {
                var rid = postModel != null && postModel.RechargeAccount != null ? postModel.RechargeAccount.Id : -1;
                viewModel.RechargeAccounts = new SelectList(user.RechargeAccounts, "Id", "AccountNum", rid);

                var pts = new List<SelectListItem>();
                pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.NinetySix).ToString(), Text = "96" });
                pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.ThreeEightyFour).ToString(), Text = "384" });
                viewModel.PlateTypes = new SelectList(pts, "Value", "Text");

                var did = postModel != null && postModel.Dyes != null ? postModel.Dyes : new List<int>();
                viewModel.Dyes = new MultiSelectList(repositoryFactory.DyeRepository.Queryable.Where(a => a.Genotyping), "Id", "Name", did);
            }

            return viewModel;
        }

        public bool JobTypeSelected()
        {
            return JobType != null;
        }
    }

    public class GenotypingPostModel
    {
        public User User { get; set; }
        public JobType JobType { get; set; }
        [Required]
        [Display(Name = "Recharge Account")]
        public RechargeAccount RechargeAccount { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Job Name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        [Required]
        [Display(Name = "Plate Type")]
        public PlateTypes PlateType { get; set; }
        [Display(Name = "Number of Plates")]
        [Range(1, 100)]
        public int NumPlates { get; set; }
        [Display(Name = "Plate Names")]
        public List<string> PlateNames { get; set; }
        public List<int> Dyes { get; set; }
    }
}