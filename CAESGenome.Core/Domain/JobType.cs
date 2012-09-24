using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class JobType : DomainObject
    {
        public virtual string Name { get; set; }
    }

    public class JobTypeMap : ClassMap<JobType>
    {
        public JobTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
