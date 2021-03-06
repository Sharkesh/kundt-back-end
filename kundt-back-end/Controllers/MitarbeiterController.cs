﻿using System;
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
        
        /// <summary>
        /// GET: Mitarbeiter
        /// </summary>
        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            MitarbeiterContainerModel McM = new MitarbeiterContainerModel();
            McM.mafilter = (MitarbeiterFilterModel)Session["MAFilterparameter"];

            if (McM.mafilter == null)
            {
                McM.malist = db.pMAAnzeigen(null, null, null, null).ToList();
            }
            else
            {
                string tempAnrede = null;
                if (McM.mafilter.Anrede != null)
                {
                    if (McM.mafilter.Anrede.ToUpper() == "W")
                    {

                        tempAnrede = "Frau";
                    }
                    else if (McM.mafilter.Anrede.ToUpper() == "M")
                    {
                        tempAnrede = "Herr";
                    }
                }
                
                McM.malist = db.pMAAnzeigen(McM.mafilter.Vorname, McM.mafilter.Nachname, McM.mafilter.MaId, tempAnrede).ToList();
            }

            return View(McM);
        }
        
        /// <summary>
        /// POST: Mitarbeiter
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "A")]
        public ActionResult Index (MitarbeiterFilterModel MaF)
        {
            Session["MAFilterparameter"] = MaF;
            return RedirectToAction("Index", "Mitarbeiter");
        }
        
        /// <summary>
        /// GET: MItarbeiter/Create
        /// </summary>
        [Authorize(Roles = "A")]
        public ActionResult Create()
        {
            MitarbeiterModel test = new MitarbeiterModel();
            ViewBag.IDMitarbeiter = new SelectList(db.tblLogin, "IDLogin", "Email");
            return View(test);
        }
        
        /// <summary>
        /// POST: Mitarbeiter/Create
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "A")]
        public ActionResult Create(MitarbeiterModel MM)
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
        
        /// <summary>
        /// GET: Mitarbeiter/Edit/5
        /// </summary>
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
        
        /// <summary>
        /// POST: Mitarbeiter/Edit
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "A,M")]
        public ActionResult Edit(MitarbeiterModel MM)
        {
            if (ModelState.IsValid)
            {
               
                db.pMitarbeiterEditieren(MM.IDMitarbeiter, MM.Email, MM.Passwort,MM.Rolle.ToString(), MM.Deaktiviert, MM.MAVorname, MM.MANachname, MM.MAAnrede);

                //Nach Erfolgreichem Ändern schick den Benutzer zur View ÄnderungenErfolgreich
                return RedirectToAction("Index");
            }
            else
            {
                //Gehe zurück zum Bearbeiten wen das verändern nicht funktioniert hat! 
                return RedirectToAction("Edit", MM.IDMitarbeiter);
            }
        }
        
        /// <summary>
        /// GET: Mitarbeiter/PasswortZuruecksetzenAdmin
        /// </summary>
        [Authorize(Roles = "A")]
        public ActionResult PasswortZuruecksetzenAdmin(int id)
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
        
        /// <summary>
        /// GET: Mitarbeiter/PasswortZuruecksetzenMitarbeiter
        /// </summary>
        [Authorize(Roles = "M")]
        public ActionResult PasswortZuruecksetzenMitarbeiter(int? id)
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
        
        /// <summary>
        /// POST: MItarbeiter/PasswortZuruecksetzenMitarbeiter
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "M")]
        public ActionResult PasswortZuruecksetzenMitarbeiter(MitarbeiterModel MM)
        {

            //var dbLogin = db.tblLogin.Find(MM.IDMitarbeiter);
            //dbLogin.Passwort = Logic.Helper.PasswordConverter(MM.Passwort);
            //db.Entry(dbLogin).State = EntityState.Modified;
            //db.SaveChanges();
            MM.Passwort = Logic.Helper.PasswordConverter(MM.Passwort);
            db.pMitarbeiterEigenesPasswortZuruecksetzen(MM.IDMitarbeiter, MM.Passwort);

            return RedirectToAction("ÄnderungenErfolgreich", "Mitarbeiter");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// GET: Mitarbeiter/MitarbeiterDaten
        /// </summary>
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
        
        /// <summary>
        /// POST: Mitarbeiter/MitarbeiterDaten
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "M")]
        public ActionResult MitarbeiterDaten(MitarbeiterModel MM)
        {
            if (ModelState.IsValid)
            {
                try { 
 
                var res = db.pMitarbeiterEditieren(MM.IDMitarbeiter, MM.Email, MM.Passwort, MM.Rolle.ToString(), MM.Deaktiviert, MM.MAVorname, MM.MANachname, MM.MAAnrede);

                //Nach Erfolgreichem Ändern schick den Benutzer zur View ÄnderungenErfolgreich
                return RedirectToAction("ÄnderungenErfolgreich" , "Mitarbeiter");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            else
            {
                //Gehe zurück zum Bearbeiten wen das verändern nicht funktioniert hat! 
                return RedirectToAction("Edit", MM.IDMitarbeiter);
            }
        }

        /// <summary>
        /// GET: Mitarbeiter/ÄnderungenErfolgreich
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "M")]
        public ActionResult ÄnderungenErfolgreich()
        {
            return View();
        }      

    }
}





