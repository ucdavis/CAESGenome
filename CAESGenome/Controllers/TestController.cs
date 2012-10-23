using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UCDArch.Web.ActionResults;

namespace CAESGenome.Controllers
{
    public class TestController : Controller
    {
        public ActionResult UploadFile()
        {
            return View();
        }

        
        [HttpPost]
        public JsonNetResult UploadFile(HttpPostedFileBase file)
        {
            var name = file.FileName;
            var size = file.ContentLength;

            var result = new List<FileUploadResult>();
            result.Add(new FileUploadResult() {Name = name, Size = size});

            return new JsonNetResult(result);
        }

        public FileResult DownloadFile()
        {
            throw new NotImplementedException();
        }
    }

    public class FileUploadResult
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string url {
            get
            {
                var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
                return url.Action("DownloadFile", "Test"); // Will output the proper link accordi
            } 
        }
    }
}
