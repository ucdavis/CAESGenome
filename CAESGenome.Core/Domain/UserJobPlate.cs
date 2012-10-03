using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobPlate : DomainObject
    {
        public UserJobPlate()
        {
            Barcodes = new List<Barcode>();
        }

        public virtual UserJob UserJob { get; set; }
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }

        public virtual IList<Barcode> Barcodes { get; set; }

        public virtual void AddBarcode(Barcode barcode)
        {
            barcode.UserJobPlate = this;
            Barcodes.Add(barcode);
        }
    }

    public class UserJobPlateMap : ClassMap<UserJobPlate>
    {
        public UserJobPlateMap()
        {
            Id(x => x.Id);

            References(x => x.UserJob);
            Map(x => x.Name);

            HasMany(x => x.Barcodes).KeyColumn("UserJobPlateId").Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
