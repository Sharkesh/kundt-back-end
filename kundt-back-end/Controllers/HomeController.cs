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
        public ActionResult Index()
        {
            return View();
        }
        // it22Autoverlei in eine Objeckt mit namen db für weiterverwendung und weiterleitung
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        //GET: /Home/KundenBearbeiten
        public ActionResult KundenUebersicht()//(endl)
        {
            //Setzte alle Parameter auf null, um beim Aufruf alle Kunden anzuzeigen
            string searchName = null;
            int? searchKundenNr = null;
            string searchOrt = null;
            string searchPlz = null;
            var ku = db.pKundenAnzeigen(searchName, searchKundenNr, searchOrt, searchPlz);
            return View(ku);
        }


        //ObjectResult ist vielleicht falsch. 
        [HttpPost]
        public ObjectResult KundenUebersicht(string Name, int KundenNr, string Ort, string PLZ) //Funktioniert so sicher noch nicht?(endl)
        {
            string FilterName = Name;
            int FilterKundenNr = KundenNr;
            string FilterOrt = Ort;
            string FilterPLZ = PLZ;

            var ku = db.pKundenAnzeigen(FilterName, FilterKundenNr, FilterOrt, FilterPLZ);

            return (ku);    
        }

        //GET: /Home/KundenBearbeiten/id
        public ActionResult KundenBearbeiten(int? id) //(endl)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKunde tblKundeID = db.tblKunde.Find(id);
            if (tblKundeID == null)
            {
                return HttpNotFound();
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-AT");
            //ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "MietPreis", tblKunde.FKPLZOrt);
            //ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "IDKunde", tblBuchung.FKKunde);
            return View(tblKundeID);
        }



        public ActionResult AutoUebersicht()
        {
            // die variable "tblAuto" enthält die daten aus der Tabelle Kunde/ort/Login
            var tblAuto = db.tblAuto;
            // Schmeis dem View die Liste mit allen Daten aus der Variable "tblAuto" ins Gsicht!
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
        // Get: tblMitarbeiter 
        public ActionResult MitarbeiterHinzufuegen()
        {// Mario Anfang
            var tblMA = db.tblMitarbeiter.Include(tblMitarbeiter => tblMitarbeiter.tblLogin);
            return View(tblMA.ToList());
        }
        // Überprüfung ob die ID die mitgegeben wurde, wenn null ist mach Fehlerbehebung
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

        public ActionResult MitarbeiterDetail()
        {// Mario Anfang
            var tblMA = db.tblMitarbeiter.Include(tblMitarbeiter => tblMitarbeiter.tblLogin);
            return View(tblMA.ToList());
        }
        // Überprüfung ob die ID die mitgegeben wurde, wenn null ist mach Fehlerbehebung
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