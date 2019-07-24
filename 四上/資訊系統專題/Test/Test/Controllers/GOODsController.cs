using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    [Authorize]
    public class GOODsController : Controller
    {
        private ConnectDB db = new ConnectDB();

        // GET: GOODs
        //public ActionResult Index()
        //{
        //    return View(db.Good1.ToList());
        //}

        //https://social.msdn.microsoft.com/Forums/zh-TW/394c3de5-8466-4f21-b1a2-1dcbe51a13a5/35531218392459027171214872019738480210461998121516299922514236?forum=236

        public async Task<ActionResult> Index(string searchString)
        {
            var text = from m in db.Good1
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
            GOOD gOOD = db.Good1.Find(id);
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
        public ActionResult Create([Bind(Include = "id,UID,name,price,discount")] GOOD gOOD)
        {
            if (ModelState.IsValid)
            {
                double discount_price;
              
                discount_price = (0.1 * gOOD.discount) * gOOD.price;
                gOOD.discount_price = (int)discount_price;

                var result = db.Good1.Where(x => x.UID == gOOD.UID);
                int count = result.Count();
                if (count == 0)
                {
                    //TempData["創建訊息"] = "儲存成功";
                    db.Good1.Add(gOOD);
                    db.SaveChanges();
                }
                else
                {
                    TempData["創建訊息"] = "已有相同UID名稱";
                }

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
            GOOD gOOD = db.Good1.Find(id);
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
        public ActionResult Edit([Bind(Include = "id,UID,name,price,discount")] GOOD gOOD)
        {
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
            GOOD gOOD = db.Good1.Find(id);
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
            GOOD gOOD = db.Good1.Find(id);
            db.Good1.Remove(gOOD);
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
