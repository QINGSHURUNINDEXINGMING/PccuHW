using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Testtb
    {
        [Key]
        public int id { get; set; }
        [Required]
        public double price { get;  set;}
        [Required]
        public string type { get; set; }
        [Required]
        public string comment { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
    }
}