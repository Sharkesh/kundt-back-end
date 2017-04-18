using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kundt_back_end.Models;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Net.Mail;

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
                    MM.Passwort = Logic.Helper.PasswordConverter(MM.Passwort);
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
            tblMitarbeiter ma = db.tblMitarbeiter.Find(id);
            if (ma == null)
            {
                return HttpNotFound();
            }

            MitarbeiterModel b = new MitarbeiterModel();
            b.IDMitarbeiter = ma.IDMitarbeiter;
            b.Email = ma.tblLogin.Email;
            b.Deaktiviert = ma.tblLogin.Deaktiviert;
            b.MAAnrede = ma.MAAnrede;
            b.Passwort = ma.tblLogin.Passwort;
            b.Rolle = Convert.ToChar(ma.tblLogin.Rolle);
            b.MANachname = ma.MANachname;
            b.MAVorname = ma.MAVorname;


            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email", ma.IDMitarbeiter);
            return View(b);
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
                if (MM.Passwort == "PasswortResetten")
                {

                    //MM.Passwort = db.p8Passwort;
                    // Passwort neu setzen auf das RND Generierte der procedure 

                    //db.pMitarbeiterPasswort
                    using (MailMessage mailmessage = new MailMessage(/*"noreply@sharkesh.com"*/"test.sharkesh@gmail.com", MM.Email))
                    {
                        mailmessage.Subject = "Account Aktivierung";
                        //Nachrichten Text wird "zusammengebaut".
                        string body = "<p>Hallo," + MM.MAAnrede + MM.MANachname;
                        body += "<br /><br />Hier ist ihr neues Passwort: " + MM.Passwort;
                        body += "<br /><a Bei problemen wenden sie sich bitte an den Admin ihres Vertrauens ;)</a>";
                        body += "<br /><br />Danke!</p>";
                        //Nachrichten Text wird an das MailMessage Objekt gehängt.
                        mailmessage.Body = body;
                        mailmessage.IsBodyHtml = true;
                        //Logindaten für den SmtpClient weiter unten.
                        NetworkCredential NetworkCred = new NetworkCredential(/*"noreply"*/"test.sharkesh@gmail.com", /*"~S[%a(1<`(eN"*/"123user!");
                        //Verbindungs Variablen werden gesetzt.
                        SmtpClient smtp = new SmtpClient()
                        {
                            Host = "smtp.gmail.com"/*"cloud.sharkesh.com"*/,
                            EnableSsl = true,
                            UseDefaultCredentials = true,
                            Credentials = NetworkCred,
                            Port = 587
                        };
                        smtp.Send(mailmessage);
                    }
                }
                
                //Das Passwort nun Hashen und ab in die Datenbank
                MM.Passwort = Logic.Helper.PasswordConverter(MM.Passwort);

                //ObjectParameter MID = new ObjectParameter("IDMitarbeiter", MM.IDMitarbeiter);

                //Nach dem Gespeichert wurde schick den Benutzer zum Index zurück
                db.pMitarbeiterEditieren(MM.IDMitarbeiter, MM.Email, MM.Passwort,MM.Rolle.ToString(), MM.Deaktiviert, MM.MAVorname, MM.MANachname, MM.MAAnrede);

                //Nach Erfolgreichem Ändern schick den Benutzer auf den Index zurück
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





