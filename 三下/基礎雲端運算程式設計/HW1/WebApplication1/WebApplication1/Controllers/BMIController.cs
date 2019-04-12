using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewModels
{
    public class BMIController : Controller
    {
        // GET: Math
        public ActionResult BMICompute()
        {
            return View();
        }

        [HttpPost]
        // 利用Include只要自動繫結模型中的Num
        public ActionResult BMICompute([Bind(Include = "Num1, Num2")] BMIViewModel data, string btnCompute)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            try
            {
                double num1, num2, result;
                num1 = data.Num1;
                num2 = data.Num2;

                switch (btnCompute)
                {
                    case "運算":
                        result = num2 / ((num1 / 100) * (num1 / 100));
                        break;

                    default:
                        result = num1 + num2;
                        break;
                }
                data.Result = result.ToString("#0.00");
                return View(data);
            }
            catch (Exception ex)
            {
                string str = "數學運算時發生例外，原因如下:\r\n" + ex.Message;
                data.Result = str;
                return View(data);
            }

        }
    }
}