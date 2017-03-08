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

        // GET: BuchungUebersicht
        public ActionResult Index()
        {
            var buchungAbholung = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde).Where(b => b.BuchungVon == DateTime.Today && !b.Storno);

            var buchungRueckgabe = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde).Where(b => b.BuchungBis == DateTime.Today && b.BuchungStatus == "abgeholt");

            var buchungProblem = db.tblBuchung.Include(b => b.tblHistorie).Where(b => (b.BuchungVon < DateTime.Today && b.BuchungStatus == "erstellt") || (b.BuchungBis < DateTime.Today && b.BuchungStatus == "abgeholt"));

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
