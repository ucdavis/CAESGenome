using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Resources;
using CAESGenome.Services;
using UCDArch.Web.ActionResults;

namespace CAESGenome.Controllers
{
    [Authorize(Roles = RoleNames.Staff)]
    public class ResultsController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPhredService _phredService;
        private readonly IBarcodeService _barcodeService;

        public ResultsController(IRepositoryFactory repositoryFactory, IPhredService phredService, IBarcodeService barcodeService)
        {
            _repositoryFactory = repositoryFactory;
            _phredService = phredService;
            _barcodeService = barcodeService;
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

        public JsonNetResult RunValidation(int id)
        {
            _phredService.ExecuteValidation(id);

            return new JsonNetResult(true);
        }

        public ActionResult QualityControl(int id)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            if (barcode == null)
            {
                Message = "Barcode not found.";
                return RedirectToAction("Index", "UserJob");
            }

            return View(barcode);
        }

        [HttpPost]
        public RedirectToRouteResult DecideQualityControl(int id, bool accepted)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            if (barcode != null)
            {
                if (accepted)
                {
                    _barcodeService.AcceptQualityControl(_repositoryFactory, barcode);
                    Message = "Barcode data has been accepted and user will be notified.";
                }
                else
                {
                    _barcodeService.DeclineQualityControl(_repositoryFactory, barcode);
                    Message = "Barcode data has been declined and new barcode has been created";
                }
            }
            else
            {
                Message = "Barcode not found";
                return RedirectToAction("Index", "UserJob");
            }

            return RedirectToAction("QualityControl", new {id = id});
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
