using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW2.Models;

namespace HW2.Controllers
{
    public class HWTBsController : Controller
    {
        private Model1 db = new Model1();

        // GET: HWTBs
        public ActionResult Index()
        {
            return View(db.HWTBs.ToList());
        }

        // GET: HWTBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HWTB hWTB = db.HWTBs.Find(id);
            if (hWTB == null)
            {
                return HttpNotFound();
            }
            return View(hWTB);
        }

        // GET: HWTBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HWTBs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,comment,date")] HWTB hWTB)
        {
            if (ModelState.IsValid)
            {
                db.HWTBs.Add(hWTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hWTB);
        }

        // GET: HWTBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HWTB hWTB = db.HWTBs.Find(id);
            if (hWTB == null)
            {
                return HttpNotFound();
            }
            return View(hWTB);
        }

        // POST: HWTBs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,comment,date")] HWTB hWTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hWTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hWTB);
        }

        // GET: HWTBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HWTB hWTB = db.HWTBs.Find(id);
            if (hWTB == null)
            {
                return HttpNotFound();
            }
            return View(hWTB);
        }

        // POST: HWTBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HWTB hWTB = db.HWTBs.Find(id);
            db.HWTBs.Remove(hWTB);
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
            var comments = from m in db.HWTBs select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                comments = comments.Where(s => s.comment.Contains(searchString));
            }

            return View(comments);
        }
    }
}
