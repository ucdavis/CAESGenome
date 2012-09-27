using System.ComponentModel.DataAnnotations;
using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobDna : DomainObject
    {
        public UserJobDna()
        {
            SequenceDirection = SequenceDirection.Forward;
        }

        public virtual SequenceDirection SequenceDirection { get; set; }
        [Required]
        public virtual Primer Primer1 { get; set; }
        public virtual Primer Primer2 { get; set; }
    }

    public class UserJobDnaMap : ClassMap<UserJobDna>
    {
        public UserJobDnaMap()
        {
            Table("UserJobDna");

            Id(x => x.Id);

            Map(x => x.SequenceDirection).CustomType<NHibernate.Type.EnumStringType<SequenceDirection>>();
            References(x => x.Primer1).Column("Primer1Id");
            References(x => x.Primer2).Column("Primer2Id");
        }
    }
}
