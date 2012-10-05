using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [Display(Name="Dye(s)")]
        public virtual string DyeNames
        {
            get { return string.Join(", ", Dyes.Select(a => a.Name)); }
        }
    }

    public class UserJobGenotypingMap : ClassMap<UserJobGenotyping>
    {
        public UserJobGenotypingMap()
        {
            Table("UserJobGenotyping");

            Id(x => x.Id);

            HasManyToMany(x => x.Dyes).Table("UserJobsGenotypingXDyes").ParentKeyColumn("UserJobGenotypingId").ChildKeyColumn("DyeId").Cascade.SaveUpdate();
        }
    }
}
