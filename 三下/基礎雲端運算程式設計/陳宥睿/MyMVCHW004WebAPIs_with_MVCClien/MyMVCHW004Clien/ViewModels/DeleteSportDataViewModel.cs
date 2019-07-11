using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMVCHW004Clien.ViewModels
{
	public class DeleteSportDataViewModel
	{
		[DisplayName("起始日期：")]
		[Required(ErrorMessage = "請輸入起始日期!")]
		[DataType(DataType.Date)]
		public DateTime startDate { get; set; }

		[DisplayName("結束日期：")]
		[Required(ErrorMessage = "請輸入結束日期!")]
		[DataType(DataType.Date)]
		public DateTime endDate { get; set; }

		public string result { get; set; }
	}
}