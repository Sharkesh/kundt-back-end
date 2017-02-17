using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kundt_back_end.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UebersichtKunden()
        {
            return View();
        }
        public ActionResult UebersichtAuto()
        {
            return View();
        }
        public ActionResult Einstellung()
        {
            return View();
        }
        public ActionResult HinzufuegenAuto()
        {
            return View();
        }
    }
}