using System;
using System.Collections.Generic;
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
        }

        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual University University { get; set; }
        public virtual Department Department { get; set; }

        public virtual IList<RechargeAccount> RechargeAccounts { get; set; }
        public virtual IList<RechargeAccount> OwnedRechargeAcccounts { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("UserProfile");

            Id(x => x.Id);

            Map(x => x.UserName);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Title);
            Map(x => x.Phone);
            Map(x => x.Fax);
            Map(x => x.DateCreated);

            References(x => x.University);
            References(x => x.Department);

            HasManyToMany(x => x.RechargeAccounts).Table("UserProfilesXRechargeAccounts")
                .ParentKeyColumn("UserProfileId").ChildKeyColumn("RechargeAccountId")
                .Cascade.SaveUpdate();
            
            HasMany(x => x.OwnedRechargeAcccounts).KeyColumn("UserProfileId").Inverse();
        }
    }
}
