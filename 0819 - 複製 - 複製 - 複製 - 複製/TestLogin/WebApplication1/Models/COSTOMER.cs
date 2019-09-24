using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class COSTOMER
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        public string UID { get; set; }

        [Required]
        public int wallet { get; set; }

        //[Required]
        //public int deposit { get; set; }

        //[Required]
        //public int debt { get; set; }

    }
}