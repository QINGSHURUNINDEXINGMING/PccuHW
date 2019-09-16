using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A6409001_WebPD_Week2.Controllers
{
    public class A6409001_Week2_2Controller : Controller
    {
        // GET: A6409001_Week2_2
        public ActionResult Index()
        {
            return View();
        }

        public string Sum1to10()
        {
            int sum = 0;
            for (int i = 0; i <= 10; i++)
            {
                sum += i;
            }
            return "1-10 相加 = " + sum;
        }

        public ActionResult Sum2()
        {
            int sum = 0;
            for (int i = 0; i <= 10; i++)
            {
                sum += i;
            }
            ViewBag.summ = sum;
            return View();
        }

        public ActionResult PassViewData()
        {
            ViewData["SID"] = "A6409001";
            ViewData["Name"] = "人";
            ViewData["Age"] = 20;
            ViewData["Single"] = true;

            List<string> petList = new List<string>();
            petList.Add("狗");
            petList.Add("貓");
            petList.Add("羊");
            petList.Add("魚");
            petList.Add("牛");
            ViewData["petList"] = petList;

            ViewData.Model = petList;

            return View();
        }
        public ActionResult PassViewBag()
        {
            ViewBag.SID = "A6409001";
            ViewBag.Name = "人";
            ViewBag.Age = 20;
            ViewBag.Single = true;

            List<string> petList = new List<string>();
            petList.Add("狗");
            petList.Add("貓");
            petList.Add("羊");
            petList.Add("魚");
            petList.Add("牛");
            ViewBag.petList = petList;
            return View(petList);
        }

    }
}