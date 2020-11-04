using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032AssignmentV1.Models;
using FIT5032AssignmentV1.Utils;
using Microsoft.AspNet.Identity;

namespace FIT5032AssignmentV1.Controllers
{

    public class BookCourseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
      

        public ActionResult Book(int id) 
        {
            var userId = User.Identity.GetUserId();
            var check = db.BookCourses.Where(p => p.ProviderCourseId == id && p.ApplicationUserId.Equals(userId)).FirstOrDefault();

            if (check == null)
            {
                BookCourse bookCourse = new BookCourse { ApplicationUserId = userId, ProviderCourseId = id };
                bookCourse.Rating = 0;
                db.BookCourses.Add(bookCourse);
                db.SaveChanges();
                SendEmail();
                var userBooking = db.BookCourses.Include(p => p.providerCourse).Include(p => p.applicationUser).Where(m => m.ApplicationUserId == userId);
                return View("index",userBooking.ToList());
            }
            else
            {
                return Content("<script>alert('You has been book this course before, you cannot book twice.'); window.location.href='/ProviderCourses' </script>");
            }

        }

        public ActionResult UserBooking() {

            var userId = User.Identity.GetUserId();
            var userBooking = db.BookCourses.Include(p=>p.providerCourse).Include(p=>p.applicationUser).Where(m => m.ApplicationUserId == userId);
            return View(userBooking.ToList());
        }

        public ActionResult EditRating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCourse bookCourse = db.BookCourses.Find(id);
            if (bookCourse == null)
            {
                return HttpNotFound();
            }
            return View(bookCourse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRating([Bind(Include = "BookCourseId,Rating,ApplicationUserId,ProviderCourseId")] BookCourse bookCourse) {
            if (ModelState.IsValid)
            {
                db.Entry(bookCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserBooking");
            }
            ViewBag.ProviderCourseId = new SelectList(db.ProviderCourses, "ProviderCourseId", "CourseName", bookCourse.ProviderCourseId);
            return View(bookCourse);
        
        }

        // GET: BookCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCourse bookCourse = db.BookCourses.Find(id);
            if (bookCourse == null)
            {
                return HttpNotFound();
            }
            return View(bookCourse);
        }

        // POST: BookCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookCourse bookCourse = db.BookCourses.Find(id);
            db.BookCourses.Remove(bookCourse);
            db.SaveChanges();
            var userId = User.Identity.GetUserId();
            var userBooking = db.BookCourses.Include(p => p.providerCourse).Include(p => p.applicationUser).Where(m => m.ApplicationUserId == userId);
            return RedirectToAction("index", userBooking.ToList());
        }

        public void SendEmail() {
            string toEmail = User.Identity.GetUserName();
            string subject = "Course Confirmation";
            string contents = "You have successfully book the course";

            EmailSender es = new EmailSender();
            es.SendAttach(toEmail, subject, contents);
        }

        public ActionResult Chart() {
            var courseList = db.ProviderCourses.ToList();
            List<string> chartName = new List<string>();
            List<int> chartCount = new List<int>();
            foreach (var clists in courseList) 
            {
                chartName.Add(clists.CourseName);
                var bookList = db.BookCourses.Where(b => b.ProviderCourseId == clists.ProviderCourseId).ToList();
                chartCount.Add(bookList.Count);
            }
            ViewBag.ChartName = chartName.ToList();
            ViewBag.ChartCount = chartCount.ToList();
            return View();
        
        
        }


    }
}