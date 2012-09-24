using System.Collections.Generic;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class JobType : DomainObject
    {
        public JobType()
        {
            Stages = new List<Stage>();
        }

        public virtual string Name { get; set; }

        public virtual IList<Stage> Stages { get; set; }
    }

    public class JobTypeMap : ClassMap<JobType>
    {
        public JobTypeMap()
        {
            ReadOnly();

            Id(x => x.Id);
            Map(x => x.Name);

            HasMany(x => x.Stages).Inverse();
        }
    }
}
