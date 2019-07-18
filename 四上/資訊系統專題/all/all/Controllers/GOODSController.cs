using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using all.Models;

namespace all.Controllers
{
    public class GOODSController : Controller
    {
        private ConnectDB db = new ConnectDB();

        // GET: GOODS
        public ActionResult Index()
        {
            return View(db.Goods1.ToList());
        }

        // GET: GOODS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS gOODS = db.Goods1.Find(id);
            if (gOODS == null)
            {
                return HttpNotFound();
            }
            return View(gOODS);
        }

        // GET: GOODS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GOODS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UID,name,price,discount,discount_price")] GOODS gOODS)
        {
            if (ModelState.IsValid)
            {
                db.Goods1.Add(gOODS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gOODS);
        }

        // GET: GOODS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS gOODS = db.Goods1.Find(id);
            if (gOODS == null)
            {
                return HttpNotFound();
            }
            return View(gOODS);
        }

        // POST: GOODS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UID,name,price,discount,discount_price")] GOODS gOODS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gOODS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gOODS);
        }

        // GET: GOODS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS gOODS = db.Goods1.Find(id);
            if (gOODS == null)
            {
                return HttpNotFound();
            }
            return View(gOODS);
        }

        // POST: GOODS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GOODS gOODS = db.Goods1.Find(id);
            db.Goods1.Remove(gOODS);
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
