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
            BMIResultData BMIResultDataobj = new BMIResultData();

            string str = "";
            str += Math.Round(kg / Math.Pow(Math.Round(cm / 100, 2), 2), 2);

            //=== 並將BMI服務之運算結果存入BMI服務結果資料物件對應的欄位 ====
            BMIResultDataobj.Status = "OK";
            BMIResultDataobj.BMIResult += str;

            // 回傳BMI服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(BMIResultDataobj);
        }

        //[Route("api/BMIPost/{cm}/{kg}")]
        [Route("api/BMIPost/")]
        [HttpPost]
        public IHttpActionResult resultPost(double cm, double kg)
        {
            // 建立記帳簿服務結果資料物件
            BMIResultData BMIResultDataobj = new BMIResultData();

            //=== 並將BMI服務之運算結果存入BMI服務結果資料物件對應的欄位 ====
            BMIResultDataobj.Status = "OK";
            BMIResultDataobj.BMIResult += Math.Round(kg / Math.Pow(Math.Round(cm / 100, 2), 2), 2);

            // 回傳BMI服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(BMIResultDataobj);
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
