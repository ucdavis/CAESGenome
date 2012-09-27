using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Dye : DomainObject
    {
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        public virtual bool Supplied { get; set; }
        public virtual bool Genotyping { get; set; }
    }

    public class DyeMap : ClassMap<Dye>
    {
        public DyeMap()
        {
            Table("Dyes");

            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Supplied);
            Map(x => x.Genotyping);
        }
    }
}
