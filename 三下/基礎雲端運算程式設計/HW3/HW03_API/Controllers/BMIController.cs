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
            return Ok(kg / Math.Pow(Math.Round(cm / 100, 2), 2));
        }
    }
}
