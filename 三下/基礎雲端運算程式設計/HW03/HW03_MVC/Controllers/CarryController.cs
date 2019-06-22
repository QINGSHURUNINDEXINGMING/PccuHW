using HW03_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HW03_MVC.Controllers
{
    public class CarryController : Controller
    {
        HttpClient client = new HttpClient();

        // GET: Carry
        public ActionResult Carry()
        {
            return View();
        }

        [HttpPost]
        // 只要自動繫結模型中的Num1及Num2
        public async Task<ActionResult> Carry([Bind(Include = "num")] CarryViewModel carrydata, string btnCompute)
        {
            string urlstring, op, result;

            if (!ModelState.IsValid)
            {
                return View(carrydata);
            }
            try
            {
                switch (btnCompute)
                {
                    case "Binary":
                        op = "Binary";
                        break;
                    case "Octal":
                        op = "Octal";
                        break;
                    case "Hexadecimal":
                        op = "Hexadecimal";
                        break;
                    default:
                        op = "Binary";
                        break;
                }
                urlstring = "http://140.137.41.136:5558/A6409001/HW03/api/Carry/" + op + "/" + carrydata.num ;
                // 以非同步GET方式呼叫數學服務之API
                HttpResponseMessage response = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之字串
                result = await response.Content.ReadAsStringAsync();
                // 從結果字串存入carrydata物件的result欄位
                carrydata.result = result;
                // 將carrydata物件傳送到View顯示
                return View(carrydata);
            }
            catch (Exception ex)
            {
                result = "數學運算時發生例外，原因如下: " + ex.Message;
                carrydata.result = result;
                // 將carrydata物件傳送到View顯示
                return View(carrydata);
            }

        }
    }
}