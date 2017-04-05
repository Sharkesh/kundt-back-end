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
        [Authorize(Roles = "A")]
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
        [Authorize(Roles = "A")]
        public ActionResult Create()
        {
            MitarbeiterModel test = new MitarbeiterModel();
            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email");
            return View(test);
        }

        // POST: Mitarbeiter/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "A")]
        public ActionResult Create(MitarbeiterModel MM)
        //public ActionResult Edit(BuchungEditModel BEM)
        {
            //Versuch diese Prozedur "pNeuenMitarbeiterAnlegen" ansonsten schicke den user zur 404 Error Seite
            // Sollte noch ersetzt werden durch ServerSeitige Validierung der email!
            try
            {
                if (ModelState.IsValid) //ModelStat.IsValid funtzt nicht!
                {
                    // Die gespeicherte Prozedur in der Datenbank werden mit den Parametern (richtige reihenfolge beachten) gefüttert!
                    db.pNeuenMitarbeiterAnlegen(MM.Email, MM.Passwort, MM.Deaktiviert = false, MM.MAVorname, MM.MANachname, MM.MAAnrede);
                    return RedirectToAction("Index");
                    //Nach dem Gespeichert wurde schick den Benutzer zum Index zurück
                }
                else
                {
                    //Gehe zurück zum Bearbeiten wen das verändern nicht funktioniert hat! 
                    return RedirectToAction("Edit", MM.IDMitarbeiter);
                }
            }
            catch
            {
                return HttpNotFound(); 
            }

        }

        // GET: Mitarbeiter/Edit/5
        [Authorize(Roles = "A")]
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
        [Authorize(Roles = "A")]
        public ActionResult Edit(MitarbeiterModel MM)
        {
            if (ModelState.IsValid)
            {

                //db.pNeuenMitarbeiterAnlegen(MM.Email, MM.Passwort, MM.Deaktiviert = false, MM.MAVorname, MM.MANachname, MM.MAAnrede);
                //Nach dem Gespeichert wurde schick den Benutzer zum Index zurück
               /* db.pMitarbeiterEditieren(MM.IDMitarbeiter, MM.Email, MM.Passwort, MM.Rolle, MM.Deaktiviert, MM.MAVorname, MM.MANachname, MM.MAAnrede);*/ // Probleme mit IDMitarbeiter da er es in irgend ein Data ding convertieren will
                //Und eventuell auch mit Rolle da er den "eigentlichen" char in einen string umwandeln will....warum ?
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
