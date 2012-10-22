using System.Web;
using System.Web.Mvc;

namespace CAESGenome.Controllers
{
    public class TestController : Controller
    {
        public ActionResult UploadFile()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase[] files)
        {
            return View();
        }
    }
}
