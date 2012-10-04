using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Filters;
using CAESGenome.Helpers;
using CAESGenome.Services;

namespace CAESGenome.Controllers
{
    public class UserJobController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IBarcodeService _barcodeService;

        public UserJobController(IRepositoryFactory repositoryFactory, IBarcodeService barcodeService)
        {
            _repositoryFactory = repositoryFactory;
            _barcodeService = barcodeService;
        }

        [StaffAndUserOnly]
        public ActionResult Index()
        {
            var userJobs = new List<UserJob>();

            if (CurrentUser.IsInRole(RoleNames.Staff))
            {
                var user = GetCurrentUser();
                userJobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.User == user).OrderBy(a => a.IsOpen).OrderBy(a => a.DateTimeCreated).ToList();
            }
            else if (CurrentUser.IsInRole(RoleNames.User))
            {
                userJobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.IsOpen).OrderBy(a => a.JobType.Id).ThenBy(a => a.DateTimeCreated).ToList();
            }
            
            return View(userJobs);
        }

        [StaffAndUserOnly]
        public ActionResult Details(int id)
        {
            var uj = _repositoryFactory.UserJobRepository.GetNullableById(id);
            
            if (uj == null)
            {
                Message = "Job could not be located, please try again.";
                return RedirectToAction("Index");
            }

            return View(uj);
        }

        /// <summary>
        /// Advances barcode to the next intended stage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [StaffOnly]
        [HttpPost]
        public ActionResult AdvanceBarcode(int id)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            if (barcode == null)
            {
                Message = "There was an error loading the selected barcode.";
                return RedirectToAction("Index");
            }

            _barcodeService.AdvanceStage(_repositoryFactory, barcode, barcode.UserJobPlate.UserJob);

            return RedirectToAction("Details", new {id = barcode.UserJobPlate.UserJob.Id});
        }

        /// <summary>
        /// Prints label for a particular barcode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [StaffOnly]
        [HttpPost]
        public ActionResult PrintBarcode(int id)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            if (barcode == null)
            {
                Message = "There was an error loading the selected barcode.";
                return RedirectToAction("Index");
            }

            _barcodeService.Print(barcode.Id, barcode.PlateName);

            Message = "Label has been sent to the printer";
            return RedirectToAction("Details", new { id = barcode.UserJobPlate.UserJob.Id });
        }

        /// <summary>
        /// Prints all labels for a specific stage
        /// </summary>
        /// <param name="id">User job Id</param>
        /// <param name="stageId">Stage Id</param>
        /// <returns></returns>
        [StaffOnly]
        [HttpPost]
        public ActionResult PrintStageBarcodes(int id, string stageId)
        {
            var userJob = _repositoryFactory.UserJobRepository.GetNullableById(id);
            var stage = _repositoryFactory.StageRepository.GetNullableById(stageId);

            if (userJob == null || stage == null)
            {
                Message = "There was an error loading the userjob";
                return RedirectToAction("Index");
            }

            var barcodes = userJob.UserJobPlates.SelectMany(a => a.Barcodes).Where(a => a.Stage == stage);

            foreach(var b in barcodes)
            {
                _barcodeService.Print(b.Id, b.PlateName);
            }

            Message = "Labels have been sent to the printer";
            return RedirectToAction("Details", new { id = userJob.Id });
        }

    }
}
