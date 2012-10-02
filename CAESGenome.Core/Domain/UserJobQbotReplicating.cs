using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobQbotReplicating : DomainObject
    {
        public virtual Vector Vector { get; set; }
        public virtual Strain Strain { get; set; }

        public virtual int NumSourcePlates { get; set; }
        public virtual PlateTypes DestinationPlateType { get; set; }
        public virtual int NumCopies { get; set; }
    }

    public class UserJobQbotReplicatingMap : ClassMap<UserJobQbotReplicating>
    {
        public UserJobQbotReplicatingMap()
        {
            Table("UserJobQbotReplicating");

            Id(x => x.Id);

            References(x => x.Vector);
            References(x => x.Strain);

            Map(x => x.NumSourcePlates);
            Map(x => x.DestinationPlateType).CustomType<NHibernate.Type.EnumStringType<PlateTypes>>();
            Map(x => x.NumCopies);
        }
    }
}
