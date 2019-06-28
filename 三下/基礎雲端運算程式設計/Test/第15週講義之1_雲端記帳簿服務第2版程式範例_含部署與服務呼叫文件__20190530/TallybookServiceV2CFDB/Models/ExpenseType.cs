using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallybookServiceV2CFDB.Models
{
    // 此為消費種類資料表紀錄之類別，Code First將根據這一類別建立ExpenseTypes資料表
    public class ExpenseType
    {   // 定義消費種類資料表紀錄之欄位名稱與資料型態
        public int id {get; set;}
        public string expenseType { get; set; }
    }
}