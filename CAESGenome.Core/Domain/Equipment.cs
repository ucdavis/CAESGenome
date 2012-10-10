using System.Collections.Generic;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Equipment : DomainObject
    {
        public Equipment()
        {
            EquipmentReservations = new List<EquipmentReservation>();
        }

        public virtual string Name { get; set; }
        public virtual string Operator { get; set; }
        public virtual bool IsReservable { get; set; }
        public virtual string Message { get; set; }

        public virtual IList<EquipmentReservation> EquipmentReservations { get; set; }
    }

    public class EquipmentMap : ClassMap<Equipment>
    {
        public EquipmentMap()
        {
            Table("Equipments");

            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Operator);
            Map(x => x.IsReservable);
            Map(x => x.Message);

            HasMany(x => x.EquipmentReservations);
        }
    }
}
