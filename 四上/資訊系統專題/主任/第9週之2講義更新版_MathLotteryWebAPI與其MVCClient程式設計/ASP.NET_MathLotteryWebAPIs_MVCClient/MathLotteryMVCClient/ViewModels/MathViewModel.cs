using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MathLotteryMVCClient.ViewModels
{
    public class MathViewModel
    {
        [DisplayName("數字1：")]
        [Required(ErrorMessage = "請輸入數字1")]
        public double Num1 { get; set; }

        [DisplayName("數字2：")]
        [Required(ErrorMessage = "請輸入數字2")]
        public double Num2 { get; set; }

        [DisplayName("結果：")]
        public string Result { get; set; }
    }
}