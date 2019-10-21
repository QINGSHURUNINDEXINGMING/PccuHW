using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A6409001_WebPD_Week7.Models
{
    public class Student
    {
        [Display(Name="學號")]
        public int ID { get; set; }
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Display(Name = "國文")]
        public int Chinese { get; set; }
        [Display(Name = "英文")]
        public int English { get; set; }
        [Display(Name = "數學")]
        public int Math { get; set; }
        [Display(Name = "總分")]
        public int Total { get; set; }
    }
}