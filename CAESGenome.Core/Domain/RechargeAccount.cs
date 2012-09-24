using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class RechargeAccount : DomainObject
    {
        public RechargeAccount()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            Start = DateTime.Now.Date;
            End = DateTime.Now.AddDays(14).Date;
            IsValid = true;
        }

        [StringLength(50)]
        [Required]
        [Display(Name="Account Number")]
        public virtual string AccountNum { get; set; }
        [StringLength(100)]
        public virtual string Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Name="Start Date")]
        public virtual DateTime Start { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public virtual DateTime End { get; set; }
        [Display(Name = "Is Active")]
        public virtual bool IsValid { get; set; }
        [Required]
        public virtual User User { get; set; }

        public virtual bool IsActive()
        {
            return (DateTime.Now.Date <= End && IsValid);
        }
    }

    public class RechargeAccountMap : ClassMap<RechargeAccount>
    {
        public RechargeAccountMap()
        {
            Id(x => x.Id);

            Map(x => x.AccountNum);
            Map(x => x.Description);
            Map(x => x.Start).Column("`Start`");
            Map(x => x.End).Column("`End`");
            Map(x => x.IsValid);

            References(x => x.User).Column("UserProfileId");
        }
    }
}
