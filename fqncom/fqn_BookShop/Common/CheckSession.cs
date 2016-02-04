using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShop.Common
{
    public class CheckSession : System.Web.UI.Page
    {
        /// <summary>
        /// 检查用户是否登入，如果未登入则跳转到登入界面
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            if (Session["userInfo"] == null)
            {
                Common.CommonTools.PageRedirectToLogin();
            }
            else
            {
                if (Request.Url.ToString().ToLower().Contains("login.aspx"))
                {
                    Response.Redirect("Index.aspx");
                }
            }
            base.OnInit(e);
        }
    }
}
