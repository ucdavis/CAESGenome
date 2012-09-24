using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Primer : DomainObject
    {
        public Primer()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            Supplied = false;
        }

        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
        [StringLength(255)]
        public virtual string ShortName { get; set; }
        public virtual bool Supplied { get; set; }
        public virtual string Sequence { get; set; }
    }

    public class PrimerMap : ClassMap<Primer>
    {
        public PrimerMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.ShortName);
            Map(x => x.Supplied);
            Map(x => x.Sequence);
        }
    }
}
