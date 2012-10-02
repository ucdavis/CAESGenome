using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Resources;

namespace CAESGenome.Models
{
    public class UserJobPostModelBase
    {
        // shared fields
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
    }

    public class SequencingPostModel : UserJobPostModelBase
    {
        public SequencingPostModel()
        {
            NumPlates = 1;
        }

        // standard sequencing jobs
        [Display(Name = "Plate Type")]
        public PlateTypes? PlateType { get; set; }
        [Display(Name = "Number of Plates")]
        [Range(0, 100)]
        public int NumPlates { get; set; }
        [Display(Name = "Plate Names")]
        public List<string> PlateNames { get; set; }

        // bacterial clone
        [Display(Name = "Sequence Direction")]
        public SequenceDirection? SequenceDirection { get; set; }
        [Display(Name = "Host")]
        public Strain Strain { get; set; }
        [Display(Name = "New Host")]
        public string NewStrain { get; set; }
        public Bacteria Bacteria { get; set; }

        [Display(Name = "Primer One")]
        public Primer Primer1 { get; set; }
        [Display(Name = "Primer Two")]
        public Primer Primer2 { get; set; }
        public Vector Vector { get; set; }
        public Antibiotic Antibiotic { get; set; }

        // user run sequencing
        public Dye Dye { get; set; }

        // sublibrary jobs
        [Display(Name = "Type of Samples")]
        public TypeOfSamples? TypeOfSample { get; set; }
        [Display(Name = "DNA Concentration")]
        public float? Concentration { get; set; }
        [Display(Name = "Insert Genome Size")]
        public float? GenomeSize { get; set; }
        public int? Coverage { get; set; }
    }

    public class QbotPostModel : UserJobPostModelBase
    {
        [Required]
        [Display(Name = "Plate Type")]
        public PlateTypes? PlateType { get; set; }
        [Required]
        [Display(Name = "Host")]
        public Strain Strain { get; set; }
        [Required]
        public Vector Vector { get; set; }

        [Display(Name="Q-Tray(s) Qty.")]
        public int? NumQTrays { get; set; }
        [Display(Name="Glycerol Qty.")]
        public int? NumGlycerol { get; set; }
        [Display(Name="Glycerol Concentration")]
        public string Concentration { get; set; }
        public int? Replication { get; set; }
        [Display(Name="Colonies Expected")]
        public int? NumColonies { get; set; }

        // shared fields for replicating or gridding
        [Display(Name = "New Vector")]
        public string NewVector { get; set; }
        [Display(Name = "Vector Type")]
        public VectorType VectorType { get; set; }
        [Display(Name="New Vector Antibiotic 1")]
        public Antibiotic Antibiotic1 { get; set; }
        [Display(Name = "New Vector Antibiotic 2")]
        public Antibiotic Antibiotic2 { get; set; }
        [Display(Name = "New Host")]
        public string NewStrain { get; set; }
        public Bacteria Bacteria { get; set; }

        // specific for replicating
        [Display(Name="# Source Plates")]
        public int? SourcePlates { get; set; }
        [Display(Name="Destination Plate Type")]
        public PlateTypes? DestinationPlateType { get; set; }
        [Display(Name="# Copies")]
        public int? NumCopies { get; set; }
    }

    public class GenotypingPostModel : UserJobPostModelBase
    {
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