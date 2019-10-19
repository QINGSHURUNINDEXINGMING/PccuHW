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
using TallybookServiceCFDB.Models;

namespace TallybookServiceCFDB.Controllers
{
    public class TallybooksController : ApiController
    {
        // 建立雲端記帳簿服務資料庫的物件，可以存取Talltbooks及ExpenseTypes資料表
        private TallybookServiceModel db = new TallybookServiceModel();

        // 依照不同查詢模式(依照消費日期或依照消費種類)查詢起訖日期間的消費資料之方法，
        // GET: api/Tallybooks/{startDate:string}/{endDate}/{querymode}
        [ResponseType(typeof(string))]
        [HttpGet]
        [Route("api/Tallybooks/{startDate}/{endDate}/{querymode}")]
        public IHttpActionResult GetTallybooks(string startDate, string endDate, int querymode)
        {
            DateTime sDate = Convert.ToDateTime(startDate); // 將起始日期字串轉成DateTime物件
            DateTime eDate = Convert.ToDateTime(endDate);   // 將結束日期字串轉成DateTime物件
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
                    var result = from a in db.Tallybooks
                                 where ((a.payDate >= sDate) && (a.payDate <= eDate))
                                 orderby a.payDate
                                 select a;
                    rowCount = result.Count(); // 取得記錄數量
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "共有" + rowCount + "筆消費紀錄:" + Environment.NewLine;

                    foreach (var record in result) // 利用迴圈逐一讀取每一筆紀錄
                    {
                        totalAmount += record.price; //讀取price欄位(即消費金額price)，並加總到消費總金額中
                    }
                    str += "共計消費 " + totalAmount + " 元" + Environment.NewLine; // 將消費總金額串接到顯示字串(str)中

                    // 顯示消費紀錄之每一個欄位之抬頭，將每一個欄位的抬頭串接到顯示字串(str)中	
                    string[] colNames = { "編號", "消費金額", "消費類別", "消費日期", "消費說明" };
                    foreach (var name in colNames)
                    {
                        str += string.Format("{0}    ", name);
                    }

                    str += Environment.NewLine;

                    // 利用迴圈逐一讀取每一筆紀錄
                    int i = 0;
                    foreach (var record in result)
                    {
                        str += string.Format("{0:d4}  ", (i + 1)); // 串接記錄編號(索引值+1)
                        str += string.Format("{0,8}   ", record.price);       // 串接price欄位值(即消費金額price)
                        str += string.Format("{0,6}     ", record.expenseType);       // 串接expenseType欄位值(即消費種類expenseType)
                        str += string.Format("{0,10}    ", record.payDate.Date.ToString("d"));  // 串接payDate欄位值(即消費日期payDate之日期)
                        str += string.Format("{0,-8}", record.comment);     // 串接comment欄位值(即消費說明comment)
                        str += Environment.NewLine;
                        i++;
                    }
                    return Ok(str);
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            else if (querymode == 1)  // querymode==1，按照消類種類查詢
            {
                str = "";
                totalAmount = 0;

                // 使用LINQ語法查詢起訖日期間的消費紀錄，並依照消費種類群組，每種消費種類只取出第1筆紀錄 
                var result = from a in db.Tallybooks
                                where ((a.payDate >= sDate) && (a.payDate <= eDate))
                                group a by a.expenseType into g
                                select g.FirstOrDefault();

                var records = result.ToArray<Tallybook>(); // 將結果轉換成消費紀錄陣列

                //
                rowCount = result.Count(); // 取得紀錄數
                if (rowCount == 0)  // 若記錄數為0，則回傳有消費紀錄之訊息
                {
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "並沒有消費紀錄";
                    return Ok(str);
                }
                else // 
                {
                    try
                    {
                        str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "之消費統計如下:" + Environment.NewLine;
                        foreach (var record in records) // 逐一取出消費資料的每一筆紀錄record
                        {
                            // 使用LINQ語法查詢起訖日期間消費種類與record消費種類相同的所有消費紀錄
                            // use LINQ to query data 
                            var result1 = (from b in db.Tallybooks
                                            where ((b.payDate >= sDate) && (b.payDate <= eDate) && (b.expenseType == record.expenseType))
                                            select b);

                            // 計算該消費種類的總金額
                            typeSum = 0;
                            foreach (var record1 in result1)
                            {
                                typeSum += record1.price;
                            }
                            str += string.Format("{0}: {1}元" + Environment.NewLine, record.expenseType, typeSum); //格式化串接該消費種類之總金額
                            totalAmount += typeSum; //  累加消費總金額
                                
                        }
                        str += "消費金額總計" + totalAmount + "元" + Environment.NewLine;
                        return Ok(str);

                    }
                    catch (Exception ex)
                    {
                        return Ok(ex.InnerException.ToString());
                    }
                }
            }
            else
            {
                return Ok("無效的查詢");
            }
        }


        // 新增消費紀錄之方法
        // POST: api/Tallybooks
        [ResponseType(typeof(string))]
        [HttpPost]
        [Route("api/Tallybooks")]
        public async Task<IHttpActionResult> PostTallybook(Tallybook tallybook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Tallybooks.Add(tallybook);
                await db.SaveChangesAsync();
                string result = "已成功儲存一筆消費紀錄" + Environment.NewLine;
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok("儲存消費紀錄時發生例外，原因如下： " + ex.Message);
            }
        }

        // 刪除起訖日期間消費紀錄之方法
        // DELETE: api/Tallybooks/delete/2017-5-1/2017-10-2
        [ResponseType(typeof(int))]
        [HttpGet]
        [Route("api/Tallybooks/delete/{startDate}/{endDate}")]
        public async Task<IHttpActionResult> DeleteTallybooks(string startDate, string endDate)
        {
            DateTime sDate = Convert.ToDateTime(startDate);
            DateTime eDate = Convert.ToDateTime(endDate);
            string str = "";

            try
            {
                // 使用 LINQ 取出起訖日期間的消費資料紀錄 (依照消費日期排序)
                var result = from a in db.Tallybooks
                             where ((a.payDate >= sDate) && (a.payDate <= eDate))
                             orderby a.payDate
                             select a;
                int count = result.Count(); // 取得紀錄數
                if (count == 0)  // 若記錄數為0，則回傳有消費紀錄之訊息
                {
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "沒有消費紀錄:!";
                    return Ok(str);
                }

                db.Tallybooks.RemoveRange(result); // 從db物件刪除result中的消費資料紀錄
                await db.SaveChangesAsync();       // 更新資料庫
                str = "已經成功刪除" + count + "筆紀錄!";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok("刪除消費紀錄時發生例外，原因如下： " + ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TallybookExists(int id)
        {
            return db.Tallybooks.Count(e => e.id == id) > 0;
        }
    }
}