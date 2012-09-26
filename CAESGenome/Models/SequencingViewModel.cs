using System;
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
        public SelectList RechargeAccounts { get; set; }
        public SelectList PlateTypes { get; set; }
        public SelectList SequenceDirections { get; set; }
        public SelectList Strains { get; set; }
        public SelectList Primers { get; set; }
        public SelectList Primers2 { get; set; }
        public SelectList Vectors { get; set; }
        public SelectList Antibiotics { get; set; }
        public SelectList Bacterias { get; set; }

        // dna submission only
        public SelectList DnaJobTypes { get; set; }

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
                var rid = postModel != null && postModel.RechargeAccount != null ? postModel.RechargeAccount.Id : -1;
                viewModel.RechargeAccounts = new SelectList(user.RechargeAccounts, "Id", "AccountNum", rid);

                // shared for bacterial clone, dna submission, user run sequencing
                if (jobType.Id == (int)JobTypeIds.BacterialClone || jobType.Id == (int)JobTypeIds.DnaSubmission || jobType.Id == (int)JobTypeIds.UserRunSequencing)
                {
                    var pts = new List<SelectListItem>();
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.NinetySix).ToString(), Text = "96" });
                    pts.Add(new SelectListItem() { Value = ((int)Core.Resources.PlateTypes.ThreeEightyFour).ToString(), Text = "384" });
                    viewModel.PlateTypes = new SelectList(pts, "Value", "Text");
                }

                // shared for bacterial clone, dna submission
                if (jobType.Id == (int)JobTypeIds.BacterialClone || jobType.Id == (int)JobTypeIds.DnaSubmission)
                {
                    var pid1 = postModel != null && postModel.Primer1 != null ? postModel.Primer1.Id : -1;
                    viewModel.Primers = new SelectList(repositoryFactory.PrimerRepository.Queryable.Where(a => a.Supplied), "Id", "Name", pid1);
                }

                // only for baacterial clone
                if (jobType.Id == (int)JobTypeIds.BacterialClone)
                {
                    var sd = new List<SelectListItem>();
                    sd.Add(new SelectListItem() { Value = ((int)SequenceDirection.Forward).ToString(), Text = "One" });
                    sd.Add(new SelectListItem() { Value = ((int)SequenceDirection.Backward).ToString(), Text = "Two" });
                    viewModel.SequenceDirections = new SelectList(sd, "Value", "Text");

                    var sid = postModel != null && postModel.Strain != null ? postModel.Strain.Id : -1;
                    viewModel.Strains = new SelectList(repositoryFactory.StrainRepository.Queryable.Where(a => a.Supplied), "Id", "Name", sid);

                    var pid2 = postModel != null && postModel.Primer2 != null ? postModel.Primer2.Id : -1;
                    viewModel.Primers2 = new SelectList(repositoryFactory.PrimerRepository.Queryable.Where(a => a.Supplied), "Id", "Name", pid2);

                    var vid = postModel != null && postModel.Vector != null ? postModel.Vector.Id : -1;
                    viewModel.Vectors = new SelectList(repositoryFactory.VectorRepository.Queryable.OrderByDescending(a => a.Name), "Id", "Name", vid);

                    var aid = postModel != null && postModel.Antibiotic != null ? postModel.Antibiotic.Id : -1;
                    viewModel.Antibiotics = new SelectList(repositoryFactory.AntibioticRepository.Queryable.OrderBy(a => a.Name), "Id", "Name", aid);

                    var bid = postModel != null && postModel.Bacteria != null ? postModel.Bacteria.Id : -1;
                    viewModel.Bacterias = new SelectList(repositoryFactory.BacteriaRepository.Queryable, "Id", "Name", bid);
                }

                if (jobType.Id == (int)JobTypeIds.DnaSubmission)
                {
                    var jid = postModel != null && postModel.JobType != null ? postModel.JobType.Id : -1;
                    viewModel.DnaJobTypes = new SelectList(repositoryFactory.JobTypeRepository.Queryable.Where(a => a.DNASequencing), "Id", "Name", jid);
                }
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
        public SequencingPostModel()
        {
            NumPlates = 1;
        }

        // shared fields
        public User User { get; set; }
        public JobType JobType { get; set; }
        [Required]
        [Display(Name="Recharge Account")]
        [UIHint("DropDownList")]
        public RechargeAccount RechargeAccount { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Job Name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        // standard sequencing jobs
        [Display(Name="Plate Type")]
        public PlateTypes? PlateType { get; set; }
        [Display(Name="Number of Plates")]
        [Range(0, 100)]
        public int NumPlates { get; set; }
        [Display(Name = "Plate Names")]
        public List<string> PlateNames { get; set; }

        // bacterial clone
        [Display(Name="Sequence Direction")]
        public SequenceDirection? SequenceDirection { get; set; }
        [Display(Name="Host")]
        public Strain Strain { get; set; }
        [Display(Name="New Host")]
        public string NewStrain { get; set; }
        public Bacteria Bacteria { get; set; }

        [Display(Name="Primer One")]
        public Primer Primer1 { get; set; }
        [Display(Name="Primer Two")]
        public Primer Primer2 { get; set; }
        public Vector Vector { get; set; }
        public Antibiotic Antibiotic { get; set; }
    }
}