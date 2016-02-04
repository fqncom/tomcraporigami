<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairManager.aspx.cs"
    Inherits="RepairManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/RepairManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Basic.js" type="text/javascript"></script>
    <script src="js/RepairManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>会员管理</span>&nbsp;&gt;&nbsp;<span>返修退换货管理</span></div>
    <div class="toolbar">
        <ul>
            <li><span>返修号</span><input type="text" id="txtRepairNo" style="width: 100px;" /></li>
            <li><span>开始申请日期</span><input type="text" id="txtFromDate" style="width: 100px;" /></li>
            <li><span>结束申请日期</span><input type="text" id="txtToDate" style="width: 100px;" /></li>
            <li><span>返修状态</span><select id="sltRepairStatus" style="width: 100px"></select></li>
            <li>
                <input type="button" value="查询返修" onclick="QueryRepairList();" style="width: 70px;
                    margin-left: 10px;" /></li>
            <li>
                <input type="button" value="审核通过" onclick="OnVerifySuccess();" style="width: 70px;" /></li>
            <li>
                <input type="button" value="审核失败" onclick="OnVerifyFail();" style="width: 70px;" /></li>
            <li>
                <input type="button" value="商品处理" onclick="OnProductRepair();" style="width: 70px;" /></li>
            <li>
                <input type="button" value="商品配送" onclick="OnProductDelivery();" style="width: 70px;" /></li>
            <li>
                <input type="button" value="处理完成" onclick="OnRepairFinish();" style="width: 70px;" /></li>
            <li>
                <input type="button" value="客户确认" onclick="OnCustomerConfirm();" style="width: 70px;" /></li>
        </ul>
    </div>
    <div id="divRepairList">
        <table id="tbRepairList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="5%">
                    选择
                </th>
                <th width="13%">
                    返修编号
                </th>
                <th width="10%">
                    申请时间
                </th>
                <th width="10%">
                    会员
                </th>
                <th width="15%">
                    商品
                </th>
                <th width="20%">
                    问题描述
                </th>
                <th width="10%">
                    外观和包装情况
                </th>
                <th width="7%">
                    状态
                </th>
                <th width="10%">
                    处理情况
                </th>
            </tr>
        </table>
    </div>
</body>
</html>
