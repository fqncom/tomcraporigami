<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleManager.aspx.cs"
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
    <link rel="Stylesheet" type="text/css" href="css/RoleManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/RoleManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>基本管理</span>&nbsp;&gt;&nbsp;<span>商品管理</span></div>
    <div id="divRoleList">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" value="新 建" onclick="OnNewRole();" style="width: 70px;" /></li>
            </ul>
        </div>
        <div id="divRoleQuery">
            <ul>
                <li><span>角色代码</span><input type="text" id="txtRoleNoQuery" /></li><li><span>角色名称</span>
                    <input type="text" id="txtRoleNameQuery" /></li><li>
                        <input type="button" id="btnRoleList()" value="查询" onclick="OnQueryRoleList();" style="width: 80px;" /></li></ul>
        </div>
        <table id="tbRoleList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="15%">
                    角色代码
                </th>
                <th width="25%">
                    角色名称
                </th>
                <th width="25%">
                    状态
                </th>
                <th width="25%">
                    备注
                </th>
                <th width="10%">
                    操作
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    <div id="divRoleEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" id="btnSaveRole" value="保存" onclick="OnSaveRole();" style="width: 80px;" /></li>
                <li>
                    <input type="button" id="btnBackToRoleList" value="返回" onclick="OnBackToRoleList();"
                        style="width: 80px;" /></li>
            </ul>
        </div>
        <%--        <input type="hidden" id="hdnRoleID" value="0" />--%>
        <table style="width: 900px;">
            <tr>
                <td style="width: 100px; text-align: right;">
                    角色代码
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtRoleCode" style="width: 280px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right;">
                    角色名称
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtRoleName" style="width: 280px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right;">
                    状态
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtRoleStatus" style="width: 280px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right;">
                    备注
                </td>
                <td style="text-align: left; width: 300px;">
                    <input type="text" id="txtRoleRemark" style="width: 280px;" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
