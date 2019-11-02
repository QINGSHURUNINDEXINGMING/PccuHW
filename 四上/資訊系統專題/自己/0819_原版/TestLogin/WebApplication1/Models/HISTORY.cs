using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HISTORY
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "商品名稱")]
        public string GName { get; set; }
        [Required]
        [Display(Name = "商品UID")]
        public string GUID { get; set; }
        [Required]
        [Display(Name = "用戶")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "時間")]
        public string time { get; set; }
        [Required]
        [Display(Name = "價格")]
        public int price { get; set; }
    }
}