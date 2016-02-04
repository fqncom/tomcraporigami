var m_ProductNoQuery;
var m_ProductNameQuery;
var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

ProductCategorySelector_Append();

$(document).ready(function () {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

    SwitchPage("ProductList");

    QueryProductBrandList();
    ProductCategorySelector_Init();

    InitProductTab();
    InitTinyMCE();
});


/****** 商品选项卡 ******/
function InitProductTab() {

    var jqMenuItemList = $("#divProductEditor .tabmenu ul li");

    jqMenuItemList.click(function () {

        $(this).addClass("selected")                    //当前<li>元素高亮
				   .siblings().removeClass("selected"); //去掉其他同辈<li>元素的高亮

        var index = jqMenuItemList.index(this);         // 获取当前点击的<li>元素 在 全部li元素中的索引。
        $("#divProductEditor .tabbox > div")   	            //选取子节点。不选取子节点的话，会引起错误。如果里面还有div 
					.eq(index).show()                   //显示 <li>元素对应的<div>元素
					.siblings().hide();                 //隐藏其他几个同辈的<div>元素
    });

    jqMenuItemList.hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    });
}


function InitTinyMCE() {

    tinyMCE.init({
        // General options
        mode: "textareas",
        theme: "advanced",
        plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist",

        // Theme options
        theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak,restoredraft",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,

        // Example content CSS (should be your site CSS)
        //content_css: "tiny_mce/css/content.css",
        content_css: "css/Product.css",
        language: "cn",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "tiny_mce/lists/template_list.js",
        external_link_list_url: "tiny_mce/lists/link_list.js",
        external_image_list_url: "tiny_mce/lists/image_list.js",
        media_external_list_url: "tiny_mce/lists/media_list.js",

        // Style formats
        style_formats: [
			{ title: 'Bold text', inline: 'b' },
			{ title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
			{ title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
			{ title: 'Example 1', inline: 'span', classes: 'example1' },
			{ title: 'Example 2', inline: 'span', classes: 'example2' },
			{ title: 'Table styles' },
			{ title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
		],

        // Replace values for the template plugin
        template_replace_values: {
            username: "Some User",
            staffid: "991234"
        }
    });
}


/****** 商品品牌 ******/
var ProductBrandList;
var ProductBrandNames = new Array();

function QueryProductBrandList() {

    $.post("ProductService.aspx", { "TransCode": "QueryProductBrandList" }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        ProductBrandList = $(data).find("ReturnContent");

        InitProductBrandSelector();
    });
}

function InitProductBrandSelector() {

    var index = 0;

    ProductBrandList.find("ProductBrand").each(function () {

        var jqProductBrand = $(this);
        ProductBrandNames[index] = $("ProductBrandName", jqProductBrand).text();
        index++;
    });

    $("#txtProductBrand").autocomplete(ProductBrandNames);

    $('input#txtProductBrand').result(function (event, data, formatted) {
        //alert("data=" + data + ",formatted=" + formatted);
    });

}


/****** 商品类型 ******/
function OnSelectProductCategory(ProductCategoryID) {

    var ProductCategoryName = GetProductCategoryName(ProductCategoryID);
    $("#txtProductCategory").val(ProductCategoryID + " - " + ProductCategoryName);
    $("#hdnProductCategoryID").val(ProductCategoryID);

    ProductCategorySelector_Hide();
}


/****** 商品 ******/
function OnQueryProductList() {

    if (!IsPageReady()) {
        alert("页面还未完全载入！");
        return;
    }

    m_ProductNoQuery = $("#txtProductNoQuery").val();
    m_ProductNameQuery = $("#txtProductNameQuery").val();

    QueryProductCount();
}

function QueryProductCount() {

    $.post("ProductService.aspx",
    { "TransCode": "QueryProductCount",
        "ProductNo": m_ProductNoQuery,
        "ProductName": m_ProductNameQuery
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_RowCount = new Number($(data).find("RowCount").text());

        m_PageCount = parseInt(m_RowCount / m_PageSize);
        if (m_RowCount % m_PageSize > 0) m_PageCount++;

        QueryProductList(m_CurrentPageIndex);
    });
}

PageClick = function (pageclickednumber) {

    QueryProductList(pageclickednumber);
}

function QueryProductList(PageIndex) {

    $.post("ProductService.aspx",
    { "TransCode": "QueryProductList",
        "ProductNo": m_ProductNoQuery,
        "ProductName": m_ProductNameQuery,
        "ProductCategory": 0,
        "PageIndex": PageIndex,
        "PageSize": m_PageSize
    }, function (data) {

        var htmlData = "";
        $("#tbProductList tr:gt(0)").remove();

        $(data).find("Product").each(function () {

            var $Product = $(this);
            var ProductID = $Product.attr("ProductID");
            var ProductNo = $Product.attr("ProductNo");
            var ProductName = $Product.attr("ProductName");
            var ProductCategoryID = $Product.attr("ProductCategoryID");
            var ProductBrandName = $Product.attr("ProductBrandName");
            var ProductUnit = $Product.attr("ProductUnit");
            var SalesPrice = $Product.attr("SalesPrice");

            var ProductCategoryName = GetProductCategoryName(ProductCategoryID);

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + ProductID + "' /></td>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductCategoryName + "</td>" +
                            "<td>" + ProductBrandName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + SalesPrice + "</td>" +
                            "<td><a href='#none' onclick='OnEditProduct(" + ProductID + ");return true;'>修改</a>|<a href='#none' onclick='OnDeleteProduct(" + ProductID + ");return true;'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbProductList tr:eq(0)");

        m_CurrentPageIndex = PageIndex;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}

function OnNewProduct() {

    $("#hdnProductID").val(0);
    $("#txtProductNo").val("");
    $("#txtProductName").val("");
    $("#txtProductSpec").val("");
    $("#txtProductUnit").val("");
    $("#txtBarCode").val("");
    $("#txtProductCategory").val("");
    $("#hdnProductCategoryID").val("0");
    $("#txtProductBrand").val("");
    $("#hdnProductBrandID").val("0");
    $("#txtMarketPrice").val("");
    $("#txtSalesPrice").val("");
    $("#txtPurchasePrice").val("");
    $("#txtFitAge").val("");
    tinyMCE.get("txtDescription").setContent("");

    SwitchPage("ProductEditor");
}

function OnEditProduct(ProductID) {

    $.post("ProductService.aspx",
        {
            "TransCode": "QueryProduct",
            "ProductID": ProductID,
            "CheckTempFlag": true
        }, function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            var ProductNo = $(data).find("Product ProductNo").text();
            var ProductName = $(data).find("Product ProductName").text();
            var ProductSpec = $(data).find("ProductSpec ProductSpec").text();
            var ProductUnit = $(data).find("Product ProductUnit").text();
            var BarCode = $(data).find("Product BarCode").text();
            var ProductCategoryID = $(data).find("Product ProductCategoryID").text();
            var ProductCategoryName = $(data).find("ProductCategory ProductCategoryName").text();
            var ProductBrandID = $(data).find("Product ProductBrandID").text();
            var ProductBrandName = $(data).find("ProductBrand ProductBrandName").text();
            var MarketPrice = $(data).find("Product MarketPrice").text();
            var SalesPrice = $(data).find("Product SalesPrice").text();
            var PurchasePrice = $(data).find("Product PurchasePrice").text();
            var FitAge = $(data).find("Product FitAge").text();
            var Description = $(data).find("Product Description").text();
            MarketPrice = ToMoney(MarketPrice);
            SalesPrice = ToMoney(SalesPrice);
            PurchasePrice = ToMoney(PurchasePrice);
            Description = unescape(Description);

            $("#hdnProductID").val(ProductID);
            $("#txtProductNo").val(ProductNo);
            $("#txtProductName").val(ProductName);
            $("#txtProductSpec").val(ProductSpec);
            $("#txtProductUnit").val(ProductUnit);
            $("#txtBarCode").val(BarCode);
            $("#hdnProductCategoryID").val(ProductCategoryID);
            $("#txtProductCategory").val(ProductCategoryName);
            $("#hdnProductBrandID").val(ProductBrandID);
            $("#txtProductBrand").val(ProductBrandName);
            $("#txtMarketPrice").val(MarketPrice);
            $("#txtSalesPrice").val(SalesPrice);
            $("#txtPurchasePrice").val(PurchasePrice);
            $("#txtFitAge").val(FitAge);
            tinyMCE.get("txtDescription").setContent(Description);

            SwitchPage("ProductEditor");

            QueryProductSpecList();

            QueryProductPictureList();
        });
}

function OnSubmitProductEdit() {

    if (!window.confirm("确认要提交商品编辑吗？")) {
        return false;
    }

    var ProductID = $("#hdnProductID").val();
    var ProductNo = $("#txtProductNo").val();
    var ProductName = $("#txtProductName").val();
    var ProductSpec = $("#txtProductSpec").val();
    var ProductUnit = $("#txtProductUnit").val();
    var BarCode = $("#txtBarCode").val();
    var ProductCategoryID = $("#hdnProductCategoryID").val();
    var ProductBrand = $("#txtProductBrand").val();
    var MarketPrice = $("#txtMarketPrice").val();
    var SalesPrice = $("#txtSalesPrice").val();
    var PurchasePrice = $("#txtPurchasePrice").val();
    var FitAge = $("#txtFitAge").val();
    var Description = tinyMCE.get("txtDescription").getContent();
    Description = escape(Description);

    var TransCode = ProductID > 0 ? "SaveProductTemp" : "SaveProduct";

    $.post("ProductService.aspx",
        {
            "TransCode": TransCode,
            "ProductID": ProductID,
            "ProductNo": ProductNo,
            "ProductName": ProductName,
            "ProductSpec": ProductSpec,
            "ProductUnit": ProductUnit,
            "BarCode": BarCode,
            "ProductCategoryID": ProductCategoryID,
            "ProductBrand": ProductBrand,
            "MarketPrice": MarketPrice,
            "SalesPrice": SalesPrice,
            "PurchasePrice": PurchasePrice,
            "FitAge": FitAge,
            "Description": Description
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            if (ProductID == 0) {
                ProductID = $(data).find("ProductID").text();
                $("#hdnProductID").val(ProductID);

                //QueryProductPictureList();
            }
            //QueryProductSpecList();

            alert("提交商品编辑成功！");
        });
}

function OnDeleteProduct(ProductID) {

    if (!window.confirm("确认要删除吗？")) {
        return false;
    }

    $.post("ProductService.aspx", { "TransCode": "DeleteProduct", "ProductID": ProductID }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        QueryProductList();
    });
}


/****** 商品规格 ******/
var jqProductSpecList;

function QueryProductSpecList() {

    var ProductID = $("#hdnProductID").val();

    jqProductSpecList = null;
    $("#tbProductSpecList tr:gt(0)").remove();
    OnNewProductSpec();

    $.post("ProductService.aspx", { "TransCode": "QueryProductSpecList", "ProductID": ProductID }, function (data) {

        var htmlData = "";
        jqProductSpecList = $(data).find("Response");

        jqProductSpecList.find("ProductSpec").each(function () {

            var jqProductSpec = $(this);
            var ProductSpecID = jqProductSpec.attr("ProductSpecID");
            var ProductID = jqProductSpec.attr("ProductID");
            var ProductNo = jqProductSpec.attr("ProductNo");
            var ProductName = jqProductSpec.attr("ProductName");
            var ProductUnit = jqProductSpec.attr("ProductUnit");
            var SalesPrice = jqProductSpec.attr("SalesPrice");
            var ProductSpec = jqProductSpec.attr("ProductSpec");
            var StockInitQuantity = jqProductSpec.attr("StockInitQuantity");
            var StockLowerLimit = jqProductSpec.attr("StockLowerLimit");
            var StockUpperLimit = jqProductSpec.attr("StockUpperLimit");
            var DefaultFlag = jqProductSpec.attr("DefaultFlag");
            var Remark = jqProductSpec.attr("Remark");

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + ProductID + "' /></td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + SalesPrice + "</td>" +
                            "<td>" + StockLowerLimit + "</td>" +
                            "<td>" + StockUpperLimit + "</td>" +
                            "<td>" + DefaultFlag + "</td>" +
                            "<td>" + Remark + "</td>" +
                            "<td><a href='#none' onclick='OnEditProductSpec(" + ProductSpecID + ");'>修改</a>|<a href='#none' onclick='OnDeleteProductSpec(" + ProductSpecID + ");'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbProductSpecList tr:eq(0)");
    });
}

function OnNewProductSpec() {

    $("#hdnProductSpecID").val(0);
    $("#txtProductSpecName").val("");
    $("#txtStockLowerLimit").val("");
    $("#txtStockUpperLimit").val("");
    $("#txtProductSpecRemark").val("");
}

function OnEditProductSpec(ProductSpecID) {

    var jqProductSpec = jqProductSpecList.find("ProductSpec[ProductSpecID='" + ProductSpecID + "']");

    $("#hdnProductSpecID").val(jqProductSpec.attr("ProductSpecID"));
    $("#txtProductSpecName").val(jqProductSpec.attr("ProductSpec"));
    $("#txtStockLowerLimit").val(jqProductSpec.attr("StockLowerLimit"));
    $("#txtStockUpperLimit").val(jqProductSpec.attr("StockUpperLimit"));
    $("#txtProductSpecRemark").val(jqProductSpec.attr("Remark"));
}

function OnSaveProductSpec() {

    if (!window.confirm("确认要保存商品规格吗？")) {
        return false;
    }

    var ProductSpecID = $("#hdnProductSpecID").val();
    var ProductID = $("#hdnProductID").val();
    var ProductSpec = $("#txtProductSpecName").val();
    var StockLowerLimit = $("#txtStockLowerLimit").val();
    var StockLowerLimit = $("#txtStockUpperLimit").val();
    var Remark = $("#txtProductSpecRemark").val();

    $.post("ProductService.aspx",
        { "TransCode": "SaveProductSpec",
            "ProductID": ProductID,
            "ProductSpecID": ProductSpecID,
            "ProductSpec": ProductSpec,
            "StockLowerLimit": StockLowerLimit,
            "StockLowerLimit": StockLowerLimit,
            "Remark": Remark
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            alert("保存成功！");

            QueryProductSpecList();
        });
}

function OnDeleteProductSpec(ProductSpecID) {

    if (!window.confirm("确认要删除商品规格吗？")) {
        return false;
    }

    $.post("ProductService.aspx", { "TransCode": "DeleteProductSpec", "ProductSpecID": ProductSpecID }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        alert("删除商品规格成功！");

        QueryProductSpecList();
    });
}


/****** 商品图片 ******/
var jqProductPictureList;

function QueryProductPictureList() {

    var ProductID = $("#hdnProductID").val();

    jqProductPictureList = null;
    $("#tbProductPictureList tr:gt(0)").remove();
    OnNewProductPicture();

    $.post("ProductService.aspx", { "TransCode": "QueryProductPictureList", "ProductID": ProductID }, function (data) {

        var htmlData = "";
        jqProductPictureList = $(data).find("Response");

        jqProductPictureList.find("ProductPicture").each(function () {

            var jqProductPicture = $(this);
            var ProductPictureID = jqProductPicture.attr("ProductPictureID");
            var ProductID = jqProductPicture.attr("ProductID");
            var FileName = jqProductPicture.attr("FileName");
            var FileType = jqProductPicture.attr("FileType");
            var OrderNo = jqProductPicture.attr("OrderNo");
            var Title = jqProductPicture.attr("Title");
            var Remark = jqProductPicture.attr("Remark");

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + ProductID + "' /></td>" +
                            "<td>" + FileName + "</td>" +
                            "<td>" + FileType + "</td>" +
                            "<td>" + OrderNo + "</td>" +
                            "<td>" + Title + "</td>" +
                            "<td>" + Remark + "</td>" +
                            "<td><a href='#none' onclick='OnEditProductPicture(" + ProductPictureID + ");'>修改</a>|<a href='#none' onclick='OnDeleteProductPicture(" + ProductPictureID + ");'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbProductPictureList tr:eq(0)");
    });
}

function OnNewProductPicture() {

    $("#hdnProductPictureID").val(0);
    $("#txtProductPictureFileName").val("");
    $("#txtProductPictureFileType").val("");
    $("#txtProductPictureOrderNo").val("");
    $("#txtProductPictureTitle").val("");
    $("#txtProductPictureRemark").val("");
}

function OnEditProductPicture(ProductPictureID) {

    var jqProductPicture = jqProductPictureList.find("ProductPicture[ProductPictureID='" + ProductPictureID + "']");

    $("#hdnProductPictureID").val(jqProductPicture.attr("ProductPictureID"));
    $("#txtProductPictureFileName").val(jqProductPicture.attr("FileName"));
    $("#txtProductPictureFileType").val(jqProductPicture.attr("FileType"));
    $("#txtProductPictureOrderNo").val(jqProductPicture.attr("OrderNo"));
    $("#txtProductPictureTitle").val(jqProductPicture.attr("Title"));
    $("#txtProductPictureRemark").val(jqProductPicture.attr("Remark"));
}

function OnSaveProductPicture() {

    if (!window.confirm("确认要保存商品图片吗？")) {
        return false;
    }

    var ProductPictureID = $("#hdnProductPictureID").val();
    var ProductID = $("#hdnProductID").val();
    var FileName = $("#txtProductPictureFileName").val();
    var FileType = $("#txtProductPictureFileType").val();
    var OrderNo = $("#txtProductPictureOrderNo").val();
    var Title = $("#txtProductPictureTitle").val();
    var Remark = $("#txtProductPictureRemark").val();

    $.post("ProductService.aspx",
        { "TransCode": "SaveProductPicture",
            "ProductID": ProductID,
            "ProductPictureID": ProductPictureID,
            "FileName": FileName,
            "FileType": FileType,
            "OrderNo": OrderNo,
            "Title": Title,
            "Remark": Remark
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            alert("保存成功！");

            QueryProductPictureList();
        });
}

function OnDeleteProductPicture(ProductPictureID) {

    if (!window.confirm("确认要删除商品图片吗？")) {
        return false;
    }

    $.post("ProductService.aspx", { "TransCode": "DeleteProductPicture", "ProductPictureID": ProductPictureID }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        alert("删除商品图片成功！");

        QueryProductPictureList();
    });
}


/****** 全局功能 ******/
function OnBackToProductList() {

    SwitchPage("ProductList");
}

function SwitchPage(PageName) {

    if (PageName == "ProductList") {

        $("#divProductList").show();
        $("#divProductEditor").hide();
        $("#btnBackToProductList").hide();

    }
    else if (PageName == "ProductEditor") {

        $("#divProductEditor").show();
        $("#divProductList").hide();
        $("#btnBackToProductList").show();

    }
}

function IsPageReady() {
    return m_ProductCategoryReady;
}


