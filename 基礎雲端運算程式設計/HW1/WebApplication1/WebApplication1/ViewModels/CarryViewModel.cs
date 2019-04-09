using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class CarryViewModel
    {
        [DisplayName("十進位數字：")]
        [Required(ErrorMessage = "請輸入十進位數字")]
        public int Num { get; set; }

        [DisplayName("二進位：")]
        public string Result1 { get; set; }

        [DisplayName("八進位：")]
        public string Result2 { get; set; }

        [DisplayName("十六進位：")]
        public string Result3 { get; set; }
    }
}