using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using kundt_back_end.Models;
using System.Web;

namespace kundt_back_end.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // it22Autoverlei mit namen DB ist eine neue blaaaa eh scho wissen. Wichtige zeile damit die weiterleitung
        // funtzt.
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        public ActionResult KundenUebersicht()
        {
            // die variable "tblKunde" enthält die daten aus der Tabelle Kunde/ort/Login
            var tblBu = db.tblBuchung.Include(t => t.tblKunde).Include(t => t.tblKunde.tblPLZOrt);
            // Schmeis dem View die Liste mit allen Daten aus der Variable "tblKunde" ins Gsicht!
            return View(tblBu.ToList());
        }


        public ActionResult KundenDetail()
        {
            return View();
        }
        public ActionResult AutoUebersicht()
        {
            // die variable "tblKunde" enthält die daten aus der Tabelle Kunde/ort/Login
            var tblAuto = db.tblAuto;
            // Schmeis dem View die Liste mit allen Daten aus der Variable "tblKunde" ins Gsicht!
            return View(tblAuto.ToList());
        }
        public ActionResult Einstellung()
        {
            return View();
        }
        public ActionResult AutoHinzufuegen()
        {
            return View();
        }
        public ActionResult AutoDetail()
        {
            return View();
        }
        public ActionResult BuchungDetail()
        {
            return View();
        }
        public ActionResult MitarbeiterHinzufuegen()
        {
            return View();
        }
        public ActionResult MitarbeiterDetail()

        {
            return View();
        }
        public ActionResult MitarbeiterUebersicht()

        {
            return View();
        }
        public ActionResult BuchungUebersicht()
        {
            return View();
        }
    }
}