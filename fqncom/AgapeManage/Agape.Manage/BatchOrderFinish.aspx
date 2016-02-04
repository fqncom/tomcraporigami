<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchOrderFinish.aspx.cs"
    Inherits="BatchOrderFinish" %>

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
    <link rel="stylesheet" type="text/css" href="css/BatchOrderFinish.css" />

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

    <script src="js/BatchOrderFinish.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>订单管理</span>&nbsp;&gt;&nbsp;<span>订单完成</span></div>
    <div id="divBatchOrderList">
        <div class="toolbar">
            <ul>
                <li><span>批次号</span><input type="text" id="txtBatchNo" class="SelectorButton" style="width: 150px;"
                    onfocus="BatchSelector_Show(this);" readonly /><input type="hidden" id="hdnBatchID"
                        value="0" /></li>
                <li><span>订单编号</span><input type="text" id="txtQueryOrderNo" style="width: 100px;" /></li>
                <li><span>条形码</span><input type="text" id="txtQueryBarCode" style="width: 100px;" /></li>
                <li>
                    <input type="button" value="查询订单" onclick="QueryBatchOrderList();" style="width: 70px;" /></li>
                <li>
                    <input type="button" value="订单完成" onclick="BatchFinishCheckedOrders();" style="width: 70px;" /></li></ul>
        </div>
        <table id="tbBatchOrderList" class="list" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <th width="10%">
                    选择
                </th>
                <th width="15%">
                    订单编号
                </th>
                <th width="20%">
                    下单时间
                </th>
                <th width="15%">
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
                <th width="10%">
                    处理结果
                </th>
            </tr>
        </table>
    </div>
</body>
</html>
