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

            /// Hier gehört noch die Abfrage der Buchungsauflistung der nächsten 14 Tage her
            var buchungUebersicht = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde);

            return View(buchungUebersicht.ToList());            
        }

        //[HttpPost]
        //public ActionResult Index(BuchungSearchModel filter)
        //{
        //    List<OffeneBuchungenTodayPlus13> buchungslist = new List<OffeneBuchungenTodayPlus13>();

        //    if (filter != null)
        //    {              
        //        if (filter.name.Length > 0)
        //        {
        //            buchungslist = (from b in db.OffeneBuchungenTodayPlus13 where b.Nachname == filter.name select b).ToList<OffeneBuchungenTodayPlus13>();                    
        //        }

        //    }
        //    return View(buchungslist);
        //}



        // GET: Buchung/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBuchung tblBuchung = db.tblBuchung.Find(id);
            if (tblBuchung == null)
            {
                return HttpNotFound();
            }
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
                /// Hol dir die IDBuchung aus dem veränderten BuchungEditModel und suchen den gleichen
                /// Datensatz mit der selben ID aus der Datenbank von der tblBuchung
                /// und update die bearbeiteten Felder

                var Buchung = (from b in db.tblBuchung where b.IDBuchung == tblBuchung.IDBuchung select b).FirstOrDefault<tblBuchung>();
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

                

                db.SaveChanges();

                return RedirectToAction("Index");
            }
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
