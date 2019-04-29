using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW2.Models;
using Korzh.EasyQuery.Linq;

namespace HW2.Controllers
{
    public class TestTBsController : Controller
    {
        private Model1 db = new Model1();

        // GET: TestTBs
        public ActionResult Index()
        {
            return View(db.TestTables.ToList());
        }

        // GET: TestTBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTB testTB = db.TestTables.Find(id);
            if (testTB == null)
            {
                return HttpNotFound();
            }
            return View(testTB);
        }

        // GET: TestTBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestTBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,comment,date")] TestTB testTB)
        {
            if (ModelState.IsValid)
            {
                db.TestTables.Add(testTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testTB);
        }

        // GET: TestTBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTB testTB = db.TestTables.Find(id);
            if (testTB == null)
            {
                return HttpNotFound();
            }
            return View(testTB);
        }

        // POST: TestTBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,comment,date")] TestTB testTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testTB);
        }

        // GET: TestTBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTB testTB = db.TestTables.Find(id);
            if (testTB == null)
            {
                return HttpNotFound();
            }
            return View(testTB);
        }

        // POST: TestTBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestTB testTB = db.TestTables.Find(id);
            db.TestTables.Remove(testTB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public string SearchIndex(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]SearchIndex: " + searchString + "</h3>";
        }



        public ActionResult SearchIndex(string searchString)
        {
            var comments = from m in db.TestTables select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                comments = comments.Where(s => s.comment.Contains(searchString));
            }

            return View(comments);
        }
    }
}
