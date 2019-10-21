using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A6409001_WebPD_Week7.Models;

namespace A6409001_WebPD_Week7.Controllers
{
    public class A6409001_Week7_1Controller : Controller
    {
        static Random rnd = new Random();
        List<Student> students = new List<Student>
        {
            new Student{ID=1, Name="王曉明1", Chinese=rnd.Next(40, 100), English=rnd.Next(40, 100), Math=rnd.Next(40, 100)},
            new Student{ID=2, Name="王曉明2", Chinese=rnd.Next(40, 100), English=rnd.Next(40, 100), Math=rnd.Next(40, 100)},
            new Student{ID=3, Name="王曉明3", Chinese=rnd.Next(40, 100), English=rnd.Next(40, 100), Math=rnd.Next(40, 100)},
            new Student{ID=4, Name="王曉明4", Chinese=rnd.Next(40, 100), English=rnd.Next(40, 100), Math=rnd.Next(40, 100)},
            new Student{ID=5, Name="王曉明5", Chinese=rnd.Next(40, 100), English=rnd.Next(40, 100), Math=rnd.Next(40, 100)},
            new Student{ID=6, Name="王曉明6", Chinese=rnd.Next(40, 100), English=rnd.Next(40, 100), Math=rnd.Next(40, 100)}

        };
        // GET: A6409001_Week7_1
        public ActionResult Index()
        {
            return View(students);
        }
        public ActionResult Score()
        {
            int topID = 0, topScore = 0;

            foreach(var student in students)
            {
                student.Total = student.Chinese + student.English + student.Math;
                if (topScore < student.Total)
                {
                    topScore = student.Total;
                    topID = student.ID;
                }
            }
            ViewBag.topID = topID;
            return View(students);
        }
        public ActionResult ScoreHelper1()
        {
            return View(students);
        }
        public ActionResult ScoreRazor11()
        {
            return View(students);
        }
    }
}