using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBookShop
{
    public partial class FindMyPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string transCode = Request["TransCode"] ?? "";
            if (transCode == "")
            {
                return;
            }

            string loginId = Request["LoginId"] ?? "-1";
            string loginMail = Request["LoginMail"] ?? "";
            switch (transCode)
            {
                case "FindMyPassword":
                    if (CheckLoginInfo(loginId, loginMail))
                    {
                        SendEmailForFindPassword(loginId,loginMail);
                        Response.Write("success");
                    }
                    else
                    {
                        Response.Write("falied");
                    }

                    break;
                default:
                    break;
            }
            Response.End();

        }

        //发送邮件
        private void SendEmailForFindPassword(string loginId, string loginMail)
        {
            BLL.CheckEmailBll bll = new BLL.CheckEmailBll();
            
        }

        //检测用户名是否存在
        private bool CheckLoginInfo(string loginId, string loginMail)
        {
            BLL.UsersBll bll = new BLL.UsersBll();
            return bll.CheckUserMailByLoginId(loginId, loginMail);
        }
    }
}