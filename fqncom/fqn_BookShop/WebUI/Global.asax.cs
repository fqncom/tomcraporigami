using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MyBookShop
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        //在此处进行url重写
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string url = Request.AppRelativeCurrentExecutionFilePath ?? "";
            Match match = Regex.Match(url, @"Index_(\d+).aspx");
            if (match.Success)
            {
                Context.RewritePath(string.Format("Index.aspx?pageIndex={0}", match.Groups[1].Value));
            }
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