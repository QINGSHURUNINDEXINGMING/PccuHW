using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A6409001_WebPD_Week3.Models;


namespace A6409001_WebPD_Week3.Controllers
{
    public class A6409001_Week3_1Controller : Controller
    {
        // GET: A6409001_Week3_1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PassTempData()
        {
            TempData["Message"] = "FromWeek3_1 PassTempData";
            TempData["Name"] = "A6409001_邱郁涵";
            TempData["Time"] = DateTime.Now.ToLongTimeString();

            ViewBag.Msg = "Data from ViewBag";

            return RedirectToAction("FromWeek3_1", "A6409001_Week3_2");
        }
        public ActionResult EmployeeList()
        {
            List<Employee> empList = new List<Employee>
            {
                new Employee{ID=100001, Name="王曉明_1", Phone="0919975131", Email="Smal1@qq.com"},
                new Employee{ID=100002, Name="王曉明_2", Phone="0919975132", Email="Smal2@qq.com"},
                new Employee{ID=100003, Name="王曉明_3", Phone="0919975133", Email="Smal3@qq.com"},
                new Employee{ID=100004, Name="王曉明_4", Phone="0919975134", Email="Smal4@qq.com"},
                new Employee{ID=100005, Name="王曉明_5", Phone="0919975135", Email="Smal5@qq.com"},
            };
            ViewData.Model = empList;
            return View();
        }
        public ActionResult EmployeeOne()
        {
            Employee emp = new Employee();
            emp.ID = 10001;
            emp.Name = "王孝明";
            emp.Phone = "0919975131";
            emp.Email = "Smal1@qq.com";

            ViewData.Model = emp;

            return View();
        }
        public ActionResult EmployeeOneStringType()
        {
            Employee emp = new Employee
            {
                ID = 10001,
                Name = "王孝明",
                Phone = "0919975131",
                Email = "Smal1@qq.com"
            };
       
            return View(emp);
        }
        public ActionResult test()
        {
            return View();
        }
    }

}
