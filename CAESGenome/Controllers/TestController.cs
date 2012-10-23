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

            // extract information about where to upload
            var barcode = name.Substring(0, name.IndexOf("_"));

            // split after the barcode
            var location = name.Substring(name.IndexOf("_") + 1);
            // remove the extension
            location = location.Substring(0, location.IndexOf("."));

            var col = location[0];
            var row = location.Substring(1);

            var result = new List<FileUploadResult>();
            result.Add(new FileUploadResult() {name = name, size = size, id = 1});

            return new JsonNetResult(result);
        }

        public FileResult DownloadFile()
        {
            throw new NotImplementedException();
        }
    }

    public class FileUploadResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string url {
            get
            {
                var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
                return url.Action("DownloadFile", "Test", new {Id = id}); // Will output the proper link accordi
            } 
        }
    }
}
