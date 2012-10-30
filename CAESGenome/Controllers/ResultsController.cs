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
        private string _storageLocation = ConfigurationManager.AppSettings["StorageLocation"];
        
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

            //var result = new List<FileUploadResult>();

            //if (file != null)
            //{
            //    // check the filename
            //    // ^[0123456789]+_[ABCDEFGH]{1}[0123456789]{2}\.
            //    // ex. 2020717_A01.ab1
            //    var pattern = @"^[0123456789]+_[ABCDEFGH]{1}[0123456789]{2}\.";
            //    var regex = new Regex(pattern);
            //    var match = regex.Match(file.FileName);

            //    if (!match.Success)
            //    {
            //        result.Add(new FileUploadResult() { error = FileUploadErrorKeys.InvalidFileName });
            //        return new JsonNetResult(result);
            //    }

                

            //    // parse the name
            //    var name = file.FileName;
            //    var barcodeId = Convert.ToInt32(name.Substring(0, name.IndexOf("_")));

            //    // split after the barcode
            //    var location = name.Substring(name.IndexOf("_") + 1);
            //    // remove the extension
            //    location = location.Substring(0, location.IndexOf("."));

            //    var row = location[0];
            //    var col = Convert.ToInt32(location.Substring(1));

            //    // load up the barcode
            //    var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(barcodeId);

            //    if (barcode == null)
            //    {
            //        result.Add(new FileUploadResult() { error = FileUploadErrorKeys.InvalidBarcode });
            //        return new JsonNetResult(result);
            //    }

            //    // check for an existing file
            //    var existingFile = _repositoryFactory.BarcodeFileRepository.Queryable.FirstOrDefault(a => a.Barcode == barcode && a.WellColumn == col && a.WellRow == row);

            //    if (System.IO.File.Exists(string.Format(@"{0}\raw\{1}\{2}", _storageLocation, barcodeId, file.FileName)))
            //    {
            //        result.Add(new FileUploadResult() { error = FileUploadErrorKeys.DuplicateFile });
            //        return new JsonNetResult(result);
            //    }

            //    if (WriteFile(file.FileName, data, string.Format(@"{0}\raw\{1}", _storageLocation, barcodeId)))
            //    {
            //        if (existingFile == null)
            //        {
            //            var bfile = new BarcodeFile()
            //            {
            //                Barcode = barcode,
            //                WellColumn = col,
            //                WellRow = row,
            //                Uploaded = true
            //            };

            //            _repositoryFactory.BarcodeFileRepository.EnsurePersistent(bfile);

            //            result.Add(new FileUploadResult() { name = name, size = file.ContentLength, id = bfile.Id });
            //            return new JsonNetResult(result);
            //        }

            //        result.Add(new FileUploadResult() { name = name, size = file.ContentLength, id = existingFile.Id });
            //        return new JsonNetResult(result);
            //    }
            //}

            
        }



    }
}
