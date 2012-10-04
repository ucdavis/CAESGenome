using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAESGenome.Core.Resources;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.Staff)]
    public class StaffController : ApplicationController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
