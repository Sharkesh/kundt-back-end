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

        //// GET: Mitarbeiter/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblMitarbeiter tblMitarbeiter = db.tblMitarbeiter.Find(id);
        //    if (tblMitarbeiter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblMitarbeiter);
        //}

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
        public ActionResult Edit(MitarbeiterModel MM)
        {
            if (ModelState.IsValid)
            {
                //Nach dem Gespeichert wurde schick den Benutzer zum Index zurück
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                //Gehe zurück zum Bearbeiten wen das verändern nicht funktioniert hat! 
                return RedirectToAction("Edit", MM.IDMitarbeiter);
            }
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
        public ActionResult Create(MitarbeiterModel MM)
        //public ActionResult Edit(BuchungEditModel BEM)
        {
            if (ModelState.IsValid) //ModelStat.IsValid funtzt nicht!
            {
                //Nach dem Gespeichert wurde schick den Benutzer zum Index zurück

                db.pNeuenMitarbeiterAnlegen(MM.Email, MM.Passwort, MM.Deaktiviert = false, MM.MitarbeiterVorname, MM.MitarbeiterNachname, MM.MitarbeiterAnrede);
                //db.SaveChanges;
                return RedirectToAction("Index");
            }
            else
            {
                //Gehe zurück zum Bearbeiten wen das verändern nicht funktioniert hat! 
                return RedirectToAction("Edit", MM.IDMitarbeiter);
            }

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
