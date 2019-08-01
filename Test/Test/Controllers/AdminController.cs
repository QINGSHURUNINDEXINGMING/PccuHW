using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string userName = form["txtEmail"];
            string email = form["txtEmail"];
            string pwd = form["txtPassword"];

            var user = new ApplicationUser();
            user.UserName = userName;
            user.Email = email;

            var newUser = userManager.Create(user, pwd);

            return View();
        }
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(FormCollection form)
        {
            string roleName = form["roleName"];
            return View();
        }
        public ActionResult AssignRole()
        {
            return View();
        }
    }
}