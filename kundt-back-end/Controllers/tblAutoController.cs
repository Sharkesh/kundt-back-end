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
        [Authorize(Roles = "M,A")]
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
        [Authorize(Roles = "M,A")]
        public ActionResult AutoHinzu(AutoModel am, int[] ausstattungListe, HttpPostedFileBase upload)
        //public ActionResult AutoHinzu([Bind(Include = "myBauJahr,myPS,myGetriebe,myTueren,mySitze,myMietPreis,myVerkaufsPreis,myKilometerStand,myAnzeigen, myTreibstoff,myTyp,myKategorie,ausstattungListe")] AutoModel am)
        {

            
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        am.myAutobild = reader.ReadBytes(upload.ContentLength);
                    }
                }

            db.pAutoHinzufuegen(Convert.ToInt16(am.myBauJahr), am.myPS, am.myGetriebe, am.myTueren, Convert.ToByte(am.mySitze), am.myMietPreis, am.myVerkaufsPreis, am.myKilometerStand, am.myAutobild, am.myAnzeigen, am.myTreibstoff, am.myTyp, am.myKategorie);

            foreach (int? item in ausstattungListe)
            {
                if (item != null)
                {
                    db.pAusstattungZuAuto2(item);
                }
            }



            return View();

        }

        [Authorize(Roles = "M,A")]
        public ActionResult AutoUebersicht(AutoModel am)
        {

            //AutoModel am = new AutoModel();
            am.autoListe = db.tblAuto.ToList();
            am.typListe = db.tblTyp.ToList();
            am.markeListe = db.tblMarke.ToList();
            am.kategorieListe = db.tblKategorie.ToList();
            
            if (am.myIDAuto != 0)
            {
                am.autoBearbeitenFilter = db.pAutoBearbeitenInklFilterFinal2(
                                            am.myIDAuto, am.myMarke, am.myTyp,
                                            am.myKategorie, umwandlerINT16(am.myBauJahrAB),
                                            umwandlerINT16(am.myBauJahrBIS),
                                            umwandlerDEC(am.myKilometerStandAB),
                                            umwandlerDEC(am.myKilometerStandBIS),
                                            am.myAnzeigen).ToList();
            }
            else
            {
                am.autoBearbeitenFilter = db.pAutoBearbeitenInklFilterFinal2(
                                            null, am.myMarke, am.myTyp,
                                            am.myKategorie, umwandlerINT16(am.myBauJahrAB),
                                            umwandlerINT16(am.myBauJahrBIS),
                                            umwandlerDEC(am.myKilometerStandAB),
                                            umwandlerDEC(am.myKilometerStandBIS),
                                            am.myAnzeigen).ToList();
            }

            return View(am);
        }
        private short? umwandlerINT16(int? x)
        {
            short? output = null;
            try
            {
                return Convert.ToInt16(x);

            }
            catch (Exception)
            {
                return output;
            }
        }
        private decimal? umwandlerDEC(decimal? x)
        {
            decimal? output = null;
            try
            {
                return Convert.ToDecimal(x);
            }
            catch (Exception)
            {
                return output;
            }
        }

        // GET: tblAuto/Details/5
        [Authorize(Roles = "M,A")]
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
        [Authorize(Roles = "M,A")]
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
        [Authorize(Roles = "M,A")]
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
        [HttpGet]
        [Authorize(Roles = "M,A")]
        public ActionResult AutoBearbeiten(int? id)
        {
            AutoModel am = new AutoModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            am.myIDAuto = (int)id;
            am.markeListe = db.tblMarke.SqlQuery("SELECT* FROM tblMarke").ToList();
            am.ausstattungListe = db.pAusstattung(id).ToList();
            am.autoBearbeiten = db.pAutoBearbeitenAnzeigen2(id).ToList();
            am.markeListe = db.tblMarke.ToList();
            am.typListe = db.tblTyp.ToList();
            am.treibstoffListe = db.tblTreibstoff.ToList();
            am.kategorieListe = db.tblKategorie.ToList();
            am.plainAusstattungListe = db.tblAusstattung.SqlQuery("SELECT* FROM tblAusstattung").ToList();
            for (int i = 0; i < am.ausstattungListe.Count; i++)
            {
                if (am.plainAusstattungListe[i].IDAusstattung == am.ausstattungListe[i].IDAusstattung)
                {
                    am.plainAusstattungListe.RemoveAt(i);
                }
            }
            return View(am);
        }
        [HttpPost]
        [Authorize(Roles = "M,A")]
        public ActionResult AutoBearbeiten(AutoModel am, int[] ausstattungListe, int[] plainAusstattungListe)
        {
            //update-proc implementieren!
            return RedirectToAction("AutoUebersicht", "tblAuto");
        }

        // POST: tblAuto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "M,A")]
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
