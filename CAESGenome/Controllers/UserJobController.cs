using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;
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

        [Authorize(Roles = RoleNames.User + "," + RoleNames.Staff)]
        public ActionResult Index(bool owned = false)
        {
            var userJobs = new List<UserJob>();

            if (CurrentUser.IsInRole(RoleNames.Staff) && !owned)
            {
                userJobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.IsOpen).OrderBy(a => a.DateTimeCreated).ToList();
            }
            else if (CurrentUser.IsInRole(RoleNames.User))
            {
                var user = GetCurrentUser();
                userJobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.User.Id == user.Id && a.IsOpen).OrderBy(a => a.JobType.Id).ThenBy(a => a.DateTimeCreated).ToList();
            }

            ViewBag.Owned = owned;
            return View(userJobs);
        }

        [Authorize(Roles = RoleNames.User)]
        public ActionResult UserCompleted()
        {
            var user = GetCurrentUser();
            var userJobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => !a.IsOpen && a.User.Id == user.Id).OrderBy(a => a.DateTimeCreated).ToList();
            return View(userJobs);
        }

        [Authorize(Roles = RoleNames.User + "," + RoleNames.Staff)]
        public ActionResult Details(int id, bool completed = false)
        {
            var uj = _repositoryFactory.UserJobRepository.GetNullableById(id);
            
            if (uj == null)
            {
                Message = "Job could not be located, please try again.";
                return RedirectToAction("Index");
            }

            ViewBag.Completed = completed;

            return View(uj);
        }

        /// <summary>
        /// Advances barcode to the next intended stage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles=RoleNames.Staff)]
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

        [HttpPost]
        [Authorize(Roles = RoleNames.Staff)]
        public ActionResult AdvanceStageBarcodes(int id, string stage)
        {
            var userJob = _repositoryFactory.UserJobRepository.GetNullableById(id);

            if (userJob == null)
            {
                Message = "Unable to load job, please try again.";
            }
            else
            {
                var barcodes = userJob.UserJobPlates.SelectMany(a => a.Barcodes).Where(a => a.Stage.Id == stage && !a.Done).ToList();
                foreach(var barcode in barcodes)
                {
                    _barcodeService.AdvanceStage(_repositoryFactory, barcode, barcode.UserJobPlate.UserJob);
                }
            }

            return RedirectToAction("Details", new {id = id});
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Staff)]
        public ActionResult CloseJob(int id)
        {
            var userjob = _repositoryFactory.UserJobRepository.GetNullableById(id);

            if (userjob == null)
            {
                Message = "Job not found.";
                return RedirectToAction("Index");
            }

            userjob.IsOpen = false;
            userjob.LastUpdate = DateTime.Now;
            _repositoryFactory.UserJobRepository.EnsurePersistent(userjob);

            Message = "User job has been closed";

            return RedirectToAction("Details", new {id = id});
        }

        /// <summary>
        /// Prints label for a particular barcode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles=RoleNames.Staff)]
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
        [Authorize(Roles=RoleNames.Staff)]
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

            foreach (var b in barcodes)
            {
                _barcodeService.Print(b.Id, b.PlateName);
            }

            Message = "Labels have been sent to the printer";
            return RedirectToAction("Details", new { id = userJob.Id });
        }

        [Authorize(Roles = RoleNames.Staff)]
        public ActionResult Completed()
        {
            var results = new List<UsageDetailsModel>();

            // get the years
            var years = _repositoryFactory.UserJobRepository.Queryable.Where(a => !a.IsOpen).Select(a => a.LastUpdate.Year).Distinct();
            
            foreach(var y in years)
            {
                var jobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.LastUpdate.Year == y).GroupBy(
                        a => a.LastUpdate.Month).Select(a => new UsageDetailsModel() {Year = y, Month = a.Key, Count = a.Count()});

                results.AddRange(jobs);
            }
            
            return View(results);
        }

        [Authorize(Roles = RoleNames.Staff)]
        public ActionResult MonthlyDetails(int year, int month)
        {
            var jobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.LastUpdate.Year == year && a.LastUpdate.Month == month && !a.IsOpen);
            return View(jobs);
        }

        [Authorize(Roles = RoleNames.Staff)]
        public RedirectToRouteResult SearchByBarcode(int id)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            if (barcode != null)
            {
                return RedirectToAction("Details", new {id = barcode.UserJobPlate.UserJob.Id});
            }

            Message = "Barcode not found.";
            return RedirectToAction("Index");
        }
    }
}
