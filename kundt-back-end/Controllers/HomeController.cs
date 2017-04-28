using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using System.Threading;
using kundt_back_end.Models;
using Microsoft.Ajax.Utilities;


namespace kundt_back_end.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: Home
        /// </summary>
        [Authorize(Roles = "M,A")]
        public ActionResult Index()
        {
            return RedirectToAction("Index","BuchungUebersicht");
        }
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        /// <summary>
        /// GET: Home/KundenUebersicht
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "M,A")]
        public ActionResult KundenUebersicht()//(endl)
        {
            KundenUebersichtContainerModel cm = new KundenUebersichtContainerModel();
            cm.filtermodel = (KundenUebersichtFilterModel)Session["Filterparameter"];

            

            if (cm.filtermodel == null)
            {
                cm.kundenlist = db.pKundenAnzeigen(null,null,null,null,null).ToList();
            }
            else
            {
                cm.kundenlist = db.pKundenAnzeigen(cm.filtermodel.Vorname,cm.filtermodel.Nachname, cm.filtermodel.KundenNr, cm.filtermodel.Ort, cm.filtermodel.Plz).ToList();
            }

            return View(cm);
        }

        /// <summary>
        /// POST: Home/KundenUebersichtFilter
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "M,A")]
        public ActionResult KundenUebersichtFilter(KundenUebersichtFilterModel km) //(endl)
        {
           
            Session["Filterparameter"] = km;
            return RedirectToAction("KundenUebersicht", "Home");
        }

        /// <summary>
        /// GET: Home/KundenBearbeiten/5
        /// </summary>
        [Authorize(Roles = "M,A")]
        public ActionResult KundenBearbeiten(int? id) //(endl)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKunde idk = db.tblKunde.Find(id);
            if (idk == null)
            {
                return HttpNotFound();
            }
           
            KundeEditModel kem = new KundeEditModel();
            kem.idkunde = idk.IDKunde;
            kem.Vorname = idk.Vorname;
            kem.Nachname = idk.Nachname;
            kem.Straße = idk.Strasse;
            kem.Plz = idk.tblPLZOrt.PLZ;
            kem.Ort = idk.tblPLZOrt.Ort;
            kem.Email = idk.tblLogin.Email;
            kem.Telefon = idk.Telefon;
            kem.PassNr = idk.ReisepassNr;
            kem.GebDat = idk.GebDatum;

            return View(kem);
        }

        /// <summary>
        /// POST: Home/KundenBearbeiten
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "M,A")]
        public ActionResult KundenBearbeiten(KundeEditModel kem) //(endl)
        {
            
            if (ModelState.IsValid)
            {
                //abfragen ob kem.Plz in db.tblPLZOrt enthalten ist
                if (db.tblPLZOrt.Any(t => t.PLZ == kem.Plz))
                {
                    db.pKundeEdit(kem.idkunde, kem.Vorname, kem.Nachname, kem.Straße, kem.Plz, kem.Ort, kem.Email,
                    kem.Telefon, kem.PassNr, kem.GebDat);
                }
                return RedirectToAction("KundenUebersicht", "Home");
            }
            return View(kem);
        }
    }
}