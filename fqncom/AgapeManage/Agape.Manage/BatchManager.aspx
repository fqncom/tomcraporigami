<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchManager.aspx.cs"
    Inherits="BatchManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/BatchManager.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/Basic.js" type="text/javascript"></script>

    <script src="js/BatchManager.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>订单管理</span>&nbsp;&gt;&nbsp;<span>批次管理</span></div>
    <div class="toolbar">
        <ul>
            <li><span>批次号</span><input type="text" id="txtBatchNo" style="width: 150px;" /></li>
            <li><span>开始日期</span><input type="text" id="txtFromDate" style="width: 100px;" /></li>
            <li><span>结束日期</span><input type="text" id="txtToDate" style="width: 100px;" /></li>
            <li>
                <input type="checkbox" id="ckbHasOrder" checked /><span>有订单</span></li>
            <li>
                <input type="button" value="查询批次" onclick="OnQueryBatchList();" style="width: 70px;" /></li>
        </ul>
    </div>
    <div id="divBatchList">
        <table id="tbBatchList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="3%" rowspan="2">
                    选择
                </th>
                <th width="7%" rowspan="2">
                    批次号
                </th>
                <th width="5%" rowspan="2">
                    批次日期
                </th>
                <th width="5%" rowspan="2">
                    批次序号
                </th>
                <th width="10%" rowspan="2">
                    开始时间
                </th>
                <th width="10%" rowspan="2">
                    结束时间
                </th>
                <th width="10%" colspan="3">
                    订单确认
                </th>
                <th width="10%" colspan="3">
                    订单拣货
                </th>
                <th width="10%" colspan="3">
                    订单打包
                </th>
                <th width="10%" colspan="3">
                    订单配送
                </th>
                <th width="10%" colspan="3">
                    订单完成
                </th>
                <th width="5%" rowspan="2">
                    状态
                </th>
                <th width="5%" rowspan="2">
                    操作
                </th>
            </tr>
            <tr>
                <th width="3%">
                    订单数
                </th>
                <th width="3%">
                    总数量
                </th>
                <th width="4%">
                    总金额
                </th>
                <th width="3%">
                    订单数
                </th>
                <th width="3%">
                    总数量
                </th>
                <th width="4%">
                    总金额
                </th>
                <th width="3%">
                    订单数
                </th>
                <th width="3%">
                    总数量
                </th>
                <th width="4%">
                    总金额
                </th>
                <th width="3%">
                    订单数
                </th>
                <th width="3%">
                    总数量
                </th>
                <th width="4%">
                    总金额
                </th>
                <th width="3%">
                    订单数
                </th>
                <th width="3%">
                    总数量
                </th>
                <th width="4%">
                    总金额
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    </form>
</body>
</html>
