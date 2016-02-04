<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LimitSalesProductManager.aspx.cs"
    Inherits="LimitSalesProductManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/LimitSalesProductManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Selector.js" type="text/javascript"></script>
    <script src="js/LimitSalesProductManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>商品管理</span>&nbsp;&gt;&nbsp;<span>限购商品管理</span></div>
    <div id="divLimitSalesProductList">
        <div class="toolbar">
            <ul>
                <li><span>商品编号</span><input type="text" id="txtQueryProductNo" style="width: 100px;" /></li>
                <li><span>商品名称</span><input type="text" id="txtQueryProductName" style="width: 200px;" /></li>
                <li>
                    <input type="button" value="查 询" onclick="OnQueryLimitSalesProductList();" style="width: 70px;
                        margin-left: 20px;" /></li>
                <li>
                    <input type="button" value="新 建" onclick="OnNewLimitSalesProduct();" style="width: 70px;" /></li>
            </ul>
        </div>
        <table id="tbLimitSalesProductList" class="list" cellpadding="0" cellspacing="0">
            <tr>
                <th width="100px;">
                    操作
                </th>
                <th width="100px;">
                    商品编号
                </th>
                <th width="200px;">
                    商品名称
                </th>
                <th width="100px;">
                    商品规格
                </th>
                <th width="50px;">
                    商品单位
                </th>
                <th width="100px;">
                    销售价格
                </th>
                <th width="100px;">
                    限购价格
                </th>
                <th width="50px;">
                    折扣
                </th>
                <th width="100px;">
                    标题
                </th>
                <th width="100px;">
                    推荐序号
                </th>
                <th width="100px;">
                    权重值
                </th>
                <th width="100px;">
                    开始时间
                </th>
                <th width="100px;">
                    结束时间
                </th>
            </tr>
        </table>
    </div>
    <div id="divLimitSalesProductEditor" style="display: none;">
        <div id="divLimitSalesProduct">
            <div class="Editor">
                <input type="hidden" id="hdnLimitSalesProductID" value="0" />
                <ul>
                    <li><span>商品名称</span><input type="text" id="txtProductName" class="SelectorButton"
                        style="width: 300px;" onfocus="ProductSelector_Show(this);" readonly />
                        <input type="hidden" id="hdnProductID" value="0" /></li>
                    <li><span>商品规格</span><input type="text" id="txtProductSpec" style="width: 150px;"
                        readonly /></li>
                    <li><span>商品单位</span><input type="text" id="txtProductUnit" style="width: 150px;"
                        readonly /></li>
                    <li><span>销售价格</span><input type="text" id="txtSalesPrice" style="width: 150px;"
                        readonly /></li>
                    <li><span>限购价格</span><input type="text" id="txtLimitSalesPrice" style="width: 150px;" /></li>
                    <li><span>标题</span><input type="text" id="txtTitle" style="width: 300px;" /></li>
                    <li><span>推荐序号</span><input type="text" id="txtRecommendOrder" style="width: 150px;" /></li>
                    <li><span>推荐标题</span><textarea id="txtRecommendTitle" cols="50" rows="5"></textarea></li>
                    <li><span>推荐摘要</span><textarea id="txtRecommendBrief" cols="50" rows="5"></textarea></li>
                    <li><span>权重值</span><input type="text" id="txtWeightValue" style="width: 150px;" /></li>
                    <li><span>开始时间</span><input type="text" id="txtBeginTime" style="width: 150px;" /></li>
                    <li><span>结束时间</span><input type="text" id="txtEndTime" style="width: 150px;" /></li>
                    <li><span></span>
                        <input type="button" id="btnSaveLimitSalesProduct" onclick="OnSaveLimitSalesProduct();"
                            value="保存限购商品" style="width: 100px;" />&nbsp;&nbsp;<input type="button" value="返 回"
                                onclick="BackToLimitSalesProductList();" style="width: 100px;" /></li></ul>
            </div>
        </div>
    </div>
</body>
</html>
