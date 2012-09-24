using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobBacterialClone : DomainObject
    {
        public virtual SequenceDirection SequenceDirection { get; set; }
        public virtual Primer Primer1 { get; set; }
        public virtual Primer Primer2 { get; set; }
        public virtual Strain Strain { get; set; }
        public virtual Vector Vector { get; set; }
        public virtual Antibiotic Antibiotic { get; set; }
    }

    public class UserJobBacterialCloneMap : ClassMap<UserJobBacterialClone>
    {
        public UserJobBacterialCloneMap()
        {
            Id(x => x.Id);

            Map(x => x.SequenceDirection).CustomType<NHibernate.Type.EnumStringType<SequenceDirection>>();
            References(x => x.Primer1).Column("Primer1Id");
            References(x => x.Primer2).Column("Primer2Id");
            References(x => x.Strain);
            References(x => x.Vector);
            References(x => x.Antibiotic);
        }
    }
}
