using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032AssignmentV1.Models;

namespace FIT5032AssignmentV1.Controllers
{
    public class fileUploadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: fileUploads
        public ActionResult Index()
        {
            return View(db.fileUpload.ToList());
        }

        // GET: fileUploads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        // GET: fileUploads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fileUploads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileId,Name")] fileUpload fileUpload, HttpPostedFileBase postedFile)
        {
            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            fileUpload.Path = myUniqueFileName;
            TryValidateModel(fileUpload);


            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Uploads/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = fileUpload.Path + fileExtension;
                fileUpload.Path = filePath;
                postedFile.SaveAs(serverPath + fileUpload.Path);
                db.fileUpload.Add(fileUpload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        // GET: fileUploads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        // POST: fileUploads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,Path,Name")] fileUpload fileUpload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileUpload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileUpload);
        }

        // GET: fileUploads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        // POST: fileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fileUpload fileUpload = db.fileUpload.Find(id);
            db.fileUpload.Remove(fileUpload);
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
