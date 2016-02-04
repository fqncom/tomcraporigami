using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Common;

public partial class HelpManager : BaseManagePage
{
    protected string m_HelpItemOptionListHtml;

    protected void Page_Load(object sender, EventArgs e)
    {
        MainEntry();
    }

    public override string GetPageName()
    {
        return "帮助管理";
    }

    /// <summary>
    /// 页面处理主流程。
    /// </summary>
    protected void MainEntry()
    {
        FillHelpItemOptionListHtml();
    }

    protected XReturn FillHelpItemOptionListHtml()
    {
        XReturn xReturn = new XReturn();
        XmlDocument m_xDoc;
        string _HelpItemOptionListHtml = String.Empty;

        string ConfigFilePath = LeopardConfigs.GetAbsolutePath("config\\HelpMenu.xml");

        try
        {
            m_xDoc = new XmlDocument();
            m_xDoc.Load(ConfigFilePath);
        }
        catch (Exception e)
        {
            string strErrMsg = string.Format("打开配置文件[{0:S}]失败,错误信息[{1:S}]", ConfigFilePath, e.Message);
            return xReturn.ReturnError(strErrMsg);
        }

        XmlElement xMenuListElement = (XmlElement)m_xDoc.SelectSingleNode("/HelpList");
        foreach (XmlElement xMenuGroupElement in xMenuListElement.ChildNodes)
        {
            string MenuGroupName = xMenuGroupElement.GetAttribute("HelpName");

            foreach (XmlElement xMenuItemElement in xMenuGroupElement.ChildNodes)
            {
                string MenuItemCode = xMenuItemElement.GetAttribute("HelpCode");
                string MenuItemName = xMenuItemElement.GetAttribute("HelpName");
                _HelpItemOptionListHtml += string.Format("<option value='{0:S}'>{2:S} - {1:S}</option>", MenuItemCode, MenuItemName, MenuGroupName);
            }
        }

        m_HelpItemOptionListHtml = _HelpItemOptionListHtml;

        return xReturn.ReturnSuccess();
    }
}
