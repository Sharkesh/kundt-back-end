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
    public class BuchungUebersichtController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        /// <summary>
        /// GET: BuchungUebersicht
        /// </summary>
        public ActionResult Index()
        {
            /// Zeigt alle Buchungen an die am heutigen Tag abgeholt werden , der Status erstellt ist, 
            /// und wo die Buchung nicht storniert wurde.
            var buchungAbholung = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde).Where(b => b.BuchungVon == DateTime.Today && !b.Storno && b.BuchungStatus == "erstellt");


            /// Zeigt alle Buchungen an die am heutigen Tag zurück gegeben werden , der Status abgeholt ist.   
            var buchungRueckgabe = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde).Where(b => b.BuchungBis == DateTime.Today && b.BuchungStatus == "abgeholt");

            /// Zeigt alle Buchungen an :
            /// Wo das Abholdatum kleiner ist als das heutige Datum, und der Status erstellt und nicht storniert ist.
            /// Wo das Rückgabedatum kleiner ist als das heutige Datum, und der Status abgeholt ist.
            var buchungProblem = db.tblBuchung.Include(b => b.tblHistorie).Where(b => (b.BuchungVon < DateTime.Today && b.BuchungStatus == "erstellt" && !b.Storno) || (b.BuchungBis < DateTime.Today && b.BuchungStatus == "abgeholt"));


            /// Die gespeicherten Abfragen werden in das Model BuchungUebersicht übertragen und 
            /// auf der Index View des BackEnds verarbeitet und angezeigt.
            BuchungsUebersicht bu = new BuchungsUebersicht();
            bu.buchungAbholung = buchungAbholung.ToList();
            bu.buchungRueckgabe = buchungRueckgabe.ToList();
            bu.buchungProblem = buchungProblem.ToList();
            return View(bu);
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
