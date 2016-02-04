using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fqn.ItcastOA.WebApp.Models
{
    public class SimpleErrorFilter : HandleErrorAttribute
    {
        public static Queue<Exception> ExQueue = new Queue<Exception>();

        public override void OnException(ExceptionContext filterContext)
        {
            ExQueue.Enqueue(filterContext.Exception);//入队操作
            HttpContext.Current.Response.Redirect("/error.html");
        }
    }
}