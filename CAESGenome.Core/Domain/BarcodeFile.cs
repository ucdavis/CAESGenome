using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class BarcodeFile : DomainObject
    {
        // file information
        public virtual byte[] ResultFile { get; set; }
        public virtual string ResultFileName { get; set; }
        public virtual string ResultContentType { get; set; }
        public virtual byte[] ValidationFile { get; set; }
        public virtual string ValidationFileName { get; set; }
        public virtual string ValidationContentType { get; set; }

        public virtual int Column { get; set; }
        public virtual int Row { get; set; }
        public virtual Barcode Barcode { get; set; }
    }

    public class BarcodeFileMap : ClassMap<BarcodeFile>
    {
        public BarcodeFileMap()
        {
            Id(x => x.Id);

            Map(x => x.ResultFile).CustomType("BinaryBlob").LazyLoad();
            Map(x => x.ResultFileName);
            Map(x => x.ResultContentType);

            Map(x => x.ValidationFile).CustomType("BinaryBlob").LazyLoad();
            Map(x => x.ValidationFileName);
            Map(x => x.ValidationContentType);

            Map(x => x.Column).Column("`Column`");
            Map(x => x.Row).Column("`Row`");
            References(x => x.Barcode);
        }
    }
}
