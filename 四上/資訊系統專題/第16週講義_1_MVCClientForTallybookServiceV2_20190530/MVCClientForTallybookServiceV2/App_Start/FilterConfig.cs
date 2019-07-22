using System.Web;
using System.Web.Mvc;

namespace MVCClientForTallybookServiceV2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
