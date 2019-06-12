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
        // 產生二進位方法
        [Route("api/Carry/Binary/{num}")]
        [HttpGet]
        public IHttpActionResult Binary(int num)
        {
            string result;
            result = Convert.ToString(num, 2);
            //return Ok(result);

            // 並將進位服務之運算結果存入進位服務結果資料物件對應的欄位 ====
            carryResultDataObj.Status = "OK";
            carryResultDataObj.CarryResult = result;

            // 回傳進位服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(carryResultDataObj);
        }

        // 產生八進位方法
        [Route("api/Carry/Octal/{num}")]
        [HttpGet]
        public IHttpActionResult Octal(int num)
        {
            string result;
            result = Convert.ToString(num, 8);
            //return Ok(result);

            // 並將進位服務之運算結果存入進位服務結果資料物件對應的欄位 ====
            carryResultDataObj.Status = "OK";
            carryResultDataObj.CarryResult = result;

            // 回傳進位服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(carryResultDataObj);
        }

        // 產生十六進位方法
        [Route("api/Carry/Hexadecimal/{num}")]
        [HttpGet]
        public IHttpActionResult Hexadecimal(int num)
        {
            string result;
            result = Convert.ToString(num, 16);
            //return Ok(result);

            // 並將進位服務之運算結果存入進位服務結果資料物件對應的欄位 ====
            carryResultDataObj.Status = "OK";
            carryResultDataObj.CarryResult = result;

            // 回傳進位服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(carryResultDataObj);
        }

        // 產生二進位方法
        [Route("api/Carry/BinaryPost/{num}")]
        [HttpPost]
        public IHttpActionResult BinaryPost(int num)
        {
            string result;
            result = Convert.ToString(num, 2);
            //return Ok(result);

            // 並將進位服務之運算結果存入進位服務結果資料物件對應的欄位 ====
            carryResultDataObj.Status = "OK";
            carryResultDataObj.CarryResult = result;

            // 回傳進位服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(carryResultDataObj);
        }

        // 產生八進位方法
        [Route("api/Carry/OctalPost/{num}")]
        [HttpPost]
        public IHttpActionResult OctalPost(int num)
        {
            string result;
            result = Convert.ToString(num, 8);
            //return Ok(result);

            // 並將進位服務之運算結果存入進位服務結果資料物件對應的欄位 ====
            carryResultDataObj.Status = "OK";
            carryResultDataObj.CarryResult = result;

            // 回傳進位服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(carryResultDataObj);
        }

        // 產生十六進位方法
        [Route("api/Carry/HexadecimalPost/{num}")]
        [HttpPost]
        public IHttpActionResult HexadecimalPost(int num)
        {
            string result;
            result = Convert.ToString(num, 16);
            //return Ok(result);

            // 並將進位服務之運算結果存入進位服務結果資料物件對應的欄位 ====
            carryResultDataObj.Status = "OK";
            carryResultDataObj.CarryResult = result;

            // 回傳進位服務結果資料物件，執行環境會因應Client端的請求(利用application/json或application/xml)
            // 分別自動地將物件序列化(Serialize)成JSON或XML字串
            return Ok(carryResultDataObj);
        }
    }
}
