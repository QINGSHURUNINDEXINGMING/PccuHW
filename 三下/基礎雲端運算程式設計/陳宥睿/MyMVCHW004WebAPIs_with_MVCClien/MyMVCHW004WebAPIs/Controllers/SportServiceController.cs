using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MyMVCHW004WebAPIs.Models;

namespace MyMVCHW004WebAPIs.Controllers
{
	public class SportServiceController : ApiController
	{
		#region

		// 用來體育管理服務結果資料的內部類別，
		// 每一個欄位變數名稱將是服務回傳JSON物件的Key
		private class SportServiceResult
		{
			// 儲存體育管理服務結果之狀態
			public string Status { get; set; }

			// 儲存體育管理服務之運算結果
			public string SportAPIResult { get; set; }
		}

		// 宣告體育管理服務結果物件變數： SportServiceResultObj，用來儲存體育管理服務的回傳結果
		private SportServiceResult SportServiceResultObj;

		// 建立雲端體育管理服務資料庫的物件，可以存取 SportService的Talltbooks及SportTypes資料表
		private Model1 db = new Model1();

		#endregion

		#region

		//====  SportServiceController類別的建構子(Constructor)，用於建立初始的運動種類資料表 ===
		// 若是第一次建立該資料表，便會存入10種預設的運動種類供使用者選擇
		public SportServiceController()
		{
			try
			{
				// 預設的初始運動種類陣列
				string[] initialSportTypes = { "籃球", "棒球", "壘球", "排球", "羽球", "網球", "慢跑", "拳擊", "健身", "田徑" };
				//建立運動種類紀錄物件
				SportType sportType = new SportType();
				//建立sportType型態的列舉物件(類似資料表)，可儲存運動種類紀錄
				IEnumerable<SportType> sportTypes = db.SportTypes;

				int rowcount = sportTypes.Count<SportType>();     // 計算資料表資料筆數
				if (rowcount == 0) //若沒有任何運動種類，則利用迴圈方式，將初始運動種類逐一存到SportTypes資料表中
				{
					for (int i = 0; i < initialSportTypes.Length; i++)
					{
						sportType.sportType = initialSportTypes[i];
						db.SportTypes.Add(sportType);
						db.SaveChanges();
					}
				}
			}
			catch (Exception ex) // 僅用於防止程式因為發生例外而停止運作，沒有回傳值
			{
				string str = ex.Message;
			}
		}

		#endregion

		#region

