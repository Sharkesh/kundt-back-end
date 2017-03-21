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
            var buchungUebersicht = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde);

            return View(buchungUebersicht.ToList());            
        }

        /// Hier einen Teil einbauen für die Suchmaske, HttpPost
        /// allerdings Post der Partial View oder Index?
        /// Prozedur für Suchfilter einbauen, evtl dazupassendes Model anlegen wenn unbedingt nötig?



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


            /// auto generierter Teil sinnvoll????
            ViewBag.FKAuto = new SelectList(db.tblAuto, "IDAuto", "MietPreis", tblBuchung.FKAuto);
            ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "IDKunde", tblBuchung.FKKunde);
                        
            return View(tblBuchung);
        }

        // POST: Buchung/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BuchungEditModel tblBuchung)
        //public ActionResult Edit(tblBuchung tblBuchung)
        {
            if (ModelState.IsValid)
            {
                /// Hol dir die IDBuchung aus BuchungEditModel und suchen den gleichen
                /// Datensatz mit der selben ID aus der Datenbank von der tblBuchung
                /// und update die bearbeiteten Felder.
                var Buchung = (from b in db.tblBuchung where b.IDBuchung == tblBuchung.IDBuchung select b).FirstOrDefault<tblBuchung>();

                /// Das Abfrage Statement austauschen durch die BuchungUpdate Prozedur und den Speichervorgang
                /// im unteren Teil dementsprechend bearbeiten.

                if (tblBuchung.BuchungBis < tblBuchung.BuchungVon)
                {
                    TempData["fail"] = "DatumBis muss > sein als DatumVon du spasti!";
                    return RedirectToAction("Edit", tblBuchung.IDBuchung);                    
                }
                else
                {
                    Buchung.BuchungVon = tblBuchung.BuchungVon;
                    Buchung.BuchungBis = tblBuchung.BuchungBis;
                    Buchung.BuchungStatus = tblBuchung.BuchungStatus;
                    Buchung.Versicherung = tblBuchung.Versicherung;
                    Buchung.Storno = tblBuchung.Storno;
                }

                
                /// wenn das Update oben durch Prozedur ausgetauscht wird SaveChanges() auskommentieren oder löschen?
                db.SaveChanges();


                /// Standartmäßige Weiterleitung auf BuchungUebersicht nach dem speichern, oder evtl per ViewBag
                /// den Ausgangspunkt speichern über den man zur Edit Seite gekommen ist? Wenn ja wie?
                return RedirectToAction("Index");
            }

            /// auto generierter Teil sinnvoll???
            ViewBag.FKAuto = new SelectList(db.tblAuto, "IDAuto", "PS", tblBuchung.FKAuto);
            ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "Vorname", tblBuchung.FKKunde);

            return View(tblBuchung);
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
