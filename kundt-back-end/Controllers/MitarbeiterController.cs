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
using System.Web.Security;

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
            try
            {
                if (ModelState.IsValid)
                {
                    MM.Passwort = Logic.Helper.PasswordConverter(MM.Passwort);
                    db.pNeuenMitarbeiterAnlegen(MM.Email, MM.Passwort, MM.Deaktiviert = false, MM.MAVorname, MM.MANachname, MM.MAAnrede);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit", MM.IDMitarbeiter);
                }
            }
            catch
            {
                return HttpNotFound(); 
            }

        }

        // GET: Mitarbeiter/Edit/5
        [Authorize(Roles = "A,M")]
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
            b.EmailAlt = ma.tblLogin.Email;
            b.Deaktiviert = ma.tblLogin.Deaktiviert;
            b.MAAnrede = ma.MAAnrede;
            b.Passwort = ma.tblLogin.Passwort;
            b.Rolle = Convert.ToChar(ma.tblLogin.Rolle);
            b.MANachname = ma.MANachname;
            b.MAVorname = ma.MAVorname;


            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email", ma.IDMitarbeiter);
            return View(b);
        }


        // POST: Mitarbeiter/Edit
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "A,M")]
        public ActionResult Edit(MitarbeiterModel MM)
        {
            if (ModelState.IsValid)
            {
               
                db.pMitarbeiterEditieren(MM.IDMitarbeiter, MM.Email, MM.Passwort,MM.Rolle.ToString(), MM.Deaktiviert, MM.MAVorname, MM.MANachname, MM.MAAnrede);

                //Nach Erfolgreichem Ändern schick den Benutzer zur View ÄnderungenErfolgreich
                return RedirectToAction("ÄnderungenErfolgreich");
            }
            else
            {
                //Gehe zurück zum Bearbeiten wen das verändern nicht funktioniert hat! 
                return RedirectToAction("Edit", MM.IDMitarbeiter);
            }
        }
        //Adming/MitarbeiterBearbeiten/Passwort Zurücksetzen
        [Authorize(Roles = "A,M")]
        public ActionResult PasswortZuruecksetzenA(int id)
        {

            string erzeugtesPW = "error";

            ObjectParameter output = new ObjectParameter("NPasswort", typeof(string));

            db.p8Passwort(output);

            erzeugtesPW = output.Value as string;

            if (erzeugtesPW == null)
                throw new Exception("SprocError");

            var login = db.tblLogin.Find(id);
            login.Passwort = Logic.Helper.PasswordConverter(erzeugtesPW);
            db.Entry(login).State = EntityState.Modified;
            db.SaveChanges();

            var MM = new MitarbeiterModel();
            MM.IDMitarbeiter = id;
            MM.Passwort = erzeugtesPW;

            return View(MM);
        }

        [Authorize(Roles = "M")]
        public ActionResult PasswortZuruecksetzenM(int? id)
        {

            if (id != null && id > 0)
            {
                MitarbeiterModel MM = new MitarbeiterModel();
                MM.IDMitarbeiter = id ?? -1;

                var dbMA = db.tblMitarbeiter.Find(id);
                var dbLogin = db.tblLogin.Find(id);

                MM.Email = dbLogin.Email;
                MM.MAVorname = dbMA.MAVorname;
                MM.MANachname = dbMA.MANachname;
                MM.Rolle = 'M';
                MM.Deaktiviert = false;
                MM.MAAnrede = dbMA.MAAnrede;

                return View(MM);
            }
            else
            {
                throw new Exception("MAResetPW");
            }
            
        }

        [Authorize(Roles = "M")]
        [HttpPost]
        public ActionResult PasswortZuruecksetzenM(MitarbeiterModel MM)
        {

            var dbLogin = db.tblLogin.Find(MM.IDMitarbeiter);
            dbLogin.Passwort = Logic.Helper.PasswordConverter(MM.Passwort);
            db.Entry(dbLogin).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Mitarbeiter/MitarbeiterDaten
        [Authorize(Roles = "M")]
        public ActionResult MitarbeiterDaten(/*int? id*/)
        {            
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            string email = ticket.Name;
            var blub = db.tblMitarbeiter.Include(l => l.tblLogin).Where(l => l.tblLogin.Email == email);
            tblLogin log = new tblLogin();
            foreach (var item in blub)
            {
                log.IDLogin = item.tblLogin.IDLogin;
            }


            tblMitarbeiter ma = db.tblMitarbeiter.Find(log.IDLogin);
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

        //POST: MitarbeiterDaten
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "M")]
        public ActionResult MitarbeiterDaten(MitarbeiterModel MM)
        {
            if (ModelState.IsValid)
            {
             
                
                   
                var res = db.pMitarbeiterEditieren(MM.IDMitarbeiter, MM.Email, MM.Passwort, MM.Rolle.ToString(), MM.Deaktiviert, MM.MAVorname, MM.MANachname, MM.MAAnrede);

                //Nach Erfolgreichem Ändern schick den Benutzer zur View ÄnderungenErfolgreich
                return RedirectToAction("ÄnderungenErfolgreich" , "Mitarbeiter");
            }
            else
            {
                //Gehe zurück zum Bearbeiten wen das verändern nicht funktioniert hat! 
                return RedirectToAction("Edit", MM.IDMitarbeiter);
            }
        }
        // GET: ÄnderungenErfolgreich
        [Authorize(Roles = "M")]
        public ActionResult ÄnderungenErfolgreich()
        {
            return View();
        }
       

    }
}





