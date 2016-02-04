using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsonpDemo2
{
    /// <summary>
    /// NormalRequest 的摘要说明
    /// </summary>
    public class NormalRequest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            string functionName = context.Request["callback"];
            string id = context.Request["Id"] ?? "";
            string name = context.Request["Name"] ?? "";
            string returnData = id + name + System.DateTime.Now.ToString();
            context.Response.Write(string.Format("{0}('{1}')", functionName,returnData));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}