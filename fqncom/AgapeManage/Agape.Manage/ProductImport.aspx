<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductImport.aspx.cs"
    Inherits="ProductImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/ProductImport.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/ProductImport.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>基本管理</span>&nbsp;&gt;&nbsp;<span>商品导入</span></div>
    <div class="admin-toolbar">
        <span>请选择商品导入文件</span>
        <input type="file" id="fileProductFilePath" size="60" /><input type="button" onclick="OnReadFile();"
            value="读取文件" /><input type="button" onclick="OnImportProduct();" value="导入文件" /></div>
    <div id="divProductList">
        <input type="hidden" id="hdnRowCount" value="0" /><input type="hidden" id="hdnCurrentIndex"
            value="0" />
        <table id="tbProductList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="8%">
                    商品编号
                </th>
                <th width="18%">
                    商品名称
                </th>
                <th width="10%">
                    商品规格
                </th>
                <th width="5%">
                    商品单位
                </th>
                <th width="12%">
                    条形码
                </th>
                <th width="7%">
                    商品类型
                </th>
                <th width="7%">
                    商品品牌
                </th>
                <th width="5%">
                    市场价格
                </th>
                <th width="5%">
                    销售价格
                </th>
                <th width="8%">
                    状态
                </th>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
