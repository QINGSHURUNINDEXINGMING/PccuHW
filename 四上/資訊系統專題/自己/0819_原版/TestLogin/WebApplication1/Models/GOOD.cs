using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GOOD
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "商品名稱")]
        public string UID { get; set; }
        [Required]
        [Display(Name = "商品UID")]
        public string name { get; set; }
        [Required]
        [Display(Name = "價格")]
        public int price { get; set; }
        [Required]
        [Display(Name = "折價(打幾折)")]
        public int discount { get; set; }
        [Required]
        [Display(Name = "折價後的價格")]
        public int discount_price { get; set; }
    }
}