using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kundt_back_end.Models;

namespace kundt_back_end.Controllers
{
    public class BuchungController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        // GET: Buchung
        public ActionResult Index()
        {
            var tblBuchung = db.tblBuchung.Include(t => t.tblAuto).Include(t => t.tblKunde);
            return View(tblBuchung.ToList());
        }

        // GET: Buchung/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.gesamtPreis = tblBuchung.Tage * tblBuchung.tblAuto.MietPreis;

            return View(tblBuchung);
        }

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
            ViewBag.FKAuto = new SelectList(db.tblAuto, "IDAuto", "PS", tblBuchung.FKAuto);
            ViewBag.FKKunde = new SelectList(db.tblKunde, "IDKunde", "Vorname", tblBuchung.FKKunde);
            return View(tblBuchung);
        }

        // POST: Buchung/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBuchung,BuchungAm,BuchungVon,BuchungBis,Versicherung,FKKunde,FKAuto,Tage,BuchungStatus,Storno")] tblBuchung tblBuchung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBuchung).State = EntityState.Modified;
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
