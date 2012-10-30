using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;
using CAESGenome.Resources;
using CAESGenome.Services;
using UCDArch.Web.ActionResults;

namespace CAESGenome.Controllers
{
    public class ResultsController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPhredService _phredService;

        public ResultsController(IRepositoryFactory repositoryFactory, IPhredService phredService)
        {
            _repositoryFactory = repositoryFactory;
            _phredService = phredService;
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult UploadFile(HttpPostedFileBase file)
        {
            var results = new List<FileUploadResult>();

            if (file != null)
            {
                var fileId = 0;

                // extract file contents
                var reader = new BinaryReader(file.InputStream);
                var data = reader.ReadBytes(file.ContentLength);

                var result = _phredService.SaveFiles(data, file.FileName, out fileId);

                if (string.IsNullOrEmpty(result))
                {
                    results.Add(new FileUploadResult() { name = file.FileName, size = file.ContentLength, id = fileId });
                    return new JsonNetResult(results);
                }
            }

            results.Add(new FileUploadResult(){error = FileUploadErrorKeys.EmptyResult});
            return new JsonNetResult(results);
        }

        public FileResult DownloadFile()
        {
            throw new NotImplementedException();
        }

        public JsonNetResult RunValidation(int id)
        {
            _phredService.ExecuteValidation(id);

            return new JsonNetResult(true);
        }

        public ActionResult QualityControl(int id)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);
            return View(barcode);
        }
    }

    public class FileUploadResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string url
        {
            get
            {
                var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
                return url.Action("DownloadFile", "Results", new { Id = id }); // Will output the proper link accordi
            }
        }

        public string error { get; set; }
    }
}
