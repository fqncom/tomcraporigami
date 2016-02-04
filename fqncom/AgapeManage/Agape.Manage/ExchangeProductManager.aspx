<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExchangeProductManager.aspx.cs"
    Inherits="ExchangeProductManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/ExchangeProductManager.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/ExchangeProductManager.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>商品管理</span>&nbsp;&gt;&nbsp;<span>商品入库</span></div>
    <div id="divExchangeProductList">
        <div class="toolbar">
            <ul>
                <li><span>商品编号</span><input type="text" id="txtQueryProductNo" style="width: 100px;" /></li>
                <li><span>商品名称</span><input type="text" id="txtQueryProductName" style="width: 200px;" /></li>
                <li>
                    <input type="button" value="查询兑换商品" onclick="OnQueryExchangeProductList();" style="width: 90px;
                        margin-left: 20px;" /></li>
                <li>
                    <input type="button" value="新建兑换商品" onclick="OnNewExchangeProduct();" style="width: 90px;" /></li>
            </ul>
        </div>
        <table id="tbExchangeProductList" class="list" cellpadding="0" cellspacing="0">
            <tr>
                <th width="15%">
                    商品编号
                </th>
                <th width="20%">
                    商品名称
                </th>
                <th width="20%">
                    商品规格
                </th>
                <th width="10%">
                    商品单位
                </th>
                <th width="10%">
                    兑换积分值
                </th>
                <th width="15%">
                    操作
                </th>
            </tr>
        </table>
    </div>
    <div id="divExchangeProductEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" value="返回" onclick="BackToExchangeProductList();" style="width: 70px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnExchangeProductID" value="0" />
        <div id="divExchangeProduct">
            <div>
                <ul>
                    <li><span>商品编号</span><input type="text" id="txtProductNo" style="width: 150px;" /></li><li>
                        <span>条形码</span><input type="text" id="txtBarCode" style="width: 150px;" /></li><li>
                            <input type="button" id="btnQueryProduct" onclick="OnQueryProduct();" value="查询商品"
                                style="width: 100px;" /></li></ul>
            </div>
            <div>
                <ul>
                    <li><span>商品名称</span><input type="text" id="txtProductName" style="width: 150px;"
                        readonly /></li>
                    <li><span>商品规格</span><input type="text" id="txtProductSpec" style="width: 150px;"
                        readonly /></li>
                    <li><span>商品单位</span><input type="text" id="txtProductUnit" style="width: 150px;"
                        readonly /></li></ul>
                <input type="hidden" id="hdnProductID" value="0" />
            </div>
            <div style="margin-top:20px">
                <ul>
                    <li><span style="display: inline;">兑换积分值</span><input type="text" id="txtPointValue"
                        style="width: 100px;" /></li><li>
                            <input type="button" id="btnSaveExchangeProduct" onclick="OnSaveExchangeProduct();"
                                value="保存兑换商品" style="width: 100px;" /></li></ul>
            </div>
        </div>
    </div>
</body>
</html>
