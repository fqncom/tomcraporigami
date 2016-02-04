using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using fqn_AbstractFactoryDemo.Bll;

namespace fqn_AbstractFactoryDemo.WebApp
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfoBll bll = new UserInfoBll();
            this.GridView1.DataSource = bll.GetUserInfoList();
            this.GridView1.DataBind();
        }
    }
}