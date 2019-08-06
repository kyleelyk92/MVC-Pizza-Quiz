using System.Web;
using System.Web.Mvc;

namespace Quiz_August6_KyleElyk
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
