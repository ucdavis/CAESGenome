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

        public ActionResult UploadFile()
        {
            return View();
        }

        private string _storageLocation = ConfigurationManager.AppSettings["StorageLocation"];

        [HttpPost]
        public JsonNetResult UploadFile(HttpPostedFileBase file)
        {
            var result = new List<FileUploadResult>();

            if (file != null)
            {
                // check the filename
                // ^[0123456789]+_[ABCDEFGH]{1}[0123456789]{2}\.
                // ex. 2020717_A01.ab1
                var pattern = @"^[0123456789]+_[ABCDEFGH]{1}[0123456789]{2}\.";
                var regex = new Regex(pattern);
                var match = regex.Match(file.FileName);
                
                if (!match.Success)
                {
                    result.Add(new FileUploadResult(){error = FileUploadErrorKeys.InvalidFileName});
                    return new JsonNetResult(result);
                }
            
                // extract file contents
                var reader = new BinaryReader(file.InputStream);
                var data = reader.ReadBytes(file.ContentLength);

                // parse the name
                var name = file.FileName;
                var barcodeId = Convert.ToInt32(name.Substring(0, name.IndexOf("_")));

                // split after the barcode
                var location = name.Substring(name.IndexOf("_") + 1);
                // remove the extension
                location = location.Substring(0, location.IndexOf("."));

                var row = location[0];
                var col = Convert.ToInt32(location.Substring(1));

                // load up the barcode
                var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(barcodeId);
    
                if (barcode == null)
                {
                    result.Add(new FileUploadResult() { error = FileUploadErrorKeys.InvalidBarcode });
                    return new JsonNetResult(result);
                }

                // check for an existing file
                var existingFile = _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Barcode == barcode && a.Column == col && a.Row == row).FirstOrDefault();

                if (WriteFile(file.FileName, data, string.Format(@"{0}\raw\{1}", _storageLocation, barcodeId)))
                {
                    if (existingFile == null)
                    {
                        var bfile = new BarcodeFile()
                        {
                            Barcode = barcode,
                            Column = col,
                            Row = row, Uploaded = true
                        };

                        _repositoryFactory.BarcodeFileRepository.EnsurePersistent(bfile);

                        result.Add(new FileUploadResult() { name = name, size = file.ContentLength, id = bfile.Id });
                        return new JsonNetResult(result);
                    }
                    
                    result.Add(new FileUploadResult() { name = name, size = file.ContentLength, id = existingFile.Id });
                    return new JsonNetResult(result);
                }
            }

            return new JsonNetResult(new {Error = FileUploadErrorKeys.EmptyResult});
        }

        private bool WriteFile(string filename, byte[] data, string path)
        {
            try
            {
                // check if the path exists
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                System.IO.File.WriteAllBytes(string.Format(@"{0}\{1}", path, filename), data);

                return true;
            }
            catch
            {
                return false;
            }
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
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            foreach(var file in barcode.BarcodeFiles)
            {
                //File.WriteAllBytes("\\169.237.124.27", file.ResultFile);

                //System.IO.File.WriteAllBytes(string.Format(@"\\169.237.124.27\CgfDataFiles\{0}", file.ResultFileName), file.ResultFile);
            }

            //var barcodeFile = _repositoryFactory.BarcodeFileRepository.GetNullableById(id);
            
            //if (barcodeFile != null)
            //{
            //    _phredService.PushToServer(barcodeFile.Barcode.Id.ToString(), barcodeFile.Barcode.BarcodeFiles.Select(a => new PlateResult(){File = a.ResultFile, Filename = a.ResultFileName}).ToList());
            //}

            return new JsonNetResult(true);
        }
    }

    public class FileUploadErrorKeys
    {
        public const string MaxFileSize = "maxFileSize";
        public const string MinFileSize = "minFileSize";
        public const string AcceptFileTypes = "acceptFileTypes";
        public const string MaxNumOfFiles = "maxNumberOfFiles";
        public const string UploadedBytes = "uploadedBytes";
        public const string EmptyResult = "emptyResult";
        public const string InvalidFileName = "invalidFileName";
        public const string InvalidBarcode = "invalidBarcode";
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
