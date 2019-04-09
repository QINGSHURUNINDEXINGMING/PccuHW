using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class BMIViewModel
    {
        [DisplayName("身高：")]
        [Required(ErrorMessage = "請輸入身高")]
        public double Num1 { get; set; }

        [DisplayName("體重：")]
        [Required(ErrorMessage = "請輸入體重")]
        public double Num2 { get; set; }

        [DisplayName("結  果：")]
        public string Result { get; set; }
    }
}