using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HW04.Models;

namespace HW04.Controllers
{
    public class APIController : ApiController
    {
        #region

        private class ServiceResult
        {
            public string Status { get; set; }
            public string APIResult { get; set; }
        }

        ServiceResult serviceObj;
        Service db = new Service();

        #endregion

        #region
        public APIController()
        {
            try
            {
                string[] initialTypes = { "慢走", "慢跑", "快走", "快跑" };

                AddKind addkind = new AddKind();

                IEnumerable<AddKind> addKinds = db.AddKinds;

                int rowcount = addKinds.Count<AddKind>();     // 計算資料表資料筆數
                if (rowcount == 0) //若沒有任何消費種類，則利用迴圈方式，將初始消費種類逐一存到ExpenseTypes資料表中
                {
                    for (int i = 0; i < initialTypes.Length; i++)
                    {
                        addkind.type = initialTypes[i];
                        db.AddKinds.Add(addkind);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

        }
        #endregion

        #region
        [HttpPost]
        [Route("api/MainTBs")]
        public async Task<IHttpActionResult> PostMainTB(MainTB maintb)
        {
            serviceObj = new ServiceResult();
            try
            {
                db.MainTBs.Add(maintb);
                await db.SaveChangesAsync();
                string result = "儲存一筆資料";

                serviceObj.Status = "OK";
                serviceObj.APIResult = result;

                return Ok(serviceObj);
            }
            catch (Exception ex)
            {
                serviceObj.Status = "Exception";
                serviceObj.APIResult = "儲存資料發生例外,原因如下：" + ex.Message;

                return Ok(serviceObj);
            }
        }
        #endregion

        #region
        [HttpGet]
        [Route("api/MainTBs/{startDate}/{endDate}/{queryMode}")]
        public IHttpActionResult GetMainTBs(string startDate, string endDate, int queryMode)
        {
            serviceObj = new ServiceResult();

            DateTime sDate = Convert.ToDateTime(startDate);
            DateTime eDate = Convert.ToDateTime(endDate);

            string str;
            int totalAmount;
            int count;
            int typeSum;

            if (queryMode == 0)
            {
                try
                {
                    str = "";
                    totalAmount = 0;

                    var result = from a in db.MainTBs
                                 where ((a.date >= sDate) && (a.date <= eDate))
                                 orderby a.date
                                 select a;
                    count = result.Count();
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "共有" + count + "紀錄";

                    foreach (var record in result)
                    {
                        totalAmount += record.number;
                    }
                    str += "共計步數" + totalAmount + "步\n";


                    str += string.Format("{0, -5} {1, -9} {2, -12} {3, -20} {4, -32} \n",
                                         
                                         "編號", "步數", "類別", "日期", "說明");

                    int i = 0;
                    foreach (var record in result)
                    {
                        str += string.Format("{0:-d5} {1, -9} {2, -12} {3, -20} {4, -32} \n", 
                            
                                              (i + 1), record.number, record.type, record.date.Date.ToString("d"), record.comment);
                        i++;
                    }
                    serviceObj.Status = "OK";
                    serviceObj.APIResult = str;

                    return Ok(serviceObj);
                }
                catch(Exception ex)
                {
                    serviceObj.Status = "Exception";
                    serviceObj.APIResult = "發生錯誤" + ex.Message;

                    return Ok(serviceObj);
                }
            }else if (queryMode == 1)
            {
                str = "";
                totalAmount = 0;

                var result = from a in db.MainTBs
                             where ((a.date >= sDate) && (a.date <= eDate))
                             group a by a.type into g
                             select g.FirstOrDefault();

                var records = result.ToArray<MainTB>();

                count = result.Count();

                if (count == 0)
                {
                    str = "在" + sDate.Date.ToString("yyyy-MM-dd") + "到" + eDate.Date.ToString("yyyy-MM-dd") + "並沒有運動紀錄";
                    serviceObj.Status = "No Record";
                    serviceObj.APIResult = str;

                    return Ok(serviceObj);

                }
                else
                {
                    try
                    {
                        str = "在" + sDate.Date.ToString("yyyy/MM/dd") + "到" + eDate.Date.ToString("yyyy/MM/dd") + "之步數統計如下：\n";
                        foreach(var record in records)
                        {
                            var result1 = (from b in db.MainTBs
                                           where ((b.date >= sDate) && (b.date <= eDate) && (b.type == record.type))
                                           select b
                                           );

                            typeSum = 0;
                            foreach(var record1 in result1)
                            {
                                typeSum += record1.number;
                            }
                            str += string.Format("{0}：{1}\n", record.type, typeSum);
                            totalAmount += typeSum;
                        }
                        str += "步數總計" + totalAmount + "步\n";

                        serviceObj.Status = "OK";
                        serviceObj.APIResult = str;

                        return Ok(serviceObj);

                    }
                    catch(Exception ex)
                    {
                        serviceObj.Status = "Exception";
                        serviceObj.APIResult = "查詢結果發生例外" + ex.Message;

                        return Ok(serviceObj);
                    }
                }
            }
            else
            {
                str = "無效的查詢";
                serviceObj.Status = "OK";
                serviceObj.APIResult = str;

                return Ok(serviceObj);
            }
        }
        #endregion

        #region



        #endregion

        #region

        #endregion

        #region

        #endregion

        #region

        #endregion



    }
}
