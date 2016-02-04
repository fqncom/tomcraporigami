<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductScopeConfigManager.aspx.cs"
    Inherits="ProductScopeConfigManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/ProductScopeConfigManager.css" />
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
    <script src="js/ProductScopeConfigManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>销售管理</span>&nbsp;&gt;&nbsp;<span>商品范围配置管理</span></div>
    <br />
    <div class="tabbox">
        <div id="divProductScope">
            <table id="tbProductScopeList" class="list" cellpadding="0" cellspacing="0" style="width: 1000px;">
                <tr>
                    <th style="width: 150px;">
                        范围名称
                    </th>
                    <th style="width: 300px;">
                        包含范围
                    </th>
                    <th style="width: 300px;">
                        排除范围
                    </th>
                    <th style="width: 150px;">
                        备注
                    </th>
                    <th style="width: 100px;">
                        操作
                    </th>
                </tr>
            </table>
            <div id="divProductScopeEditor">
                <input type="hidden" id="hdnProductScopeID" value="0" />
                <ul class="SimpleEditor">
                    <li><span class="Header">范围名称</span><input type="text" id="txtProductScopeName" style="width: 300px;" /></li>
                    <li><span class="Header">备注</span><input type="text" id="txtRemark" style="width: 300px;" /></li>
                </ul>
            </div>
            <div class="clear">
            </div>
            <div style="margin-top: 10px;">
                <input type="button" value="新 建" style="width: 80px;" onclick="OnNewProductScope()" /><input
                    type="button" value="保 存" style="width: 80px; margin-left: 10px;" onclick="OnSaveProductScope()" /><input
                        type="button" value="刷 新" style="width: 80px; margin-left: 10px;" onclick="QueryProductScopeList()" /></div>
        </div>
        <div id="divProductScopeItem" class="hide">
            <table id="tbProductScopeItemList" class="list" cellpadding="0" cellspacing="0" style="width: 600px;">
                <tr>
                    <th style="width: 100px;">
                        范围方式
                    </th>
                    <th style="width: 200px;">
                        商品名称
                    </th>
                    <th style="width: 200px;">
                        商品类型
                    </th>
                    <th style="width: 100px;">
                        商品品牌
                    </th>
                    <th style="width: 100px;">
                        操作
                    </th>
                </tr>
            </table>
            <div class="clear">
            </div>
            <div id="divProductScopeItemEditor">
                <input type="hidden" id="hdnProductScopeItemID" value="0" />
                <ul class="SimpleEditor">
                    <li><span class="Header">范围方式</span><select id="sltProductScopeItemMode" style="width: 100px"></select></li>
                    <li><span class="Header">商品名称</span><input type="text" id="txtProductName" class="SelectorButton"
                        style="width: 300px;" onfocus="ProductSelector_Show(this);" readonly />
                        <input type="hidden" id="hdnProductID" value="0" /></li>
                    <li><span class="Header">商品类型</span><input type="text" id="txtProductCategoryName"
                        class="SelectorButton" style="width: 300px;" onfocus="ProductCategorySelector_Show(this);"
                        readonly />
                        <input type="hidden" id="hdnProductCategoryID" value="0" /></li>
                    <li><span class="Header">商品品牌</span><input type="text" id="txtProductBrandName" class="SelectorButton"
                        style="width: 300px;" onfocus="ProductBrandSelector_Show(this);" readonly />
                        <input type="hidden" id="hdnProductBrandID" value="0" /></li>
                </ul>
            </div>
            <div class="clear">
            </div>
            <div style="margin-top: 10px;">
                <input type="button" value="保 存" style="width: 80px;" onclick="OnSaveProductScopeItem()" /><input
                    type="button" value="重 填" style="width: 80px; margin-left: 10px;" onclick="ResetProductScopeItemEditor()" /><input
                        type="button" value="返回列表" style="width: 80px; margin-left: 10px;" onclick="OnBackToProductScope()" /></div>
        </div>
    </div>
</body>
</html>
