using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW2.Models
{
    public class HWTB
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string comment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
    }
}