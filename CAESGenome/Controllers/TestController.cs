using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Services;
using UCDArch.Web.ActionResults;

namespace CAESGenome.Controllers
{
    public class TestController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPhredService _phredService;

        public TestController(IRepositoryFactory repositoryFactory, IPhredService phredService)
        {
            _repositoryFactory = repositoryFactory;
            _phredService = phredService;
        }



        

        public FileResult DownloadFile()
        {
            throw new NotImplementedException();
        }

        public ActionResult PushFileToServer()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult PushFileToServer(int id)
        {
            //_phredService.ExecuteValidation(id);
            _phredService.RunPhredValidation(id);

            return new JsonNetResult(true);
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

        public string error { get; set; }
    }
}
