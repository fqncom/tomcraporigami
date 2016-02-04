using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.WebApp.Models;

namespace fqn.ItcastOA.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new SimpleErrorFilter());
        }
    }
}