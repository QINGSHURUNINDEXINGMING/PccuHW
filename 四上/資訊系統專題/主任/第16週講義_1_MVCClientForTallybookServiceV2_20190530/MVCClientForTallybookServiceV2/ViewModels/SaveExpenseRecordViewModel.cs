using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCClientForTallybookServiceV2.ViewModels
{
    public class SaveExpenseRecordViewModel
    {
        [DisplayName("消費金額：")]
        [Required(ErrorMessage = "請輸入消費金額!")]
        public int price { get; set; }

        [DisplayName("消費種類：")]
        [Required(ErrorMessage = "請選擇消費種類!")]
        public string expenseType { get; set; }

        [DisplayName("消費說明：")]
        [Required(ErrorMessage = "請輸入消費說明!")]
        public string comment { get; set; }

        [DisplayName("消費日期：")]
        [Required(ErrorMessage = "請輸入消費日期!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public  DateTime payDate { get; set; }

        public string result { get; set; }
    }
}