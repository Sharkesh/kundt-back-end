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
    public class EyecatchersController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();



        public ActionResult Index()
        {
            return View(db.tblEyecatcher.ToList());
        }
        // GET: Eyecatchers
        public ActionResult Einstellung()
        {
            return View();
        }

        // GET: Eyecatchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEyecatcher tblEyecatcher = db.tblEyecatcher.Find(id);
            if (tblEyecatcher == null)
            {
                return HttpNotFound();
            }
            return View(tblEyecatcher);
        }

        // GET: Eyecatchers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eyecatchers/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Einstellung([Bind(Include = "IDEyecatcher,GUIDBild,Eyecatcher,Typ")] tblEyecatcher tblEyecatcher, HttpPostedFileBase upload)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (upload != null && upload.ContentLength > 0)
        //        {
        //            using (var reader = new System.IO.BinaryReader(upload.InputStream))
        //            {
        //                tblEyecatcher.Eyecatcher = reader.ReadBytes(upload.ContentLength);
        //            }
        //        }
        //        tblEyecatcher.GUIDBild = Guid.NewGuid();

        //        db.tblEyecatcher.Add(tblEyecatcher);
        //        db.SaveChanges();
        //        return RedirectToAction("Einstellung");
        //    }

        //    return View(tblEyecatcher);
        //}

        // GET: Eyecatchers/Edit/5
        public ActionResult Erstellen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEyecatcher tblEyecatcher = db.tblEyecatcher.Find(id);
            if (tblEyecatcher == null)
            {
                return HttpNotFound();
            }
            return View(tblEyecatcher);
        }

        // POST: Eyecatchers/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Einstellung([Bind(Include = "IDEyecatcher,GUIDBild,Eyecatcher,Typ")] tblEyecatcher tblEyecatcher, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        tblEyecatcher.Eyecatcher = reader.ReadBytes(upload.ContentLength);
                    }
                }

                tblEyecatcher.GUIDBild = Guid.NewGuid();
                db.Entry(tblEyecatcher).State = EntityState.Modified;
                db.SaveChanges();

            }
            return RedirectToAction("Einstellung");
        }

        // GET: Eyecatchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEyecatcher tblEyecatcher = db.tblEyecatcher.Find(id);
            if (tblEyecatcher == null)
            {
                return HttpNotFound();
            }
            return View(tblEyecatcher);
        }

        // POST: Eyecatchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEyecatcher tblEyecatcher = db.tblEyecatcher.Find(id);
            db.tblEyecatcher.Remove(tblEyecatcher);
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
