using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Suporteri.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Latitude = "47.22973968433303";
            ViewBag.Lonitude = "24.775307178497314";
            return View();
        }
    }
}