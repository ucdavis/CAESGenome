using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAESGenome.Core.Domain;

namespace CAESGenome.Transfer
{
    public static class Accounts
    {
        public static List<UserAcct> Convert()
        {
            var db = new ExistingDataDataContext();

            var users = db.users.Select(user => new UserAcct()
                {
                    SourceId = user.userid, UserName = user.username, LastName = user.lastname, FirstName = user.firstname, 
                    Title = user.title, Password = user.passwords ?? "password", Phone = user.phonenum ?? "530-555-5555", DateCreated = user.datejoined ?? DateTime.Now, 
                    PiId = (int) user.piid, User = true, IsActive = user.valid.ToLower() == "yes"
                }).ToList();

            users.AddRange(db.pis.Select(pi => new UserAcct()
                {
                    SourceId = pi.piid, UserName = pi.piemail, LastName = pi.pilastname, FirstName = pi.pifirstname, 
                    Title = pi.pititle, Password = pi.pipassword, Phone = pi.piphonenum, DateCreated = pi.pidatejoined ?? DateTime.Now, 
                    Pi = true, UniversityId = pi.universityid, DepartmentId = pi.departmentid
                }));

            users.AddRange(db.staffs.Select(st => new UserAcct()
                {
                    SourceId = (int)st.staffid, UserName = st.staffemail, LastName = st.stafflast, FirstName = st.stafffirst,
                    Title = st.stafftitle, Password = st.staffpassword, Staff = true
                }
                ));

            return users;
        }

        public static List<RechargeAcct> Recharge()
        {
            var db = new ExistingDataDataContext();

            var recharge = db.recharges.Select( a =>
                            new RechargeAcct()
                                {
                                    SourceId = a.rechargeid,
                                    AccountNum = a.accountnum,
                                    Description = a.description,
                                    Start = a.datestart ?? DateTime.Now,
                                    End = a.dateend,
                                    IsValid = a.valid.ToLower() == "yes",
                                    PiId = a.piid
                                }).ToList();

            return recharge;
        }

    }

    public class UserAcct : User
    {
        public UserAcct()
        {
            User = false;
            Pi = false;
            Staff = false;
        }

        public int SourceId { get; set; }
        public string Password { get; set; }
        public int PiId { get; set; }

        public bool User { get; set; }
        public bool Pi { get; set; }
        public bool Staff { get; set; }

        public int UniversityId { get; set; }
        public int DepartmentId { get; set; }

        public virtual bool IsActive { get; set; }
    }

    public class RechargeAcct : RechargeAccount
    {
        public int SourceId { get; set; }
        public int PiId { get; set; }
    }
}
