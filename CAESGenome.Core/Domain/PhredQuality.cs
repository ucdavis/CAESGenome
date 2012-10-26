using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class PhredQuality : DomainObject
    {
        public PhredQuality()
        {
            DateTimeSubmitted = DateTime.Now;
        }

        [Required]
        public virtual Barcode Barcode { get; set; }
        public virtual int WellColumn { get; set; }
        public virtual char WellRow { get; set; }
        public virtual int Start { get; set; }
        public virtual int End { get; set; }
        public virtual DateTime DateTimeSubmitted { get; set; }
    }

    public class PhredQualityMap : ClassMap<PhredQuality>
    {
        public PhredQualityMap()
        {
            Id(x => x.Id);

            References(x => x.Barcode);
            Map(x => x.WellColumn);
            Map(x => x.WellRow);
            Map(x => x.Start);
            Map(x => x.End).Column("`End`");
            Map(x => x.DateTimeSubmitted);
        }
    }
}
