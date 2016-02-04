<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InventoryStockOut.aspx.cs"
    Inherits="InventoryStockOut" %>

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
    <link rel="stylesheet" type="text/css" href="css/InventoryStockOut.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/InventoryStockOut.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>商品管理</span>&nbsp;&gt;&nbsp;<span>商品出库</span></div>
    <div id="divStockOutList">
        <div class="toolbar">
            <ul>
                <li><span>商品出库单号</span><input type="text" id="txtQueryStockOutNo" style="width: 100px;" /></li>
                <li><span>开始日期</span><input type="text" id="txtQueryFromDate" style="width: 100px;" /></li>
                <li><span>结束日期</span><input type="text" id="txtQueryToDate" style="width: 100px;" /></li>
                <li>
                    <input type="button" value="查询出库单" onclick="QueryStockOutList();" style="width: 90px;
                        margin-left: 20px;" /></li>
                <li>
                    <input type="button" value="新建出库单" onclick="NewStockOut();" style="width: 90px;" /></li>
            </ul>
        </div>
        <table id="tbStockOutList" class="list" cellpadding="0" cellspacing="0">
            <tr>
                <th width="15%">
                    出库单编号
                </th>
                <th width="20%">
                    出库时间
                </th>
                <th width="20%">
                    出库原因
                </th>
                <th width="10%">
                    总数量
                </th>
                <th width="10%">
                    总金额
                </th>
                <th width="10%">
                    状态
                </th>
                <th width="15%">
                    操作
                </th>
            </tr>
        </table>
    </div>
    <div id="divStockOutEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" id="btnSubmitStockOut" value="提交出库单" onclick="SubmitStockOut();"
                        style="width: 70px;" /></li>
                <li>
                    <input type="button" id="btnCancelStockOut" value="撤销出库单" onclick="CancelStockOut();"
                        style="width: 70px;" /></li>
                <li>
                    <input type="button" value="返回" onclick="BackToStockOutList();" style="width: 70px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnStockOutID" value="0" />
        <fieldset style="border-color: #E2DED6; border-width: 1px; border-style: Solid; margin: 5px;
            padding: 5px;">
            <legend style="color: #333333; font-size: 12px; font-weight: bold;">订单信息 </legend>
            <table style="width: 800px;">
                <tr>
                    <td style="width: 100px; text-align: right;">
                        订单编号
                    </td>
                    <td style="text-align: left; width: 300px;">
                        <input type="text" id="txtStockOutNo" style="width: 280px;" readonly />
                    </td>
                    <td style="width: 100px; text-align: right;">
                        出库原因
                    </td>
                    <td style="text-align: left; width: 300px;">
                        <select id="sltStockNoReason">
                            <option value="1">采购出库</option>
                            <option value="9">其他出库</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right;">
                        总数量
                    </td>
                    <td style="text-align: left; width: 300px;">
                        <input type="text" id="txtTotalQuantity" style="width: 280px;" readonly />
                    </td>
                    <td style="width: 100px; text-align: right;">
                        总金额
                    </td>
                    <td style="text-align: left; width: 300px;">
                        <input type="text" id="txtTotalAmount" style="width: 280px;" readonly />
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset id="fstSelectProduct" style="border-color: #E2DED6; border-width: 1px;
            border-style: Solid; margin: 0px 5px 10px 5px; padding: 5px;">
            <legend style="color: #333333; font-size: 12px; font-weight: bold;">选择商品 </legend>
            <div id="divStockOutItemAdder">
                <span>商品编号</span><input type="text" id="txtQueryProductNo" style="width: 100px;" /><span>条形码</span><input
                    type="text" id="txtQueryBarCode" style="width: 100px;" /><input type="button" id="btnQueryProduct"
                        onclick="QueryProduct();" value="查询商品" style="width: 70px;" /><span style="display: inline;
                            margin-left: 20px;">商品数量</span><input type="text" id="txtQuantity" style="width: 100px;" /><input
                                type="button" id="btnAddStockOutItem" onclick="AddProduct();" value="添加商品" style="width: 70px;" />
            </div>
            <div>
                <input type="hidden" id="hdnProductID" value="0" /><input type="hidden" id="hdnProductSpecID"
                    value="0" /></div>
            <table style="width: 900px;">
                <tr>
                    <td style="width: 100px; text-align: right;">
                        商品编号
                    </td>
                    <td style="text-align: left; width: 200px;">
                        <input type="text" id="txtProductNo" class="readonly" style="width: 150px;" readonly />
                    </td>
                    <td style="width: 100px; text-align: right;">
                        商品名称
                    </td>
                    <td style="text-align: left; width: 200px;">
                        <input type="text" id="txtProductName" class="readonly" style="width: 180px;" readonly />
                    </td>
                    <td style="text-align: right;">
                        商品单位
                    </td>
                    <td style="text-align: left;">
                        <input type="text" id="txtProductUnit" class="readonly" style="width: 150px;" readonly />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right;">
                        条形码
                    </td>
                    <td style="text-align: left;">
                        <input type="text" id="txtBarCode" class="readonly" style="width: 150px;" readonly />
                    </td>
                    <td style="text-align: right;">
                        商品规格
                    </td>
                    <td style="text-align: left;">
                        <input type="text" id="txtProductSpec" class="readonly" style="width: 180px;" readonly />
                    </td>
                    <td style="text-align: right;">
                        价格
                    </td>
                    <td style="text-align: left;">
                        <input type="text" id="txtSalesPrice" class="readonly" style="width: 150px;" readonly />
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset style="border-color: #E2DED6; border-width: 1px; border-style: Solid; margin: 5px;
            padding: 5px;">
            <legend style="color: #333333; font-size: 12px; font-weight: bold;">订单明细 </legend>
            <div id="divStockOutItemToolBar">
                <input type="button" id="btnDeleteStockOutItem" value="删除" style="width: 70px;" /></div>
            <table id="tbStockOutItemList" class="list" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="5%">
                        选择
                    </th>
                    <th width="10%">
                        商品编号
                    </th>
                    <th width="20%">
                        商品名称
                    </th>
                    <th width="10%">
                        商品单位
                    </th>
                    <th width="15%">
                        商品规格
                    </th>
                    <th width="15%">
                        条形码
                    </th>
                    <th width="10%">
                        价格
                    </th>
                    <th width="5%">
                        数量
                    </th>
                    <th width="10%">
                        金额
                    </th>
                    <th width="0" class="hide">
                        商品ID
                    </th>
                    <th width="0" class="hide">
                        商品规格ID
                    </th>
                </tr>
            </table>
        </fieldset>
    </div>
</body>
</html>
