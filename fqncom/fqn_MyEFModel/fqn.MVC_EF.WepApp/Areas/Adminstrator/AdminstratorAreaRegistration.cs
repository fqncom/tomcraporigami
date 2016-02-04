using System.Web.Mvc;

namespace fqn.MVC_EF.WepApp.Areas.Adminstrator
{
    public class AdminstratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Adminstrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Adminstrator_default",
                "Adminstrator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Adminstrator_error",
                "Adminstrator/{*action}",
                new { action = "Error" }
            );
        }
    }
}
