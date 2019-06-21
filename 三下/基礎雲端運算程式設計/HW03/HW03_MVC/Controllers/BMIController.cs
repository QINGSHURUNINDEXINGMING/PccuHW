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
    public class BMIController : Controller
    {
        private HttpClient client;

        // GET: BMI
        public ActionResult BMICompute()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> BMICompute([Bind(Exclude = "Result")] BMIViewModel bmidata)
        {
            string result = "", urlstring = "";
            if (!ModelState.IsValid)
            {
                return View(bmidata);
            }
            try
            {
                int type = (bmidata.Type == "大樂透") ? 0 : 1;
                urlstring = "http://140.137.41.136:5558/a1234567/MathLotteryWebAPIs/api/lottery/" + type + "/" + bmidata.Sets;
                //建立HttClient物件
                client = new HttpClient();
                // 以非同步GET方式呼叫樂透服務之API
                HttpResponseMessage response = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject = JsonConvert.DeserializeObject(result);
                // 從JSON物件中名稱為LotteryResult的值，存入lotterydata物件的Result欄位
                lotterydata.Result = jsonObject.LotteryResult;
                // 將lotterydata物件傳送到View顯示
                return View(lotterydata);
            }
            catch (Exception ex)
            {
                result = "產生樂透號碼時發生例外，原因如下: " + ex.Message;
                lotterydata.Result = result;
                // 將lotterydata物件傳送到View顯示
                return View(lotterydata);
            }
        }

    }
}