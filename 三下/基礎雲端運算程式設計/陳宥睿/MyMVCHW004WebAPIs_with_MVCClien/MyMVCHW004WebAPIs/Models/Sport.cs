using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCHW004WebAPIs.Models
{
	public class Sport
	{
		public int id { get; set; }
		public string location { get; set; }
		public int sportTime { get; set; }
		public string sportType { get; set; }
		public DateTime sportDate { get; set; }
	}
}