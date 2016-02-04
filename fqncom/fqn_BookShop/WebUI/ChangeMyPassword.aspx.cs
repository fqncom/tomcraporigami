using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBookShop
{
    public partial class ChangeMyPassword : System.Web.UI.Page
    {
        protected string LoginId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string loginId = Request["loginId"] ?? "";
            if (loginId == "")
            {
                return;
            }
            this.LoginId = loginId;

            string loginPwd = Request["newPassword"] ?? "";
            if (loginPwd == "")
            {
                return;
            }
            BLL.UsersBll bll = new BLL.UsersBll();
            Model.Users user = bll.GetModelByLoginId(loginId);
            user.LoginPwd = Common.CommonTools.GetMd5String(Common.CommonTools.GetMd5String(loginPwd));
            if (bll.Update(user))
            {
                Response.Redirect("ShowMsg.aspx?TransCode=changePwdSuccess");
            }
            else
            {
                Response.Redirect("ShowMsg.aspx?TransCode=changePwdFailed");
            }


        }
    }
}