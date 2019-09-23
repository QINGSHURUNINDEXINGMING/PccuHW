using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace A6409001_WebPD_Week3.Models
{
    public class A6409001_Week3
    {
    }

    public class Employee
    {
        [Display(Name="編號")]
        public int ID { set; get;}
        [Display(Name = "員工姓名")]
        public string Name { set; get; }
        [Display(Name = "員工電話")]
        public string Phone { set; get; }
        [Display(Name = "電子郵件")]
        public string Email { set; get; }
    }
}