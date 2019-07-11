using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMVCHW004Clien.ViewModels
{
	public class SaveSportRecordViewModel
	{
		[DisplayName("運動時數：")]
		[Required(ErrorMessage = "請輸入運動時數!")]
		public int sportTime { get; set; }

		[DisplayName("運動種類：")]
		[Required(ErrorMessage = "請選擇運動種類!")]
		public string sportType { get; set; }

		[DisplayName("運動地點：")]
		[Required(ErrorMessage = "請輸入運動地點!")]
		public string location { get; set; }

		[DisplayName("運動日期：")]
		[Required(ErrorMessage = "請輸入運動日期!")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime sportDate { get; set; }

		public string result { get; set; }
	}
}