		//================ 新增一筆體育管理之Web API ===================================
		// 路由資訊：動詞：POST，路徑：api/ Sports
		// Web API之輸入參數： Sport物件
		[HttpPost]
		[Route("api/Sports")]
		public async Task<IHttpActionResult> PostSport(Sport Sport)
		{
			// 建立體育管理服務結果資料物件
			SportServiceResultObj = new SportServiceResult();

			try
			{
				db.Sports.Add(Sport);
				await db.SaveChangesAsync();
				string result = "已成功儲存一筆運動紀錄!";

				//=== 並將體育管理服務之運算結果存入體育管理服務結果資料物件對應的欄位 ====
				SportServiceResultObj.Status = "OK";
				SportServiceResultObj.SportAPIResult = result;

				// 回體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
			catch (Exception ex)
			{
				//===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
				SportServiceResultObj.Status = "Exception";
				SportServiceResultObj.SportAPIResult = "新增體育管理時發生例外，原因如下： " + ex.Message;

				// 回傳體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
		}

		#endregion

		#region

		//============================== 查詢運動紀錄之Web API ==================================================
		//  依照不同查詢模式(依照運動日期：querymode=0; 依照運動種類:querymode=1)查詢起訖日期的運動資料之方法
		// 路由資訊：動詞：GET，路徑：api/ Sports/{startDate}/{endDate}/{querymode}
		// Web API之輸入參數：起始日期{startDate}；結束日期{endDate}；查詢模式{querymode}
		[HttpGet]
		[Route("api/Sports/{startDate}/{endDate}/{querymode}")]
		public IHttpActionResult GetSports(string startDate, string endDate, int querymode)
		{
			// 建立體育管理服務結果資料物件
			SportServiceResultObj = new SportServiceResult();

			// 將起訖日期字串轉成DateTime物件
			DateTime sDate = Convert.ToDateTime(startDate);
			DateTime eDate = Convert.ToDateTime(endDate);

			// 宣告區域變數
			string str;     //用於紀錄查詢結果字串
			int totalAmount; // 用於紀錄運動總時數
			int rowCount;   // 用於儲存紀錄數
			int typeSum;   // 用於儲存個別運動種類的運動總時數

			if (querymode == 0)  // querymode==0，按照運動日期查詢
			{
				try
				{
					str = "";
					totalAmount = 0;
					// 使用LINQ語法查詢起訖日期間的運動紀錄，並依照運動日期排序
					var result = from a in db.Sports
								 where ((a.sportDate >= sDate) && (a.sportDate <= eDate))
								 orderby a.sportDate
								 select a;
					rowCount = result.Count(); // 取得記錄數量
					str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "共有" + rowCount + "筆運動紀錄:\n";

					foreach (var record in result) // 利用迴圈逐一讀取每一筆紀錄
					{
						totalAmount += record.sportTime; //讀取sportTime欄位(即運動小時sportTime)，並加總到運動總時數中
					}
					str += "共計運動 " + totalAmount + " 小時\n"; // 將運動總時數串接到顯示字串(str)中

					// 顯示運動紀錄之每一個欄位之抬頭，將每一個欄位的抬頭串接到顯示字串(str)中
					string[] colNames = { "編號", "運動時數", "運動類別", "運動日期", "運動地點" };
					foreach (var name in colNames)
					{
						str += string.Format("{0}    ", name);
					}

					str += "\n";

					// 利用迴圈逐一讀取每一筆紀錄
					int i = 0;
					foreach (var record in result)
					{
						str += string.Format("{0:d4}  ", (i + 1)); // 串接記錄編號(索引值+1)
						str += string.Format("{0,8}   ", record.sportTime);       // 串接sportTime欄位值(即運動小時sportTime)
						str += string.Format("{0,6}     ", record.sportType);       // 串接SportType欄位值(即運動種類SportType)
						str += string.Format("{0,10}    ", record.sportDate.Date.ToString("d"));  // 串接sportDate欄位值(即運動日期sportDate之日期)
						str += string.Format("{0,-8}", record.location);     // 串接location欄位值(即運動說明location)
						str += "\n";
						i++;
					}

					//=== 並將體育管理服務之運算結果存入體育管理服務結果資料物件對應的欄位 ====
					SportServiceResultObj.Status = "OK";
					SportServiceResultObj.SportAPIResult = str;

					// 回體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
					// 分別自動地將物件序列化(Serialize)成JSON或XML字串
					return Ok(SportServiceResultObj);
				}
				catch (Exception ex)
				{
					//===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
					SportServiceResultObj.Status = "Exception";
					SportServiceResultObj.SportAPIResult = "查詢運動紀錄時發生例外，原因如下： " + ex.Message;

					// 回傳體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
					// 分別自動地將物件序列化(Serialize)成JSON或XML字串
					return Ok(SportServiceResultObj);
				}
			}
			else if (querymode == 1)  // querymode==1，按照消類種類查詢
			{
				str = "";
				totalAmount = 0;

				// 使用LINQ語法查詢起訖日期間的運動紀錄，並依照運動種類群組，每種運動種類只取出第1筆紀錄
				var result = from a in db.Sports
							 where ((a.sportDate >= sDate) && (a.sportDate <= eDate))
							 group a by a.sportType into g
							 select g.FirstOrDefault();

				var records = result.ToArray<Sport>(); // 將結果轉換成運動紀錄陣列

				//
				rowCount = result.Count(); // 取得紀錄數
				if (rowCount == 0)  // 若記錄數為0，則回傳沒有運動紀錄之訊息
				{
					str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "並沒有運動紀錄";
					//===== 並將體育管理服務之運算結果存入體育管理服務結果資料物件對應的欄位 =====
					SportServiceResultObj.Status = "No Record";
					SportServiceResultObj.SportAPIResult = str;

					// 回體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
					// 分別自動地將物件序列化(Serialize)成JSON或XML字串
					return Ok(SportServiceResultObj);
				}
				else //
				{
					try
					{
						str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "之運動統計如下:\n";
						foreach (var record in records) // 逐一取出運動資料的每一筆紀錄record
						{
							// 使用LINQ語法查詢起訖日期間運動種類與record運動種類相同的所有運動紀錄
							// use LINQ to query data
							var result1 = (from b in db.Sports
										   where ((b.sportDate >= sDate) && (b.sportDate <= eDate) && (b.sportType == record.sportType))
										   select b);

							// 計算該運動種類的總時數
							typeSum = 0;
							foreach (var record1 in result1)
							{
								typeSum += record1.sportTime;
							}
							str += string.Format("{0}: {1}小時\n", record.sportType, typeSum); //格式化串接該運動種類之總時數
							totalAmount += typeSum; //  累加運動總小時
						}
						str += "運動時數總計" + totalAmount + "小時\n";
						//===== 並將體育管理服務之運算結果存入體育管理服務結果資料物件對應的欄位 =====
						SportServiceResultObj.Status = "OK";
						SportServiceResultObj.SportAPIResult = str;

						// 回體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
						// 分別自動地將物件序列化(Serialize)成JSON或XML字串
						return Ok(SportServiceResultObj);
					}
					catch (Exception ex)
					{
						//===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
						SportServiceResultObj.Status = "Exception";
						SportServiceResultObj.SportAPIResult = "查詢運動紀錄時發生例外，原因如下： " + ex.Message;

						// 回傳體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
						// 分別自動地將物件序列化(Serialize)成JSON或XML字串
						return Ok(SportServiceResultObj);
					}
				}
			}
			else
			{
				str = "無效的查詢";
				//===== 並將體育管理服務之運算結果存入體育管理服務結果資料物件對應的欄位 =====
				SportServiceResultObj.Status = "OK";
				SportServiceResultObj.SportAPIResult = str;

				// 回體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
		}

		#endregion

		#region

		//======================= 刪除起訖日期間的運動紀錄之Web API =========================================
		// 路由資訊：動詞：GET，路徑：api/ Sports/delete/{startDate}/{endDate}
		// Web API之輸入參數：起始日期{startDate}；結束日期{endDate}
		[HttpGet]
		[Route("api/Sports/delete/{startDate}/{endDate}")]
		public async Task<IHttpActionResult> DeleteSports(string startDate, string endDate)
		{
			// 建立體育管理服務結果資料物件
			SportServiceResultObj = new SportServiceResult();

			// 將起訖日期字串轉成DateTime物件
			DateTime sDate = Convert.ToDateTime(startDate);
			DateTime eDate = Convert.ToDateTime(endDate);
			string str = "";

			try
			{
				// 使用 LINQ 取出起訖日期間的運動資料紀錄 (依照運動日期排序)
				var result = from a in db.Sports
							 where ((a.sportDate >= sDate) && (a.sportDate <= eDate))
							 orderby a.sportDate
							 select a;
				int count = result.Count(); // 取得紀錄數
				if (count == 0)  // 若記錄數為0，則回傳沒有運動紀錄之訊息
				{
					str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "沒有運動紀錄:!";

					//===== 並將體育管理服務之運算結果存入體育管理服務結果資料物件對應的欄位 =====
					SportServiceResultObj.Status = "OK";
					SportServiceResultObj.SportAPIResult = str;

					// 回傳體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
					// 分別自動地將物件序列化(Serialize)成JSON或XML字串
					return Ok(SportServiceResultObj);
				}

				db.Sports.RemoveRange(result); // 從db物件刪除result中的運動資料紀錄
				await db.SaveChangesAsync();       // 更新資料庫
				str = "已經成功刪除" + count + "筆紀錄!";

				//===== 將體育管理服務之運算結果存入體育管理服務結果資料物件對應的欄位 =====
				SportServiceResultObj.Status = "OK";
				SportServiceResultObj.SportAPIResult = str;

				// 回傳體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
			catch (Exception ex)
			{
				//===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
				SportServiceResultObj.Status = "Exception";
				SportServiceResultObj.SportAPIResult = "刪除運動紀錄時發生例外，原因如下： " + ex.Message;

				// 回傳體育管理服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
		}

		#endregion

		#region

		//===== 從SportTypes資料表中取出所有運動種類的Web API方法，回傳資料型態為sportType之列舉集合(陣列) ====
		// 路由資訊：動詞：GET，路徑：api/SportTypes
		[HttpGet]
		[Route("api/SportTypes")]
		public IEnumerable<SportType> GetSportTypes()
		{
			try
			{
				return db.SportTypes;
			}
			catch (Exception ex)
			{
				string str = ex.Message;
				return null;
			}
		}

		#endregion

		#region

		//===== 從SportTypes資料表中取出所有運動種類的Web API方法，回傳資料型態為所有運動種類之格式化字串 ====
		// 路由資訊：動詞：GET，路徑：api/SportTypes/string
		[HttpGet]
		[Route("api/SportTypes/string")]
		public IHttpActionResult GetSportTypesString()
		{
			// 建立體育管理服務結果資料物件
			SportServiceResultObj = new SportServiceResult();

			// 建立區域變數str，用於儲存運算結果
			string str = "";

			try
			{
				// 使用LINQ語法取出所有運動種類，並依照id排序
				var result = from a in db.SportTypes
							 orderby a.id
							 select a;

				// 利用迴圈逐一讀取每一筆紀錄
				var i = 0;
				foreach (var record in result)
				{
					str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
					str += string.Format("{0}", record.sportType);       // 串接運動種類欄位值
					str += "\n";
					i++;
				}

				//===== 並將運動種類服務之運算結果存入運動種類服務結果資料物件對應的欄位 =====
				SportServiceResultObj.Status = "OK";
				SportServiceResultObj.SportAPIResult = str;

				// 回傳運動種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
			catch (Exception ex)
			{
				//===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
				SportServiceResultObj.Status = "Exception";
				SportServiceResultObj.SportAPIResult = "取出運動種類字串時發生例外，原因如下： " + ex.Message;

				// 回傳運動種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
		}

		#endregion

		#region

		//===== 新增一筆運動種類之Web API，回傳包含更新後之所有運動種類格式化字串之物件 ====
		// 路由資訊：動詞：POST，路徑：api/SportTypes
		// Web API之輸入參數：sportType物件
		[HttpPost]
		[Route("api/SportTypes")]
		public IHttpActionResult PostsportType(SportType sportType)
		{
			// 建立體育管理服務結果資料物件
			SportServiceResultObj = new SportServiceResult();

			// 建立區域變數str，用於儲存運算結果
			string str = "";

			try
			{
				// 檢驗擬新增的運動種類是否已存在
				var result = db.SportTypes.Where(x => x.sportType == sportType.sportType);
				int count = result.Count(); // 取得記錄數
				if (count == 0) // 如果記錄數為0，表示該新的運動種類不存在，則可將其新增至SportTypes資料表中
				{
					db.SportTypes.Add(sportType); // 新增運動種類
					db.SaveChanges();                 // 更新至資料庫

					// 利用LINQ語法取出所有的運動種類紀錄
					var result1 = from a in db.SportTypes
								  orderby a.id
								  select a;

					// 利用迴圈逐一讀取每一筆紀錄，並加入編號說明
					var i = 0;
					foreach (var record in result1)
					{
						str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
						str += string.Format("{0}", record.sportType);       // 串接運動種類欄位值
						str += "\n";
						i++;
					}
					//===== 並將運動種類服務之運算結果存入運動種類服務結果資料物件對應的欄位 =====
					SportServiceResultObj.Status = "OK";
					SportServiceResultObj.SportAPIResult = str;
				}
				else
				{
					str = "運動種類：【" + sportType.sportType + "】已經存在，請重新輸入!";
					//===== 並將運動種類已存在之訊息存入運動種類服務結果資料物件對應的欄位 =====
					SportServiceResultObj.Status = "Exist";
					SportServiceResultObj.SportAPIResult = str;
				}

				// 回傳運動種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
			catch (Exception ex)
			{
				//===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
				SportServiceResultObj.Status = "Exception";
				SportServiceResultObj.SportAPIResult = "儲存運動種類時發生例外，原因如下： " + ex.Message;

				// 回傳運動種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
		}

		#endregion

		#region

		//============ 刪除指定編號的運動種類之Web API =============
		// 路由資訊：動詞：GET，路徑：api/SportTypes/delete/{no}
		[HttpGet]
		[Route("api/SportTypes/delete/{no}")]
		public IHttpActionResult DeletesportType(int no)
		{
			// 建立體育管理服務結果資料物件
			SportServiceResultObj = new SportServiceResult();

			// 建立區域變數str，用於儲存運算結果
			string str = "";

			try
			{
				// 利用LINQ語法取出所有的運動種類紀錄
				var result = from a in db.SportTypes
							 orderby a.id
							 select a;

				int count = result.Count(); // 取出記錄數

				// 輸入的編號不在範圍內，則回傳提示訊息
				if ((no < 1) || (no > count))
				{
					str = "輸入的編號不在範圍內，請重新輸入!";
					//===== 並將輸入的編號不在範圍內之訊息存入記運動種類服務結果資料物件對應的欄位 =====
					SportServiceResultObj.Status = "Out of Range";
					SportServiceResultObj.SportAPIResult = str;
				}
				else
				{
					var record = result.ToArray<SportType>()[no - 1]; //找出指定編號那筆紀錄
					db.SportTypes.Remove(record); // //將指定編號那筆紀錄刪除
					db.SaveChanges(); // 更新資料庫

					// 取出最新所有的運動種類
					var result1 = from a in db.SportTypes
								  orderby a.id
								  select a;

					// 利用迴圈逐一讀取每一筆紀錄
					var i = 0;
					foreach (var record1 in result1)
					{
						str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
						str += string.Format("{0}", record1.sportType);       // 串接運動種類欄位值
						str += "\n";
						i++;
					}
					//===== 並將運動種類服務之運算結果存入記運動種類服務結果資料物件對應的欄位 =====
					SportServiceResultObj.Status = "OK";
					SportServiceResultObj.SportAPIResult = str;
				}
				// 回傳運動種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
			catch (Exception ex)
			{
				//===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
				SportServiceResultObj.Status = "Exception";
				SportServiceResultObj.SportAPIResult = "刪除運動種類時發生例外，原因如下： " + ex.Message;

				// 回傳運動種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
				// 分別自動地將物件序列化(Serialize)成JSON或XML字串
				return Ok(SportServiceResultObj);
			}
		}

		#endregion
	}
}