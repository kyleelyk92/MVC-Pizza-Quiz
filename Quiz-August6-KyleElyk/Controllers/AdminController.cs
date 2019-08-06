using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiz_August6_KyleElyk.Controllers
{
    public class AdminController : Controller
    {
        private RoleManager<IdentityRole> rolesManager;
        private UserManager<IdentityUser> usersManager;

        public AdminController()
        {
            rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            usersManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddUsersToRole()
        {
            ViewBag.RoleId = new SelectList(rolesManager.Roles.ToList(), "Id", "Name");
            ViewBag.RoleId = new SelectList(usersManager.Users.ToList(), "Id", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult AddUsersToRole(int UserId, int RoleId)
        {
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult RemoveAUser()
        {
            ViewBag.UserId = new SelectList(usersManager.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult RemoveAUser(string UserId)
        {
            var user = usersManager.FindById(UserId);
            usersManager.Delete(user);

            return View();
        }
    }
}