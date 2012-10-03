using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobQbotGridding : DomainObject
    {
        public virtual Vector Vector { get; set; }
        public virtual Strain Strain { get; set; }

        public virtual int NumMembraneCopies { get; set; }
        public virtual GriddingPattern GriddingPattern { get; set; }
    }

    public class UserJobQbotGriddingMap : ClassMap<UserJobQbotGridding>
    {
        public UserJobQbotGriddingMap()
        {
            Table("UserJobQbotGridding");

            Id(x => x.Id);

            References(x => x.Vector).Cascade.SaveUpdate();
            References(x => x.Strain).Cascade.SaveUpdate();

            Map(x => x.NumMembraneCopies);
            Map(x => x.GriddingPattern).CustomType<NHibernate.Type.EnumStringType<GriddingPattern>>();
        }
    }
}
