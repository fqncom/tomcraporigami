﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Common;

public partial class InventoryStockIn : BaseManagePage
{
    public override string GetPageName()
    {
        return "商品入库";
    }
}
