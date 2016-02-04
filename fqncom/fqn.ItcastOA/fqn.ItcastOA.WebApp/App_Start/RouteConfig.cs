﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace fqn.ItcastOA.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Manager", action = "MainIndex", id = UrlParameter.Optional }
            );
            //routes.Add(new Route("Default", new MvcRouteHandler())
            //{
            //    Defaults = new RouteValueDictionary(new { controller = "Home", action = "Index", id = "" }),
            //});

        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//namespace TestMVCArch
//{
//    public class GlobalApplication : System.Web.HttpApplication
//    {
//        public static void RegisterRoutes(RouteCollection routes)
//        {
//            // Note: Change the URL to "{controller}.mvc/{action}/{id}" to enable              
//            //automatic support on IIS6 and IIS7 classic mode  
//            routes.Add(new Route("{controller}/{action}/{id}", new MvcRouteHandler())
//            {
//                Defaults = new RouteValueDictionary(new { action = "Index", id = "" }),
//            });
//            routes.Add(new Route("Default.aspx", new MvcRouteHandler())
//            {
//                Defaults = new RouteValueDictionary(new { controller = "Home", action = "Index", id = "" }),
//            });
//        }
//        protected void Application_Start(object sender, EventArgs e)
//        {

//            RegisterRoutes(RouteTable.Routes);
//        }
//    }
//}