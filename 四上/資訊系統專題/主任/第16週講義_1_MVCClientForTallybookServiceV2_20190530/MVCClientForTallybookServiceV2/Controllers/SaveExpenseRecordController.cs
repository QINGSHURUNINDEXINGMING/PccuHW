using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVCClientForTallybookServiceV2.ViewModels;
using System.Net.Http; // needed for using the HttpClient class
using System.Net.Http.Headers;
using System.Threading.Tasks;
//
using Newtonsoft.Json;      // needed for using the JsonConvert class
using Newtonsoft.Json.Linq; // needed for using the JObject class
using System.Text;

namespace MVCClientForTallybookServiceV2.Controllers
{
    public class SaveExpenseRecordController : Controller
    {
        // 建立呼叫Web API的HttpClient物件
        HttpClient client = new HttpClient();
        List<SelectListItem> expensetypeSelectItemList;

        // GET: Tallybooks
        public ActionResult Index()
        {
            string result = "", urlstring = "";
            bool flag = true;
            
            try
            {
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes";
                // 以非同步GET方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response = client.GetAsync(urlstring).Result;
                // 以非同步方式取出回傳結果之JSON格式字串
                result = response.Content.ReadAsStringAsync().Result;
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject = JsonConvert.DeserializeObject(result);
                // 建立空的資料型態選項清單物件
                expensetypeSelectItemList = new List<SelectListItem>();
                // 利用迴圈逐一讀取每一筆紀錄，取出JSON物件中名稱為expenseType的值，
                // 存入expensetypeSelectItemList物件中
                foreach (var record in jsonObject) // 
                {
                    // 將第1筆消費種類設為顯示項
                    if (record.id == "1")
                        flag = false;    
                    // 將每一筆紀錄的expenseType欄位值取出來加到下拉式選單中
                    expensetypeSelectItemList.Add(new SelectListItem()
                    {
                        Text = record.expenseType,
                        Value = record.expenseType,
                        Selected = flag
                    });
                }
                // 將消費種類清單儲存於ViewBag物件中，以便從View中取出顯示
                ViewBag.ExpenseTypeList = expensetypeSelectItemList;
            }
            catch (Exception ex)
            {
                // 建立空的資料型態選項清單物件
                expensetypeSelectItemList = new List<SelectListItem>();
                // 建立一筆以錯誤訊息為內容的選擇項目，以傳到View顯示
                expensetypeSelectItemList.Add(new SelectListItem()
                {
                    Text = ex.Message,
                    Value = ex.Message,
                    Selected = true
                });
                // 將消費種類清單儲存於ViewBag物件中，以便從View中取出顯示
                ViewBag.ExpenseTypeList = expensetypeSelectItemList;
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "price, expenseType, comment, payDate")] SaveExpenseRecordViewModel expenseRecordData)
        {
            string result = "", urlstring = "";
            bool flag = true;
            // 假如模型資料繫結錯誤，則以原來物件回傳到View顯示
            if (!ModelState.IsValid)
            {
                return View(expenseRecordData);
            }

            try
            {
                // 以下進行將消費紀錄存入資料庫中
                string payDateString = expenseRecordData.payDate.Year + "-" + expenseRecordData.payDate.Month + "-" + expenseRecordData.payDate.Day;
                // 雲端記帳簿服務V2之網址
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/Tallybooks";
                // 將tallybookdata物件序列化成JSON格式字串
                dynamic jsonString = JsonConvert.SerializeObject(expenseRecordData);
                // 建立傳遞內容HttpContent物件
                HttpContent contentPost = new StringContent(jsonString, Encoding.UTF8, "application/json");
                // 以非同步POST方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response2 = await client.PostAsync(urlstring, contentPost);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response2.Content.ReadAsStringAsync(); 
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject2 = JsonConvert.DeserializeObject(result);
                // 從JSON物件中名稱為TallybookResult的值，存入querydata物件的result欄位
                expenseRecordData.result = jsonObject2.TallybookAPIResult;
                // 將消費金額與消費說明欄位清空
                expenseRecordData.price = 0;
                expenseRecordData.comment = "";
                expenseRecordData.payDate = DateTime.Now.Date;

                //==== 重新從資料庫中取出消費種類清單 ====
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes";
                // 以非同步GET方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response1 = client.GetAsync(urlstring).Result;
                // 以非同步方式取出回傳結果之JSON格式字串
                result = response1.Content.ReadAsStringAsync().Result;
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject1 = JsonConvert.DeserializeObject(result);
                // 建立空的資料型態選項清單物件
                expensetypeSelectItemList = new List<SelectListItem>();
                // 利用迴圈逐一讀取每一筆紀錄，取出JSON物件中名稱為expenseType的值，
                // 存入expensetypeSelectItemList物件中
                foreach (var record in jsonObject1)
                {
                    // 將第1筆消費種類設為顯示項
                    if (record.id == "1")
                        flag = false;
                    // 將每一筆紀錄的expenseType欄位值取出來加到下拉式選單中
                    expensetypeSelectItemList.Add(new SelectListItem()
                    {
                        Text = record.expenseType,
                        Value = record.expenseType,
                        Selected = flag
                    });
                }
                // 將消費種類清單儲存於ViewBag物件中，以便從View中取出顯示
                ViewBag.ExpenseTypeList = expensetypeSelectItemList;
            }
            catch (Exception ex)
            {
                // 建立空的資料型態選項清單物件
                expensetypeSelectItemList = new List<SelectListItem>();
                // 建立一筆以錯誤訊息為內容的選擇項目，以傳到View顯示
                expensetypeSelectItemList.Add(new SelectListItem()
                {
                    Text = ex.Message,
                    Value = ex.Message,
                    Selected = true
                });
                // 將消費種類清單儲存於ViewBag物件中，以便從View中取出顯示
                ViewBag.ExpenseTypeList = expensetypeSelectItemList;
                // 從呼叫消費資料儲存服務時發生例外之訊息存入expenseRecordData物件的result欄位
                result = "呼叫消費資料儲存服務時發生例外，原因如下: " + ex.Message;
                expenseRecordData.result = result;

                // 將消費金額與消費說明欄位清空
                expenseRecordData.price = 0;
                expenseRecordData.comment = "";
                expenseRecordData.payDate = DateTime.Now.Date;
            }

            // 清除原先的資料繫結
            ModelState.Clear();
            // 將結果物件tallybookdata傳到View顯示
            return View(expenseRecordData);
        }
    }
}




