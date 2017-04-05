using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using kundt_back_end.Models;


namespace kundt_back_end.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "M,A")]
        public ActionResult Index()
        {
            return RedirectToAction("Index","BuchungUebersicht");
        }
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        //GET: /Home/KundenUebersicht
        [HttpGet]
        [Authorize(Roles = "M,A")]
        public ActionResult KundenUebersicht()//(endl)
        {
            KundenUebersichtContainerModel cm = new KundenUebersichtContainerModel();
            cm.filtermodel = (KundenUebersichtFilterModel)Session["Filterparameter"];

            if (cm.filtermodel == null)
            {
                cm.kundenlist = db.pKundenAnzeigen(null, null, null, null, null);
            }
            else
            {
                cm.kundenlist = db.pKundenAnzeigen(cm.filtermodel.KundenName, cm.filtermodel.KundenNr, cm.filtermodel.Ort, cm.filtermodel.Plz, cm.filtermodel.Buchungsstatus);
            }

            return View(cm);
        }

        [HttpPost]
        [Authorize(Roles = "M,A")]
        public ActionResult KundenUebersichtFilter(KundenUebersichtFilterModel km) //(endl)
        {

            Session["Filterparameter"] = km;
            return RedirectToAction("KundenUebersicht", "Home");
        }



        //GET: /Home/KundenBearbeiten/id
        [Authorize(Roles = "M,A")]
        public ActionResult KundenBearbeiten(int? id) //(endl)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKunde idk = db.tblKunde.Find(id);
            if (idk == null)
            {
                return HttpNotFound();
            }
           
            KundeEditModel kem = new KundeEditModel();
            kem.idkunde = idk.IDKunde;
            kem.Vorname = idk.Vorname;
            kem.Nachname = idk.Nachname;
            kem.Straße = idk.Strasse;
            kem.Plz = idk.tblPLZOrt.PLZ;
            kem.Ort = idk.tblPLZOrt.Ort;
            kem.Email = idk.tblLogin.Email;
            kem.Telefon = idk.Telefon;
            kem.PassNr = idk.ReisepassNr;
            kem.GebDat = idk.GebDatum;

            return View(kem);
        }

        //POST: /Home/KundeBearbeiten
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "M,A")]
        public ActionResult KundenBearbeiten(KundeEditModel kem) //(endl)
        {
            if (ModelState.IsValid)
            {
                db.pKundeEdit(kem.idkunde, kem.Vorname, kem.Nachname, kem.Straße, kem.Plz, kem.Ort, kem.Email,
                    kem.Telefon, kem.PassNr, kem.GebDat);
                return RedirectToAction("KundenUebersicht", "Home");
            }
            return View(kem);
        }


        [Authorize(Roles = "M,A")]
        public ActionResult AutoUebersicht()
        {
            // die variable "tblAuto" enthält die daten aus der Tabelle Kunde/ort/Login
            var tblAuto = db.tblAuto;
            // Schmeis dem View die Liste mit allen Daten aus der Variable "tblAuto" ins Gsicht!
            return View(tblAuto.ToList());
        }
        [Authorize(Roles = "A")]
        public ActionResult Einstellung()
        {
            return View();
        }
        [Authorize(Roles = "M,A")]
        public ActionResult AutoHinzufuegen()
        {
            return View();
        }
        [Authorize(Roles = "M,A")]
        public ActionResult AutoDetail()
        {
            return View();
        }
        [Authorize(Roles = "M,A")]
        public ActionResult BuchungDetail()
        {
            return View();
        }
        // Get: tblMitarbeiter 
        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterHinzufuegen()
        {// Mario Anfang
            var tblMA = db.tblMitarbeiter.Include(tblMitarbeiter => tblMitarbeiter.tblLogin);
            return View(tblMA.ToList());
        }
        // Überprüfung ob die ID die mitgegeben wurde, wenn null ist mach Fehlerbehebung
        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterHinzufugen(int? id) // Frage: richtige ID aus der Datenbank?
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMitarbeiter tblMA = db.tblMitarbeiter.Find(id);
            if (tblMA == null)
            {
                return HttpNotFound();
            }
            return View(tblMA);
        }
        // Generiere ein neues Datenbank Objekt
        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterHinzufuegenCreate()
        {
            ViewBag.FKKategorie = new SelectList(db.tblMitarbeiter, "IDMitarbeiter", "MAVorname", "MANachname");
            return View();
        }
        //// POST: tblAuto/Create
        //// Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        //// finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "A")]
        public ActionResult Create([Bind(Include = "IDMitarbeiter, MAVorname, MANachname")] tblMitarbeiter tblMA)
        {
            if (ModelState.IsValid)
            { //Wenn die Datein Valide sind speichere sie in die Datenbank
                db.tblMitarbeiter.Add(tblMA);
                db.SaveChanges();
                return RedirectToAction("MitarbeiterUebersicht");
            }
            // Gib dem ViewBag die Objekte von MitarbeiterHinzufuegen und Login in die Hand
            ViewBag.FKMitarbeiterHinzufuegen = new SelectList(db.tblMitarbeiter, "IDMitarbeiter", "MAVorname", "MANachname");
            ViewBag.FKLogin = new SelectList(db.tblLogin, "IDLogin", "Email");
            return View(tblMA);
        } // Mario Ende

        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterDetail()
        {// Mario Anfang
            var tblMA = db.tblMitarbeiter.Include(tblMitarbeiter => tblMitarbeiter.tblLogin);
            return View(tblMA.ToList());
        }
        // Überprüfung ob die ID die mitgegeben wurde, wenn null ist mach Fehlerbehebung
        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterDetails(int? id) // Frage: richtige ID aus der Datenbank?
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMitarbeiter tblMA = db.tblMitarbeiter.Find(id);
            if (tblMA == null)
            {
                return HttpNotFound();
            }
            return View(tblMA);
        }
        // Generiere ein neues Datenbank Objekt
        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterDetailsCreate()
        {
            ViewBag.FKKategorie = new SelectList(db.tblMitarbeiter, "IDMitarbeiter", "MAVorname", "MANachname");
            return View();
        }
        //// POST: tblAuto/Create
        //// Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        //// finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterHinzufuegenCreate([Bind(Include = "IDMitarbeiter, MAVorname, MANachname")] tblMitarbeiter tblMA)
        {
            if (ModelState.IsValid)
            { //Wenn die Datein Valide sind speichere sie in die Datenbank
                db.tblMitarbeiter.Add(tblMA);
                db.SaveChanges();
                return RedirectToAction("MitarbeiterUebersicht");
            }
            // Gib dem ViewBag die Objekte von MitarbeiterHinzufuegen und Login in die Hand
            ViewBag.FKMitarbeiterHinzufuegen = new SelectList(db.tblMitarbeiter, "IDMitarbeiter", "MAVorname", "MANachname");
            ViewBag.FKLogin = new SelectList(db.tblLogin, "IDLogin", "Email");
            return View(tblMA);
        } // Mario Ende
        [Authorize(Roles = "A")]
        public ActionResult MitarbeiterUebersicht()

        {
            return View();
        }
        [Authorize(Roles = "M,A")]
        public ActionResult BuchungUebersicht()
        {
            return View();
        }
    }
}