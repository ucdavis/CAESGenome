using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;

namespace CAESGenome.Models
{
    public class SequencingViewModel
    {
        public List<JobType> JobTypes { get; set; }
        public JobType JobType { get; set; }
        public SequencingPostModel PostModel { get; set; }

        // lists for drop downs
        public List<SelectListItem> RechargeAccounts { get; set; }
        public List<KeyValuePair<PlateTypes, string>> PlateTypes { get; set; }
        public List<KeyValuePair<SequenceDirection, string>> SequenceDirections { get; set; }
        public IEnumerable<SelectListItem> Strains { get; set; }
        public IEnumerable<SelectListItem> Primers { get; set; }
        public IEnumerable<SelectListItem> Vectors { get; set; }
        public IEnumerable<SelectListItem> Antibiotics { get; set; }
        public IEnumerable<SelectListItem> Bacterias { get; set; }

        public static SequencingViewModel Create(IRepositoryFactory repositoryFactory, User user, JobType jobType = null, SequencingPostModel postModel = null)
        {
            var viewModel = new SequencingViewModel()
                {
                    JobType = jobType,
                    JobTypes = jobType == null ? repositoryFactory.JobTypeRepository.Queryable.Where(a => a.StandardSequencing || a.CustomSequencing).ToList() : new List<JobType>(),
                    PostModel = postModel ?? new SequencingPostModel()
                };

            if (jobType != null)
            {
                viewModel.RechargeAccounts = user.RechargeAccounts.Select(a => new SelectListItem(){Text = a.AccountNum, Value = a.Id.ToString()}).ToList();
            }

            if (jobType != null && jobType.Id == (int)JobTypeIds.BacterialClone)
            {
                viewModel.PlateTypes = new List<KeyValuePair<PlateTypes, string>>();
                viewModel.PlateTypes.Add(new KeyValuePair<PlateTypes, string>(Core.Resources.PlateTypes.NinetySix, "96"));
                viewModel.PlateTypes.Add(new KeyValuePair<PlateTypes, string>(Core.Resources.PlateTypes.ThreeEightyFour, "384"));

                viewModel.SequenceDirections = new List<KeyValuePair<SequenceDirection, string>>();
                viewModel.SequenceDirections.Add(new KeyValuePair<SequenceDirection, string>(SequenceDirection.Forward, "Forward"));
                viewModel.SequenceDirections.Add(new KeyValuePair<SequenceDirection, string>(SequenceDirection.Backward, "Backward"));

                var sid = postModel != null && postModel.Strain != null ? postModel.Strain.Id : -1;
                viewModel.Strains = repositoryFactory.StrainRepository.Queryable
                                        .Where(a => a.Supplied)
                                        .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == sid});

                var pid = postModel != null && postModel.Primer != null ? postModel.Primer.Id : -1;
                viewModel.Primers = repositoryFactory.PrimerRepository.Queryable
                                        .Where(a => a.Supplied)
                                        .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == pid }).ToList();

                var vid = postModel != null && postModel.Vector != null ? postModel.Vector.Id : -1;
                viewModel.Vectors = repositoryFactory.VectorRepository.Queryable
                                        .OrderBy(a => a.Name)
                                        .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == vid }).ToList();

                var aid = postModel != null && postModel.Antibiotic != null ? postModel.Antibiotic.Id : -1;
                viewModel.Antibiotics = repositoryFactory.AntibioticRepository.Queryable
                                            .OrderBy(a => a.Name)
                                            .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == aid }).ToList();

                var bid = postModel != null && postModel.Bacteria != null ? postModel.Bacteria.Id : -1;
                viewModel.Bacterias = repositoryFactory.BacteriaRepository.Queryable
                                            .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == bid }).ToList();
            }

            return viewModel;
        }

        public bool JobTypeSelected()
        {
            return JobType != null;
        }
    }

    public class SequencingPostModel
    {
        // shared fields
        public User User { get; set; }
        public JobType JobType { get; set; }
        [Required]
        public RechargeAccount RechargeAccount { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        // standard sequencing jobs
        [Display(Name="Plate Type")]
        public PlateTypes PlateType { get; set; }
        [Display(Name="Number of Plates")]
        [Range(0, 100)]
        public int NumPlates { get; set; }
        [Display(Name = "Plate Names")]
        public List<string> PlateNames { get; set; }

        // bacterial clone
        [Display(Name="Sequence Direction")]
        public SequenceDirection SequenceDirection { get; set; }
        [Display(Name="Host")]
        public Strain Strain { get; set; }
        [Display(Name="New Host")]
        public string NewStrain { get; set; }
        public Bacteria Bacteria { get; set; }

        public Primer Primer { get; set; }
        public Vector Vector { get; set; }
        public Antibiotic Antibiotic { get; set; }
    }
}