using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class BarcodeFile : DomainObject
    {
        public virtual int Column { get; set; }
        public virtual char Row { get; set; }
        public virtual Barcode Barcode { get; set; }

        public virtual bool Uploaded { get; set; }
        public virtual bool Validated { get; set; }
    }

    public class BarcodeFileMap : ClassMap<BarcodeFile>
    {
        public BarcodeFileMap()
        {
            Id(x => x.Id);

            Map(x => x.Column).Column("`Column`");
            Map(x => x.Row).Column("`Row`");
            References(x => x.Barcode);

            Map(x => x.Uploaded);
            Map(x => x.Validated);
        }
    }
}
