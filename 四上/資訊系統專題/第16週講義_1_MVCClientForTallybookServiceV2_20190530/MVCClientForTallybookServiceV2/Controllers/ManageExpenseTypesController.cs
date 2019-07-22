using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    public class ManageExpenseTypesController : Controller
    {
        // 建立呼叫Web API的HttpClient物件
        HttpClient client;
        
        public async Task<ActionResult> Index()
        {
            string result = "", urlstring = "";

            try
            {
                //=== 以下呼叫消費種類字串Web API，取出所有的消費種類字串
                // 建立呼叫Web API的HttpClient物件
                client = new HttpClient();
                // 消費種類格式化字串Web API之網址字串
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes/string";
                // 以非同步GET方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject = JsonConvert.DeserializeObject(result);
                // 從JSON物件中取出消費種類清單字串存於expensetypedata之物件的result屬性中
                TempData["Result"] = "目前的消費種類清單如下：\n" + jsonObject.TallybookAPIResult;
            }
            catch (Exception ex)
            {
                // 將例外訊息字串存於expensetypedata之物件的result屬性中，以便從View中取出顯示
                TempData["Result"] = "呼叫消費種類字串服務時發生例外，原因如下：" + ex.Message;
            }

            return View();
        }

        #region 新增一筆消費種類
        // 載入新增一筆消費種類的部分頁面
        public ActionResult Insert()
        {
            //因此頁面用於載入至開始頁面中，故使用部分檢視回傳
            return PartialView();
        }

        // 新增一筆消費種類時的Action
        // 使用Bind的Include來定義只接受的欄位，用來避免傳入其他不相干值
        [HttpPost]
        public async Task<ActionResult> Insert([Bind(Include = "newExpenseType")] ManageExpenseTypesViewModel expensetypedata)
        {
            //使用Service來新增一筆資料
            string result = "", urlstring = "", expenseTypeString = "";
            
            try
            {
                //=== 以下呼叫消費種類字串Web API，取出所有的消費種類字串，以便判斷擬新增的消費種類是否已經存在
                // 建立呼叫Web API的HttpClient物件
                client = new HttpClient();
                // 消費種類格式化字串Web API之網址字串
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes/string";
                // 以非同步GET方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject1 = JsonConvert.DeserializeObject(result);
                // 從JSON物件中取出消費種類清單字串存於 expenseTypeString變數中
                expenseTypeString = jsonObject1.TallybookAPIResult;
                // 假如擬新增的消費種類已經存在，則清除資料繫結，重新導向頁面至開始頁面
                if (expenseTypeString.Contains(expensetypedata.newExpenseType))
                {
                    // 清除原先的資料繫結
                    ModelState.Clear();
                    //重新導向頁面至開始頁面
                    return RedirectToAction("Index");
                }

                //=== 以下進行將擬新增的消費種類存入資料庫中 ===
                // 雲端記帳簿服務V2之網址
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes";
                // 將建立新增消費種類JSON格式字串
                string newExpenseTypeJsonString = "{'expenseType':" + "'" + expensetypedata.newExpenseType + "'}";
                // 建立傳遞內容HttpContent物件
                HttpContent contentPost = new StringContent(newExpenseTypeJsonString, Encoding.UTF8, "application/json");
                // 以非同步POST方式呼叫雲端記帳簿服務之API，以便把新增的消費種類儲存到資料庫中
                HttpResponseMessage response2 = await client.PostAsync(urlstring, contentPost);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response2.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject2 = JsonConvert.DeserializeObject(result);
                // 從JSON物件中名稱為Status的值，若不等於"OK"，表示儲存消費種類不成功，
                // 則清除資料繫結，重新導向頁面至開始頁面
                if (jsonObject2.Status != "OK")
                {
                    // 清除原先的資料繫結
                    ModelState.Clear();
                    //重新導向頁面至開始頁面
                    return RedirectToAction("Index");
                }

                //=== 以下再次呼叫消費種類字串Web API，以便取出更新後的所有消費種類字串
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes/string";
                // 以非同步GET方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response3 = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response3.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject3 = JsonConvert.DeserializeObject(result);
                // 從JSON物件中取出消費種類清單字串存於TempData物件之Result屬性中，以便從View中取出
                TempData["Result"] = "目前的消費種類清單如下：\n" + jsonObject3.TallybookAPIResult;
                //重新導向頁面至開始頁面
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // 清除原先的資料繫結
                ModelState.Clear();
                string str = ex.Message; // 此行只是讓ex有被使用到，沒有功用
                //重新導向頁面至開始頁面
                return RedirectToAction("Index");
            }
        }
        #endregion
        
        #region 刪除一筆消費種類
        // 載入刪除一筆消費種類的部分頁面
        public ActionResult Delete()
        {
            //因此頁面用於載入至開始頁面中，故使用部分檢視回傳
            return PartialView();
        }

        // 刪除一筆消費種類
        // 使用Bind的Include來定義只接受的欄位，用來避免傳入其他不相干值
        [HttpPost]
        public async Task<ActionResult> Delete([Bind(Include = "no")] ManageExpenseTypesViewModel expensetypedata)
        {
            //使用Service來新增一筆資料
            string result = "", urlstring = "";

            try
            {
                //=== 以下呼叫刪除消費種類字串Web API，以刪除標號所指的的消費種類
                // 建立呼叫Web API的HttpClient物件
                client = new HttpClient();
                // 消費種類格式化字串Web API之網址字串
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes/delete/" + expensetypedata.no;
                // 以非同步GET方式呼叫雲端記帳簿服務之API
                HttpResponseMessage response1 = await client.GetAsync(urlstring);
                // 以非同步方式取出回傳結果之JSON格式字串
                result = await response1.Content.ReadAsStringAsync();
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject1 = JsonConvert.DeserializeObject(result);
                // 從JSON物件中名稱為Status的值，若不等於"OK"，表示刪除指定的消費種類不成功，
                // 則清除資料繫結，重新導向頁面至開始頁面
                if (jsonObject1.Status != "OK")
                {
                    // 清除原先的資料繫結
                    ModelState.Clear();
                    //重新導向頁面至開始頁面
                    return RedirectToAction("Index");
                }

                //=== 以下再次呼叫消費種類字串Web API，以便取出更新後的所有消費種類字串
                urlstring = "http://140.137.41.136:5558/a1234567/tallybookservicev2/api/ExpenseTypes/string";
                // 以非同步GET方式呼叫刪除消費種類之Web API
                HttpResponseMessage response2 = client.GetAsync(urlstring).Result;
                // 以非同步方式取出回傳結果之JSON格式字串
                result = response2.Content.ReadAsStringAsync().Result;
                // 將JSON格式字串轉換成.NET的JSON物件
                dynamic jsonObject2 = JsonConvert.DeserializeObject(result);
                // 從JSON物件中取出消費種類清單字串存於TempData物件之Result屬性中，以便從View中取出
                TempData["Result"] = "目前的消費種類清單如下：\n" + jsonObject2.TallybookAPIResult;
                //重新導向頁面至開始頁面
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // 清除原先的資料繫結
                ModelState.Clear();
                string str = ex.Message; // 此行只是讓ex有被使用到，沒有功用
                //重新導向頁面至開始頁面
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}