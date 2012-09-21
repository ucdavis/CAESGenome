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
        public virtual string AccountNum { get; set; }
        [StringLength(100)]
        public virtual string Description { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual bool IsValid { get; set; }

        public virtual User User { get; set; }
    }

    public class RechargeAccountMap : ClassMap<RechargeAccount>
    {
        public RechargeAccountMap()
        {
            Id(x => x.Id);

            Map(x => x.AccountNum);
            Map(x => x.Description);
            Map(x => x.Start);
            Map(x => x.End);
            Map(x => x.IsValid);

            References(x => x.User).Column("UserProfileId");
        }
    }
}
