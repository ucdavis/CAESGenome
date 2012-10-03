using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Stage : DomainObjectWithTypedId<string>
    {
        public virtual string Name { get; set; }
        public virtual int Order { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual bool IsComplete { get; set; }
    }

    public class StageMap : ClassMap<Stage>
    {
        public StageMap()
        {
            ReadOnly();

            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Order).Column("`Order`");
            References(x => x.JobType);
            Map(x => x.IsComplete);
        }
    }
}
