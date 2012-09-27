using System.Collections.Generic;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobGenotyping : DomainObject
    {
        public UserJobGenotyping()
        {
            Dyes = new List<Dye>();
        }

        public virtual IList<Dye> Dyes { get; set; }
    }

    public class UserJobGenotypingMap : ClassMap<UserJobGenotyping>
    {
        public UserJobGenotypingMap()
        {
            Id(x => x.Id);

            HasManyToMany(x => x.Dyes).Table("UserJobsGenotypingXDyes").ParentKeyColumn("UserJobGenotypingId").ChildKeyColumn("DyeId").Cascade.SaveUpdate();
        }
    }
}
