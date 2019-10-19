using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MathLotteryMVCClient.ViewModels
{
    public class LotteryViewModel
    {
        [DisplayName("樂透種類：")]
        [Required(ErrorMessage = "請選擇樂透種類!")]
        public string Type { get; set; }

        [DisplayName("號碼組數：")]
        [Required(ErrorMessage = "請輸入號碼組數(整數)!")]
        public int Sets { get; set; }

        [DisplayName("結果：")]
        public string Result { get; set; }
    }
}