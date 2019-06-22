using HW03_MVC.ViewModels;
using Newtonsoft.Json;
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
        public async Task<ActionResult> BMICompute([Bind(Exclude = "result")] BMIViewModel bmidata)
        {
            string result = "", urlstring = "";

            if (!ModelState.IsValid)
            {
                return View(bmidata);
            }
            try
            {
                urlstring = "http://140.137.41.136:5558/A6409001/HW03/api/BMI/" + bmidata.cm + " / "+ bmidata.kg;
                //建立HttClient物件
                client = new HttpClient();
                // 以非同步GET方式呼叫BMI服務之API
                HttpResponseMessage response = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject = JsonConvert.DeserializeObject(result);
                // 從JSON物件中名稱為BMIResult的值，存入bmidata物件的result欄位
                bmidata.result = jsonObject.BMIResult;
                // 將bmidata物件傳送到View顯示
                return View(bmidata);
            }
            catch (Exception ex)
            {
                result = "發生例外，原因如下: " + ex.Message;
                bmidata.result = result;
                // 將bmidata物件傳送到View顯示
                return View(bmidata);
            }
        }

        // POST: BMI
        public ActionResult BMIPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BMIPost([Bind(Exclude = "Result")] BMIViewModel bmidata)
        {
            string result = "", urlstring = "";
            if (!ModelState.IsValid)
            {
                return View(bmidata);
            }
            try
            {
                urlstring = "http://140.137.41.136:5558/a1234567/MathLotteryWebAPIs/api/lottery/" + type + "/" + bmidata.Sets;
                //建立HttClient物件
                client = new HttpClient();
                // 將lotterydata物件序列化成JSON格式字串
                dynamic jsonString = JsonConvert.SerializeObject(bmidata);
                // 建立傳遞內容HttpContent物件
                HttpContent contentPost = new StringContent(jsonString, Encoding.UTF8, "application/json");
                // 以非同步POST方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response = await client.PostAsync(urlstring, contentPost);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject = JsonConvert.DeserializeObject(result);
                // 從JSON物件中名稱為LotteryResult的值，存入lotterydata物件的Result欄位
                bmidata.result = jsonObject.LotteryResult;
                // 將lotterydata物件傳送到View顯示
                return View(bmidata);
            }
            catch (Exception ex)
            {
                result = "產生樂透號碼時發生例外，原因如下: " + ex.Message;
                bmidata.result = result;
                // 將lotterydata物件傳送到View顯示
                return View(bmidata);
            }
        }
    }
}