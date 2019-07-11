using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMVCHW004Clien.ViewModels
{
	public class ManageSportTypesViewModel
	{
		[DisplayName("新的運動種類：")]
		[Required(ErrorMessage = "請輸入擬新增之運動種類!")]
		public string newSportType { get; set; }

		[DisplayName("運動種類編號：")]
		[Required(ErrorMessage = "請輸入擬新增之運動種類!")]
		public int no { get; set; }

		public string result { get; set; }
	}
}