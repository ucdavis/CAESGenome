using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;
using CAESGenome.Models;
using CAESGenome.Services;

namespace CAESGenome.Controllers
{
    public class QualityControlController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPhredService _phredService;

        public QualityControlController(IRepositoryFactory repositoryFactory, IPhredService phredService)
        {
            _repositoryFactory = repositoryFactory;
            _phredService = phredService;
        }

        /// <summary>
        /// Displays calendars of quality control
        /// </summary>
        /// <param name="month">Leading Month</param>
        /// <param name="year">Leading Year</param>
        /// <param name="direction">Direction to change calendar</param>
        /// <returns></returns>
        public ActionResult Index(int? month, int? year, CalendarDirection? direction = null)
        {
            month = month ?? DateTime.Now.Month;
            year = year ?? DateTime.Now.Year;
            var date = new DateTime(year.Value, month.Value, 1);

            if (direction != null)
            {
                if (direction == CalendarDirection.Left)
                {
                    date = date.AddMonths(-1);
                }
                else
                {
                    if (date.AddMonths(1) < DateTime.Now)
                    {
                        date = date.AddMonths(1);    
                    }
                }
            }

            var barcodes =
                _repositoryFactory.BarcodeRepository.Queryable.Where(
                    a =>
                    a.DateTimeValidated.HasValue && a.DateTimeValidated.Value.Date >= date.AddMonths(-2).Date && a.DateTimeValidated.Value < date.AddMonths(1)
                    && a.BarcodeFiles.Any()
                    ).Select(a => a.DateTimeValidated.Value.Date).Distinct();

            ViewBag.Month = date.Month;
            ViewBag.Year = date.Year;

            var re = new Regex(@"^Backup$");
            ViewBag.PendingUpload = Directory.EnumerateDirectories(ConfigurationManager.AppSettings["UploadLocation"]).Where(d => !re.IsMatch(Path.GetFileName(d))).Count();
            // 2013-01-18 by kjt: Added additional check to exclude non-sequencing jobs from Pending Validation count.
            //ViewBag.PendingValidation = _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Uploaded && !a.Validated).Select(a => a.Barcode)..Distinct().Count();
            ViewBag.PendingValidation = _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Uploaded && !a.Validated && a.Barcode.UserJobPlate.UserJob.JobType.HasSequencing).Select(a => a.Barcode).Distinct().Count();

            return View(barcodes);
        }

        [HttpPost]
        public ActionResult ScanFiles()
        {
            _phredService.ScanFiles();

            Message = "Files have been uploaded.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PhredValidation()
        {
            // 2013-01-18 by kjt: Added additional check to exclude non-sequencing jobs from barcodes requiring validation.
            //var barcodes = _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Uploaded && !a.Validated).Select(a => a.Barcode.Id).Distinct();
            var barcodes = _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Uploaded && !a.Validated && a.Barcode.UserJobPlate.UserJob.JobType.HasSequencing).Select(a => a.Barcode.Id).Distinct();
            // For some reason there are zero barcodes after phred has been run, so 
            // let's try saving the count prior to running the validation.
            var barcodeCount = barcodes.Count(); 

            foreach(var bc in barcodes)
            {
                _phredService.ExecuteValidation(bc);
            }

            Message = string.Format("Validation for {0} barcode(s) has been run.", barcodeCount);
            return RedirectToAction("Index");
        }

        public ActionResult ByDate(DateTime date)
        {
            var viewModel = QualityControlByDateViewModel.Create(_repositoryFactory, date);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ChangeDownload(int id)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            if (barcode == null)
            {
                Message = "barcode not found";
                return RedirectToAction("Index");
            }

            barcode.AllowDownload = !barcode.AllowDownload;
            _repositoryFactory.BarcodeRepository.EnsurePersistent(barcode);

            // 2013-01-17 by kjt: Changed message to contain UserJob.Name vs. barcode.id so job could be more easily identified.
            Message = string.Format("Downloads for {0} have been {1}", barcode.UserJobPlate.UserJob.Name, barcode.AllowDownload ? "enabled" : "disabled");

            // 2013-04-09 by kjt: Added client email notification when download has been enabled/re-enabled:
            if (barcode.AllowDownload)
                EmailService.SendMessage(barcode.UserJobPlate.UserJob);

            return RedirectToAction("ByDate", new {date = barcode.DateTimeValidated.Value.Date});
        }

        public enum CalendarDirection {Left, Right}
    }
}
