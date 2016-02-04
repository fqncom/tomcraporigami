<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromotionConfigManager.aspx.cs"
    Inherits="PromotionConfigManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/PromotionConfigManager.css" />
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
    <script src="js/PromotionConfigManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>销售管理</span>&nbsp;&gt;&nbsp;<span>促销设置管理</span></div>
    <br />
    <div id="divPromotionRule">
        <table id="tbPromotionRuleList" class="list" cellpadding="0" cellspacing="0" style="width: 1700px;">
            <tr>
                <th style="width: 100px;">
                    操作
                </th>
                <th style="width: 200px;">
                    促销规则名称
                </th>
                <th style="width: 100px;">
                    条件对象
                </th>
                <th style="width: 100px;">
                    条件模式
                </th>
                <th style="width: 100px;">
                    条件参数
                </th>
                <th style="width: 200px;">
                    条件商品范围
                </th>
                <th style="width: 100px;">
                    执行对象
                </th>
                <th style="width: 100px;">
                    执行模式
                </th>
                <th style="width: 100px;">
                    执行参数
                </th>
                <th style="width: 200px;">
                    执行商品范围
                </th>
                <th style="width: 100px;">
                    执行数量上限
                </th>
                <th style="width: 100px;">
                    开始日期
                </th>
                <th style="width: 100px;">
                    结束日期
                </th>
                <th style="width: 100px;">
                    操作时间
                </th>
                <th style="width: 100px;">
                    操作员
                </th>
            </tr>
        </table>
        <div class="clear">
        </div>
        <div id="divPromotionRuleEditor">
            <input type="hidden" id="hdnPromotionRuleID" value="0" />
            <ul class="SimpleEditor">
                <li><span class="Header">促销规则名称</span><input type="text" id="txtPromotionRuleName"
                    style="width: 160px;" /></li>
                <li><span class="Header">条件对象</span><select id="sltConditionTarget" style="width: 160px;"></select></li>
                <li><span class="Header">条件模式</span><select id="sltConditionMode" style="width: 160px;"
                    onchange="OnConditionModeChanged();"></select></li>
                <li><span class="Header ConditionParameter1">条件参数</span><input type="text" id="txtConditionParameter1"
                    style="width: 160px;" /></li>
                <li><span class="Header">条件商品范围</span><input type="text" id="txtConditionProductScopeName"
                    class="SelectorButton" style="width: 300px;" onfocus="ProductScopeSelector_Show(this);"
                    readonly />
                    <input type="hidden" id="hdnConditionProductScopeID" value="0" /></li>
                <li><span class="Header">执行对象</span><select id="sltImplementTarget" style="width: 160px;"
                    onchange="OnImplementTargetChanged();"></select></li>
                <li><span class="Header">执行模式</span><select id="sltImplementMode" style="width: 160px;"
                    onchange="OnImplementModeChanged();"></select></li>
                <li class="ImplementParameter1 hide"><span class="Header">执行参数</span><input type="text"
                    id="txtImplementParameter1" style="width: 160px;" /></li>
                <li class="ImplementParameter2 hide"><span class="Header">执行参数</span><input type="text"
                    id="txtImplementParameter2" style="width: 160px;" /></li>
                <li><span class="Header">执行商品范围</span><input type="text" id="txtImplementProductScopeName"
                    class="SelectorButton" style="width: 300px;" onfocus="ProductScopeSelector_Show(this);"
                    readonly />
                    <input type="hidden" id="hdnImplementProductScopeID" value="0" /></li>
                <li><span class="Header">执行数量上限</span><input type="text" id="txtImplementMaxQuantity"
                    style="width: 160px;" /></li>
                <li><span class="Header">开始日期</span><input type="text" id="txtBeginDate" style="width: 160px;" /></li>
                <li><span class="Header">结束日期</span><input type="text" id="txtEndDate" style="width: 160px;" /></li>
                <li>
                    <input type="button" value="增 加" style="width: 60px; margin-left: 80px;" onclick="OnSavePromotionRule()" /></li>
            </ul>
        </div>
    </div>
</body>
</html>
