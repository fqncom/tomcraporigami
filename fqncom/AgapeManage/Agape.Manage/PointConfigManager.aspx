<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PointConfigManager.aspx.cs"
    Inherits="PointConfigManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/PointConfigManager.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/Selector.js" type="text/javascript"></script>

    <script src="js/PointConfigManager.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>商品管理</span>&nbsp;&gt;&nbsp;<span>积分配置管理</span></div>
    <br />
    <div class="tabmenu">
        <ul>
            <li class="selected">全局积分配置</li>
            <li>商品类型积分配置</li>
            <li>商品积分配置</li>
        </ul>
    </div>
    <div class="tabbox">
        <div id="divPointConfig" class="tabitem">
            <div id="divPointConfigEditor">
                <ul class="SimpleEditor">
                    <li><span class="Header">积分消费系数</span><input type="text" id="txtPointValue" style="width: 80px;" /><span>注：获得一个积分需要的消费金额</span></li>
                    <li><span class="Header">最小消费积分金额</span><input type="text" id="txtMinAmountToPoint"
                        style="width: 80px;" /><span>注：大于此金额的交易才算积分</span></li>
                    <li>
                        <input type="button" value="保 存" onclick="OnSavePointConfig()" style="width: 60px;
                            margin-left: 100px;" /></li>
                </ul>
            </div>
        </div>
        <div class="tabitem hide">
            <table id="tbProductCategoryPointConfigList" class="list" cellpadding="0" cellspacing="0"
                style="width: 500px;">
                <tr>
                    <th style="width: 200px;">
                        商品类型
                    </th>
                    <th style="width: 100px;">
                        积分模式
                    </th>
                    <th style="width: 100px;">
                        积分值
                    </th>
                    <th style="width: 100px;">
                        操作
                    </th>
                </tr>
            </table>
            <div id="divProductCategoryPointConfigEditor">
                <ul class="SimpleEditor">
                    <li><span class="Header">商品类型</span><input type="text" id="txtProductCategory" class="SelectorButton"
                        style="width: 280px;" onfocus="ProductCategorySelector_Show(this);" readonly />
                        <input type="hidden" id="hdnProductCategoryID" value="0" /></li>
                    <li><span class="Header">积分值</span><input type="text" id="txtProductCategoryPointValue"
                        style="width: 60px;" /></li>
                    <li>
                        <input type="button" value="增 加" style="width: 60px; margin-left: 60px;" onclick="OnSaveProductCategoryPointConfig()" /></li>
                </ul>
            </div>
        </div>
        <div class="tabitem hide">
            <table id="tbProductPointConfigList" class="list" cellpadding="0" cellspacing="0"
                style="width: 1100px;">
                <tr>
                    <th style="width: 200px;">
                        商品编号
                    </th>
                    <th style="width: 300px;">
                        商品名称
                    </th>
                    <th style="width: 200px;">
                        商品规格
                    </th>
                    <th style="width: 100px;">
                        商品单位
                    </th>
                    <th style="width: 100px;">
                        积分模式
                    </th>
                    <th style="width: 100px;">
                        积分值
                    </th>
                    <th style="width: 100px;">
                        操作
                    </th>
                </tr>
            </table>
            <div class="clear">
            </div>
            <div id="divProductPointConfigEditor">
                <ul class="SimpleEditor">
                    <li><span class="Header">商品</span><input type="text" id="txtProductName" class="SelectorButton"
                        style="width: 300px;" onfocus="ProductSelector_Show(this);" readonly />
                        <input type="hidden" id="hdnProductID" value="0" /></li>
                    <li><span class="Header">积分值</span><input type="text" id="txtProductPointValue" style="width: 60px;" /></li>
                    <li>
                        <input type="button" value="增 加" style="width: 60px; margin-left: 60px;" onclick="OnSaveProductPointConfig()" /></li>
                </ul>
            </div>
        </div>
    </div>
</body>
</html>
