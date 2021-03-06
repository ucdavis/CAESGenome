﻿using System.ComponentModel.DataAnnotations;
using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobQbotReplicating : DomainObject
    {
        public virtual Vector Vector { get; set; }
        public virtual Strain Strain { get; set; }

        [Display(Name="# Source Plates")]
        public virtual int NumSourcePlates { get; set; }
        [Display(Name = "Destination Plate Type")]
        public virtual PlateTypes DestinationPlateType { get; set; }
        [Display(Name="# Copies")]
        public virtual int NumCopies { get; set; }
    }

    public class UserJobQbotReplicatingMap : ClassMap<UserJobQbotReplicating>
    {
        public UserJobQbotReplicatingMap()
        {
            Table("UserJobQbotReplicating");

            Id(x => x.Id);

            References(x => x.Vector).Cascade.SaveUpdate();
            References(x => x.Strain).Cascade.SaveUpdate();

            Map(x => x.NumSourcePlates);
            Map(x => x.DestinationPlateType).CustomType<NHibernate.Type.EnumStringType<PlateTypes>>();
            Map(x => x.NumCopies);
        }
    }
}
