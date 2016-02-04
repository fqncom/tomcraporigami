<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberManager.aspx.cs"
    Inherits="MemberManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/MemberManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Basic.js" type="text/javascript"></script>
    <script src="js/MemberManager.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>会员管理</span>&nbsp;&gt;&nbsp;<span>会员查询</span></div>
    <div class="toolbar">
        <ul>
            <li><span>会员编号</span><input type="text" id="txtMemberNo" style="width: 100px;" /></li>
            <li><span>会员名称</span><input type="text" id="txtMemberName" style="width: 100px;" /></li>
            <li>
                <input type="button" value="查询" onclick="OnQuery();" style="width: 70px;" /></li>
            <li>
                <input type="button" value="删除会员" onclick="OnDeleteMember();" style="width: 70px;" /></li>
        </ul>
    </div>
    <div id="divMemberList">
        <table id="tbMemberList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th style="width:60px;">
                    选择
                </th>
                <th style="width:100px;">
                    会员编号
                </th>
                <th style="width:100px;">
                    会员名
                </th>
                <th style="width:100px;">
                    真实姓名
                </th>
                <th style="width:60px;">
                    性别
                </th>
                <th style="width:60px;">
                    年龄
                </th>
                <th style="width:100px;">
                    生日
                </th>
                <th style="width:100px;">
                    手机
                </th>
                <th style="width:100px;">
                    邮箱
                </th>
                <th style="width:60px;">
                    积分
                </th>
                <th style="width:60px;">
                    兑换积分
                </th>
                <th style="width:120px;">
                    创建日期
                </th>
                <th style="width:60px;">
                    状态
                </th>
                <th style="width:100px;">
                    操作
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    </form>
</body>
</html>
