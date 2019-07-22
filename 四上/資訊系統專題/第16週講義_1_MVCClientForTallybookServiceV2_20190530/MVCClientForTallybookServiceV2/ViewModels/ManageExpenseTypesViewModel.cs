using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCClientForTallybookServiceV2.ViewModels
{
    public class ManageExpenseTypesViewModel
    {
        [DisplayName("新的消費種類：")]
        [Required(ErrorMessage = "請輸入擬新增之消費種類!")]
        public string newExpenseType { get; set; }

        [DisplayName("消費種類編號：")]
        [Required(ErrorMessage = "請輸入擬新增之消費種類!")]
        public int no { get; set; }

        public string result { get; set; }
    }
}