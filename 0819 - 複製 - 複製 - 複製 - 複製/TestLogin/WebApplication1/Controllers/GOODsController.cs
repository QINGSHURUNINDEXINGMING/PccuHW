using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GOODsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GOODs
        //public ActionResult Index()
        //{
        //    return View(db.GOODs.ToList());
        //}

        public async Task<ActionResult> Index(string searchString)
        {
            var text = from m in db.GOODs
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                text = text.Where(s => s.name.Contains(searchString));
            }

            return View(await text.ToListAsync());
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UID,name,price,discount,discount_price")] GOOD gOOD)
        {
            //if (ModelState.IsValid)
            //{
            //    db.GOODs.Add(gOOD);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}


            if (ModelState.IsValid)
            {
                double discount_price;

                discount_price = (0.1 * gOOD.discount) * gOOD.price;
                gOOD.discount_price = (int)discount_price;

                var result = db.GOODs.Where(x => x.UID == gOOD.UID);
                int count = result.Count();
                if (count == 0)
                {
                    TempData["創建訊息"] = "儲存成功";
                    db.GOODs.Add(gOOD);
                    db.SaveChanges();
                }
                else
                {
                    TempData["創建訊息"] = "已有相同UID名稱";
                }

                var text = from m in db.GOODs
                           select m;

                //return RedirectToAction("Index");
                return View(gOOD);


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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UID,name,price,discount,discount_price")] GOOD gOOD)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(gOOD).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(gOOD);
            if (ModelState.IsValid)
            {
                double discount_price;

                discount_price = (0.1 * gOOD.discount) * gOOD.price;
                gOOD.discount_price = (int)discount_price;


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
