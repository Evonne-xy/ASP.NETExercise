using FIT5032AssignmentV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032AssignmentV1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index() {
            return View(db.Roles.ToList());            
        }

        //GET Roles
        public ActionResult Create() {
            return View();
        }

        //POST Roles
        [HttpPost]
        public ActionResult Create(FormCollection formCollection) {
            try
            {
                //Create new Role
                var newRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = formCollection["RoleName"]
                };
                db.Roles.Add(newRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        // GET: Roles/Edit
        public ActionResult Edit(String roleName) {
            var editrole = db.Roles.Where(role => role.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(editrole);
        }

        // POST: Roles/Edit
        [HttpPost]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        // Get: Roles/Delete
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        // Post: Roles/DeleteConfrim
        [HttpPost]
        public ActionResult Delete(String roleName)
        {
            var deleteRole = db.Roles.Where(role => role.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            db.Roles.Remove(deleteRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}




















