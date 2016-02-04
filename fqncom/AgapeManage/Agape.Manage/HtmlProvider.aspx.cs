using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Leopard.Util;
using Leopard.Data;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Cache;

public partial class HtmlProvider : BaseManagePage
{
    private string m_HtmlCode;
    private string m_HtmlContent;

    protected void Page_Load(object sender, EventArgs e)
    {
        XReturn xSubReturn;
        m_HtmlContent = String.Empty;

        GetStringParameter("HtmlCode", String.Empty, out m_HtmlCode);

        switch (m_HtmlCode)
        {
            case "ProductCategoryTree":
                {
                    xSubReturn = GetProductCategoryTreeHtml();
                    break;
                }
        }

        Response.Clear();
        Response.Write(m_HtmlContent);
        Response.End();
    }

    private XReturn GetProductCategoryTreeHtml()
    {
        XReturn xReturn = new XReturn();

        m_HtmlContent = ProductCategoryCache.Current.GetProductCategoryTreeHtml(null);

        return xReturn.ReturnSuccess();
    }
}
