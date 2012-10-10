using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class User : DomainObject
    {
        public User()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            DateCreated = DateTime.Now;

            RechargeAccounts = new List<RechargeAccount>();
            OwnedRechargeAcccounts = new List<RechargeAccount>();
        }

        [Display(Name="Email")]
        public virtual string UserName { get; set; }
        [Display(Name="First Name")]
        public virtual string FirstName { get; set; }
        [Display(Name="Last Name")]
        public virtual string LastName { get; set; }
        public virtual string Title { get; set; }
        [DataType(DataType.PhoneNumber)]
        public virtual string Phone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public virtual string Fax { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual University University { get; set; }
        public virtual Department Department { get; set; }
        public virtual User ParentUser { get; set; }

        public virtual IList<RechargeAccount> RechargeAccounts { get; set; }
        public virtual IList<RechargeAccount> OwnedRechargeAcccounts { get; set; }

        public virtual string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("UserProfile");

            Id(x => x.Id).Column("UserId");

            Map(x => x.UserName);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Title);
            Map(x => x.Phone);
            Map(x => x.Fax);
            Map(x => x.DateCreated);

            References(x => x.University);
            References(x => x.Department);
            References(x => x.ParentUser).Column("ParentUserId").Cascade.None();

            HasManyToMany(x => x.RechargeAccounts).Table("UserProfilesXRechargeAccounts")
                .ParentKeyColumn("UserProfileId").ChildKeyColumn("RechargeAccountId")
                .Cascade.SaveUpdate();
            
            HasMany(x => x.OwnedRechargeAcccounts).KeyColumn("UserProfileId").Inverse();
        }
    }
}
