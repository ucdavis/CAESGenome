using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class EquipmentReservation : DomainObject
    {
        public EquipmentReservation()
        {
            DateSubmitted = DateTime.Now;
        }

        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual Equipment Equipment { get; set; }
        public virtual DateTime DateSubmitted { get; set; }
        [DataType(DataType.DateTime)]
        public virtual DateTime Start { get; set; }
        [DataType(DataType.DateTime)]
        public virtual DateTime End { get; set; }
    }

    public class EquipmentReservationMap : ClassMap<EquipmentReservation>
    {
        public EquipmentReservationMap()
        {
            Id(x => x.Id);

            References(x => x.User);
            References(x => x.Equipment);
            Map(x => x.DateSubmitted);
            Map(x => x.Start).Column("`Start`");
            Map(x => x.End).Column("`End`");
        }
    }
}
