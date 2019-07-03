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
        public async Task<IHttpActionResult>PostMainTB(MainTB maintb)
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
            catch(Exception ex)
            {
                serviceObj.Status = "Exception";
                serviceObj.APIResult = "儲存資料發生例外,原因如下：";

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
        }



        #endregion







    }
}
