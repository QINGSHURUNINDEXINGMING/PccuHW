using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestAll.Models;

namespace TestAll.Controllers
{
    public class GOODsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GOODs
        public ActionResult Index()
        {
            return View(db.GOODs.ToList());
        }

        // GET: GOODs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOOD gOOD = db.GOODs.Find(id);
            if (gOOD == null)
            {
                return HttpNotFound();
            }
            return View(gOOD);
        }

        // GET: GOODs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GOODs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UID,name,price,discount,discount_price")] GOOD gOOD)
        {
            if (ModelState.IsValid)
            {
                db.GOODs.Add(gOOD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gOOD);
        }

        // GET: GOODs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOOD gOOD = db.GOODs.Find(id);
            if (gOOD == null)
            {
                return HttpNotFound();
            }
            return View(gOOD);
        }

        // POST: GOODs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UID,name,price,discount,discount_price")] GOOD gOOD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gOOD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gOOD);
        }

        // GET: GOODs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOOD gOOD = db.GOODs.Find(id);
            if (gOOD == null)
            {
                return HttpNotFound();
            }
            return View(gOOD);
        }

        // POST: GOODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GOOD gOOD = db.GOODs.Find(id);
            db.GOODs.Remove(gOOD);
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
