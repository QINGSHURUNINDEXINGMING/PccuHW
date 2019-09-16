using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using A6409001_WebPD_Week2.Models;

namespace A6409001_WebPD_Week2.Controllers
{
    public class A6409001Controller : Controller
    {
        private A6409001Context db = new A6409001Context();

        // GET: A6409001
        public ActionResult Index()
        {
            return View(db.Frinds.ToList());
        }

        // GET: A6409001/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frind frind = db.Frinds.Find(id);
            if (frind == null)
            {
                return HttpNotFound();
            }
            return View(frind);
        }

        // GET: A6409001/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: A6409001/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Phone,Email,City")] Frind frind)
        {
            if (ModelState.IsValid)
            {
                db.Frinds.Add(frind);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(frind);
        }

        // GET: A6409001/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frind frind = db.Frinds.Find(id);
            if (frind == null)
            {
                return HttpNotFound();
            }
            return View(frind);
        }

        // POST: A6409001/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Phone,Email,City")] Frind frind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frind);
        }

        // GET: A6409001/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frind frind = db.Frinds.Find(id);
            if (frind == null)
            {
                return HttpNotFound();
            }
            return View(frind);
        }

        // POST: A6409001/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Frind frind = db.Frinds.Find(id);
            db.Frinds.Remove(frind);
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
    }
}
