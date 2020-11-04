using FIT5032AssignmentV1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032AssignmentV1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AllocateUserController: Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager;
        public AllocateUserController() {
            var userStore = new UserStore<ApplicationUser>(db);
            UserManager = new UserManager<ApplicationUser>(userStore);
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        //GET
        public ActionResult AllocateRoleToUser() {
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllocateRoleToUser(FormCollection formCollection)
        {
            try
            {
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.roles = list;
                string UserName = Request.Form["UserName"];
                string RoleName = Request.Form["RoleName"];
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
               // var account = new AccountController();
                this.UserManager.AddToRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role created successfully !";
                return View("AllocateRoleToUser");
            }
            catch {
                return View();
            }
           
        }

        public ActionResult GettingRolesforuser() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GettingRolesforuser(string UserName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    ViewBag.RolesForThisUser = this.UserManager.GetRoles(user.Id);
                }

                return View("GettingRolesforuser");
            }
            catch {
                return View("GettingRolesforuser");
            }
            
        }

        //Get Role
        public ActionResult DeleteRoleToUser()
        {
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleToUser(string UserName, string RoleName)
        {
            try
            {
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (this.UserManager.IsInRole(user.Id, RoleName))
                {
                    this.UserManager.RemoveFromRole(user.Id, RoleName);
                    ViewBag.ResultMessage = "Role removed from this user successfully !";
                }
                else
                {
                    ViewBag.ResultMessage = "This user doesn't belong to selected role.";
                }
                return View("Index");

            }
            catch {
                return View();
            }

            

        }




    }
}