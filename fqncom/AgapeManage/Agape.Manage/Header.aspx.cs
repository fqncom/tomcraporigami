using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agape.Manage.Core.Session;

public partial class Header : System.Web.UI.Page
{
    protected string LoginName;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginName = OperatorSession.IsLogin ? OperatorSession.Operator.OperatorName : "未登录";
    }
}