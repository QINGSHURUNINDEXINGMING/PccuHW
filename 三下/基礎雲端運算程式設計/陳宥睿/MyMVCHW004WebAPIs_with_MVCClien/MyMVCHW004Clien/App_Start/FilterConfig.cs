using System.Web;
using System.Web.Mvc;

namespace MyMVCHW004Clien
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
