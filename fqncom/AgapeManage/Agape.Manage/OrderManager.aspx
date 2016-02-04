<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderManager.aspx.cs"
    Inherits="OrderManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/OrderManager.css" />

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

    <script src="js/Area.js" type="text/javascript"></script>

    <script src="js/OrderManager.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>订单管理</span>&nbsp;&gt;&nbsp;<span>订单查询</span></div>
    <div id="divOrderList">
        <div class="toolbar">
            <ul>
                <li><span>批次号</span><input type="text" id="txtBatchNo" class="SelectorButton" style="width: 150px;"
                    onfocus="BatchSelector_Show(this);" readonly /><input type="hidden" id="hdnBatchID"
                        value="0" /></li>
                <li><span>订单号</span><input type="text" id="txtOrderNo" style="width: 150px;" /></li>
                <li>
                    <input type="button" value="查询订单" onclick="OnQueryOrderList();" style="width: 70px;" /></li>
            </ul>
        </div>
        <table id="tbOrderList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="5%">
                    选择
                </th>
                <th width="15%">
                    批次号
                </th>
                <th width="15%">
                    订单编号
                </th>
                <th width="15%">
                    下单时间
                </th>
                <th width="10%">
                    会员姓名
                </th>
                <th width="10%">
                    总数量
                </th>
                <th width="10%">
                    总金额
                </th>
                <th width="5%">
                    状态
                </th>
                <th width="5%">
                    批状态
                </th>
                <th width="10%">
                    操作
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    <div id="divOrderView" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" value="返回" onclick="BackToOrderList();" style="width: 70px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnOrderID" value="0" />
        <div id="divOrder">
            <ul>
                <li><span class="title">订单编号</span><span class="content OrderNo"></span></li>
                <li><span class="title">条形码</span><span class="content BarCode"></span></li>
                <li><span class="title">会员姓名</span><span class="content MemberName"></span></li>
                <li><span class="title">下单时间</span><span class="content CreateTime"></span></li>
                <li><span class="title">总数量</span><span class="content TotalQuantity"></span></li>
                <li><span class="title">总金额</span><span class="content TotalAmount"></span></li>
                <li><span class="title">收货人</span><span class="content ReceiverName"></span></li>
                <li><span class="title">手机</span><span class="content MobilePhone"></span></li>
                <li><span class="title">地址</span><span class="content FullAddress"></span></li>
            </ul>
        </div>
        <table id="tbOrderItem" cellpadding="0" cellspacing="0" class="list" style="width: 710px;">
            <tr>
                <th style="width: 100px;">
                    商品编号
                </th>
                <th style="width: 250px;">
                    商品名称
                </th>
                <th style="width: 150px;">
                    商品规格
                </th>
                <th style="width: 100px;">
                    商品单位
                </th>
                <th style="width: 100px;">
                    价格
                </th>
                <th style="width: 100px;">
                    数量
                </th>
                <th style="width: 100px;">
                    金额
                </th>
            </tr>
        </table>
    </div>
</body>
</html>
