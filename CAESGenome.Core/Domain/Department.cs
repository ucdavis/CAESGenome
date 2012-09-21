using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Department : DomainObject
    {
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual University University { get; set; }
    }

    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            References(x => x.University);
        }
    }
}
