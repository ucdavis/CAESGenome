using System;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class BarcodeFile : DomainObject
    {
        public BarcodeFile()
        {
            DateTimeUploaded = DateTime.Now;
        }

        public virtual int WellColumn { get; set; }
        public virtual char WellRow { get; set; }
        public virtual Barcode Barcode { get; set; }

        public virtual bool Uploaded { get; set; }
        public virtual bool Validated { get; set; }

        public virtual int Start { get; set; }
        public virtual int End { get; set; }

        public virtual DateTime DateTimeUploaded { get; set; }
        public virtual DateTime? DateTimeValidated { get; set; }

        public virtual string ResultFileName
        {
            get
            {
                if (WellColumn < 10)
                {
                    return string.Format("{0}_{1}0{2}.ab1", Barcode.Id, WellRow, WellColumn);
                }

                return string.Format("{0}_{1}{2}.ab1", Barcode.Id, WellRow, WellColumn);
            }
        }
        public virtual string ValidationFileName { 
            get
            {
                if (WellColumn < 10)
                {
                    return string.Format("{0}_{1}0{2}.ab1.qual", Barcode.Id, WellRow, WellColumn);
                }

                return string.Format("{0}_{1}{2}.ab1.qual", Barcode.Id, WellRow, WellColumn);
            } 
        }
    }

    public class BarcodeFileMap : ClassMap<BarcodeFile>
    {
        public BarcodeFileMap()
        {
            Id(x => x.Id);

            Map(x => x.WellColumn);
            Map(x => x.WellRow);
            References(x => x.Barcode);

            Map(x => x.Uploaded);
            Map(x => x.Validated);

            Map(x => x.Start).Column("`Start`");
            Map(x => x.End).Column("`End`");

            Map(x => x.DateTimeUploaded);
            Map(x => x.DateTimeValidated);
        }
    }
}
