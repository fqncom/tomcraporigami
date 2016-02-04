using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MyBookShop.ashx
{
    /// <summary>
    /// AjaxUserLogin 的摘要说明
    /// </summary>
    public class AjaxUserLogin : IHttpHandler, IRequiresSessionState
    {
        protected string LoginIdCookie { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string transCode = context.Request["TransCode"] ?? "";
            if (transCode == "")
            {
                return;
            }
            switch (transCode)
            {
                case "CheckIsLogin":
                    string loginId = CheckIsLogin();
                    if (loginId != "")
                    {
                        context.Response.Write("success:" + loginId);
                    }
                    else
                    {
                        context.Response.Write("failed:未登入");
                    }
                    break;
                case "ConfirmUserLogin":
                    string userName = context.Request["UserName"] ?? "";
                    string userPwd = context.Request["UserPwd"] ?? "";
                    context.Response.Write(UserConfirmUserLogin(userName, userPwd));
                    break;
                case "LogOut":
                    UserLogOut();
                    break;
            }
        }

        //检测用户是否正确
        private string UserConfirmUserLogin(string userName, string userPwd)
        {
            string msg = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["loginId"];
            if (cookie != null)
            {
                this.LoginIdCookie = cookie.Value;
            }
            BLL.CommonBll.CheckUserLoginPwdAndGetCartCookie(userName, userPwd, cookie,out msg);
            return msg;
        }

        #region 被抛弃的登入方法

        //检测用户是否正确
        private bool UserConfirmUserLogin2(string userName, string userPwd)
        {
            BLL.UsersBll bll = new BLL.UsersBll();
            Model.Users user = bll.GetModelByLoginId(userName);
            if (user != null)
            {
                if (user.LoginPwd.Equals(Common.CommonTools.GetMd5String(Common.CommonTools.GetMd5String(userPwd))))
                {
                    HttpContext.Current.Session["userInfo"] = user;
                    return true;
                }
            }
            return false;
        }
        #endregion

        //注销登入
        private void UserLogOut()
        {
            HttpContext.Current.Session["userInfo"] = null;
            HttpContext.Current.Response.Write("success");
        }

        //检测用户是否登入
        private string CheckIsLogin()
        {
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                Model.Users user = HttpContext.Current.Session["userInfo"] as Model.Users;
                return user.LoginId;
            }
            return "";
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