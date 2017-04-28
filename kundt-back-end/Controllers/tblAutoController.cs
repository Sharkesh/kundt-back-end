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

        /// <summary>
        /// POST: tblAuto/FilterValidation
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "M,A")]
        public JsonResult FilterValidation(string Marke)
        {
            //asynchronus server request for typlist deppending on actual choice
            var data = db.tblTyp.Include(x => x.tblMarke).Where(x => x.tblMarke.Marke == Marke).Select(x => x.Typ);
            return Json(data);
        }

        /// <summary>
        /// GET: tblAuto/GetMarke
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "M,A")]
        public JsonResult GetMarke(int IDAuto)
        {
            var data = db.tblAuto.Include(x => x.tblTyp).Include(x => x.tblTyp.tblMarke).Where(x => x.IDAuto == IDAuto).Select(x => x.tblTyp.tblMarke.Marke);

            return Json(data);
        }

        /// <summary>
        /// GET: tblAuto/AutoHinzu
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "M,A")]
        public ActionResult AutoHinzu()
        {
            AutoModel am = new AutoModel(); //initialize new object of type AutoModel

            am.autoListe = db.tblAuto.ToList(); //fill the object.lists with necessary elements
            am.ausstattungListe = db.tblAusstattung.ToList();
            am.typListe = db.tblTyp.ToList();
            am.markeListe = db.tblMarke.ToList();
            am.kategorieListe = db.tblKategorie.ToList();
            am.treibstoffListe = db.tblTreibstoff.ToList();

            var res = db.tblAuto.Select(x => x.Getriebe); //LinQ used (instead of a proc)
            am.getriebeListe = res.ToList();              //to increase my experience/practice

            DateTime dtime = new DateTime(); //need of correct year validation (a future-build car can't be added yet)
            dtime = DateTime.Now;
            am.myBauJahr = dtime.Year;

            //some elements to give the user a little help/advantage cause:
            am.myTueren = "5";  //most cars are 5-doored
            am.myPS = "100";    //average PS in my opinion
            am.mySitze = 5;     //average count of seats in a car
            return View(am);
        }

        /// <summary>
        /// POST: tblAuto/AutoHinzu
        /// </summary>
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

            //this proc saves a specified car with all its properties to the db!
            db.pAutoHinzufuegen(Convert.ToInt16(am.myBauJahr), am.myPS,     //some datatypes need a convertion
                am.myGetriebe, am.myTueren, Convert.ToByte(am.mySitze),     //before used in proc!
                am.myMietPreis, am.myVerkaufsPreis, am.myKilometerStand,    
                am.myAutobild, am.myAnzeigen, am.myTreibstoff, am.myTyp,
                am.myKategorie);

            if (ausstattungListe != null)
            {
                foreach (int? item in ausstattungListe)
                {
                    if (item != null)
                    {
                        db.pAusstattungZuAuto2(item); //for each item the proc will save it to the db
                    }
                }
            }
            return RedirectToAction("AutoUebersicht", "tblAuto");
        }

        [Authorize(Roles = "M,A")]
        public ActionResult AutoUebersicht(AutoModel am, int? id)
        {
            //get some data from db; is used in the view as nonfiltered lists for multiple selections
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            am.myTime = dt.Year;
            am.autoListe = db.tblAuto.ToList();
            am.typListe = db.tblTyp.ToList();
            am.markeListe = db.tblMarke.ToList();
            am.kategorieListe = db.tblKategorie.ToList();

            // checks if id got a value or is null
            if (am.myIDAuto != 0)
            {
                bool trigger = false;
                for (int i = 0; i < am.autoListe.Count; i++)
                {
                    if (am.myIDAuto == am.autoListe[i].IDAuto)
                    {
                        trigger = true;
                    }
                }
                if (trigger == false)
                {
                    am.myIDAuto = 0; //if its null, it gets manually a 0 instead
                }
            }

            //Get elements for this list
            am.filterList = new List<string>();

            am.filterList.Add(converterStr(am.myIDAuto)); //adds each required element as a string to the list
            am.filterList.Add(converterStr(am.myMarke));
            am.filterList.Add(converterStr(am.myTyp));
            am.filterList.Add(converterStr(am.myKategorie));
            am.filterList.Add(converterStr(am.myBauJahrVon));
            am.filterList.Add(converterStr(am.myBauJahrBis));
            am.filterList.Add(converterStr(am.myKilometerStandVon));
            am.filterList.Add(converterStr(am.myKilometerStandBis));
            am.filterList.Add(converterStr(am.myAnzeigen));

            if (am.myIDAuto != 0) //pass the data to the proc dependend on int? id value
            {
                am.autoBearbeitenFilter = db.pAutoBearbeitenInklFilterFinal2(
                                            am.myIDAuto, am.myMarke, am.myTyp,
                                            am.myKategorie,
                                            umwandlerINT16(am.myBauJahrVon),
                                            umwandlerINT16(am.myBauJahrBis),
                                            umwandlerDEC(am.myKilometerStandVon),
                                            umwandlerDEC(am.myKilometerStandBis),
                                            am.myAnzeigen).ToList();
            }
            else
            {
                am.autoBearbeitenFilter = db.pAutoBearbeitenInklFilterFinal2(
                                            null, am.myMarke, am.myTyp,
                                            am.myKategorie,
                                            umwandlerINT16(am.myBauJahrVon),
                                            umwandlerINT16(am.myBauJahrBis),
                                            umwandlerDEC(am.myKilometerStandVon),
                                            umwandlerDEC(am.myKilometerStandBis),
                                            am.myAnzeigen).ToList();
            }
            ViewBag.temp = am.autoBearbeitenFilter;
            return View(am);
        }
        private short? umwandlerINT16(int? x)
        {
            if (x == null || x == 0)
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(x);
            }
        }
        private decimal? umwandlerDEC(decimal? x) //transforms nullable decimal in it's true form
        {
            if (x == null || x == 0)
            {
                return null;
            }
            else
            {
                return x;
            }
        }
        private string converterStr(object temp) //required to get a forced data value
        {
            if (temp != null)
            {
                if (temp.GetType() == typeof(int))
                {
                    return Convert.ToString(temp);
                }
                else if (temp == null)
                {
                    return "";
                }
                else if (temp.GetType() == typeof(bool))
                {
                    if ((bool)temp == true)
                    {
                        return "Sichtbar";
                    }
                    else
                    {
                        return "Versteckt";
                    }
                }
                else
                {
                    return temp.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        [Authorize(Roles = "M,A")]
        public ActionResult AutoBearbeiten(int? id)
        {
            AutoModel am = new AutoModel();
            if (id == 0 || id == null)
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
            am.myAnzeigen = db.tblAuto.Find((int)id).Anzeigen;
            DateTime dtime = new DateTime();
            dtime = DateTime.Now;
            am.myTime = dtime.Year;
            if (am.ausstattungListe != null)
            {

                for (int i = 0; i < am.ausstattungListe.Count; i++)
                {
                    for (int j = 0; j < am.plainAusstattungListe.Count; j++)
                    {
                        if (am.plainAusstattungListe[j].IDAusstattung == am.ausstattungListe[i].IDAusstattung)
                        {
                            am.plainAusstattungListe.RemoveAt(j);
                        }
                    }
                }
            }
            return View(am);
        }


        //AutoEdit/Post:
        [HttpPost]
        [Authorize(Roles = "M,A")]
        public ActionResult AutoBearbeiten(AutoModel am, int[] ausstattungListe, int[] plainAusstattungListe, HttpPostedFileBase upload)
        {

            //Abfrage ob binary-daten vorhanden
            if (upload != null && upload.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    am.myAutobild = reader.ReadBytes(upload.ContentLength);
                }
            }

            //Autoupdate proc:
            if (am.myIDAuto != 0)
            {
                db.pAutoBearbeiten(am.myIDAuto, umwandlerINT16(am.myBauJahr),
                    am.autoBearbeiten[0].PS, am.myGetriebe, am.myTueren,
                    (byte)am.mySitze, am.autoBearbeiten[0].MietPreis,
                    am.autoBearbeiten[0].VerkaufPreis, am.autoBearbeiten[0].Kilometerstand,
                    am.myAutobild, am.myAnzeigen, am.myTreibstoff,
                    am.myTyp, am.myKategorie);

                //Entfernen der bereits existierenden Ausstattung @tblAutodetail
                db.pAutoBearbeitenDelete(am.myIDAuto);

                //Neue Ausstattung eintragen @tblAutodetail
                if (ausstattungListe != null)
                {
                    foreach (var item in ausstattungListe)
                    {
                        db.pAutoBearbeitenCreate(item, am.myIDAuto);
                    }
                }
                if (plainAusstattungListe != null)
                {
                    foreach (var item in plainAusstattungListe)
                    {
                        if (item != 0)
                        {
                            db.pAutoBearbeitenCreate(item, am.myIDAuto);
                        }
                    }
                }
            }
            return RedirectToAction("AutoUebersicht", "tblAuto");
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
