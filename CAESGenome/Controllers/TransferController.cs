using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;
using CAESGenome.Services;
using CAESGenome.Transfer;
using WebMatrix.WebData;

using Dapper;

namespace CAESGenome.Controllers
{
    /// <summary>
    /// Controller to transfer existing data.  Should be deleted/commented out once deployed.
    /// </summary>
    public class TransferController : Controller
    {
        private readonly IDbService _dbService;
        private readonly IRepositoryFactory _repositoryFactory;

        public TransferController(IDbService dbService, IRepositoryFactory repositoryFactory)
        {
            _dbService = dbService;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Use this just to initialize the websecurity service, doesn't do anything.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Initialize()
        {
            return View();
        }

        /// <summary>
        /// Transfer the user accounts and merges user/staff/pi tables
        /// </summary>
        /// <returns></returns>
        public ActionResult Accounts()
        {
            var users = Transfer.Accounts.Convert();
            var recharge = Transfer.Accounts.Recharge();

            var createdUsers = InsertUsers(users.Where(a => a.User));
            InsertPIs(createdUsers, users.Where(a => a.Pi), recharge);

            return View();
        }

        private IEnumerable<UserAcct> InsertUsers(IEnumerable<UserAcct> users)
        {
            var check = new List<string>();
            foreach(var user in users.Where(a => a.IsActive))
            {
                if (check.Contains(user.UserName))
                {
                    var oUserName = user.UserName;
                    var i = 1;
                    do
                    {
                        user.UserName = string.Format("{0}.{1}", i, oUserName);
                        i++;
                    } while (check.Contains(user.UserName));
                }

                check.Add(user.UserName);
            }

            foreach (var user in users.Where(a => !a.IsActive))
            {
                if (check.Contains(user.UserName))
                {
                    var oUserName = user.UserName;
                    var i = 1;
                    do
                    {
                        user.UserName = string.Format("{0}.{1}", i, oUserName);
                        i++;
                    } while (check.Contains(user.UserName));
                }

                check.Add(user.UserName);
            }

            using (var conn = _dbService.GetConnection())
            {
                conn.Execute("SET IDENTITY_INSERT UserProfile ON");

                conn.Execute(
                    @"INSERT INTO UserProfile(UserId, UserName, FirstName, LastName, Title, Phone, DateCreated) values (@userId, @userName, @firstName, @lastName, @title, @phone, @date)",
                    users.Select(
                        a =>
                        new
                            {
                                userId = a.SourceId,
                                userName = a.UserName ?? "unknown",
                                firstName = a.FirstName,
                                lastName = a.LastName,
                                title = a.Title,
                                phone = a.Phone,
                                date = a.DateCreated
                            }).ToList()
                    );

                // roleid = 4 is the user role, change if necessary
                conn.Execute(@"INSERT INTO webpages_UsersInRoles(UserId, RoleId) values (@userId, @roleId)", users.Select(a => new { userId = a.SourceId, roleId = 4 }));

                conn.Execute("SET IDENTITY_INSERT UserProfile OFF");
            }

            foreach (var user in users.Where(a => a.IsActive))
            {
                try
                {
                    WebSecurity.CreateAccount(user.UserName ?? user.FullName
                                              , user.Password);
                }
                catch (Exception ex)
                {
                    
                }
                
            }

            return users;
        }
        private void InsertPIs(IEnumerable<UserAcct> users, IEnumerable<UserAcct> pis, List<RechargeAcct> recharges)
        {
            var check = users.Select(a => a.UserName).ToList();
            var checkPi = new List<string>();
            var pisToCreate = pis.Where(a => !check.Contains(a.UserName));
            
            foreach (var user in pisToCreate)
            {
                if (checkPi.Contains(user.UserName))
                {
                    var oUserName = user.UserName;
                    var i = 1;
                    do
                    {
                        user.UserName = string.Format("{0}.{1}", i, oUserName);
                        i++;
                    } while (check.Contains(user.UserName));
                }

                checkPi.Add(user.UserName);
            }
            
            using(var conn = _dbService.GetConnection())
            {
                conn.Execute(
                    @"INSERT INTO UserProfile(UserName, FirstName, LastName, Title, Phone, Fax, DateCreated, UniversityId, DepartmentId) values (@userName, @firstName, @lastName, @title, @phone, @fax, @date, @uid, @did)",
                    pisToCreate.Select(
                            a =>
                            new
                            {
                                userName = a.UserName ?? "unknown",
                                firstName = a.FirstName ?? "Unknown",
                                lastName = a.LastName ?? "Unknown",
                                title = a.Title,
                                phone = a.Phone ?? "530-555-5555",
                                fax = a.Fax,
                                date = a.DateCreated,
                                uid = a.UniversityId, did = a.DepartmentId
                            }).ToList()
                        );

                conn.Execute(
                    @"UPDATE UserProfile SET UniversityId = @uid, DepartmentId = @did WHERE UserName = @userName",
                    pis.Where(a => check.Contains(a.UserName)).Select(a => new {uid = a.UniversityId, did = a.DepartmentId, userName = a.UserName})
                    );
            }

            foreach(var user in pisToCreate)
            {
                WebSecurity.CreateAccount(user.UserName ?? "unknown", user.Password ?? "password");
            }

            // all the Pi's
            var allPis = pisToCreate.ToList();
            allPis.AddRange(pis.Where(a => check.Contains(a.UserName)));

            var allPiUserNames = allPis.Select(b => b.UserName).ToList();

            var usersToAddRole = _repositoryFactory.UserRepository.Queryable.Where(a => allPiUserNames.Contains(a.UserName));
            using(var conn = _dbService.GetConnection())
            {
                // 2 is pi
                conn.Execute(@"INSERT INTO webpages_UsersInRoles(UserId, RoleId) values (@userId, @roleId)", usersToAddRole.Select(a => new { userId = a.Id, roleId = 2 }));
            }

            // make the user to pi associations
            foreach(var pi in allPis)
            {
                // grab the current PI's id
                var myPi = usersToAddRole.FirstOrDefault(a => a.UserName.Equals(pi.UserName ?? "unknown"));
                if (myPi == null) continue;
                var piId = myPi.Id;
                //var piId = usersToAddRole.First(a => a.UserName == (pi.UserName ?? "unknown")).Id;

                // grab the PI's recharge accounts
                var recharge = recharges.Where(a => a.PiId == pi.SourceId);

                using (var conn = _dbService.GetConnection())
                {
                    conn.Execute(
                        @"UPDATE UserProfile SET ParentUserId = @pUserId where UserId = @userId",
                        users.Where(a => a.PiId == pi.SourceId)
                             .Select(a => new {pUserId = piId, userId = a.SourceId})
                        );

                    conn.Execute("SET IDENTITY_INSERT RechargeAccounts ON");
                    conn.Execute(
                        @"INSERT INTO RechargeAccounts (Id, AccountNum, Description, [Start], [End], IsValid, UserProfileId) VALUES (@id, @accountNum, @description, @start, @end, @isValid, @userProfileId)",
                        recharge.Select(
                            a =>
                            new
                                {
                                    id = a.SourceId,
                                    accountNum = a.AccountNum,
                                    description = a.Description,
                                    start = a.Start,
                                    end = a.End,
                                    isValid = a.IsValid,
                                    userProfileId = piId
                                })
                        );
                    conn.Execute("SET IDENTITY_INSERT RechargeAccounts OFF");
                }
            }
        }
    }
}
