using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Web.Optimization;

namespace fqn_MVCByZeroStartDemo
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.MapPageRoute("HonmeRoute", "folder/{page}", "~/Index.aspx");
            RouteTable.Routes.MapRoute("list", "list/{id}", new { controller = "Home", action = "BegForAction" });//需要一个controller的参数，但是如果不使用默认值，就不需要
            RouteTable.Routes.MapRoute("default", "{controller}/{action}", new { controller = "Home", action = "BegForAction" });

            BundleTable.Bundles.Add(new StyleBundle("~/content/css").Include("~/Assests/css/PageBar.css", "~/Assests/css/tableStyle.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/content/script").Include("~/Assests/script/datapattern.js", "~/Assests/script/jquery-1.8.0.js"));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}