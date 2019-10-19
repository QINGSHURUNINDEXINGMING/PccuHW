using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Http; // needed for using the HttpClient class
using System.Net.Http.Headers;
using System.Threading.Tasks;
//
using Newtonsoft.Json;      // needed for using the JsonConvert class
using Newtonsoft.Json.Linq; // needed for using the JObject class
using System.Text;
using MVCClientForTallybookServiceV2.ViewModels;

namespace MVCClientForTallybookServiceV2.Controllers
{
    public class QueryExpenseDataController : Controller
    {
        // 建立呼叫Web API的HttpClient物件
        HttpClient client = new HttpClient();

        // GET: QueryExpenseData
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "startDate, endDate, type")] QueryExpenseDataViewModel querydata)
        {
            string result = "", urlstring="";
            // 若資料繫結有錯誤，立刻結束此方法，回到View
            if (!ModelState.IsValid)
            {
                return View(querydata);
            }

            try
            {
                string startDateString = querydata.startDate.Year + "-" + querydata.startDate.Month + "-" + querydata.startDate.Day;
                string endDateString = querydata.endDate.Year + "-" + querydata.endDate.Month + "-" + querydata.endDate.Day;
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/Tallybooks/" + startDateString + "/" + endDateString + "/" + querydata.type;
                // 以非同步GET方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response.Content.ReadAsStringAsync(); 
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject = JsonConvert.DeserializeObject(result);
                // 從JSON物件中名稱為TallybookResult的值，存入querydata物件的result欄位
                querydata.result = jsonObject.TallybookAPIResult;

                // 將結果物件querydata傳到View顯示
                return View(querydata);
            }
            catch (Exception ex)
            {
                result = "呼叫消費資料儲存服務時發生例外，原因如下: " + ex.Message;
                querydata.result = result;
                return View(querydata);
            }
        }
    }
}