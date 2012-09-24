using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Antibiotic : DomainObject
    {
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
    }

    public class AntibioticMap : ClassMap<Antibiotic>
    {
        public AntibioticMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
