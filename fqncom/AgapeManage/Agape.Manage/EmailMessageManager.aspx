<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailMessageManager.aspx.cs"
    Inherits="EmailMessageManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/EmailMessageManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/EmailMessageManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>会员管理</span>&nbsp;&gt;&nbsp;<span>邮件管理</span></div>
    <div id="divEmailMessageList">
        <div class="toolbar">
            <ul>
                <li><span>标题</span><input type="text" id="txtTitle" style="width: 100px;" /></li>
                <li><span>收件人</span><input type="text" id="txtReceiver" style="width: 100px;" /></li>
                <li><span>开始日期</span><input type="text" id="txtFromDate" style="width: 100px;" /></li>
                <li><span>结束日期</span><input type="text" id="txtToDate" style="width: 100px;" /></li>
                <li>
                    <input type="button" value="查 询" onclick="OnQueryEmailMessageList();" style="width: 70px;" /></li>
                <li>
                    <input type="button" value="新 建" onclick="OnNewEmailMessage();" style="width: 70px;" /></li>
            </ul>
        </div>
        <table id="tbEmailMessageList" cellpadding="0" cellspacing="0" class="list" style="width: 1000px;">
            <tr>
                <th width="100px">
                    选择
                </th>
                <th width="100px;">
                    类型
                </th>
                <th width="300px;">
                    标题
                </th>
                <th width="100px;">
                    HTML格式
                </th>
                <th width="200px;">
                    发送时间
                </th>
                <th width="100px;">
                    状态
                </th>
                <th width="100px;">
                    操作
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    <div id="divEmailMessageEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" id="btnSaveEmailMessage" value="提 交" onclick="OnSubmitEmailMessage();"
                        style="width: 80px;" /></li>
                <li>
                    <input type="button" id="btnBackToEmailMessageList" value="返回列表" onclick="OnBackToEmailMessageList();"
                        style="width: 80px;" /></li>
            </ul>
        </div>
        <table style="width: 900px;">
            <tr>
                <td style="text-align: right; vertical-align: top;">
                    标题
                </td>
                <td style="text-align: left; padding-bottom: 10px;">
                    <input type="text" id="txtEmailMessageTitle" style="width: 800px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;">
                    是否HTML格式
                </td>
                <td style="text-align: left; padding-bottom: 10px;">
                    <input type="checkbox" id="ckbIsBodyHtml" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right; vertical-align: top;">
                    收件人列表
                </td>
                <td style="text-align: left; padding-bottom: 10px;">
                    <textarea id="txtReceiverList" rows="10" cols="100" style="width: 800px; height: 100px;"></textarea>
                    <span style="color: Red;">注意：多个收件人用分号分开</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;">
                    邮件内容
                </td>
                <td style="text-align: left;">
                    <textarea id="txtEmailMessageBody" rows="50" cols="120" style="width: 800px; height: 800px;"></textarea>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
