using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobQbotColonyPicking : DomainObject
    {
        public virtual Vector Vector { get; set; }
        public virtual Strain Strain { get; set; }
        public virtual int? NumQTrays { get; set; }
        public virtual int NumGlycerol { get; set; }
        public virtual string Concentration { get; set; }
        public virtual int Replication { get; set; }
        public virtual int NumColonies { get; set; }
    }

    public class UserJobQbotColonyPickingMap : ClassMap<UserJobQbotColonyPicking>
    {
        public UserJobQbotColonyPickingMap()
        {
            Table("UserJobQbotColonyPicking");

            Id(x => x.Id);

            References(x => x.Vector).Cascade.SaveUpdate();
            References(x => x.Strain).Cascade.SaveUpdate();
            Map(x => x.NumQTrays);
            Map(x => x.NumGlycerol);
            Map(x => x.Concentration);
            Map(x => x.Replication).Column("`Replication`");
            Map(x => x.NumColonies);
        }
    }
}
