using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    /// <summary>
    /// Usere job run for sequencing
    /// </summary>
    public class UserJobUserRun : DomainObject
    {
        public virtual SequenceDirection SequenceDirection { get; set; }
        public virtual Dye Dye { get; set; }
    }

    public class UserJobUserRunMap : ClassMap<UserJobUserRun>
    {
        public UserJobUserRunMap()
        {
            Table("UserJobUserRun");

            Id(x => x.Id);

            Map(x => x.SequenceDirection).CustomType<NHibernate.Type.EnumStringType<SequenceDirection>>();
            References(x => x.Dye);
        }
    }
}
