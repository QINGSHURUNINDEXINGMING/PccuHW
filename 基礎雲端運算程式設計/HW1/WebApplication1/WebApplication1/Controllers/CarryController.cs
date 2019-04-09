using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewModels
{
    public class CarryController : Controller
    {
        // GET: Math
        public ActionResult CarryCompute()
        {
            return View();
        }

        [HttpPost]
        // 利用Include只要自動繫結模型中的Num
        public ActionResult CarryCompute([Bind(Include = "Num")] CarryViewModel data, string btnCompute)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            try
            {
                int num;
                string result1, result2, result3;
                num = data.Num;
                result1 = data.Result1;
                result2 = data.Result2;
                result3 = data.Result3;

                switch (btnCompute)
                {
                    case "運算":

                        //十進位轉二進位
                        result1 = Convert.ToString(num, 2);

                        //十六進位轉十進位
                        result2 = Convert.ToString(num, 8);

                        //十進位轉十六進位
                        result3 = Convert.ToString(num, 16);
                        break;

                    default:
                        break;
                }
                data.Result1 = result1;
                data.Result2 = result2.ToString();
                data.Result3 = result3.ToString();
                return View(data);
            }
            catch (Exception ex)
            {
                string str = "數學運算時發生例外，原因如下:\r\n" + ex.Message;
                data.Result1 = str;
                return View(data);
            }

        }
    }
}