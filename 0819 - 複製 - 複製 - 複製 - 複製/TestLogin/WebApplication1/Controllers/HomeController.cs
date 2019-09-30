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


            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            GOOD gOOD = context.GOODs.Where(u => u.UID.Equals(goodUID, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var queryUser = context.Users.Where(u => u.UserName == userName);
            var queryGood = context.GOODs.Where(u => u.UID == goodUID);

            int countUser = queryUser.Count();
            int countGood = queryGood.Count();


            if (countUser == 1 && countGood == 1)
            {

                int deductMoney = gOOD.discount_price;

                int userOrignalWallet = Convert.ToInt32(user.Wallet);

                int userTotalMoney = userOrignalWallet - deductMoney;

                if (userTotalMoney < 0)
                {
                    TempData["創建訊息"] = "餘額不足";
                }
                else
                {
                    TempData["創建訊息"] = "扣款成功";

                    user.Wallet = Convert.ToString(userTotalMoney);
                    context.SaveChanges();
                }
            }
            else if (countUser == 0)
            {
                TempData["創建訊息"] = "扣款失敗，沒有此UserName";
            }
            else
            {
                TempData["創建訊息"] = "扣款失敗，沒有此商品";
            }


            return RedirectToAction("Index");
        }
    }
}