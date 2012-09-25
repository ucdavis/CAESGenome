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
        public virtual string DisplayName { get; set; }
        public virtual bool StandardSequencing { get; set; }
        public virtual bool DNASequencing { get; set; }
        public virtual bool CustomSequencing { get; set; }
        public virtual bool Genotyping { get; set; }
        public virtual bool Qbot { get; set; }

        public virtual IList<Stage> Stages { get; set; }
    }

    public class JobTypeMap : ClassMap<JobType>
    {
        public JobTypeMap()
        {
            ReadOnly();

            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DisplayName);
            Map(x => x.StandardSequencing);
            Map(x => x.DNASequencing);
            Map(x => x.CustomSequencing);
            Map(x => x.Genotyping);
            Map(x => x.Qbot);

            HasMany(x => x.Stages).Inverse();
        }
    }
}
