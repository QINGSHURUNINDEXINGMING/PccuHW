using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIMathLotteryServices.Controllers
{
    public class MathController : ApiController
    {
        [Route("api/Math/add/{num1}/{num2}")]
        [HttpGet]
        public IHttpActionResult add(double num1, double num2)
        {
            return Ok(num1 + num2);
        }

        [Route("api/Math/substract/{num1}/{num2}")]
        [HttpGet]
        public IHttpActionResult substract(double num1, double num2)
        {
            return Ok(num1 - num2);
        }

        [Route("api/Math/multiply/{num1}/{num2}")]
        [HttpGet]
        public IHttpActionResult multiply(double num1, double num2)
        {
            return Ok(num1 * num2);
        }

        [Route("api/Math/divide/{num1}/{num2}")]
        [HttpGet]
        public IHttpActionResult divide(double num1, double num2)
        {
            return Ok(num1 / num2);
        }
    }
}
