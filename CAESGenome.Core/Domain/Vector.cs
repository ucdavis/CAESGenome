using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Vector : DomainObject
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Vector")]
        public virtual string Name { get; set; }

        public virtual VectorType VectorType { get; set; }
        public virtual Antibiotic Antibiotic1 { get; set; }
        public virtual Antibiotic Antibiotic2 { get; set; }

        public virtual bool IsOther()
        {
            return Name.ToLower() == "other";
        }
    }

    public class VectorMap : ClassMap<Vector>
    {
        public VectorMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            References(x => x.VectorType);
            References(x => x.Antibiotic1).Column("Antibiotic1Id");
            References(x => x.Antibiotic2).Column("Antibiotic2Id");

        }
    }
}
