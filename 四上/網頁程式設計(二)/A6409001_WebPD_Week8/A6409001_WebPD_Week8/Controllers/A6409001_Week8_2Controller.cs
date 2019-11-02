using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
namespace A6409001_WebPD_Week8.Controllers
{
    public class A6409001_Week8_2Controller : Controller
    {
        // GET: A6409001_Week8_2
        public ActionResult Index()
        {
            return View();
        }

        public string ReturnText()
        {
            string str = "<h3>A6409001_Week8_2</h3>";
            return str;
        }
        public HttpStatusCodeResult code404()
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
        }

        public ActionResult code404_2()
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
        }

        public ActionResult code500()
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
        }

        public ActionResult code401()
        {
            return new HttpUnauthorizedResult("Access is defined");
        }

        public ActionResult code404_3(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
            return new HttpUnauthorizedResult("Access is defined 2");
            }
        }
        public ContentResult about()
        {
            return Content("A6409001_王曉明_Week8");
        }

        public ContentResult about2()
        {
            string script = "<script>alert('A6409001_王曉明_Week8')</script>";
            return Content(script, "application/javascript");
        }

        public JavaScriptResult about3()
        {
            string script = "<script>alert('A6409001_王曉明_Week8')</script>";
            return JavaScript(script);
        }

        public FilePathResult file1()
        {
            return File("~/assests/css/mycss.cs/", "text/css");
        }

        public RedirectResult getLineBacic()
        {
            return Redirect("A6409001_Week8/LineBasic");
        }





    }
}