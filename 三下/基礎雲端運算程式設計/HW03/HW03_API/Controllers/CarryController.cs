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
            return Ok(result);
        }

        // 產生八進位方法
        [Route("api/Carry/Octal/{num}")]
        [HttpGet]
        public IHttpActionResult Octal(int num)
        {
            string result;
            result = Convert.ToString(num, 8);
            return Ok(result);
        }

        // 產生十六進位方法
        [Route("api/Carry/Hexadecimal/{num}")]
        [HttpGet]
        public IHttpActionResult Hexadecimal(int num)
        {
            string result;
            result = Convert.ToString(num, 16);
            return Ok(result);
        }
    }
}
