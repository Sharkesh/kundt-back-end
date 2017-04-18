using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kundt_back_end.Models;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web.Security;

namespace kundt_back_end.Controllers
{
    public class BuchungController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        /// <summary>
        /// GET: Buchung
        /// </summary>
        /// <returns>Liste offener Buchungen von heute und der nächsten 13 Tage</returns>
        [HttpGet]
        [Authorize(Roles = "M,A")]
        public ActionResult Index()
        {
            /// Hier die Prozedur der Buchungsauflistung der nächsten 14 Tage einfügen
            /// Evtl. neues Model dafür erstellen und die Buchungsauflistung als Liste übergeben
            //var buchungUebersicht = db.tblBuchung.Include(b => b.tblAuto).Include(b => b.tblKunde);
            List<GefilterteBuchungen> buchungList = new List<GefilterteBuchungen>();

            var test = db.OffeneBuchungenTodayPlus13();
            //var BuchungListe = new List<BuchungViewModel>();

            foreach (var b in test)
            {
                GefilterteBuchungen res = new GefilterteBuchungen();
                res.IDBuchung = b.IDBuchung;
                res.IDKunde = b.IDKunde;
                res.BuchungStatus = b.BuchungStatus;
                res.Nachname = b.Nachname;
                res.Vorname = b.Vorname;
                res.Ort = b.Ort;
                res.PLZ = b.PLZ;



                buchungList.Add(res);
            }
            //return View(buchungUebersicht.ToList());
            return View(buchungList);
        }


        /// <summary>
        /// POST: Buchung
        /// </summary>
        /// <param name="idbuchung"></param>
        /// <param name="nachname"></param>
        /// <param name="idkunde"></param>
        /// <param name="ort"></param>
        /// <param name="plz"></param>
        /// <param name="checkStatus"></param>
        /// <returns>Gefilterte Liste von Buchungen</returns>
        [HttpPost]
        [Authorize(Roles = "M,A")]
        public ActionResult Index(int? idbuchung, string nachname, int? idkunde, string ort, string plz, string checkStatus)
        {
            bool open = false;
            bool problems = false;
            bool finished = false;
            /// Wenn der Wert des Radiobuttons mit dem name "checkStatus" == o ist
            /// wird der bool Wert auf true gesetzt und ein Tempdata["open"] generiert
            /// und in der Buchungen View Index zum differenzieren herangezogen welchen Status der
            /// Radio Button haben soll (checked/unchecked)
            if (checkStatus == "o")
            {
                open = true;
                TempData["open"] = "offen";

            }
            /// Wenn der Wert des Radiobuttons mit dem name "checkStatus" == a ist
            /// wird der bool Wert auf true gesetzt und ein Tempdata["finished"] generiert
            /// und in der Buchungen View Index zum differenzieren herangezogen welchen Status der
            /// Radio Button haben soll (checked/unchecked)
            else if (checkStatus == "a")
            {
                finished = true;
                TempData["finished"] = "abgeschlossen";
            }
            /// Wenn der Wert des Radiobuttons mit dem name "checkStatus" == p ist
            /// wird der bool Wert auf true gesetzt und ein Tempdata["problems"] generiert
            /// und in der Buchungen View Index zum differenzieren herangezogen welchen Status der
            /// Radio Button haben soll (checked/unchecked)
            else if (checkStatus == "p")
            {
                problems = true;
                TempData["problems"] = "problem";
            }
            /// Wenn kein Wert (null) im Suchfeld für IDBuchung angegeben wird setze IDBuchung auf 0
            if (idbuchung == null)
            {
                idbuchung = 0;
            }
            /// Wenn kein Wert (null) im Suchfeld für IDKunde angegeben wird setze IDBuchung auf 0
            if (idkunde == null)
            {
                idkunde = 0;
            }


            List<GefilterteBuchungen> buchungList = new List<GefilterteBuchungen>();

            DbConnection con = db.Database.Connection;
            ///Checkt ob die Datenbankverbindung offen ist, falls nicht wird sie geöffnet
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }


            /// Erzeugt ein SQLCommand Objekt mit Parametern das an die Db geschickt wird
            SqlCommand cmd = new SqlCommand(GefilterteBuchungen.SQL, (SqlConnection)con);
            cmd.Parameters.AddWithValue("@idBuchung", idbuchung);
            cmd.Parameters.AddWithValue("@nachname", nachname);
            cmd.Parameters.AddWithValue("@idkunde", idkunde);
            cmd.Parameters.AddWithValue("@ort", ort);
            cmd.Parameters.AddWithValue("@plz", plz);
            cmd.Parameters.AddWithValue("@offen", open);
            cmd.Parameters.AddWithValue("@abgeschlossen", finished);
            cmd.Parameters.AddWithValue("@problem", problems);

