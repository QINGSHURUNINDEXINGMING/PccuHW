using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MyMVCHW004Clien.ViewModels;
using System.Net.Http; // needed for using the HttpClient class
using System.Net.Http.Headers;
using System.Threading.Tasks;

//
using Newtonsoft.Json;      // needed for using the JsonConvert class
using Newtonsoft.Json.Linq; // needed for using the JObject class
using System.Text;

namespace MyMVCHW004Clien.Controllers
{
	public class SaveSportRecordController : Controller
	{
		// 建立呼叫Web API的HttpClient物件
		private HttpClient client = new HttpClient();

		private List<SelectListItem> sporttypeSelectItemList;

		// GET: sports
		public ActionResult Index()
		{
			string result = "", urlstring = "";
			bool flag = true;

			try
			{
				urlstring = "http://140.137.41.136:5558/a5420876/MyMVCHW004WebAPIs/api/SportTypes";
				// 以非同步GET方式呼叫運動紀錄簿服務之API
				HttpResponseMessage response = client.GetAsync(urlstring).Result;
				// 以非同步方式取出回傳結果之JSON格式字串
				result = response.Content.ReadAsStringAsync().Result;
				// 將JSON格式字串轉換成.NET的JSON物件
				dynamic jsonObject = JsonConvert.DeserializeObject(result);
				// 建立空的資料型態選項清單物件
				sporttypeSelectItemList = new List<SelectListItem>();
				// 利用迴圈逐一讀取每一筆紀錄，取出JSON物件中名稱為sportType的值，
				// 存入sporttypeSelectItemList物件中
				foreach (var record in jsonObject) //
				{
					// 將第1筆運動種類設為顯示項
					if (record.id == "1")
						flag = false;
					// 將每一筆紀錄的sportType欄位值取出來加到下拉式選單中
					sporttypeSelectItemList.Add(new SelectListItem()
					{
						Text = record.sportType,
						Value = record.sportType,
						Selected = flag
					});
				}
				// 將運動種類清單儲存於ViewBag物件中，以便從View中取出顯示
				ViewBag.sportTypeList = sporttypeSelectItemList;
			}
			catch (Exception ex)
			{
				// 建立空的資料型態選項清單物件
				sporttypeSelectItemList = new List<SelectListItem>();
				// 建立一筆以錯誤訊息為內容的選擇項目，以傳到View顯示
				sporttypeSelectItemList.Add(new SelectListItem()
				{
					Text = ex.Message,
					Value = ex.Message,
					Selected = true
				});
				// 將運動種類清單儲存於ViewBag物件中，以便從View中取出顯示
				ViewBag.sportTypeList = sporttypeSelectItemList;
			}

			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index([Bind(Include = "sportTime, sportType, location, sportDate")] SaveSportRecordViewModel sportRecordData)
		{
			string result = "", urlstring = "";
			bool flag = true;
			// 假如模型資料繫結錯誤，則以原來物件回傳到View顯示
			if (!ModelState.IsValid)
			{
				return View(sportRecordData);
			}

			try
			{
				// 以下進行將運動紀錄存入資料庫中
				string sportDateString = sportRecordData.sportDate.Year + "-" + sportRecordData.sportDate.Month + "-" + sportRecordData.sportDate.Day;
				// 運動紀錄簿服務V2之網址
				urlstring = "http://140.137.41.136:5558/a5420876/MyMVCHW004WebAPIs/api/Sports";
				// 將sportdata物件序列化成JSON格式字串
				dynamic jsonString = JsonConvert.SerializeObject(sportRecordData);
				// 建立傳遞內容HttpContent物件
				HttpContent contentPost = new StringContent(jsonString, Encoding.UTF8, "application/json");
				// 以非同步POST方式呼叫運動紀錄簿服務之API
				HttpResponseMessage response2 = await client.PostAsync(urlstring, contentPost);
				// 以非同步方式取出回傳結果之JSON格式字串
				result = await response2.Content.ReadAsStringAsync();
				// 將JSON格式字串轉換成.NET的JSON物件
				dynamic jsonObject2 = JsonConvert.DeserializeObject(result);
				// 從JSON物件中名稱為sportResult的值，存入querydata物件的result欄位
				sportRecordData.result = jsonObject2.sportAPIResult;
				// 將運動時數與運動地點欄位清空
				sportRecordData.sportTime = 0;
				sportRecordData.location = "";
				sportRecordData.sportDate = DateTime.Now.Date;

				//==== 重新從資料庫中取出運動種類清單 ====
				urlstring = "http://140.137.41.136:5558/a5420876/MyMVCHW004WebAPIs/api/SportTypes";
				// 以非同步GET方式呼叫運動紀錄簿服務之API
				HttpResponseMessage response1 = client.GetAsync(urlstring).Result;
				// 以非同步方式取出回傳結果之JSON格式字串
				result = response1.Content.ReadAsStringAsync().Result;
				// 將JSON格式字串轉換成.NET的JSON物件
				dynamic jsonObject1 = JsonConvert.DeserializeObject(result);
				// 建立空的資料型態選項清單物件
				sporttypeSelectItemList = new List<SelectListItem>();
				// 利用迴圈逐一讀取每一筆紀錄，取出JSON物件中名稱為sportType的值，
				// 存入sporttypeSelectItemList物件中
				foreach (var record in jsonObject1)
				{
					// 將第1筆運動種類設為顯示項
					if (record.id == "1")
						flag = false;
					// 將每一筆紀錄的sportType欄位值取出來加到下拉式選單中
					sporttypeSelectItemList.Add(new SelectListItem()
					{
						Text = record.sportType,
						Value = record.sportType,
						Selected = flag
					});
				}
				// 將運動種類清單儲存於ViewBag物件中，以便從View中取出顯示
				ViewBag.sportTypeList = sporttypeSelectItemList;
			}
			catch (Exception ex)
			{
				// 建立空的資料型態選項清單物件
				sporttypeSelectItemList = new List<SelectListItem>();
				// 建立一筆以錯誤訊息為內容的選擇項目，以傳到View顯示
				sporttypeSelectItemList.Add(new SelectListItem()
				{
					Text = ex.Message,
					Value = ex.Message,
					Selected = true
				});
				// 將運動種類清單儲存於ViewBag物件中，以便從View中取出顯示
				ViewBag.sportTypeList = sporttypeSelectItemList;
				// 從呼叫運動資料儲存服務時發生例外之訊息存入sportRecordData物件的result欄位
				result = "呼叫運動資料儲存服務時發生例外，原因如下: " + ex.Message;
				sportRecordData.result = result;

				// 將運動時數與運動地點欄位清空
				sportRecordData.sportTime = 0;
				sportRecordData.location = "";
				sportRecordData.sportDate = DateTime.Now.Date;
			}

			// 清除原先的資料繫結
			ModelState.Clear();
			// 將結果物件sportdata傳到View顯示
			return View(sportRecordData);
		}
	}
}