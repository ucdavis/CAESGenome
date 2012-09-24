using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class VectorType : DomainObject
    {
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
    }

    public class VectorTypeMap : ClassMap<VectorType>
    {
        public VectorTypeMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
        }
    }
}
