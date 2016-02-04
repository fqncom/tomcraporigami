<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Header.aspx.cs" Inherits="Header" %>

<%@ Import Namespace="Agape.Manage.Core.Session" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <style type="text/css">
        #bar
        {
            margin-top: 10px;
            margin-right: 10px;
            text-align: right;
            vertical-align: top;
        }
        ul li
        {
            display: inline;
            margin-right: 10px;
        }
        a
        {
            font-size: 18px;
            font-weight: bold;
            text-decoration: none;
            color: Orange;
            outline: none;
            font-family: 宋体;
            vertical-align: baseline;
            text-decoration: underline;
        }
        a:hover
        {
            text-decoration: underline;
            color: Red;
            outline: none;
        }
        span.LoginInfo
        {
            font-size: 18px;
            color: Red;
            font-weight: bolder;
        }
    </style>
</head>
<body style="background: url(images/admin_header_backgroud.jpg) repeat-x; margin-top: 0px;">
    <div id="bar">
        <ul>
            <li><span class="LoginInfo">
                <%=LoginName %>,欢迎您！</span></li>
            <li><a href="http://www.ibaby361.com" target="_blank">浏览首页</a></li><li><a href="http://www.ibaby361.com/RefreshCache.aspx"
                target="_blank">刷新内存</a></li><li><a href="Login.aspx?Code=Logout" target="_parent">退出管理台</a></li></ul>
    </div>
</body>
</html>
