<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpManager.aspx.cs"
    Inherits="HelpManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/HelpManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/HelpManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>咨询管理</span>&nbsp;&gt;&nbsp;<span>帮助管理</span></div>
    <div id="divHelpEditor">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" id="btnSaveHelpItem" value="保存" onclick="OnSaveHelpItem();"
                        style="width: 80px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnHelpID" value="0" />
        <table style="width: 900px;">
            <tr>
                <td style="width: 100px; text-align: right;">
                    帮助名称
                </td>
                <td style="text-align: left;">
                    <select id="sltHelpItem" style="width: 200px;" onchange="OnHelpItemSelected()">
                        <option value="">请选择</option>
                        <%=m_HelpItemOptionListHtml %>
                    </select>
                </td>
                <td style="width: 100px; text-align: right;">
                </td>
                <td style="text-align: left; width: 300px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;">
                    帮助内容
                </td>
                <td style="text-align: left;" colspan="3">
                    <textarea id="txtHelpContent" rows="50" cols="120" style="width: 800px; height: 800px;"
                        class="tinymce"></textarea>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
