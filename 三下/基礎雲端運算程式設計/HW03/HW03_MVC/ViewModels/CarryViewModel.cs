using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW03_MVC.ViewModels
{
    public class CarryViewModel
    {
        [DisplayName("數字：")]
        [Required(ErrorMessage = "請輸入數字")]
        public double num { get; set; }

        [DisplayName("結果：")]
        public string result { get; set; }
    }
}