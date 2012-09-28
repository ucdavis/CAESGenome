using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobQbotColonyPicking : DomainObject
    {
        public Vector Vector { get; set; }
        public Strain Strain { get; set; }
        public int NumQTrays { get; set; }
        public int NumGlycerol { get; set; }
        public string Concentration { get; set; }
        public int Replication { get; set; }
        public int NumColonies { get; set; }
    }

    public class UserJobQbotColonyPickingMap : ClassMap<UserJobQbotColonyPicking>
    {
        public UserJobQbotColonyPickingMap()
        {
            Table("UserJobQbotColonyPicking");

            Id(x => x.Id);

            References(x => x.Vector);
            References(x => x.Strain);
            Map(x => x.NumQTrays);
            Map(x => x.NumGlycerol);
            Map(x => x.Concentration);
            Map(x => x.Replication);
            Map(x => x.NumColonies);
        }
    }
}
