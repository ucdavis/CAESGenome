using System;
using System.Web.Http;
using CAESGenome.Core.Resources;

namespace CAESGenome.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        public AdminOnlyAttribute()
        {
            Roles = RoleNames.Admin;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserOnlyAttribute : AuthorizeAttribute
    {
        public UserOnlyAttribute()
        {
            Roles = RoleNames.User;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class StaffOnlyAttribute : AuthorizeAttribute
    {
        public StaffOnlyAttribute()
        {
            Roles = RoleNames.Staff;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PIOnlyAttribute : AuthorizeAttribute
    {
        public PIOnlyAttribute()
        {
            Roles = RoleNames.PI;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class StaffAndUserOnlyAttribute : AuthorizeAttribute
    {
        public StaffAndUserOnlyAttribute()
        {
            Roles = RoleNames.Staff + "," + RoleNames.User;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoOneAttribute : AuthorizeAttribute
    {
        public NoOneAttribute()
        {
            Roles = "noone";   // no one can use this
        }
    }
}