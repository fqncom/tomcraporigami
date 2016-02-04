using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookShop.ashx
{
    /// <summary>
    /// CheckActiveCodePage 的摘要说明
    /// </summary>
    public class CheckActiveCodePage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int userId = Convert.ToInt32(context.Request["id"] ?? "-1");
            string activeCode = context.Request["activeCode"] ?? "";
            if (activeCode == "")
            {
                return;
            }
            BLL.CheckEmailBll bll = new BLL.CheckEmailBll();
            if (bll.CheckActiveCode(userId, activeCode))
            {
                context.Response.Redirect("ShowMsg.aspx?TransCode=activeCodeRight");
            }
            context.Response.Redirect("ShowMsg.aspx?TransCode=activeCodeWrong");
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