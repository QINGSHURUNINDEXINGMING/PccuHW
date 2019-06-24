using HW04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HW04.Controllers
{
    public class ServiceController : ApiController
    {
        #region
        // 用來儲存記帳簿服務結果資料的內部類別，
        // 每一個欄位變數名稱將是服務回傳JSON物件的Key
        private class ServiceResult
        {
            // 儲存記帳簿服務結果之狀態
            public string Status { get; set; }
            // 儲存記帳簿服務之運算結果
            public string APIResult { get; set; }
        }

        ServiceResult ServiceResultObj;

        ExerciseModel db = new ExerciseModel();
        #endregion

        #region

        public ServiceController()
        {
            try
            {
                // 預設的初始消費種類陣列
                string[] initialExpenseTypes = { "慢走", "慢跑", "快走", "快跑" };
                //建立消費種類紀錄物件
                Kind expenseType = new Kind();
                //建立ExpenseType型態的列舉物件(類似資料表)，可儲存消費種類紀錄
                IEnumerable<Kind> expenseTypes = db.Kinds;

                int rowcount = expenseTypes.Count<Kind>();     // 計算資料表資料筆數
                if (rowcount == 0) //若沒有任何消費種類，則利用迴圈方式，將初始消費種類逐一存到ExpenseTypes資料表中
                {
                    for (int i = 0; i < initialExpenseTypes.Length; i++)
                    {
                        expenseType.exerciseType = initialExpenseTypes[i];
                        db.Kinds.Add(expenseType);
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
        //================ 新增一筆消費紀錄之Web API ===================================
        // 路由資訊：動詞：POST，路徑：api/Tallybooks
        // Web API之輸入參數：Tallybook物件
        [HttpPost]
        [Route("api/Exercise")]
        public async Task<IHttpActionResult> PostTallybook(Col tallybook)
        {
            // 建立記帳簿服務結果資料物件
            ServiceResultObj = new ServiceResult();

            try
            {
                db.Cols.Add(tallybook);
                await db.SaveChangesAsync();
                string result = "已成功儲存一筆運動紀錄!";

                //=== 並將記帳簿服務之運算結果存入記帳簿服務結果資料物件對應的欄位 ====
                ServiceResultObj.Status = "OK";
                ServiceResultObj.APIResult = result;

                // 回記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
            catch (Exception ex)
            {
                //===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
                ServiceResultObj.Status = "Exception";
                ServiceResultObj.APIResult = "刪除運動紀錄時發生例外，原因如下： " + ex.Message;

                // 回傳記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
        }
        #endregion

        #region
        //============================== 查詢消費紀錄之Web API ==================================================
        //  依照不同查詢模式(依照消費日期：querymode=0; 依照消費種類:querymode=1)查詢起訖日期的消費資料之方法
        // 路由資訊：動詞：GET，路徑：api/Tallybooks/{startDate}/{endDate}/{querymode}
        // Web API之輸入參數：起始日期{startDate}；結束日期{endDate}；查詢模式{querymode}
        [HttpGet]
        [Route("api/Exercise/{startDate}/{endDate}/{querymode}")]
        public IHttpActionResult GetTallybooks(string startDate, string endDate, int querymode)
        {
            // 建立記帳簿服務結果資料物件
            ServiceResultObj = new ServiceResult();

            // 將起訖日期字串轉成DateTime物件
            DateTime sDate = Convert.ToDateTime(startDate);
            DateTime eDate = Convert.ToDateTime(endDate);

            // 宣告區域變數
            string str;     //用於紀錄查詢結果字串
            int totalAmount; // 用於紀錄消費總金額
            int rowCount;   // 用於儲存紀錄數
            int typeSum;   // 用於儲存個別消費種類的消費總金額

            if (querymode == 0)  // querymode==0，按照消類日期查詢
            {
                try
                {
                    str = "";
                    totalAmount = 0;
                    // 使用LINQ語法查詢起訖日期間的消費紀錄，並依照消費日期排序
                    var result = from a in db.Cols
                                 where ((a.Date >= sDate) && (a.Date <= eDate))
                                 orderby a.Date
                                 select a;
                    rowCount = result.Count(); // 取得記錄數量
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "共有" + rowCount + "筆運動紀錄:\n";

                    foreach (var record in result) // 利用迴圈逐一讀取每一筆紀錄
                    {
                        totalAmount += record.number; //讀取price欄位(即消費金額price)，並加總到消費總金額中
                    }
                    str += "共計消費 " + totalAmount + " 元\n"; // 將消費總金額串接到顯示字串(str)中

                    // 顯示消費紀錄之每一個欄位之抬頭，將每一個欄位的抬頭串接到顯示字串(str)中	
                    string[] colNames = { "編號", "運動步數", "運動類別", "運動日期", "運動說明" };
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
                        str += string.Format("{0,8}   ", record.number);       // 串接price欄位值(即消費金額price)
                        str += string.Format("{0,6}     ", record.exerciseType);       // 串接expenseType欄位值(即消費種類expenseType)
                        str += string.Format("{0,10}    ", record.Date.Date.ToString("d"));  // 串接payDate欄位值(即消費日期payDate之日期)
                        str += string.Format("{0,-8}", record.comment);     // 串接comment欄位值(即消費說明comment)
                        str += "\n";
                        i++;
                    }

                    //=== 並將記帳簿服務之運算結果存入記帳簿服務結果資料物件對應的欄位 ====
                    ServiceResultObj.Status = "OK";
                    ServiceResultObj.APIResult = str;

                    // 回記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                    // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                    return Ok(ServiceResultObj);
                }
                catch (Exception ex)
                {
                    //===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
                    ServiceResultObj.Status = "Exception";
                    ServiceResultObj.APIResult = "查詢運動紀錄時發生例外，原因如下： " + ex.Message;

                    // 回傳記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                    // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                    return Ok(ServiceResultObj);
                }
            }
            else if (querymode == 1)  // querymode==1，按照消類種類查詢
            {
                str = "";
                totalAmount = 0;

                // 使用LINQ語法查詢起訖日期間的消費紀錄，並依照消費種類群組，每種消費種類只取出第1筆紀錄 
                var result = from a in db.Cols
                             where ((a.Date >= sDate) && (a.Date <= eDate))
                             group a by a.exerciseType into g
                             select g.FirstOrDefault();

                var records = result.ToArray<Col>(); // 將結果轉換成消費紀錄陣列

                //
                rowCount = result.Count(); // 取得紀錄數
                if (rowCount == 0)  // 若記錄數為0，則回傳沒有消費紀錄之訊息
                {
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "並沒有運動紀錄";
                    //===== 並將記帳簿服務之運算結果存入記帳簿服務結果資料物件對應的欄位 =====
                    ServiceResultObj.Status = "No Record";
                    ServiceResultObj.APIResult = str;

                    // 回記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                    // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                    return Ok(ServiceResultObj);
                }
                else // 
                {
                    try
                    {
                        str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "之步數統計如下:\n";
                        foreach (var record in records) // 逐一取出消費資料的每一筆紀錄record
                        {
                            // 使用LINQ語法查詢起訖日期間消費種類與record消費種類相同的所有消費紀錄
                            // use LINQ to query data 
                            var result1 = (from b in db.Cols
                                           where ((b.Date >= sDate) && (b.Date <= eDate) && (b.exerciseType == record.exerciseType))
                                           select b);

                            // 計算該消費種類的總金額
                            typeSum = 0;
                            foreach (var record1 in result1)
                            {
                                typeSum += record1.number;
                            }
                            str += string.Format("{0}: {1}步\n", record.exerciseType, typeSum); //格式化串接該消費種類之總金額
                            totalAmount += typeSum; //  累加消費總金額

                        }
                        str += "運動步數總計" + totalAmount + "步\n";
                        //===== 並將記帳簿服務之運算結果存入記帳簿服務結果資料物件對應的欄位 =====
                        ServiceResultObj.Status = "OK";
                        ServiceResultObj.APIResult = str;

                        // 回記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                        // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                        return Ok(ServiceResultObj);

                    }
                    catch (Exception ex)
                    {
                        //===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
                        ServiceResultObj.Status = "Exception";
                        ServiceResultObj.APIResult = "查詢運動紀錄時發生例外，原因如下： " + ex.Message;

                        // 回傳記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                        // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                        return Ok(ServiceResultObj);
                    }
                }
            }
            else
            {
                str = "無效的查詢";
                //===== 並將記帳簿服務之運算結果存入記帳簿服務結果資料物件對應的欄位 =====
                ServiceResultObj.Status = "OK";
                ServiceResultObj.APIResult = str;

                // 回記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
        }
        #endregion


        #region
        //======================= 刪除起訖日期間的消費紀錄之Web API =========================================
        // 路由資訊：動詞：GET，路徑：api/Tallybooks/delete/{startDate}/{endDate}
        // Web API之輸入參數：起始日期{startDate}；結束日期{endDate}
        [HttpGet]
        [Route("api/Exercise/delete/{startDate}/{endDate}")]
        public async Task<IHttpActionResult> DeleteTallybooks(string startDate, string endDate)
        {
            // 建立記帳簿服務結果資料物件
            ServiceResultObj = new ServiceResult();

            // 將起訖日期字串轉成DateTime物件
            DateTime sDate = Convert.ToDateTime(startDate);
            DateTime eDate = Convert.ToDateTime(endDate);
            string str = "";

            try
            {
                // 使用 LINQ 取出起訖日期間的消費資料紀錄 (依照消費日期排序)
                var result = from a in db.Cols
                             where ((a.Date >= sDate) && (a.Date <= eDate))
                             orderby a.Date
                             select a;
                int count = result.Count(); // 取得紀錄數
                if (count == 0)  // 若記錄數為0，則回傳沒有消費紀錄之訊息
                {
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "沒有運動紀錄:!";

                    //===== 並將記帳簿服務之運算結果存入記帳簿服務結果資料物件對應的欄位 =====
                    ServiceResultObj.Status = "OK";
                    ServiceResultObj.APIResult = str;

                    // 回傳記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                    // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                    return Ok(ServiceResultObj);
                }

                db.Cols.RemoveRange(result); // 從db物件刪除result中的消費資料紀錄
                await db.SaveChangesAsync();       // 更新資料庫
                str = "已經成功刪除" + count + "筆紀錄!";

                //===== 將記帳簿服務之運算結果存入記帳簿服務結果資料物件對應的欄位 =====
                ServiceResultObj.Status = "OK";
                ServiceResultObj.APIResult = str;

                // 回傳記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
            catch (Exception ex)
            {
                //===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
                ServiceResultObj.Status = "Exception";
                ServiceResultObj.APIResult = "刪除運動紀錄時發生例外，原因如下： " + ex.Message;

                // 回傳記帳簿服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
        }
        #endregion

        #region
        //===== 從ExpeneTypes資料表中取出所有消費種類的Web API方法，回傳資料型態為ExpenseType之列舉集合(陣列) ====
        // 路由資訊：動詞：GET，路徑：api/ExpenseTypes
        [HttpGet]
        [Route("api/ExerciseTypes")]
        public IEnumerable<Kind> GetExpenseTypes()
        {
            try
            {
                return db.Kinds;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return null;
            }
        }
        #endregion

        #region
        //===== 從ExpeneTypes資料表中取出所有消費種類的Web API方法，回傳資料型態為所有消費種類之格式化字串 ====
        // 路由資訊：動詞：GET，路徑：api/ExpenseTypes/string
        [HttpGet]
        [Route("api/ExerciseTypes/string")]
        public IHttpActionResult GetExpenseTypesString()
        {
            // 建立記帳簿服務結果資料物件
            ServiceResultObj = new ServiceResult();

            // 建立區域變數str，用於儲存運算結果
            string str = "";

            try
            {
                // 使用LINQ語法取出所有消費種類，並依照id排序
                var result = from a in db.Kinds
                             orderby a.id
                             select a;

                // 利用迴圈逐一讀取每一筆紀錄
                var i = 0;
                foreach (var record in result)
                {
                    str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
                    str += string.Format("{0}", record.exerciseType);       // 串接消費種類欄位值
                    str += "\n";
                    i++;
                }

                //===== 並將消費種類服務之運算結果存入消費種類服務結果資料物件對應的欄位 =====
                ServiceResultObj.Status = "OK";
                ServiceResultObj.APIResult = str;

                // 回傳消費種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
            catch (Exception ex)
            {
                //===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
                ServiceResultObj.Status = "Exception";
                ServiceResultObj.APIResult = "取出運動種類字串時發生例外，原因如下： " + ex.Message;

                // 回傳消費種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
        }
        #endregion

        #region
        //===== 新增一筆消費種類之Web API，回傳包含更新後之所有消費種類格式化字串之物件 ====
        // 路由資訊：動詞：POST，路徑：api/ExpenseTypes
        // Web API之輸入參數：ExpenseType物件
        [HttpPost]
        [Route("api/ExerciseTypes")]
        public IHttpActionResult PostExpenseType(Kind expenseType)
        {
            // 建立記帳簿服務結果資料物件
            ServiceResultObj = new ServiceResult();

            // 建立區域變數str，用於儲存運算結果
            string str = "";

            try
            {
                // 檢驗擬新增的消費種類是否已存在
                var result = db.Kinds.Where(x => x.exerciseType == expenseType.exerciseType);
                int count = result.Count(); // 取得記錄數
                if (count == 0) // 如果記錄數為0，表示該新的消費種類不存在，則可將其新增至ExpenseTypes資料表中
                {
                    db.Kinds.Add(expenseType); // 新增消費種類
                    db.SaveChanges();                 // 更新至資料庫

                    // 利用LINQ語法取出所有的消費種類紀錄
                    var result1 = from a in db.Cols
                                  orderby a.id
                                  select a;

                    // 利用迴圈逐一讀取每一筆紀錄，並加入編號說明
                    var i = 0;
                    foreach (var record in result1)
                    {
                        str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
                        str += string.Format("{0}", record.exerciseType);       // 串接消費種類欄位值
                        str += "\n";
                        i++;
                    }
                    //===== 並將消費種類服務之運算結果存入消費種類服務結果資料物件對應的欄位 =====
                    ServiceResultObj.Status = "OK";
                    ServiceResultObj.APIResult = str;
                }
                else
                {
                    str = "運動種類：【" + expenseType.exerciseType + "】已經存在，請重新輸入!";
                    //===== 並將消費種類已存在之訊息存入消費種類服務結果資料物件對應的欄位 =====
                    ServiceResultObj.Status = "Exist";
                    ServiceResultObj.APIResult = str;
                }

                // 回傳消費種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
            catch (Exception ex)
            {
                //===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
                ServiceResultObj.Status = "Exception";
                ServiceResultObj.APIResult = "儲存運動種類時發生例外，原因如下： " + ex.Message;

                // 回傳消費種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
        }
        #endregion

        #region
        //============ 刪除指定編號的消費種類之Web API =============
        // 路由資訊：動詞：GET，路徑：api/ExpenseTypes/delete/{no}
        [HttpGet]
        [Route("api/ExerciseTypes/delete/{no}")]
        public IHttpActionResult DeleteExpenseType(int no)
        {
            // 建立記帳簿服務結果資料物件
            ServiceResultObj = new ServiceResult();

            // 建立區域變數str，用於儲存運算結果
            string str = "";

            try
            {
                // 利用LINQ語法取出所有的消費種類紀錄
                var result = from a in db.Kinds
                             orderby a.id
                             select a;

                int count = result.Count(); // 取出記錄數

                // 輸入的編號不在範圍內，則回傳提示訊息
                if ((no < 1) || (no > count))
                {
                    str = "輸入的編號不在範圍內，請重新輸入!";
                    //===== 並將輸入的編號不在範圍內之訊息存入記消費種類服務結果資料物件對應的欄位 =====
                    ServiceResultObj.Status = "Out of Range";
                    ServiceResultObj.APIResult = str;
                }
                else
                {
                    var record = result.ToArray<Kind>()[no - 1]; //找出指定編號那筆紀錄
                    db.Kinds.Remove(record); // //將指定編號那筆紀錄刪除
                    db.SaveChanges(); // 更新資料庫

                    // 取出最新所有的消費種類
                    var result1 = from a in db.Kinds
                                  orderby a.id
                                  select a;

                    // 利用迴圈逐一讀取每一筆紀錄
                    var i = 0;
                    foreach (var record1 in result1)
                    {
                        str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
                        str += string.Format("{0}", record1.exerciseType);       // 串接消費種類欄位值
                        str += "\n";
                        i++;
                    }
                    //===== 並將消費種類服務之運算結果存入記消費種類服務結果資料物件對應的欄位 =====
                    ServiceResultObj.Status = "OK";
                    ServiceResultObj.APIResult = str;
                }
                // 回傳消費種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
            catch (Exception ex)
            {
                //===== 由於發生例外，因此僅將例外發生原因存入Status欄位，其餘欄位設為空字串 ====
                ServiceResultObj.Status = "Exception";
                ServiceResultObj.APIResult = "刪除運動種類時發生例外，原因如下： " + ex.Message;

                // 回傳消費種類服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(ServiceResultObj);
            }
        }
        #endregion

    }
}
