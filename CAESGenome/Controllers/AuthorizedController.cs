using System.Web.Mvc;
using CAESGenome.Core.Resources;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.User)]
    public class AuthorizedController : Controller
    {
        //
        // GET: /Authorized/

        public ActionResult Index()
        {
            return View();
        }

    }
}
