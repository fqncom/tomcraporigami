<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchOrderPack.aspx.cs"
    Inherits="BatchOrderPack" %>

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
    <link rel="stylesheet" type="text/css" href="css/BatchOrderPack.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Basic.js" type="text/javascript"></script>
    <script src="js/Area.js" type="text/javascript"></script>
    <script src="js/Selector.js" type="text/javascript"></script>
    <script src="js/BatchOrderPack.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>订单管理</span>&nbsp;&gt;&nbsp;<span>订单打包</span></div>
    <div id="divBatchOrderList">
        <div class="toolbar">
            <ul>
                <li><span>批次号</span><input type="text" id="txtBatchNo" class="SelectorButton" style="width: 150px;"
                    onfocus="BatchSelector_Show(this);" readonly /><input type="hidden" id="hdnBatchID"
                        value="0" /></li>
                <li><span>订单编号</span><input type="text" id="txtQueryOrderNo" style="width: 100px;" /></li>
                <li>
                    <input type="button" value="查询订单" onclick="QueryBatchOrderList();" style="width: 70px;" /></li>
            </ul>
        </div>
        <table id="tbBatchOrderList" class="list" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <th width="15%">
                    订单编号
                </th>
                <th width="20%">
                    下单时间
                </th>
                <th width="20%">
                    会员
                </th>
                <th width="10%">
                    总数量
                </th>
                <th width="10%">
                    总金额
                </th>
                <th width="10%">
                    状态
                </th>
                <th width="15%">
                    操作
                </th>
            </tr>
        </table>
    </div>
    <div id="divBatchOrderEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" value="完成打包" onclick="BatchOrderPack();" style="width: 70px;" /></li>
                <li>
                    <input type="button" value="返回" onclick="BackToBatcOrderList();" style="width: 70px;" /></li>
                <li>
                    <input type="button" value="打印订单" onclick="OnPrint();" style="width: 70px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnBatchOrderID" value="0" />
        <table style="width: 800px;">
            <tr style="height: 30px;">
                <td style="width: 100px; text-align: right;">
                    订单编号
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtOrderNo" style="width: 280px; background-color: #E0E0E0;
                        height: 16px;" readonly />
                </td>
                <td style="width: 100px; text-align: right;">
                    下单时间
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtCreateTime" style="width: 280px; background-color: #E0E0E0;
                        height: 16px;" readonly />
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 100px; text-align: right;">
                    会员姓名
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtRealName" style="width: 280px; background-color: #E0E0E0;
                        height: 16px;" readonly />
                </td>
                <td style="width: 100px; text-align: right;">
                    送货地址
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtAddress" style="width: 280px; background-color: #E0E0E0;
                        height: 16px;" readonly />
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 100px; text-align: right;">
                    总数量
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtTotalQuantity" style="width: 280px; background-color: #E0E0E0;
                        height: 16px;" readonly />
                </td>
                <td style="width: 100px; text-align: right;">
                    总金额
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtTotalAmount" style="width: 280px; background-color: #E0E0E0;
                        height: 16px;" readonly />
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 100px; text-align: right;">
                    条形码
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtBarCode" style="width: 280px; background-color: #FFFFC0;
                        height: 16px;" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table id="tbBatchOrderItemList" class="list" cellpadding="0" cellspacing="0" width="100%">
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
                <th width="10%">
                    数量
                </th>
                <th width="10%">
                    金额
                </th>
                <th width="10%">
                    库存数量
                </th>
            </tr>
        </table>
    </div>
</body>
</html>
