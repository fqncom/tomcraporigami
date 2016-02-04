using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Session;

public partial class Login : BaseManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MainEntry();        
    }

    public override bool IsCheckLogin()
    {
        return false;
    }

    public override string GetPageName()
    {
        return "登录界面";
    }

    /// <summary>
    /// 页面处理主流程。
    /// </summary>
    protected void MainEntry()
    {
        ParseParameters();    

        CheckProcess();
    }

    /// <summary>
    /// 解析请求参数。
    /// </summary>
    protected XReturn ParseParameters()
    {
        XReturn xReturn = new XReturn();

        string Code;
        GetStringParameter("Code", String.Empty, out Code);

        if (Code.ToLower().Trim().Equals("logout"))
        {
            if (OperatorSession.IsLogin)
            {
                OperatorSession.Logout();
            }
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 检查处理。
    /// </summary>
    /// <returns></returns>
    protected XReturn CheckProcess()
    {
        XReturn xReturn = new XReturn();

        if (OperatorSession.IsLogin)
        {
            Redirect("MainFrame.htm");
        }

        return xReturn.ReturnSuccess();
    }
}
