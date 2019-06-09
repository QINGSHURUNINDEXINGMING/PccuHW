using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HW03_API.Controllers
{
    public class CarryController : ApiController
    {
        // 建立進位服務結果資料物件
        CarryResultData carryResultDataObj = new CarryResultData();

        // 產生樂透彩號碼之方法
        [Route("api/Carry/Binary/{num}")]
        [HttpGet]
        public IHttpActionResult Binary(int num)
        {
            string result;
            result = Convert.ToString(num, 2);
            return Ok(result);
        }

        // 產生樂透彩號碼之方法
        [Route("api/Carry/Octal/{num}")]
        [HttpGet]
        public IHttpActionResult Octal(int num)
        {
            string result;
            result = Convert.ToString(num, 8);
            return Ok(result);
        }// 產生樂透彩號碼之方法

        [Route("api/Carry/Hexadecimal/{num}")]
        [HttpGet]
        public IHttpActionResult Hexadecimal(int num)
        {
            string result;
            result = Convert.ToString(num, 16);
            return Ok(result);     
        }

        //=== 並將樂透服務之運算結果存入記帳簿服務結果資料物件對應的欄位 ====
        lotteryResultDataobj.Status = "OK";
                lotteryResultDataobj.LotteryResult = str;

                // 回傳樂透服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
                // 分別自動地將物件序列化(Serialize)成JSON或XML字串
                return Ok(lotteryResultDataobj);


        // 用來儲存進位服務結果資料的內部類別，
        // 每一個欄位變數名稱將是回傳JSON物件的Key
        public class CarryResultData
        {
            // 儲存進位服務結果之狀態
            public string Status { get; set; }
            // 儲存進位服務之運算結果
            public string CarryResult { get; set; }
        }
    }
}
