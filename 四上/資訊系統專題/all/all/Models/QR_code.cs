using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace all.Models
{
    public class QR_code
    {
        [Key]
        public int user_ID { get; set; }
        [Required]
        public string QRcode { get; set; }
    }
}