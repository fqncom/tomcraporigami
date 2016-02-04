<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchOrderPick.aspx.cs"
    Inherits="BatchOrderPick" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/BatchOrderPick.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/Basic.js" type="text/javascript"></script>

    <script src="js/Selector.js" type="text/javascript"></script>

    <script src="js/BatchOrderPick.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>订单管理</span>&nbsp;&gt;&nbsp;<span>订单拣货</span></div>
    <div class="toolbar">
        <ul>
            <li><span>批次号</span><input type="text" id="txtBatchNo" class="SelectorButton" style="width: 150px;" onfocus="BatchSelector_Show(this);"
                readonly /><input type="hidden" id="hdnBatchID" value="0" /></li>
            <li>
                <input type="button" value="查询订单商品" onclick="QueryBatchProductList();" style="width: 100px;" /></li>
            <li>
                <input type="button" value="订单拣货" onclick="BatchOrderPick();" style="width: 70px;" /></li>
            <li>
                <input type="button" value="打印" onclick="OnPrint();" style="width: 70px;" /></li>
        </ul>
    </div>
    <div id="divBatchProductList">
        <table id="tbBatchProductList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="15%">
                    商品编号
                </th>
                <th width="25%">
                    商品名称
                </th>
                <th width="10%">
                    商品单位
                </th>
                <th width="20%">
                    商品规格
                </th>
                <th width="15%">
                    总数量
                </th>
                <th width="15%">
                    总金额
                </th>
            </tr>
        </table>
    </div>
</body>
</html>
