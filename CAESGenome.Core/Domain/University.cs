using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class University : DomainObject
    {
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
        [StringLength(100)]
        public virtual string Address { get; set; }
        [StringLength(100)]
        [Required]
        public virtual string City { get; set; }
        [StringLength(2)]
        [Required]
        public virtual string State { get; set; }
        [StringLength(10)]
        public virtual string Zip { get; set; }
        [StringLength(3)]
        public virtual string Country { get; set; }
    }

    public class UniversityMap : ClassMap<University>
    {
        public UniversityMap()
        {
            Table("Universities");

            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Address);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.Zip);
            Map(x => x.Country);
        }
    }
}