            /// Hier wird ein SqlDataReader Objekt angelegt das die Ergebnise des abgesetzten SQLCommands
            /// ausliest und in eine Liste speichert
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                buchungList.Add(new GefilterteBuchungen(reader));
            }

            return View(buchungList);
        }



        // GET: Buchung/Edit/5
        [Authorize(Roles = "M,A")]
        public ActionResult Edit(int? id)
        {
            /// Hier wird die aufrufende Seite ermittelt und abgespeichert
            var url = Convert.ToString(Request.UrlReferrer);
            TempData["AusgangsURL"] = url;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            /// Besser wäre es das eigene Modell zu verwenden anstelle der tblBuchung
            /// allerdings hat unser eigenes modell nicht alle Felder aus tblbuchung
            tblBuchung tblBuchung = db.tblBuchung.Find(id);
            if (tblBuchung == null)
            {
                return HttpNotFound();
            }

            BuchungEditModel BEM = new BuchungEditModel();
            BEM.BuchungAm = tblBuchung.BuchungAm;
            BEM.BuchungVon = tblBuchung.BuchungVon;
            BEM.BuchungBis = tblBuchung.BuchungBis;
            BEM.BuchungStatus = tblBuchung.BuchungStatus;
            BEM.FKAuto = tblBuchung.FKAuto;
            BEM.FKKunde = tblBuchung.FKKunde;
            BEM.IDBuchung = tblBuchung.IDBuchung;
            BEM.Versicherung = tblBuchung.Versicherung;
            BEM.Tage = tblBuchung.Tage;
            BEM.MietPreis = tblBuchung.tblAuto.MietPreis;
            BEM.Vorname = tblBuchung.tblKunde.Vorname;
            BEM.Nachname = tblBuchung.tblKunde.Nachname;


            return View(BEM);
        }

        // POST: Buchung/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "M,A")]
        public ActionResult Edit(BuchungEditModel BEM)
        //public ActionResult Edit(tblBuchung tblBuchung)
        {
            if (ModelState.IsValid)
            {
                /// Alte Variante
                //var Buchung = (from b in db.tblBuchung where b.IDBuchung == BEM.IDBuchung select b).FirstOrDefault<tblBuchung>();


                /// Wenn im Edit-View das Feld BuchungBis kleiner als BuchungVon ist wird ein TempData erzeugt
                /// das im Edit View eine Fehlermeldung einblendet und den User darauf hinweist
                /// dass das BuchungBis Feld nur Werte annimmt die größer sind als der Wert im BuchungVon Feld.
                if (BEM.BuchungBis < BEM.BuchungVon)
                {
                    TempData["fail"] = "DatumBis muss > sein als DatumVon!";
                    return RedirectToAction("Edit", BEM.IDBuchung);
                }
                /// bool Wert der ermittelt wird um den BuchungStatus auf 'abgeholt' zu setzen
                if (BEM.abgeholt)
                {
                    BEM.BuchungStatus = "abgeholt";

                    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    string email = ticket.Name;

                    db.ptblHistorieInsert(BEM.IDBuchung, email);


                }
                /// bool Wert der ermittelt wird um den BuchungStatus auf 'zurueck' zu setzen
                if (BEM.zurueck)
                {
                    BEM.BuchungStatus = "zurueck";

                    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    string email = ticket.Name;

                    db.ptblHistorieInsert(BEM.IDBuchung, email);


                }
                /// Abklären was passiert wenn storniert wurde.
                /// Eintrag in tblHistorie machen oder nicht?
                /// Wenn ja proc dafür machen?
                /// Wenn nein was dann?
                if (BEM.Storno)
                {
                    BEM.BuchungStatus = "zurueck";
                }

                if (!BEM.Storno && !BEM.zurueck && !BEM.abgeholt)
                {
                    BEM.BuchungStatus = db.tblBuchung.Find(BEM.IDBuchung).BuchungStatus;
                }


                /// Hol dir die IDBuchung aus BuchungEditModel und suchen den gleichen
                /// Datensatz mit der selben ID aus der Datenbank von der tblBuchung
                /// und update die bearbeiteten Felder.
                db.BuchungUpdate(BEM.IDBuchung, BEM.BuchungVon, BEM.BuchungBis, BEM.BuchungStatus, BEM.Storno, BEM.Versicherung);

                /// Alte Variante
                //Buchung.BuchungVon = BEM.BuchungVon;
                //Buchung.BuchungBis = BEM.BuchungBis;
                //Buchung.BuchungStatus = BEM.BuchungStatus;
                //Buchung.Versicherung = BEM.Versicherung;
                //Buchung.Storno = BEM.Storno;                
                //db.SaveChanges();

                /// Nach dem Speichern wird man zum Aufrufpunkt der Edit Seite redirected
                var urlStr = (string)TempData["AusgangsURL"];
                return Redirect(urlStr);
            }

            return View(BEM);
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
