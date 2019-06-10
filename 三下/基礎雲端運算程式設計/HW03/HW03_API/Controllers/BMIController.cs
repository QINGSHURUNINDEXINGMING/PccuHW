using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HW03_API.Controllers
{
    public class BMIController : ApiController
    {
        [Route("api/BMI/{cm}/{kg}")]
        [HttpGet]
        public IHttpActionResult result(double cm, double kg)
        {
            // 建立記帳簿服務結果資料物件
            BMIResultData BMIResultDataOBJ = new BMIResultData();
            return Ok(kg / Math.Pow(Math.Round(cm / 100, 2), 2));
        }



        [Route("api/BMIPost/{cm}/{kg}")]
        [HttpPost]
        public IHttpActionResult resultPost(double cm, double kg)
        {
            return Ok(kg / Math.Pow(Math.Round(cm / 100, 2), 2));
        }

        // 用來儲存進位服務結果資料的內部類別，
        // 每一個欄位變數名稱將是回傳JSON物件的Key
        public class BMIResultData
        {
            // 儲存BMI服務結果之狀態
            public string Status { get; set; }
            // 儲存BMI服務之運算結果
            public string BMIResult { get; set; }
        }

    }
}
