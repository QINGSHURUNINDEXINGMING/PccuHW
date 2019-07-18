using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace all.Models
{
    public class HISTORY
    {
        [Key]
        public int user_ID { get; set; }
        [Required]
        public string good { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public DateTime date { get; set; }
    }
}