using System.ComponentModel.DataAnnotations;
using CAESGenome.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class UserJobSublibrary : DomainObject
    {
        [Required]
        public virtual TypeOfSamples SampleType { get; set; }
        public virtual float? DnaConcentration { get; set; }
        public virtual float GenomeSize { get; set; }
        public virtual int Coverage { get; set; }
        public virtual Vector Vector { get; set; }
        [Required]
        public virtual Antibiotic Antibiotic { get; set; }
    }

    public class UserJobSublibraryMap : ClassMap<UserJobSublibrary>
    {
        public UserJobSublibraryMap()
        {
            Table("UserJobSublibrary");

            Id(x => x.Id);

            Map(x => x.SampleType).CustomType<NHibernate.Type.EnumStringType<TypeOfSamples>>();
            Map(x => x.DnaConcentration);
            Map(x => x.GenomeSize);
            Map(x => x.Coverage);
            References(x => x.Vector);
            References(x => x.Antibiotic);
        }
    }
}
