<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InventoryProductStockQuery.aspx.cs"
    Inherits="InventoryProductStockQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/InventoryProductStockQuery.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/InventoryProductStockQuery.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>商品管理</span>&nbsp;&gt;&nbsp;<span>库存查询</span></div>
    <div class="toolbar">
        <ul>
            <li><span>商品编号</span><input type="text" id="txtQueryProductNo" style="width: 100px;" /></li>
            <li>
                <input type="button" value="查询" onclick="OnQuery();" style="width: 70px;" /></li>
        </ul>
    </div>
    <div id="divProductStockList">
        <table id="tbProductStockList" class="list" cellpadding="0" cellspacing="0">
            <tr>
                <th width="15%">
                    仓库
                </th>
                <th width="20%">
                    商品编号
                </th>
                <th width="20%">
                    商品名称
                </th>
                <th width="10%">
                    商品单位
                </th>
                <th width="20%">
                    商品规格
                </th>
                <th width="5%">
                    库存数量
                </th>
                <th width="5%">
                    冻结数量
                </th>
                <th width="5%">
                    可用数量
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    </form>
</body>
</html>
