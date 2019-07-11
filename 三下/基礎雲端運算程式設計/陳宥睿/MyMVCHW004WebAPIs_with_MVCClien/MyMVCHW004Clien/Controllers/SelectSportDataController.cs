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
using MyMVCHW004Clien.ViewModels;

namespace MyMVCHW004Clien.Controllers
{
	public class SelectSportDataController : Controller
	{
		// 建立呼叫Web API的HttpClient物件
		private HttpClient client = new HttpClient();

		// GET: QueryExpenseData
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index([Bind(Include = "startDate, endDate, type")] SelectSportDataViewModel querydata)
		{
			string result = "", urlstring = "";
			// 若資料繫結有錯誤，立刻結束此方法，回到View
			if (!ModelState.IsValid)
			{
				return View(querydata);
			}

			try
			{
				string startDateString = querydata.startDate.Year + "-" + querydata.startDate.Month + "-" + querydata.startDate.Day;
				string endDateString = querydata.endDate.Year + "-" + querydata.endDate.Month + "-" + querydata.endDate.Day;
				urlstring = "http://140.137.41.136:5558/a5420876/MyMVCHW004WebAPIs/api/Sports/" + startDateString + "/" + endDateString + "/" + querydata.type;
				// 以非同步GET方式呼叫雲端運動紀錄簿服務之API
				HttpResponseMessage response = await client.GetAsync(urlstring);
				// 以非同步方式取出回傳結果之JSON格式字串
				result = await response.Content.ReadAsStringAsync();
				// 將JSON格式字串轉換成.NET的JSON物件
				dynamic jsonObject = JsonConvert.DeserializeObject(result);
				// 從JSON物件中名稱為SportResult的值，存入querydata物件的result欄位
				querydata.result = jsonObject.SportAPIResult;

				// 將結果物件querydata傳到View顯示
				return View(querydata);
			}
			catch (Exception ex)
			{
				result = "呼叫運動資料儲存服務時發生例外，原因如下: " + ex.Message;
				querydata.result = result;
				return View(querydata);
			}
		}
	}
}