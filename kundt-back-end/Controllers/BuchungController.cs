using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kundt_back_end.Models;
using System.Threading;
using System.Globalization;

namespace kundt_back_end.Controllers
{
    public class BuchungController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        // GET: Buchung
        [HttpGet]
        public ActionResult Index()
        {
            /// Hier die Prozedur der Buchungsauflistung der nächsten 14 Tage einfügen
            /// Evtl. neues Model dafür erstellen und die Buchungsauflistung als Liste übergeben
            //var buchungUebersicht = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde);

            var test = db.OffeneBuchungenTodayPlus13();
            var BuchungListe = new List<BuchungViewModel>();
            
            foreach (var b in test)
            {
                BuchungViewModel res = new BuchungViewModel();
                res.IDBuchung = b.IDBuchung;
                res.IDKunde = b.IDKunde;
                res.BuchungStatus = b.BuchungStatus;
                res.Nachname = b.Nachname;
                res.Vorname = b.Vorname;
                res.Ort = b.Ort;
                res.PLZ = b.PLZ;

                BuchungListe.Add(res);
            }
            //return View(buchungUebersicht.ToList());
            return View(BuchungListe);
        }

        /// Hier einen Teil einbauen für die Suchmaske, HttpPost
        /// allerdings Post der Partial View oder Index?
        /// Prozedur für Suchfilter einbauen, evtl dazupassendes Model anlegen wenn unbedingt nötig?

        //[HttpPost]
        //public ActionResult Index(int idbuchung, string nachname, int idkunde, string ort, string plz, bool offen, bool abgeschlossen, bool problem)
        //{

        //    return View();
        //}



        // GET: Buchung/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            /// Hier abklären ob evtl ein eigenes Model sinn macht oder ob das original Model ausreichend ist
            /// auch in Bezug auf Sicherheit

            tblBuchung tblBuchung = db.tblBuchung.Find(id);
            if (tblBuchung == null)
            {
                return HttpNotFound();
            }

            BuchungEditModel BEM = new BuchungEditModel();
            BEM.BuchungAm = tblBuchung.BuchungAm;
            BEM.BuchungVon = tblBuchung.BuchungVon;
            BEM.BuchungBis = tblBuchung.BuchungBis;
            BEM.BuchungStatus = tblBuchung.BuchungStatus;
            BEM.FKAuto = tblBuchung.FKAuto;
            BEM.FKKunde = tblBuchung.FKKunde;
            BEM.IDBuchung = tblBuchung.IDBuchung;
            BEM.Versicherung = tblBuchung.Versicherung;
            BEM.Tage = tblBuchung.Tage;
            BEM.MietPreis = tblBuchung.tblAuto.MietPreis;


            /// auto generierter Teil sinnvoll????
            ViewBag.FKAuto = new SelectList(db.tblAuto, "IDAuto", "MietPreis", tblBuchung.FKAuto);
            ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "IDKunde", tblBuchung.FKKunde);

            return View(BEM);
        }

        // POST: Buchung/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BuchungEditModel BEM)
        //public ActionResult Edit(tblBuchung tblBuchung)
        {
            if (ModelState.IsValid)
            {
                /// Alte Variante
                //var Buchung = (from b in db.tblBuchung where b.IDBuchung == BEM.IDBuchung select b).FirstOrDefault<tblBuchung>();


                if (BEM.BuchungBis < BEM.BuchungVon)
                {
                    TempData["fail"] = "DatumBis muss > sein als DatumVon!";
                    return RedirectToAction("Edit", BEM.IDBuchung);
                }
                if (BEM.abgeholt)
                {
                    BEM.BuchungStatus = "abgeholt";
                }
                if (BEM.zurueck)
                {
                    BEM.BuchungStatus = "zurueck";
                }

                /// Hol dir die IDBuchung aus BuchungEditModel und suchen den gleichen
                /// Datensatz mit der selben ID aus der Datenbank von der tblBuchung
                /// und update die bearbeiteten Felder.
                db.BuchungUpdate(BEM.IDBuchung, BEM.BuchungVon, BEM.BuchungBis, BEM.BuchungStatus, BEM.Storno, BEM.Versicherung);

                /// Alte Variante
                //Buchung.BuchungVon = BEM.BuchungVon;
                //Buchung.BuchungBis = BEM.BuchungBis;
                //Buchung.BuchungStatus = BEM.BuchungStatus;
                //Buchung.Versicherung = BEM.Versicherung;
                //Buchung.Storno = BEM.Storno;

                /// Alte Variante
                //db.SaveChanges();
                /// Standartmäßige Weiterleitung auf BuchungUebersicht nach dem speichern, oder evtl per ViewBag
                /// den Ausgangspunkt speichern über den man zur Edit Seite gekommen ist? Wenn ja wie?
                return RedirectToAction("Index");
            }

            /// auto generierter Teil sinnvoll???
            ViewBag.FKAuto = new SelectList(db.tblAuto, "IDAuto", "PS", BEM.FKAuto);
            ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "Vorname", BEM.FKKunde);

            return View(BEM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
