using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallybookServiceV2CFDB.Models
{
    // 此為記帳簿資料表紀錄之類別，Code First將根據這一類別建立Tallybooks資料表
    public class Tallybook
    {  // 定義記帳簿資料表紀錄之欄位名稱與資料型態
        public int id { get; set; }
        public int price { get; set; }
        public string expenseType { get; set; }
        public string comment { get; set; }
        public DateTime payDate { get; set; }
    }
}