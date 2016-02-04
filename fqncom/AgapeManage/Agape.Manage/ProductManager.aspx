<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductManager.aspx.cs"
    Inherits="ProductManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/ProductManager.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/Selector.js" type="text/javascript"></script>

    <script src="js/ProductManager.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>基本管理</span>&nbsp;&gt;&nbsp;<span>商品管理</span></div>
    <div class="admin-toolbar">
        <input type="button" value="新 建" onclick="OnNewProduct();" style="width: 70px;" /><input
            type="button" id="btnBackToProductList" value="返回列表" onclick="OnBackToProductList();"
            style="width: 80px;" /></div>
    <div id="divProductList">
        <div id="divProductQuery">
            <ul>
                <li><span>商品编号</span><input type="text" id="txtProductNoQuery" /></li><li><span>商品名称</span><input
                    type="text" id="txtProductNameQuery" /></li><li>
                        <input type="button" id="btnQueryProductList()" value="查询" onclick="OnQueryProductList();"
                            style="width: 80px;" /></li></ul>
        </div>
        <table id="tbProductList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="10%">
                    选择
                </th>
                <th width="15%">
                    商品代码
                </th>
                <th width="25%">
                    商品名称
                </th>
                <th width="10%">
                    商品类型
                </th>
                <th width="15%">
                    商品品牌
                </th>
                <th width="5%">
                    商品单位
                </th>
                <th width="10%">
                    销售价格
                </th>
                <th width="10%">
                    操作
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    <div id="divProductEditor" style="display: none;">
        <input type="hidden" id="hdnProductID" value="0" />
        <div class="tabmenu">
            <ul>
                <li class="selected">基本信息</li>
                <li>规格设置</li>
                <li>图片设置</li>
            </ul>
        </div>
        <div class="tabbox">
            <div>
                <table style="width: 900px;">
                    <tr>
                        <td style="width: 150px; text-align: right;">
                            商品编号
                        </td>
                        <td style="text-align: left; width: 300px;">
                            <input type="text" id="txtProductNo" style="width: 280px;" />
                        </td>
                        <td style="width: 150px; text-align: right;">
                            条形码
                        </td>
                        <td style="text-align: left; width: 300px;">
                            <input type="text" id="txtBarCode" style="width: 280px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            商品名称
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <input type="text" id="txtProductName" style="width: 600px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            商品规格
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtProductSpec" style="width: 280px;" />
                        </td>
                        <td style="text-align: right;">
                            商品单位
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtProductUnit" style="width: 280px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            商品类型
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtProductCategory" class="SelectorButton" style="width: 280px;"
                                onfocus="ProductCategorySelector_Show(this);" readonly />
                            <input type="hidden" id="hdnProductCategoryID" value="0" />
                        </td>
                        <td style="text-align: right;">
                            商品品牌
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtProductBrand" style="width: 280px;" />
                            <input type="hidden" id="hdnProductBrandID" value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            市场价格
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtMarketPrice" style="width: 280px;" />
                        </td>
                        <td style="text-align: right;">
                            销售价格
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtSalesPrice" style="width: 280px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            采购价格
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtPurchasePrice" style="width: 280px;" />
                        </td>
                        <td style="text-align: right;">
                            适合年龄
                        </td>
                        <td style="text-align: left;">
                            <input type="text" id="txtFitAge" style="width: 280px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            简介
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <textarea id="txtDescription" rows="50" cols="120" style="width: 800px; height: 800px;"
                                class="tinymce"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <input type="button" id="btnSaveProductTemp" value="提交商品编辑" onclick="OnSubmitProductEdit();" style="width: 80px;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="hide">
                <table id="tbProductSpecList" class="list" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="10%">
                            选择
                        </th>
                        <th width="20%">
                            商品规格
                        </th>
                        <th width="15%">
                            商品单位
                        </th>
                        <th width="5%">
                            价格
                        </th>
                        <th width="10%">
                            最低库存量
                        </th>
                        <th width="10%">
                            最高库存量
                        </th>
                        <th width="10%">
                            默认标志
                        </th>
                        <th width="10%">
                            备注
                        </th>
                        <th width="10%">
                            操作
                        </th>
                    </tr>
                </table>
                <input type="hidden" id="hdnProductSpecID" value="0" />
                <table id="tbProductSpecEditor" class="editor" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="title">
                            商品规格
                        </td>
                        <td class="content">
                            <input type="text" id="txtProductSpecName" />
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            最低库存量
                        </td>
                        <td class="content">
                            <input type="text" id="txtStockLowerLimit" />
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            最高库存量
                        </td>
                        <td class="content">
                            <input type="text" id="txtStockUpperLimit" />
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            备注
                        </td>
                        <td class="content">
                            <input type="text" id="txtProductSpecRemark" />
                        </td>
                    </tr>
                </table>
                <div id="divProductSpecOperation">
                    <input type="button" id="btnNewProductSpec" onclick="OnNewProductSpec()" value="新 建" /><input
                        type="button" id="btnSaveProductSpec" onclick="OnSaveProductSpec()" value="保 存" /></div>
            </div>
            <div class="hide">
                <table id="tbProductPictureList" class="list" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="10%">
                            选择
                        </th>
                        <th width="20%">
                            图片名称
                        </th>
                        <th width="10%">
                            文件类型
                        </th>
                        <th width="5%">
                            序号
                        </th>
                        <th width="15%">
                            标题
                        </th>
                        <th width="30%">
                            备注
                        </th>
                        <th width="10%">
                            操作
                        </th>
                    </tr>
                </table>
                <input type="hidden" id="hdnProductPictureID" value="0" />
                <table id="tbProductPictureEditor" class="editor" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="title">
                            图片名称
                        </td>
                        <td class="content">
                            <input type="text" id="txtProductPictureFileName" />
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            文件类型
                        </td>
                        <td class="content">
                            <input type="text" id="txtProductPictureFileType" />
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            序号
                        </td>
                        <td class="content">
                            <input type="text" id="txtProductPictureOrderNo" />
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            标题
                        </td>
                        <td class="content">
                            <input type="text" id="txtProductPictureTitle" />
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            备注
                        </td>
                        <td class="content">
                            <input type="text" id="txtProductPictureRemark" />
                        </td>
                    </tr>
                </table>
                <div id="divProductPictureOperation">
                    <input type="button" id="btnNewProductPicture" onclick="OnNewProductPicture()" value="新 建" /><input
                        type="button" id="btnSaveProductPicture" onclick="OnSaveProductPicture()" value="保 存" /></div>
            </div>
        </div>
    </div>
</body>
</html>
