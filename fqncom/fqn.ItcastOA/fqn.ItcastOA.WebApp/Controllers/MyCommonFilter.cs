using fqn.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class MyCommonFilter : ActionFilterAttribute
    {
        public string Msg { get; set; }
        /// <summary>
        /// 自定义过滤器方法，在方法执行之前进行过滤,或者对整个类进行过滤
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Result == null)
            {
                filterContext.HttpContext.Response.Write("Result has nothing " + this.Msg);
            }
            else
            {
                filterContext.HttpContext.Response.Write("Result has something " + this.Msg);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}