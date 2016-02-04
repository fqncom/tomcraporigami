using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBookShop
{
    public partial class ShowMsg : System.Web.UI.Page
    {

        protected string UrlAddress { get; set; }
        protected string Msg { get; set; }
        protected string UrlName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string transCode = Request["TransCode"] ?? "";

            if (string.IsNullOrEmpty(transCode))
            {
                return;
            }

            this.UrlAddress = "Index.aspx";
            this.UrlName = "网站首页";
            this.Msg = "出错了";
            switch (transCode)
            {
                case "registerSuccess"://注册成功
                    this.UrlAddress = "Login.aspx";
                    this.UrlName = "登入界面";
                    this.Msg = "注册成功";
                    break;
                case "registerFailed"://注册失败
                    this.UrlAddress = "Register.aspx";
                    this.UrlName = "注册界面";
                    this.Msg = "注册失败";
                    break;
                case "activeCodeRight"://激活成功
                    this.UrlAddress = "Login.aspx";
                    this.UrlName = "登入界面";
                    this.Msg = "激活成功";
                    break;
                case "activeCodeWrong"://激活失败
                    this.UrlAddress = "Index.aspx";
                    this.UrlName = "网站首页";
                    this.Msg = "激活失败";
                    break;
                case "findMyPassword":
                    this.UrlAddress = "Login.aspx";
                    this.UrlName = "登入界面";
                    this.Msg = "修改密码链接已经发往您的邮箱，请前往邮箱进行下一步操作。";
                    break;
                case "changePwdSuccess":
                    this.UrlAddress = "Login.aspx";
                    this.UrlName = "登界面";
                    this.Msg = "修改密码成功";
                    break;
                case "changePwdFailed":
                    this.UrlAddress = "Index.aspx";
                    this.UrlName = "网站首页";
                    this.Msg = "修改密码失败";
                    break;
            }
        }
    }
}