﻿using System;
using System.Collections.Generic;
using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJob : DomainObject
    {
        public UserJob()
        {
            IsOpen = true;
            LastUpdate = DateTime.Now;
            DateTimeCreated = DateTime.Now;
        }

        public virtual User User { get; set; }
        public virtual RechargeAccount RechargeAccount { get; set; }
        public virtual string Name { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual int NumberPlates { get; set; }
        public virtual PlateTypes PlateType { get; set; }
        public virtual string Comments { get; set; }
        // might not be used
        public virtual Stage Stage { get; set; }
        public virtual bool IsOpen { get; set; }

        public virtual DateTime LastUpdate { get; set; }
        public virtual DateTime DateTimeCreated { get; set; }

        public virtual IList<UserJobPlate> UserJobPlates { get; set; }
    }

    public class UserJobMap : ClassMap<UserJob>
    {
        public UserJobMap()
        {
            Id(x => x.Id);

            References(x => x.User);
            References(x => x.RechargeAccount);
            Map(x => x.Name);
            References(x => x.JobType);
            Map(x => x.NumberPlates);
            Map(x => x.PlateType).CustomType<NHibernate.Type.EnumStringType<PlateTypes>>();
            Map(x => x.Comments);
            References(x => x.Stage);
            Map(x => x.IsOpen);

            Map(x => x.LastUpdate);
            Map(x => x.DateTimeCreated);

            HasMany(x => x.UserJobPlates).Inverse();
        }
    }
}
