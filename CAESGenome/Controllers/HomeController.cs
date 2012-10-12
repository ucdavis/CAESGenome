using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CAESGenome.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult Rates()
        {
            return View();
        }

        [Authorize]
        public ActionResult Test2()
        {
            return View();
        }

        
        public ActionResult Test()
        {
            WebSecurity.CreateUserAndAccount("lai@caes.ucdavis.edu", "password", new { FirstName = "Alan", LastName = "Lai", Title = "Programmer", Phone = "530-754-7771", fax = string.Empty});
            return View();
        }
    }
}
