using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Models;
using CAESGenome.Services;

namespace CAESGenome.Controllers
{
    //[Authorize(Roles = RoleNames.User)]
    public class UserJobController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IBarcodeService _barcodeService;

        public UserJobController(IRepositoryFactory repositoryFactory, IBarcodeService barcodeService)
        {
            _repositoryFactory = repositoryFactory;
            _barcodeService = barcodeService;
        }

        public ActionResult Index()
        {
            var user = GetCurrentUser();
            var userJobs = _repositoryFactory.UserJobRepository.Queryable.Where(a => a.User.Id == user.Id);
            return View(userJobs);
        }

        /// <summary>
        /// Create a genotyping job submission
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateGenotyping()
        {
            return View();
        }

        /// <summary>
        /// Create a qbot job submission
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateQbot()
        {
            return View();
        }

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

    }
}
