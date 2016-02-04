<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/Login.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/Login.js" type="text/javascript"></script>

</head>
<body>
    <div id="divBody">
        <div id="divTitle">
            <p>
                欢迎使用网上购物商城后台管理平台</p>
        </div>
        <div id="divLogin">
            <ul>
                <li class="content"><span>登录名</span><input type="text" id="txtLoginName" /></li>
                <li class="content"><span>登录密码</span><input type="password" id="txtLoginPassword" /></li>
                <li class="submit">
                    <input type="button" id="btnLogin" onclick="OnLogin();" value="登录" style="width: 70px;" /></li>
            </ul>
        </div>
    </div>
</body>
</html>
