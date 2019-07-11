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
	public class DeleteSportDataController : Controller
	{
		// 建立呼叫Web API的HttpClient物件
		private HttpClient client = new HttpClient();

		// GET: QueryExpenseData
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index([Bind(Include = "startDate, endDate")] DeleteSportDataViewModel deletedata)
		{
			string result = "", urlstring = "";

			try
			{
				string startDateString = deletedata.startDate.Year + "-" + deletedata.startDate.Month + "-" + deletedata.startDate.Day;
				string endDateString = deletedata.endDate.Year + "-" + deletedata.endDate.Month + "-" + deletedata.endDate.Day;
				urlstring = "http://140.137.41.136:5558/a5420876/MyMVCHW004WebAPIs/api/Sports/delete/" + startDateString + "/" + endDateString;
				// 以非同步GET方式呼叫運動紀錄簿服務之API
				HttpResponseMessage response = await client.GetAsync(urlstring);
				// 以非同步方式取出回傳結果之JSON格式字串
				result = await response.Content.ReadAsStringAsync();
				// 將JSON格式字串轉換成.NET的JSON物件
				dynamic jsonObject = JsonConvert.DeserializeObject(result);
				// 從JSON物件中名稱為TallybookResult的值，存入querydata物件的result欄位
				deletedata.result = jsonObject.SportAPIResult;

				// 將結果物件querydata傳到View顯示
				return View(deletedata);
			}
			catch (Exception ex)
			{
				result = "呼叫運動資料儲存服務時發生例外，原因如下: " + ex.Message;
				deletedata.result = result;
				return View(deletedata);
			}
		}
	}
}