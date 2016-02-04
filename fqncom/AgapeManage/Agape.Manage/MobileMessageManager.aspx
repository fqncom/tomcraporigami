<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileMessageManager.aspx.cs"
    Inherits="MobileMessageManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/MobileMessageManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/MobileMessageManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>会员管理</span>&nbsp;&gt;&nbsp;<span>短信管理</span></div>
    <div id="divMobileMessageList">
        <div class="toolbar">
            <ul>
                <li><span>手机号码</span><input type="text" id="txtMobilePhone" style="width: 100px;" /></li>
                <li><span>短信内容</span><input type="text" id="txtMessageContent" style="width: 100px;" /></li>
                <li><span>开始日期</span><input type="text" id="txtFromDate" style="width: 100px;" /></li>
                <li><span>结束日期</span><input type="text" id="txtToDate" style="width: 100px;" /></li>
                <li>
                    <input type="button" value="查 询" onclick="OnQueryMobileMessageList();" style="width: 70px;" /></li>
                <li>
                    <input type="button" value="新 建" onclick="OnNewMobileMessage();" style="width: 70px;" /></li>
            </ul>
        </div>
        <table id="tbMobileMessageList" cellpadding="0" cellspacing="0" class="list" style="width: 1200px;">
            <tr>
                <th width="100px">
                    选择
                </th>
                <th width="100px;">
                    类型
                </th>
                <th width="100px;">
                    手机号码
                </th>
                <th width="400px;">
                    短信内容
                </th>
                <th width="200px;">
                    发送时间
                </th>
                <th width="100px;">
                    返回码
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
    <div id="divMobileMessageEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" id="btnSaveMobileMessage" value="提 交" onclick="OnSubmitMobileMessage();"
                        style="width: 80px;" /></li>
                <li>
                    <input type="button" id="btnBackToMobileMessageList" value="返回列表" onclick="OnBackToMobileMessageList();"
                        style="width: 80px;" /></li>
            </ul>
        </div>
        <table style="width: 900px;">
            <tr>
                <td style="width: 100px; text-align: right; vertical-align: top;">
                    接收号码列表
                </td>
                <td style="text-align: left; padding-bottom: 10px;">
                    <textarea id="txtReceiverList" rows="10" cols="100" style="width: 800px; height: 100px;"></textarea>
                    <span style="color: Red;">注意：多个接收号码用分号分开</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;">
                    短信内容
                </td>
                <td style="text-align: left;">
                    <textarea id="txtMessageContent" rows="50" cols="120" style="width: 800px; height: 100px;"></textarea>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
