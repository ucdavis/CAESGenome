using System.Linq;
using System.Web.Mvc;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using CAESGenome.Services;
using Ionic.Zip;

namespace CAESGenome.Controllers
{
    public class DownloadsController : ApplicationController
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPhredService _phredService;

        public DownloadsController(IRepositoryFactory repositoryFactory, IPhredService phredService)
        {
            _repositoryFactory = repositoryFactory;
            _phredService = phredService;
        }

        [Authorize(Roles=RoleNames.User)]
        public ActionResult Index()
        {
            var barcodes = _repositoryFactory.BarcodeRepository.Queryable.Where(a => a.UserJobPlate.UserJob.User.UserName == CurrentUser.Identity.Name && a.Done && a.AllowDownload).OrderBy(a => a.DateTimeValidated);
            return View(barcodes);
        }

        [Authorize(Roles=RoleNames.User)]
        public FileResult File(int id)
        {
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(id);

            if (barcode != null)
            {
                return File(_phredService.DownloadResults(barcode), "application/zip", string.Format("{0}.zip", barcode.PlateName));
            }

            // return empty result
            return File(new byte[0], string.Empty);
        }
    }
}
