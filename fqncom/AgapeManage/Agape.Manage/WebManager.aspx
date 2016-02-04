<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebManager.aspx.cs" Inherits="WebManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>状态管理器</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/WebManager.js" type="text/javascript"></script>

</head>
<body>
    <input type="button" id="btnUpdateTopProductList" value="更新排行榜列表" onclick="OnUpdateTopProductList()" />
    <input type="button" id="btnUpdateAllProductCategoryFullPath" value="更新所有商品类型全路径" onclick="OnUpdateAllProductCategoryFullPath()" />
    <input type="button" id="btnUpdateAllProductWordKey" value="更新所有商品检索词" onclick="OnUpdateAllProductWordKey()" />
</body>
</html>
