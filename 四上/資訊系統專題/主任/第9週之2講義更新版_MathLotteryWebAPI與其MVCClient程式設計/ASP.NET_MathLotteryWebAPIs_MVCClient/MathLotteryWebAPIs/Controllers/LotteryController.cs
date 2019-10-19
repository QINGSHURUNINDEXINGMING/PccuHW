using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
//
using Newtonsoft.Json; // needed for using the JsonConvert class
using Newtonsoft.Json.Linq; // needed for using the JObject class

namespace WebAPIMathLotteryServices.Controllers
{
    public class LotteryController : ApiController
    {
        // 產生樂透彩號碼之方法
        [Route("api/Lottery/{type}/{times}")]
        [HttpGet]
        public IHttpActionResult GenLotteryNum(int type, int times)
        //public string GenLotteryNum(int type, int times)
        {
            try
            {
                // delcare local variables
                int num, count, num2, biggestNum;
                bool flag;
                int[] lotteryNum = new int[6];
                string str;
                Random rand = new Random();
                //
                if (type == 0) //大樂透
                {
                    biggestNum = 49;
                    str = "大樂透" + times + "組號碼:\n";
                }
                else           //威力彩
                {
                    biggestNum = 38;
                    str = "威力彩" + times + "組號碼:\n";
                    str += "          第一區               第二區\n";
                }
                //
                for (int i = 0; i < times; i++)
                {
                    count = 0;
                    for (int j = 0; j < 6; j++) lotteryNum[j] = 0;
                    //
                    do
                    {
                        num = rand.Next(biggestNum) + 1;
                        flag = exist(lotteryNum, count, num);
                        if (flag == false)
                        {
                            lotteryNum[count] = num;
                            count++;
                        }
                    } while (count < 6);
                    //
                    Array.Sort(lotteryNum);
                    //
                    str += "第" + String.Format("{0:d2}", (i + 1)) + "組: ";
                    for (int k = 0; k < 6; k++)
                    {
                        str += String.Format("{0:d2}", lotteryNum[k]) + "  ";
                    }
                    if (type == 0)  //大樂透
                    {
                        str += "\n";
                    }
                    else            //威力彩
                    {
                        num2 = rand.Next(8) + 1;
                        str += "  " + num2 + "\n";
                    }
                }
                
                return Content(HttpStatusCode.OK, str);
                
            }
            catch (Exception ex)
            {
                return BadRequest("產生樂透號碼時發生例外，原因如下: " + ex.Message);
                //return "產生樂透號碼時發生例外，原因如下: " + ex.Message;
            }
        }

        public bool exist(int[] numarray, int count, int number)
        {
            try
            {
                bool status = false;
                for (int i = 0; i < count; i++)
                {
                    if (numarray[i] == number)
                    {
                        status = true;
                        break;
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }

        }
    }
}
