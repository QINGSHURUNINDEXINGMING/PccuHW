using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DeductMoney()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeductMoney(FormCollection form)
        {
            string userName = form["txtUserName"];
            string goodUID = form["txtGoodUID"];
            string deductMoney = form["txtMoney"];

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            ApplicationUser user1;


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var query1 = context.Users.Where(u => u.UserName == userName);
            var query2 = context.GOODs.Where(u => u.UID == goodUID);

            int count1 = query1.Count();
            int count2 = query2.Count();


            if (count1 == 1 && count2 == 1)
            {
                

                int DMoney = Convert.ToInt32(deductMoney);
                int userOrignalWallet = Convert.ToInt32(user.Wallet);

                int userTotalMoney = userOrignalWallet - DMoney;

                if (userTotalMoney < 0)
                {
                    TempData["創建訊息"] = "餘額不足";
                }
                else
                {
                    user.Wallet = Convert.ToString(userTotalMoney);

                    context.SaveChanges();
                }

                
            }
            else
            {
                TempData["創建訊息"] = "儲存失敗，沒有此UserName";
            }

            return RedirectToAction("Index");
        }
    }
}