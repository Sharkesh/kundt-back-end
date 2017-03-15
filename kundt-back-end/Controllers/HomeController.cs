using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Net;
using System.Threading;
using kundt_back_end.Models;


namespace kundt_back_end.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // it22Autoverlei in eine Objeckt mit namen db für weiterverwendung und weiterleitung
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        public ActionResult KundenUebersicht()
        {
            // Variable deklarieren und mit der tblKunde befüllen
            var varKundenListe = db.tblKunde;
            // Gieb dem View die Liste von Kunden
            return View(varKundenListe.ToList());
        }


        public ActionResult KundenDetail()
        {
            return View();
        }

        public ActionResult KundenBearbeiten(int? id) //Funktioniert so sicher noch nicht?(endl)
        {

        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKunde tblKundenKunde = db.tblKunde.Find(id);
            if (tblKundenKunde == null)
            {
                return HttpNotFound();
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-AT");
            //ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "MietPreis", tblKunde.FKPLZOrt);
            //ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "IDKunde", tblBuchung.FKKunde);
            return View(tblKundenKunde);
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