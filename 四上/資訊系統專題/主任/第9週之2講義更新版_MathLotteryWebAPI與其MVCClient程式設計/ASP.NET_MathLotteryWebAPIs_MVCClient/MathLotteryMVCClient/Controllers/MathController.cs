using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http; // needed for using the HttpClient class
using System.Net.Http.Headers;
using System.Threading.Tasks;

using MathLotteryMVCClient.ViewModels;

namespace MathLotteryMVCClient.Controllers
{
    public class MathController : Controller
    {
        HttpClient client = new HttpClient();

        // GET: Math
        public ActionResult MathCompute()
        {
            return View();
        }

        [HttpPost]
        // 只要自動繫結模型中的Num1及Num2
        //public ActionResult MathCompute([Bind(Include = "Num1, Num2")] MathViewModel mathdata, string btnCompute)
        // 不可自動繫結模型中的Result
        public ActionResult MathCompute([Bind(Exclude = "Result")] MathViewModel mathdata, string btnCompute)
        {
            string urlstring, op, result;

            if (!ModelState.IsValid)
            {
                return View(mathdata);
            }
            try
            {
                switch (btnCompute)
                {
                    case "加":
                        op = "add";  
                        break;
                    case "減":
                        op = "substract";
                        break;
                    case "乘":
                        op = "multiply";
                        break;
                    case "除":
                        op = "divide";
                        break;
                    default:
                        op = "add";
                        break;
                }
                urlstring = "http://140.137.41.136:5558/MathLotteryWebAPIs/api/math/" + op + "/" + mathdata.Num1 + "/" + mathdata.Num2;
                client.BaseAddress = new Uri(urlstring);
                // Get data response.
                HttpResponseMessage response = client.GetAsync("").Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result = response.StatusCode + response.ReasonPhrase;
                }

                mathdata.Result = result;
                return View(mathdata);
            }
            catch (Exception ex)
            {
                result = "數學運算時發生例外，原因如下: " + ex.Message;
                mathdata.Result = result;
                return View(mathdata);
            }

        }
    }
}