<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductHintConfigManager.aspx.cs"
    Inherits="ProductHintConfigManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/ProductHintConfigManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Basic.js" type="text/javascript"></script>
    <script src="js/Selector.js" type="text/javascript"></script>
    <script src="js/ProductHintConfigManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>销售管理</span>&nbsp;&gt;&nbsp;<span>商品提示设置管理</span></div>
    <br />
    <div id="divProductHint">
        <table id="tbProductHintList" class="list" cellpadding="0" cellspacing="0" style="width: 900px;">
            <tr>
                <th style="width: 100px;">
                    操作
                </th>
                <th style="width: 400px;">
                    商品范围
                </th>
                <th style="width: 200px;">
                    提示图片代码
                </th>
                <th style="width: 200px;">
                    提示标题
                </th>
            </tr>
        </table>
        <div class="clear">
        </div>
        <div id="divProductHintEditor">
            <input type="hidden" id="hdnProductHintID" value="0" />
            <ul class="SimpleEditor">
                <li><span class="Header">商品范围</span><input type="text" id="txtProductScopeName"
                    class="SelectorButton" style="width: 300px;" onfocus="ProductScopeSelector_Show(this);"
                    readonly />
                    <input type="hidden" id="hdnProductScopeID" value="0" /></li>
                <li><span class="Header">提示图片代码</span><input type="text" id="txtHintImageCode" style="width: 160px;" /></li>
                <li><span class="Header">提示标题</span><input type="text" id="txtHintTitle" style="width: 160px;" /></li>
                <li>
                    <input type="button" value="增 加" style="width: 60px; margin-left: 80px;" onclick="OnSaveProductHint()" /></li>
            </ul>
        </div>
    </div>
</body>
</html>
