using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAESGenome.Core.Resources;

namespace CAESGenome.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [Authorize(Roles = RoleNames.Admin)]
        public ActionResult PIs()
        {
            return View();
        }

    }
}
