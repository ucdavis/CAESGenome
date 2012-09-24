using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Bacteria : DomainObject
    {
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
    }

    public class BacteriaMap : ClassMap<Bacteria>
    {
        public BacteriaMap()
        {
            Table("Bacteria");

            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
