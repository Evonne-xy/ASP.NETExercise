using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032AssignmentV1.Models;

namespace FIT5032AssignmentV1.Controllers
{
    public class CourseTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseTypes
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("ProviderManager"))
            {
                return View("Index",db.CourseTypes.ToList());
            }
            else {
                return View("StudentIndex", db.CourseTypes.ToList());
            }
            
        }

        // GET: CourseTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = db.CourseTypes.Find(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // GET: CourseTypes/Create
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Create([Bind(Include = "CourseTypeID,CourseTypeName,CourseTypeDec,Image")] CourseType courseType)
        {
            if (ModelState.IsValid)
            {
                db.CourseTypes.Add(courseType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseType);
        }

        // GET: CourseTypes/Edit/5
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = db.CourseTypes.Find(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // POST: CourseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Edit([Bind(Include = "CourseTypeID,CourseTypeName,CourseTypeDec,Image")] CourseType courseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseType);
        }

        // GET: CourseTypes/Delete/5
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = db.CourseTypes.Find(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // POST: CourseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseType courseType = db.CourseTypes.Find(id);
            db.CourseTypes.Remove(courseType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CourseTypes/FindCourses
        [Authorize(Roles = "Student")]
        public ActionResult FindCourses()
        {
            return View();
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
