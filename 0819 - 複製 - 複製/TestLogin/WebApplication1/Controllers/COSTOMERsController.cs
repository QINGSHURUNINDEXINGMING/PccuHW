using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class COSTOMERsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: COSTOMERs
        public async Task<ActionResult> Index()
        {
            return View(await db.COSTOMERs.ToListAsync());
        }

        public ActionResult InitialWallet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InitialWallet([Bind(Include = "id, UserName, UID")]COSTOMER cOSTOMER)
        {
            if (ModelState.IsValid)
            {
                int wallet = 0;

                cOSTOMER.wallet = wallet;

                var result1 = db.Users.Where(u => u.UserName == cOSTOMER.UserName);
                var result2 = db.COSTOMERs.Where(x => x.UID == cOSTOMER.UID);



                if (result1.Count() == 1)
                {
                    
                    if (result2.Count() == 0)
                    {
                        TempData["創建訊息"] = "錢包初始化成功";
                        db.COSTOMERs.Add(cOSTOMER);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["創建訊息"] = "已創立錢包";
                    }
                }
                else
                {
                    TempData["創建訊息"] = "未創立UserName";
                }


                //var result = db.COSTOMERs.Where(x => x.UserName == cOSTOMER.UserName);

                //cOSTOMER.UserName = user.UserName;
              
                //string userName = cOSTOMER.UserName;
                //string UID = cOSTOMER.UID;

                //cOSTOMER.wallet = wallet;
                //cOSTOMER.UserName = userName;
                //cOSTOMER.UID = UID;

                return RedirectToAction("Index");
            }
            return View(cOSTOMER);
        }

        // GET: COSTOMERs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COSTOMER cOSTOMER = await db.COSTOMERs.FindAsync(id);
            if (cOSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cOSTOMER);
        }

        // GET: COSTOMERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: COSTOMERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,UserName,UID,wallet,deposit,debt")] COSTOMER cOSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.COSTOMERs.Add(cOSTOMER);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cOSTOMER);
        }

        // GET: COSTOMERs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COSTOMER cOSTOMER = await db.COSTOMERs.FindAsync(id);
            if (cOSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cOSTOMER);
        }

        // POST: COSTOMERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,UserName,UID,wallet,deposit,debt")] COSTOMER cOSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOSTOMER).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cOSTOMER);
        }

        // GET: COSTOMERs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COSTOMER cOSTOMER = await db.COSTOMERs.FindAsync(id);
            if (cOSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cOSTOMER);
        }

        // POST: COSTOMERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            COSTOMER cOSTOMER = await db.COSTOMERs.FindAsync(id);
            db.COSTOMERs.Remove(cOSTOMER);
            await db.SaveChangesAsync();
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
