using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032AssignmentV1.Models;
using FIT5032AssignmentV1.Utils;

namespace FIT5032AssignmentV1.Controllers
{
    public class ProviderCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string CourseTime { get; set; }

        // GET: ProviderCourses
        public ActionResult Index()
        {
            var providerCourses = db.ProviderCourses.Include(p => p.CourseType).Include(p => p.Provider).ToList();
            var courseList = new List<ProviderCourse>();
            //iterate providerCourses and caculate average rating
            foreach (var pc in providerCourses)
            {
                pc.AggregateRating = 0;
                var booking = db.BookCourses.Where(p => p.ProviderCourseId == pc.ProviderCourseId).ToList();
                var totalRating = 0;
                foreach (var pc2 in booking)
                {
                    totalRating = totalRating + pc2.Rating;
                }
                double avarageRating = double.Parse(totalRating.ToString()) / double.Parse(booking.Count.ToString());
                if (double.IsNaN(avarageRating))
                {
                    avarageRating = 0.0;
                }
                pc.AggregateRating = avarageRating;
                courseList.Add(pc);
            }
            // display different view to different role
            if (User.IsInRole("Admin") || User.IsInRole("ProviderManager"))
            {

                return View("Index", courseList);
            }
            else
            {
                return View("StudentIndex", courseList);
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
            providerCourse.AggregateRating = 0.0;
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
        public ActionResult Edit(int? id,ProviderCourse providerCourse)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            providerCourse = db.ProviderCourses.Find(id);
            //ProviderCourse providerCourse = db.ProviderCourses.Find(id);
            if (providerCourse == null)
            {
                return HttpNotFound();
            }
            var bookCourse = db.BookCourses.Include(p => p.applicationUser).Where(p => p.ProviderCourseId == providerCourse.ProviderCourseId).ToList();
            List<string> email = new List<string>();
            foreach (BookCourse book in bookCourse) {
                email.Add(book.applicationUser.Email);
            }
            string subject = "Course information changed(Bulk Email)";
            string content = "Your Course:" + providerCourse.CourseName + "some information has been changed. Please check it";
            EmailSender es = new EmailSender();
            es.SendBulkAttachEmial(email, subject, content);

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
        public ActionResult Edit([Bind(Include = "ProviderCourseId,CourseName,CourseTime,CourseTypeId,ProviderId,AggregateRating")] ProviderCourse providerCourse)
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

        public ActionResult findAll() {
            JsonResult result = new JsonResult();
            try
            {
                // Loading.  
                var providerCourses = db.ProviderCourses.Include(p => p.CourseType).Include(p => p.Provider).ToList();

                // Processing.  
                result = this.Json(providerCourses, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;

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
