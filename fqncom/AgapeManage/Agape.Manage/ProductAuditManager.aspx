<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductAuditManager.aspx.cs"
    Inherits="ProductAuditManager" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/ProductAuditManager.css" />
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.pager.js" type="text/javascript"></script>
    <script src="js/jquery.thickbox.js" type="text/javascript"></script>
    <script src="js/jquery.treeview.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Basic.js" type="text/javascript"></script>
    <script src="js/ProductAuditManager.js" type="text/javascript"></script>
</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>基本管理</span>&nbsp;&gt;&nbsp;<span>审核管理</span></div>
    <div id="admin-content">
        <div id="divProductQuery">
            <ul>
                <li><span>商品编号</span><input type="text" id="txtProductNoQuery" /></li>
                <li><span>商品名称</span><input type="text" id="txtProductNameQuery" /></li>
                <li><span>编辑员</span>
                    <select id="sltEditOperator">
                    </select>
                </li>
                <li><span>审核员</span>
                    <select id="sltAuditOperator">
                    </select>
                </li>
                <li><span>编辑日期</span><input type="text" id="txtFromEditDate" style="width: 80px" />-<input
                    type="text" id="txtToEditDate" style="width: 80px" /></li>
                <li><span>审核状态</span>
                    <select id="sltStatus">
                    </select>
                </li>
                <li>
                    <input type="button" id="btnQueryProductList()" value="查询" onclick="OnQueryProductTempList();"
                        style="width: 80px;" /></li></ul>
        </div>
        <div id="divProductList">
            <table id="tbProductList" cellpadding="0" cellspacing="0" class="list">
                <tr>
                    <th style="width: 30px;">
                        选择
                    </th>
                    <th style="width: 80px;">
                        商品代码
                    </th>
                    <th style="width: 150px;">
                        商品名称
                    </th>
                    <th style="width: 80px;">
                        商品类型
                    </th>
                    <th style="width: 80px;">
                        商品品牌
                    </th>
                    <th style="width: 40px;">
                        商品单位
                    </th>
                    <th style="width: 60px;">
                        销售价格
                    </th>
                    <th style="width: 60px;">
                        编辑员
                    </th>
                    <th style="width: 80px;">
                        编辑时间
                    </th>
                    <th style="width: 60px;">
                        审核员
                    </th>
                    <th style="width: 80px;">
                        审核时间
                    </th>
                    <th style="width: 150px;">
                        备注
                    </th>
                    <th style="width: 80px;">
                        状态
                    </th>
                    <th style="width: 100px;">
                        操作
                    </th>
                </tr>
            </table>
        </div>
        <div id="pager" class="admin-pager">
        </div>
    </div>
</body>
</html>
