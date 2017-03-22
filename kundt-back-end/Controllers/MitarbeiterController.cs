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
    public class MitarbeiterController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        // GET: Mitarbeiter
        public ActionResult Index()
        {
            var tblMitarbeiter = db.tblMitarbeiter.Include(t => t.tblLogin);
            return View(tblMitarbeiter.ToList());
        }

        // GET: Mitarbeiter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMitarbeiter tblMitarbeiter = db.tblMitarbeiter.Find(id);
            if (tblMitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(tblMitarbeiter);
        }

        // GET: Mitarbeiter/Create
        public ActionResult Create()
        {
            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email");
            return View();
        }

        // POST: Mitarbeiter/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMitarbeiter,MAVorname,MANachname,MAAnrede")] tblMitarbeiter tblMitarbeiter)
        {
            if (ModelState.IsValid)
            {
                db.tblMitarbeiter.Add(tblMitarbeiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email", tblMitarbeiter.IDMitarbeiter);
            return View(tblMitarbeiter);
        }

        // GET: Mitarbeiter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMitarbeiter tblMitarbeiter = db.tblMitarbeiter.Find(id);
            if (tblMitarbeiter == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email", tblMitarbeiter.IDMitarbeiter);
            return View(tblMitarbeiter);
        }

        // POST: Mitarbeiter/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMitarbeiter,MAVorname,MANachname,MAAnrede")] tblMitarbeiter tblMitarbeiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMitarbeiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email", tblMitarbeiter.IDMitarbeiter);
            return View(tblMitarbeiter);
        }

        // GET: Mitarbeiter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMitarbeiter tblMitarbeiter = db.tblMitarbeiter.Find(id);
            if (tblMitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(tblMitarbeiter);
        }

        // POST: Mitarbeiter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMitarbeiter tblMitarbeiter = db.tblMitarbeiter.Find(id);
            db.tblMitarbeiter.Remove(tblMitarbeiter);
            db.SaveChanges();
            return RedirectToAction("Index");
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
