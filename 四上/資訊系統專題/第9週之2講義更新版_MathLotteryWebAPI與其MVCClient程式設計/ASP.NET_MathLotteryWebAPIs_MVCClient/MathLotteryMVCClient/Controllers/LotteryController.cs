using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http; // needed for using the HttpClient class
using System.Net.Http.Headers;
using System.Threading.Tasks;
//
using Newtonsoft.Json;      // needed for using the JsonConvert class
using Newtonsoft.Json.Linq; // needed for using the JObject class


using MathLotteryMVCClient.ViewModels;

namespace MathLotteryMVCClient.Controllers
{
    public class LotteryController : Controller
    {
        HttpClient client = new HttpClient();

        // GET: Lottery
        public ActionResult LotteryGen()
        {
            return View();
        }

        [HttpPost]
        //public ActionResult LotteryGen([Bind(Include = "Type, Sets")] LotteryViewModel lotterydata)

        //public async Task<ActionResult> LotteryGen([Bind(Exclude = "Result")] LotteryViewModel lotterydata)
        public ActionResult LotteryGen([Bind(Exclude = "Result")] LotteryViewModel lotterydata)
        {
            string result;
            if (!ModelState.IsValid)
            {
                return View(lotterydata);
            }
            try
            {
                int type = (lotterydata.Type == "大樂透") ? 0 : 1; 
                string urlstring = "http://140.137.41.136:5558/MathLotteryWebAPIs/api/lottery/" + type + "/" + lotterydata.Sets;
                client.BaseAddress = new Uri(urlstring);
                // Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/plain-text"));

                // Get data response.
                //HttpResponseMessage response = await client.GetAsync(""); // Get the response asynchrously
                HttpResponseMessage response = client.GetAsync("").Result;  // Get the response synchrously
                if (response.IsSuccessStatusCode)
                {
                    //result = await response.Content.ReadAsStringAsync(); // Get the result string asynchrously
                    result = response.Content.ReadAsStringAsync().Result;  // Get the result string synchrously
                }
                else
                {
                    result = response.StatusCode + response.ReasonPhrase;
                }
                
                result = result.Replace("\\n", "\n"); //因為Json字串化會將\n escape成\\n，因此要還原成\n
                result = result.Replace("\"",""); //因為Json字串化會將" escape成\"，因此要還原成空字元
                lotterydata.Result = result;
                return View(lotterydata);
            }
            catch (Exception ex)
            {
                result = "產生樂透號碼時發生例外，原因如下: " + ex.Message;
                lotterydata.Result = result;
                return View(lotterydata);
            }
        }

 
    }
}