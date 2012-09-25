using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Strain : DomainObject
    {
        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }
        public virtual bool Supplied { get; set; }
        [Required]
        public virtual Bacteria Bacteria { get; set; }
    }

    public class StrainMap : ClassMap<Strain>
    {
        public StrainMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Supplied);
            References(x => x.Bacteria);
        }
    }
}
