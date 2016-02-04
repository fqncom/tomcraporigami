<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponConfigManager.aspx.cs"
    Inherits="CouponConfigManager" %>

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
    <link rel="stylesheet" type="text/css" href="css/CouponConfigManager.css" />
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
    <script src="js/CouponConfigManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>商品管理</span>&nbsp;&gt;&nbsp;<span>优惠劵配置管理</span></div>
    <br />
    <div class="tabmenu">
        <ul>
            <li>优惠劵发放金额区间配置</li>
        </ul>
    </div>
    <div class="tabbox">
        <div class="tabitem">
            <table id="tbCouponGrantAmountConfigList" class="list" cellpadding="0" cellspacing="0"
                style="width: 600px;">
                <tr>
                    <th style="width: 100px;">
                        开始金额
                    </th>
                    <th style="width: 100px;">
                        结束金额
                    </th>
                    <th style="width: 100px;">
                        每张面值
                    </th>
                    <th style="width: 100px;">
                        发放数量
                    </th>
                    <th style="width: 100px;">
                        状态
                    </th>
                    <th style="width: 100px;">
                        操作
                    </th>
                </tr>
            </table>
            <div class="clear">
            </div>
            <div id="divCouponGrantAmountConfigEditor">
                <ul class="SimpleEditor">
                    <li><span class="Header">开始金额</span><input type="text" id="txtBeginAmount" style="width: 100px;" /></li>
                    <li><span class="Header">结束金额</span><input type="text" id="txtEndAmount" style="width: 100px;" /></li>
                    <li><span class="Header">每张面值</span><input type="text" id="txtParValue" style="width: 100px;" /></li>
                    <li><span class="Header">发放数量</span><input type="text" id="txtCount" style="width: 100px;" /></li>
                    <li>
                        <input type="button" value="增 加" style="width: 60px; margin-left: 60px;" onclick="OnSaveCouponGrantAmountConfig()" /></li>
                </ul>
            </div>
        </div>
        <div class="tabitem hide">
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
        </div>
    </div>
</body>
</html>
