using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobPlate : DomainObject
    {
        public virtual UserJob UserJob { get; set; }
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
    }

    public class UserJobPlateMap : ClassMap<UserJobPlate>
    {
        public UserJobPlateMap()
        {
            Id(x => x.Id);

            References(x => x.UserJob);
            Map(x => x.Name);
        }
    }
}
