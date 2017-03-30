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
    public class tblAutoController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        [HttpGet]
        public ActionResult AutoHinzu()
        {
            AutoModel am = new AutoModel();
            am.autoListe = db.tblAuto.ToList();
            am.ausstattungListe = db.tblAusstattung.ToList();
            am.typListe = db.tblTyp.ToList();
            am.markeListe = db.tblMarke.ToList();
            am.kategorieListe = db.tblKategorie.ToList();
            am.treibstoffListe = db.tblTreibstoff.ToList();
            var res = db.tblAuto.Select(x => x.Getriebe);
            am.getriebeListe = res.ToList();
            return View(am);
        }

        [HttpPost]
        public ActionResult AutoHinzu(AutoModel am, int[] ausstattungListe)
        //public ActionResult AutoHinzu([Bind(Include = "myBauJahr,myPS,myGetriebe,myTueren,mySitze,myMietPreis,myVerkaufsPreis,myKilometerStand,myAnzeigen, myTreibstoff,myTyp,myKategorie,ausstattungListe")] AutoModel am)
        {            
            db.pAutoHinzufuegen(Convert.ToInt16(am.myBauJahr), am.myPS, am.myGetriebe, am.myTueren, Convert.ToByte(am.mySitze), am.myMietPreis, am.myVerkaufsPreis, am.myKilometerStand, null, am.myAnzeigen, am.myTreibstoff, am.myTyp, am.myKategorie);
            foreach (string item in ausstattungListe)
            {
                if (item != null)
                {
                    db.pAusstattungZuAuto2(item);
                }
            }

            return View();

        }
        // GET: tblAuto
        public ActionResult Index()
        {
            var tblAuto = db.tblAuto.Include(t => t.tblKategorie).Include(t => t.tblTreibstoff).Include(t => t.tblTyp);
            return View(tblAuto.ToList());
        }

        // GET: tblAuto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAuto tblAuto = db.tblAuto.Find(id);
            if (tblAuto == null)
            {
                return HttpNotFound();
            }
            return View(tblAuto);
        }

        // GET: tblAuto/Create
        public ActionResult Create()
        {
            ViewBag.FKKategorie = new SelectList(db.tblKategorie, "IDKategorie", "Kategorie");
            ViewBag.FKTreibstoff = new SelectList(db.tblTreibstoff, "IDTreibstoff", "Treibstoff");
            ViewBag.FKTyp = new SelectList(db.tblTyp, "IDTyp", "Typ");
            return View();
        }

        // POST: tblAuto/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAuto,Baujahr,PS,Getriebe,Tueren,Sitze,MietPreis,VerkaufPreis,Kilometerstand,AutoBild,Anzeigen,FKTreibstoff,FKTyp,FKKategorie")] tblAuto tblAuto)
        {
            if (ModelState.IsValid)
            {
                db.tblAuto.Add(tblAuto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKKategorie = new SelectList(db.tblKategorie, "IDKategorie", "Kategorie", tblAuto.FKKategorie);
            ViewBag.FKTreibstoff = new SelectList(db.tblTreibstoff, "IDTreibstoff", "Treibstoff", tblAuto.FKTreibstoff);
            ViewBag.FKTyp = new SelectList(db.tblTyp, "IDTyp", "Typ", tblAuto.FKTyp);
            return View(tblAuto);
        }

        // GET: tblAuto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAuto tblAuto = db.tblAuto.Find(id);
            if (tblAuto == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKKategorie = new SelectList(db.tblKategorie, "IDKategorie", "Kategorie", tblAuto.FKKategorie);
            ViewBag.FKTreibstoff = new SelectList(db.tblTreibstoff, "IDTreibstoff", "Treibstoff", tblAuto.FKTreibstoff);
            ViewBag.FKTyp = new SelectList(db.tblTyp, "IDTyp", "Typ", tblAuto.FKTyp);
            return View(tblAuto);
        }

        // POST: tblAuto/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAuto,Baujahr,PS,Getriebe,Tueren,Sitze,MietPreis,VerkaufPreis,Kilometerstand,AutoBild,Anzeigen,FKTreibstoff,FKTyp,FKKategorie")] tblAuto tblAuto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAuto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKKategorie = new SelectList(db.tblKategorie, "IDKategorie", "Kategorie", tblAuto.FKKategorie);
            ViewBag.FKTreibstoff = new SelectList(db.tblTreibstoff, "IDTreibstoff", "Treibstoff", tblAuto.FKTreibstoff);
            ViewBag.FKTyp = new SelectList(db.tblTyp, "IDTyp", "Typ", tblAuto.FKTyp);
            return View(tblAuto);
        }

        // GET: tblAuto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAuto tblAuto = db.tblAuto.Find(id);
            if (tblAuto == null)
            {
                return HttpNotFound();
            }
            return View(tblAuto);
        }

        // POST: tblAuto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAuto tblAuto = db.tblAuto.Find(id);
            db.tblAuto.Remove(tblAuto);
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
