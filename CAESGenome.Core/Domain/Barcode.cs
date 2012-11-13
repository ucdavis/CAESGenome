using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Barcode : DomainObject
    {
        public Barcode()
        {
            DateCreated = DateTime.Now;
            Done = false;
            AllowDownload = false;

            SubPlateId = 0;

            BarcodeFiles = new List<BarcodeFile>();
        }

        public virtual UserJobPlate UserJobPlate { get; set; }
        public virtual Primer Primer { get; set; }
        public virtual Stage Stage { get; set; }
        public virtual Barcode SourceBarcode { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool Done { get; set; }
        public virtual bool AllowDownload { get; set; }
        public virtual int SubPlateId { get; set; }

        public virtual IList<BarcodeFile> BarcodeFiles { get; set; }
        public virtual IList<PhredQuality> PhredQualities { get; set; }

        public virtual string PlateName
        {
            get { 
                var result = new StringBuilder();
                result.Append(UserJobPlate.Name);

                if (SubPlateId > 0)
                {
                    result.Append("_");
                    switch(SubPlateId)
                    {
                        case 1:
                            result.Append("I");
                            break;
                        case 2:
                            result.Append("II");
                            break;
                        case 3:
                            result.Append("III");
                            break;
                        case 4:
                            result.Append("IV");
                            break;

                    }
                }

                if (Primer != null)
                {
                    result.Append("_");
                    result.Append(Primer.Name);
                }

                return result.ToString();
            }
        }

        /// <summary>
        /// Whether or not all the validated files have been validated
        /// </summary>
        public virtual bool Validated
        {
            get { return BarcodeFiles.All(a => a.Validated); }
        }

        /// <summary>
        /// Accepted the quality control
        /// </summary>
        public virtual bool Accepted
        {
            get { return Done && AllowDownload; }
        }
    }

    public class BarcodeMap : ClassMap<Barcode>
    {
        public BarcodeMap()
        {
            Id(x => x.Id);

            References(x => x.UserJobPlate);
            References(x => x.Primer);
            References(x => x.Stage);
            References(x => x.SourceBarcode).Column("SourceBarcodeId");

            Map(x => x.DateCreated);
            Map(x => x.Done);
            Map(x => x.AllowDownload);
            Map(x => x.SubPlateId);

            HasMany(x => x.BarcodeFiles).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.PhredQualities).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
