using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAESGenome.Core.Domain;
using CAESGenome.Services;

namespace CAESGenome.Controllers
{
    public class PrinterController : ApplicationController
    {
        private readonly IBarcodeService _barcodeService;

        public PrinterController(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        public ActionResult Print()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Print(int id, string name)
        {
            _barcodeService.Print(id, name);
            return View();
        }

    }
}
