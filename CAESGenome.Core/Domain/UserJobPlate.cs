using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobPlate : DomainObject
    {
        public UserJobPlate()
        {
            Completed = false;
            Barcodes = new List<Barcode>();
        }

        public virtual UserJob UserJob { get; set; }
        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
        public virtual bool Completed { get; set; }
        public virtual DateTime? DateTimeCompleted { get; set; }

        public virtual IList<Barcode> Barcodes { get; set; }
        public virtual void AddBarcode(Barcode barcode)
        {
            barcode.UserJobPlate = this;
            Barcodes.Add(barcode);
        }

        public virtual DateTime? WebDate { 
            get { return GetBarcodeDateByStatus(StageIds.WebPlateIds); }
        }
        public virtual DateTime? LabDate
        {
            get { return GetBarcodeDateByStatus(StageIds.LabPlateIds); }
        }
        public virtual DateTime? RCADate
        {
            get { return GetBarcodeDateByStatus(StageIds.RcaPlateIds); }
        }
        public virtual DateTime? SeqDate
        {
            get { return GetBarcodeDateByStatus(StageIds.SequencingPlateIds); }
        } 
        public virtual DateTime? Xl3730Date
        {
            get { return GetBarcodeDateByStatus(StageIds.Xl3730PlateIds); }
        }

        private DateTime? GetBarcodeDateByStatus(List<string> stageIds )
        {
            var barcode = Barcodes.FirstOrDefault(a => stageIds.Contains(a.Stage.Id));

            if (barcode != null)
            {
                return barcode.DateCreated;
            }

            return null;
        }
}

    public class UserJobPlateMap : ClassMap<UserJobPlate>
    {
        public UserJobPlateMap()
        {
            Id(x => x.Id);

            References(x => x.UserJob);
            Map(x => x.Name);
            Map(x => x.Completed);
            Map(x => x.DateTimeCompleted);

            HasMany(x => x.Barcodes).KeyColumn("UserJobPlateId").Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
