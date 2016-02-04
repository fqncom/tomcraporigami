<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperatorManager.aspx.cs"
    Inherits="OperatorManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/OperatorManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/OperatorManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>基本管理</span>&nbsp;&gt;&nbsp;<span>商品管理</span></div>
    <div id="divOperatorList">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" value="新 建" onclick="OnNewOperator();" style="width: 70px;" /></li>
            </ul>
        </div>
        <div id="divOperatorQuery">
            <ul>
                <li><span>操作员编号</span><input type="text" id="txtOperatorNoQuery" /></li><li><span>操作员名称</span>
                    <input type="text" id="txtOperatorNameQuery" /></li><li>
                        <input type="button" id="btnOperatorList()" value="查询" onclick="OnQueryOperatorList();"
                            style="width: 80px;" /></li></ul>
        </div>
        <div class="clear">
        </div>
        <table id="tbOperatorList" cellpadding="0" cellspacing="0" class="list" style="width: 500px;">
            <tr>
                <th width="50px">
                    选择
                </th>
                <th width="100px">
                    操作员编号
                </th>
                <th width="200px">
                    操作员名字
                </th>
                <th width="150px">
                    操作
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    <div id="divOperatorEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" id="btnSaveOperator" value="保存" onclick="OnSaveOperator();"
                        style="width: 80px;" /></li>
                <li>
                    <input type="button" id="btnBackToOperatorList" value="返回" onclick="OnBackToOperatorList();"
                        style="width: 80px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnOperatorID" value="0" />
        <table style="width: 400px;">
            <tr>
                <td style="width: 100px; text-align: right; height:21px;">
                    操作员编号
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtOperatorNo" style="width: 150px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right; height:21px;">
                    操作员名字
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtOperatorName" style="width: 150px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right; height:21px;">
                    操作员密码
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtOperatorPassword" style="width: 150px;" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
