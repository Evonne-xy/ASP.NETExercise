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
    public class ProviderCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProviderCourses
        public ActionResult Index()
        {
            var providerCourses = db.ProviderCourses.Include(p => p.CourseType).Include(p => p.Provider);
            //return View(providerCourses.ToList());
            if (User.IsInRole("Admin") || User.IsInRole("ProviderManager"))
            {
                return View("Index", providerCourses.ToList());
            }
            else
            {
                return View("StudentIndex", providerCourses.ToList());
            }
            
        }

        // GET: ProviderCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderCourse providerCourse = db.ProviderCourses.Find(id);
            if (providerCourse == null)
            {
                return HttpNotFound();
            }
            db.Entry(providerCourse).Reference(p => p.CourseType).Load();
            db.Entry(providerCourse).Reference(p => p.Provider).Load();
            return View(providerCourse);
        }

        // GET: ProviderCourses/Create
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Create()
        {
            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "CourseTypeID", "CourseTypeName");
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "ProviderName");
            return View();
        }

        // POST: ProviderCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Create([Bind(Include = "ProviderCourseId,CourseName,CourseTime,CourseTypeId,ProviderId")] ProviderCourse providerCourse)
        {
            if (ModelState.IsValid)
            {
                db.ProviderCourses.Add(providerCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "CourseTypeID", "CourseTypeName", providerCourse.CourseTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "ProviderName", providerCourse.ProviderId);
            return View(providerCourse);
        }

        // GET: ProviderCourses/Edit/5
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderCourse providerCourse = db.ProviderCourses.Find(id);
            if (providerCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "CourseTypeID", "CourseTypeName", providerCourse.CourseTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "ProviderName", providerCourse.ProviderId);
            return View(providerCourse);
        }

        // POST: ProviderCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Edit([Bind(Include = "ProviderCourseId,CourseName,CourseTime,CourseTypeId,ProviderId")] ProviderCourse providerCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(providerCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "CourseTypeID", "CourseTypeName", providerCourse.CourseTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "ProviderName", providerCourse.ProviderId);
            return View(providerCourse);
        }

        // GET: ProviderCourses/Delete/5
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderCourse providerCourse = db.ProviderCourses.Find(id);
            if (providerCourse == null)
            {
                return HttpNotFound();
            }
            return View(providerCourse);
        }

        // POST: ProviderCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProviderManager")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProviderCourse providerCourse = db.ProviderCourses.Find(id);
            db.ProviderCourses.Remove(providerCourse);
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
