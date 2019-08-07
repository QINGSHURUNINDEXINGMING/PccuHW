using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAll.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles ="Admin")]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult AssignRole()
        {
            return View();
        }
    }
}