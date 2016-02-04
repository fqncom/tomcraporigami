using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using Leopard.Util;
using Leopard.Cache;
using Leopard.Data;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Impl;
using Agape.Manage.Core.Session;
using Agape.Manage.Core.Cache;

public partial class WebManagerService : BaseServicePage
{
    #region 服务加载
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #endregion

    /// <summary>
    /// 更新商品排行榜列表。
    /// </summary>
    /// <returns></returns>
    public XReturn UpdateTopProductList()
    {
        return StatImpl.SubmitStatProductRankingList();
    }

    /// <summary>
    /// 更新所有商品类型全路径
    /// </summary>
    /// <returns></returns>
    public XReturn UpdateAllProductCategoryFullPath()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ProductCategoryCache.Current.UpdateAllProductCategoryFullPath();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "更新所有商品类型全路径失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 更新所有商品检索词
    /// </summary>
    /// <returns></returns>
    public XReturn UpdateAllProductWordKey()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ProductImpl.UpdateAllProductWordKey();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "更新所有商品检索词失败");
        }

        return xReturn.ReturnSuccess();
    }
}
