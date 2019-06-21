using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW03_MVC.ViewModels
{
    public class BMIViewModel
    {
        [DisplayName("身高：")]
        [Required(ErrorMessage = "請輸入身高")]
        public double CM { get; set; }

        [DisplayName("體重：")]
        [Required(ErrorMessage = "請輸入體重")]
        public double KG { get; set; }

        [DisplayName("結果：")]
        public string Result { get; set; }
    }
}