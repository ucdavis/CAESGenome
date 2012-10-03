using System.ComponentModel.DataAnnotations;
using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobBacterialClone : DomainObject
    {
        [UIHint("Enum")]
        [Display(Name = "Sequence Direction")]
        public virtual SequenceDirection SequenceDirection { get; set; }
        [Display(Name = "Primer One")]
        public virtual Primer Primer1 { get; set; }
        [Display(Name = "Primer Two")]
        public virtual Primer Primer2 { get; set; }
        [Display(Name = "Host")]
        public virtual Strain Strain { get; set; }
        public virtual Vector Vector { get; set; }
        public virtual Antibiotic Antibiotic { get; set; }
    }

    public class UserJobBacterialCloneMap : ClassMap<UserJobBacterialClone>
    {
        public UserJobBacterialCloneMap()
        {
            Table("UserJobBacterialClone");

            Id(x => x.Id);

            Map(x => x.SequenceDirection).CustomType<NHibernate.Type.EnumStringType<SequenceDirection>>();
            References(x => x.Primer1).Column("Primer1Id");
            References(x => x.Primer2).Column("Primer2Id");
            References(x => x.Strain).Cascade.SaveUpdate();
            References(x => x.Vector);
            References(x => x.Antibiotic);
        }
    }
}
