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
    public class ExpenseTypesController : ApiController
    {
        private TallybookServiceModel db = new TallybookServiceModel();

        // ExpenseTypesController類別的建構子(Constructor)
        public ExpenseTypesController()
        {
            // 若消費種類資料表(ExpenseTypes)沒有任何紀錄，則將10個預先設好的消費種類存入消費種類資料表中
            try
            {
                //初始消費種類陣列
                string[] initialExpenseTypes = { "餐飲", "購物", "交通", "娛樂", "醫療", "投資", "保險", "教育", "社交", "住宿" };
                ExpenseType expenseType = new ExpenseType();
                IEnumerable<ExpenseType> expenseTypes = db.ExpenseTypes;

                int rowcount = expenseTypes.Count<ExpenseType>();     //計算資料庫資料筆數
                if (rowcount == 0) //若沒有任何消費種類，則利用迴圈方式，將初始消費種類逐一存到ExpenseTypes資料表中
                {
                    for (int i = 0; i < initialExpenseTypes.Length; i++)
                    {
                        expenseType.expenseType = initialExpenseTypes[i];
                        db.ExpenseTypes.Add(expenseType);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        // 從ExpeneTypes資料表中取出所有消費種類的方法，回傳資料型態為ExpenseType之列舉集合(陣列)
        // GET: api/ExpenseTypes 
        [ResponseType(typeof(IEnumerable<ExpenseType>))]
        [HttpGet]
        [Route("api/ExpenseTypes")]
        public IEnumerable<ExpenseType> GetExpenseTypes()
        {
            try
            {
                return db.ExpenseTypes;
            }
            catch(Exception ex)
            {
                string str = ex.Message;
                return null;
            }
            
        }

        // 從ExpeneTypes資料表中取出所有消費種類的方法，回傳資料型態為加上說明的字串
        // GET: api/ExpenseTypes/string
        [ResponseType(typeof(string))]
        [HttpGet]
        [Route("api/ExpenseTypes/string")]
        public IHttpActionResult GetExpenseTypesString()
        {
            string str = "";
            try
            {
                // 使用LINQ語法取出所有消費種類，並依照id排序
                var result = from a in db.ExpenseTypes
                             orderby a.id
                             select a;

                // 利用迴圈逐一讀取每一筆紀錄
                var i = 0;
                foreach (var record in result)
                {
                    str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
                    str += string.Format("{0}", record.expenseType);       // 串接price欄位值(即消費金額price)
                    str += Environment.NewLine;
                    i++;
                }
                return Ok(str);
            }
            catch(Exception ex)
            {
                str = "取出消費種類時發生例外，原因如下： " + ex.Message;
                return Ok(str);

            }
            
        }

        // 新增消費種類的方法
        // POST: api/ExpenseTypes
        [ResponseType(typeof(string))]
        [HttpPost]
        [Route("api/ExpenseTypes")]
        public IHttpActionResult PostExpenseType(ExpenseType expenseType)
        {
            string str = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // 檢驗擬新增的消費種類是否已存在
                var result = db.ExpenseTypes.Where(x => x.expenseType == expenseType.expenseType);
                int count = result.Count(); // 取得記錄數
                if (count == 0) // 如果記錄數為0，表示該新的消費種類不存在，則可將其新增至ExpenseTypes資料表中
                {
                    db.ExpenseTypes.Add(expenseType); // 新增消費種類
                    db.SaveChanges();                 // 更新至資料庫

                    // 利用LINQ語法取出所有的消費種類紀錄
                    var result1 = from a in db.ExpenseTypes
                                  orderby a.id
                                  select a;

                    // 利用迴圈逐一讀取每一筆紀錄，並加入編號說明
                    var i = 0;
                    foreach (var record in result1)
                    {
                        str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
                        str += string.Format("{0}", record.expenseType);       // 串接price欄位值(即消費金額price)
                        str += Environment.NewLine;
                        i++;
                    }
                    return Ok(str);
                }
                else
                {
                    str = "消費種類：【" + expenseType.expenseType + "】已經存在，請重新輸入!";
                    return Ok(str);
                }
            }
            catch (Exception ex)
            {
                str = "儲存消費種類時發生例外，原因如下： " + ex.Message;
                return Ok(str);
            }

            
        }

        //刪除指定編號的消費種類之方法
        // DELETE: api/ExpenseTypes/delete/{no}
        [ResponseType(typeof(string))]
        [HttpGet]
        [Route("api/ExpenseTypes/delete/{no}")]
        public IHttpActionResult DeleteExpenseType(int no)
        {
            string str="";
            try
            {
                // 利用LINQ語法取出所有的消費種類紀錄
                var result = from a in db.ExpenseTypes
                             orderby a.id
                             select a;

                int count = result.Count(); // 取出記錄數

                // 輸入的編號不在範圍內，則回傳提示訊息
                if ((no < 1) || (no > count))
                {
                    str = "輸入的編號不在範圍內，請重新輸入!";
                    return Ok(str);
                }
                else
                {
                    var record = result.ToArray<ExpenseType>()[no - 1]; //找出指定編號那筆紀錄
                    db.ExpenseTypes.Remove(record); // //將指定編號那筆紀錄刪除
                    db.SaveChanges(); // 更新資料庫

                    // 取出最新所有的消費種類
                    var result1 = from a in db.ExpenseTypes
                                  orderby a.id
                                  select a;

                    // 利用迴圈逐一讀取每一筆紀錄
                    var i = 0;
                    foreach (var record1 in result1)
                    {
                        str += string.Format("{0:d2}  ", (i + 1)); // 串接記錄編號(索引值+1)
                        str += string.Format("{0}", record1.expenseType);       // 串接price欄位值(即消費金額price)
                        str += Environment.NewLine;
                        i++;
                    }
                    return Ok(str);
                }
            }
            catch (Exception ex)
            {
                str = "刪除消費種類時發生例外，原因如下： " + ex.Message;
                return Ok(str);
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

        private bool ExpenseTypeExists(int id)
        {
            return db.ExpenseTypes.Count(e => e.id == id) > 0;
        }
    }
}