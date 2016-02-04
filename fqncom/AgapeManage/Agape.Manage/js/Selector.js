/****** 商品类型下拉选择框处理 ******/
var m_ProductCategoryReady = false;

function ProductCategorySelector_Append() {
    var strHtml =
        "<div id='divProductCategorySelector' class='Selector'>" +
        "   <div class='SelectorHeader'><span>请选择商品类型</span><a href='#none'>关闭</a></div>" +
        "   <div class='SelectorBody'></div>" +
        "</div>";
    document.write(strHtml);
}

function ProductCategorySelector_Init() {

    $("#divProductCategorySelector .SelectorBody").load("HtmlProvider.aspx", { "HtmlCode": "ProductCategoryTree" }, function () {

        $("#ProductCategoryTree").treeview({
            animated: "fast",
            collapsed: true,
            unique: true,
            persist: "cookie",
            toggle: function () {
                window.console && console.log("%o was toggled", this);
            }
        });

        $("#divProductCategorySelector .SelectorHeader a").click(function () {
            ProductCategorySelector_Hide();
        });

        m_ProductCategoryReady = true;
    });
}

function ProductCategorySelector_Show(obj) {

    var Point = GetAbsoluteLocationEx(obj);

    $("#divProductCategorySelector").css("left", Point.X);
    $("#divProductCategorySelector").css("top", Point.Y + obj.offsetHeight);
    $("#divProductCategorySelector").show();
}

function GetProductCategoryName(ProductCategoryID) {

    var liID = "pc" + ProductCategoryID;
    var ProductCategoryName = $("#divProductCategorySelector li[id='" + liID + "'] > span").text();
    return ProductCategoryName;
}

function ProductCategorySelector_Hide() {

    $("#divProductCategorySelector").hide();
}


/****** 商品下拉选择框处理 ******/
var m_ProductSelector_ProductNoQuery;
var m_ProductSelector_ProductNameQuery;
var m_ProductSelector_RowCount;
var m_ProductSelector_PageCount;
var m_ProductSelector_CurrentPageIndex = 1;
var m_ProductSelector_PageSize = 10;

function ProductSelector_Append() {

    var strHtml =
        "<div id='divProductSelector' class='Selector ProductSelector'>" +
        "    <div class='SelectorHeader'><span>请选择商品</span><a href='#none'>关闭</a></div>" +
        "    <div class='SelectorBody'>" +
        "        <div class='SelectorFilter'>" +
        "            <ul>" +
        "                <li><span>商品编号</span><input type='text' id='txtSelectorFilterProductNo' style='width: 150px;' /></li>" +
        "                <li><span>商品名称</span><input type='text' id='txtSelectorFilterProductName' style='width: 150px;' /></li>" +
        "                <li><input type='button' class='QueryButton' value='查询' /></li>" +
        "            </ul>" +
        "        </div>" +
        "        <div class='clear'></div>" +
        "        <div class='SelectorContent'>" +
        "            <table class='list' cellpadding='0' cellspacing='0'>" +
        "                <tr>" +
        "                    <th style='width: 100px;'>商品编号</th>" +
        "                    <th style='width: 300px;'>商品名称</th>" +
        "                    <th style='width: 100px;'>商品类型</th>" +
        "                    <th style='width: 100px;'>商品品牌</th>" +
        "                    <th style='width: 80px;'>商品单位</th>" +
        "                    <th style='width: 80px;'>销售价格</th>" +
        "                    <th style='width: 60px;'>操作</th>" +
        "                </tr>" +
        "            </table>" +
        "            <div id='divProductSelectorPager' class='admin-pager'></div>" +
        "        </div>" +
        "    </div>" +
        "</div>";
    document.write(strHtml);
}

function ProductSelector_Init() {

    $("#divProductSelector .SelectorBody .SelectorFilter input.QueryButton").click(function () {

        m_ProductSelector_ProductNoQuery = $("#txtSelectorFilterProductNo").val();
        m_ProductSelector_ProductNameQuery = $("#txtSelectorFilterProductName").val();

        ProductSelector_QueryProductList(m_ProductSelector_CurrentPageIndex);
    });

    $("#divProductSelector .SelectorHeader a").click(function () {
        ProductSelector_Hide();
    });
}

function ProductSelector_Show(obj) {

    var Point = GetAbsoluteLocationEx(obj);

    $("#divProductSelector").css("left", Point.X);
    $("#divProductSelector").css("top", Point.Y + obj.offsetHeight);
    $("#divProductSelector").show();
}

function ProductSelector_Hide() {

    $("#divProductSelector").hide();
}

ProductSelectorPageClick = function (pageclickednumber) {

    ProductSelector_QueryProductList(pageclickednumber);
}

function ProductSelector_QueryProductList(PageIndex) {

    $.post("ProductService.aspx",
    { "TransCode": "QueryProductList",
        "ProductNo": m_ProductSelector_ProductNoQuery,
        "ProductName": m_ProductSelector_ProductNameQuery,
        "ProductCategory": 0,
        "PageIndex": PageIndex,
        "PageSize": m_ProductSelector_PageSize
    }, function (data) {

        var htmlData = "";
        $("#divProductSelector .SelectorBody .SelectorContent .list tr:gt(0)").remove();

        $(data).find("Product").each(function () {

            var jqProduct = $(this);
            var ProductID = jqProduct.attr("ProductID");
            var ProductNo = jqProduct.attr("ProductNo");
            var ProductName = jqProduct.attr("ProductName");
            var ProductCategoryID = jqProduct.attr("ProductCategoryID");
            var ProductCategoryName = jqProduct.attr("ProductCategoryName");
            var ProductBrandName = jqProduct.attr("ProductBrandName");
            var ProductUnit = jqProduct.attr("ProductUnit");
            var SalesPrice = jqProduct.attr("SalesPrice");
            SalesPrice = ToMoney(SalesPrice);

            htmlData += "<tr>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td style='text-align:left;'>" + ProductName + "</td>" +
                            "<td>" + ProductCategoryName + "</td>" +
                            "<td>" + ProductBrandName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + SalesPrice + "</td>" +
                            "<td><a href='#none' onclick='ProductSelector_OnSelectProduct(this," + ProductID + ");return true;'>选择</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#divProductSelector .SelectorBody .SelectorContent .list tr:eq(0)");

        m_ProductSelector_PageCount = $(data).find("PageCount").text();
        m_ProductSelector_CurrentPageIndex = PageIndex;
        $("#divProductSelectorPager").pager({ pagenumber: m_ProductSelector_CurrentPageIndex, pagecount: m_ProductSelector_PageCount, buttonClickCallback: ProductSelectorPageClick });
    });
}


/****** 商品品牌下拉选择框处理 ******/
var m_ProductBrandSelector_ProductBrandCodeQuery;
var m_ProductBrandSelector_ProductBrandNameQuery;
var m_ProductBrandSelector_RowCount;
var m_ProductBrandSelector_PageCount;
var m_ProductBrandSelector_CurrentPageIndex = 1;
var m_ProductBrandSelector_PageSize = 10;

function ProductBrandSelector_Append() {

    var strHtml =
        "<div id='divProductBrandSelector' class='Selector ProductBrandSelector'>" +
        "    <div class='SelectorHeader'><span>请选择商品品牌</span><a href='#none'>关闭</a></div>" +
        "    <div class='SelectorBody'>" +
        "        <div class='SelectorFilter'>" +
        "            <ul>" +
        "                <li><span>商品品牌编号</span><input type='text' id='txtSelectorFilterProductBrandCode' style='width: 150px;' /></li>" +
        "                <li><span>商品品牌名称</span><input type='text' id='txtSelectorFilterProductBrandName' style='width: 150px;' /></li>" +
        "                <li><input type='button' class='QueryButton' value='查询' /></li>" +
        "            </ul>" +
        "        </div>" +
        "        <div class='clear'></div>" +
        "        <div class='SelectorContent'>" +
        "            <table class='list' cellpadding='0' cellspacing='0'>" +
        "                <tr>" +
        "                    <th style='width: 100px;'>商品品牌编号</th>" +
        "                    <th style='width: 300px;'>商品品牌名称</th>" +
        "                    <th style='width: 200px;'>产地</th>" +
        "                    <th style='width: 200px;'>生产商</th>" +
        "                    <th style='width: 60px;'>操作</th>" +
        "                </tr>" +
        "            </table>" +
        "            <div id='divProductBrandSelectorPager' class='admin-pager'></div>" +
        "        </div>" +
        "    </div>" +
        "</div>";
    document.write(strHtml);
}

function ProductBrandSelector_Init() {

    $("#divProductBrandSelector .SelectorBody .SelectorFilter input.QueryButton").click(function () {

        m_ProductBrandSelector_ProductBrandCodeQuery = $("#txtSelectorFilterProductBrandCode").val();
        m_ProductBrandSelector_ProductBrandNameQuery = $("#txtSelectorFilterProductBrandName").val();

        ProductBrandSelector_QueryProductBrandList(m_ProductBrandSelector_CurrentPageIndex);
    });

    $("#divProductBrandSelector .SelectorHeader a").click(function () {
        ProductBrandSelector_Hide();
    });
}

function ProductBrandSelector_Show(obj) {

    var Point = GetAbsoluteLocationEx(obj);

    $("#divProductBrandSelector").css("left", Point.X);
    $("#divProductBrandSelector").css("top", Point.Y + obj.offsetHeight);
    $("#divProductBrandSelector").show();
}

function ProductBrandSelector_Hide() {

    $("#divProductBrandSelector").hide();
}

ProductBrandSelectorPageClick = function (pageclickednumber) {

    ProductBrandSelector_QueryProductBrandList(pageclickednumber);
}

function ProductBrandSelector_QueryProductBrandList(PageIndex) {

    $.post("ProductService.aspx",
    { "TransCode": "QueryProductBrandList",
        "ProductBrandCode": m_ProductBrandSelector_ProductBrandCodeQuery,
        "ProductBrandName": m_ProductBrandSelector_ProductBrandNameQuery,
        "PageIndex": PageIndex,
        "PageSize": m_ProductBrandSelector_PageSize,
        "StateFlag": false
    }, function (data) {

        var htmlData = "";
        $("#divProductBrandSelector .SelectorBody .SelectorContent .list tr:gt(0)").remove();

        $(data).find("ProductBrand").each(function () {

            var jqProductBrand = $(this);
            var ProductBrandID = jqProductBrand.attr("ProductBrandID");
            var ProductBrandCode = jqProductBrand.attr("ProductBrandCode");
            var ProductBrandName = jqProductBrand.attr("ProductBrandName");
            var ProducingArea = jqProductBrand.attr("ProducingArea");
            var Producer = jqProductBrand.attr("Producer");

            htmlData += "<tr>" +
                            "<td>" + ProductBrandCode + "</td>" +
                            "<td style='text-align:left;'>" + ProductBrandName + "</td>" +
                            "<td>" + ProducingArea + "</td>" +
                            "<td>" + Producer + "</td>" +
                            "<td><a href='#none' onclick='ProductBrandSelector_OnSelectProductBrand(this," + ProductBrandID + ");return true;'>选择</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#divProductBrandSelector .SelectorBody .SelectorContent .list tr:eq(0)");

        m_ProductBrandSelector_PageCount = $(data).find("PageCount").text();
        m_ProductBrandSelector_CurrentPageIndex = PageIndex;
        $("#divProductBrandSelectorPager").pager({ pagenumber: m_ProductBrandSelector_CurrentPageIndex, pagecount: m_ProductBrandSelector_PageCount, buttonClickCallback: ProductBrandSelectorPageClick });
    });
}


/****** 批次下拉选择框处理 ******/
var m_BatchSelector_BatchNoQuery;
var m_BatchSelector_BatchNameQuery;
var m_BatchSelector_RowCount;
var m_BatchSelector_PageCount;
var m_BatchSelector_CurrentPageIndex = 1;
var m_BatchSelector_PageSize = 10;

function BatchSelector_Append() {

    var strHtml =
        "<div id='divBatchSelector' class='Selector BatchSelector'>" +
        "    <div class='SelectorHeader'><span>请选择批次</span><a href='#none'>关闭</a></div>" +
        "    <div class='SelectorBody'>" +
        "        <div class='SelectorFilter'>" +
        "            <ul>" +
        "                <li><span>批次编号</span><input type='text' id='txtSelectorFilterBatchNo' style='width: 150px;' /></li>" +
        "                <li><input type='button' class='QueryButton' value='查询' /></li>" +
        "            </ul>" +
        "        </div>" +
        "        <div class='clear'></div>" +
        "        <div class='SelectorContent'>" +
        "            <table class='list' cellpadding='0' cellspacing='0'>" +
        "                <tr>" +
        "                    <th style='width: 120px;'>批次编号</th>" +
        "                    <th style='width: 100px;'>批次日期</th>" +
        "                    <th style='width: 80px;'>批次序号</th>" +
        "                    <th style='width: 180px;'>开始时间</th>" +
        "                    <th style='width: 180px;'>结束时间</th>" +
        "                    <th style='width: 120px;'>状态</th>" +
        "                    <th style='width: 60px;'>操作</th>" +
        "                </tr>" +
        "            </table>" +
        "            <div id='divBatchSelectorPager' class='admin-pager'></div>" +
        "        </div>" +
        "    </div>" +
        "</div>";
    document.write(strHtml);
}

function BatchSelector_Init() {

    $("#divBatchSelector .SelectorBody .SelectorFilter input.QueryButton").click(function () {

        m_BatchSelector_BatchNoQuery = $("#txtSelectorFilterBatchNo").val();

        BatchSelector_QueryBatchCount();
    });

    $("#divBatchSelector .SelectorHeader a").click(function () {
        BatchSelector_Hide();
    });
}

function BatchSelector_Show(obj) {

    var Point = GetAbsoluteLocationEx(obj);

    $("#divBatchSelector").css("left", Point.X);
    $("#divBatchSelector").css("top", Point.Y + obj.offsetHeight);
    $("#divBatchSelector").show();
}

function BatchSelector_Hide() {

    $("#divBatchSelector").hide();
}

BatchSelectorPageClick = function (pageclickednumber) {

    BatchSelector_QueryBatchList(pageclickednumber);
}

function BatchSelector_QueryBatchCount() {

    $.post("BatchService.aspx",
    {
        "TransCode": "QueryBatchCount",
        "BatchNo": m_BatchSelector_BatchNoQuery
    },
    function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_BatchSelector_RowCount = new Number($(data).find("RowCount").text());

        m_BatchSelector_PageCount = parseInt(m_BatchSelector_RowCount / m_BatchSelector_PageSize);
        if (m_BatchSelector_RowCount % m_BatchSelector_PageSize > 0) m_BatchSelector_PageCount++;

        BatchSelector_QueryBatchList(m_BatchSelector_CurrentPageIndex);
    });
}

function BatchSelector_QueryBatchList(PageIndex) {

    $.post("BatchService.aspx",
    { "TransCode": "QueryBatchList",
        "BatchNo": m_BatchSelector_BatchNoQuery,
        "PageIndex": PageIndex,
        "PageSize": m_BatchSelector_PageSize
    }, function (data) {

        var htmlData = "";
        $("#divBatchSelector .SelectorBody .SelectorContent .list tr:gt(0)").remove();

        $(data).find("Batch").each(function () {

            var jqBatch = $(this);
            var BatchID = jqBatch.attr("BatchID");
            var BatchNo = jqBatch.attr("BatchNo");
            var BatchDate = jqBatch.attr("BatchDate");
            var BatchOrder = jqBatch.attr("BatchOrder");
            var FromTime = jqBatch.attr("FromTime");
            var ToTime = jqBatch.attr("ToTime");
            var Status = jqBatch.attr("Status");
            Status = Status + "-" + GetDictItemValue("BatchStatus", Status);

            htmlData += "<tr>" +
                            "<td>" + BatchNo + "</td>" +
                            "<td style='text-align:left;'>" + BatchDate + "</td>" +
                            "<td>" + BatchOrder + "</td>" +
                            "<td>" + FromTime + "</td>" +
                            "<td>" + ToTime + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td><a href='#none' onclick='BatchSelector_OnSelectBatch(this," + BatchID + ");return true;'>选择</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#divBatchSelector .SelectorBody .SelectorContent .list tr:eq(0)");

        m_BatchSelector_CurrentPageIndex = PageIndex;
        $("#divBatchSelectorPager").pager({ pagenumber: m_BatchSelector_CurrentPageIndex, pagecount: m_BatchSelector_PageCount, buttonClickCallback: BatchSelectorPageClick });
    });
}


/****** 商品范围下拉选择框处理 ******/
var m_ProductScopeSelector_ProductScopeNameQuery;
var m_ProductScopeSelector_RowCount;
var m_ProductScopeSelector_PageCount;
var m_ProductScopeSelector_CurrentPageIndex = 1;
var m_ProductScopeSelector_PageSize = 10;
var m_ProductScopeSelector_Binder = null;

function ProductScopeSelector_Append() {

    var strHtml =
        "<div id='divProductScopeSelector' class='Selector ProductScopeSelector'>" +
        "    <div class='SelectorHeader'><span>请选择商品范围</span><a href='#none'>关闭</a></div>" +
        "    <div class='SelectorBody'>" +
        "        <div class='SelectorFilter'>" +
        "            <ul>" +
        "                <li><span>商品范围名称</span><input type='text' id='txtSelectorFilterProductScopeName' style='width: 150px;' /></li>" +
        "                <li><input type='button' class='QueryButton' value='查询' /></li>" +
        "            </ul>" +
        "        </div>" +
        "        <div class='clear'></div>" +
        "        <div class='SelectorContent'>" +
        "            <table class='list' cellpadding='0' cellspacing='0'>" +
        "                <tr>" +
        "                    <th style='width: 150px;'>范围名称</th>" +
        "                    <th style='width: 300px;'>包含范围</th>" +
        "                    <th style='width: 300px;'>排除范围</th>" +
        "                    <th style='width: 150px;'>备注</th>" +
        "                    <th style='width: 60px;'>操作</th>" +
        "                </tr>" +
        "            </table>" +
        "            <div id='divProductScopeSelectorPager' class='admin-pager'></div>" +
        "        </div>" +
        "    </div>" +
        "</div>";
    document.write(strHtml);
}

function ProductScopeSelector_Init() {

    $("#divProductScopeSelector .SelectorBody .SelectorFilter input.QueryButton").click(function () {

        m_ProductScopeSelector_ProductScopeNameQuery = $("#txtSelectorFilterProductScopeName").val();

        ProductScopeSelector_QueryProductScopeList(m_ProductScopeSelector_CurrentPageIndex);
    });

    $("#divProductScopeSelector .SelectorHeader a").click(function () {
        ProductScopeSelector_Hide();
    });
}

function ProductScopeSelector_Show(obj) {

    var Point = GetAbsoluteLocationEx(obj);

    $("#divProductScopeSelector").css("left", Point.X);
    $("#divProductScopeSelector").css("top", Point.Y + obj.offsetHeight);
    $("#divProductScopeSelector").show();

    m_ProductScopeSelector_Binder = obj;
}

function ProductScopeSelector_Hide() {

    $("#divProductScopeSelector").hide();

    m_ProductScopeSelector_Binder = null;
}

ProductScopeSelectorPageClick = function (pageclickednumber) {

    ProductScopeSelector_QueryProductScopeList(pageclickednumber);
}

function ProductScopeSelector_QueryProductScopeList(PageIndex) {

    $.post("ConfigService.aspx",
    { "TransCode": "QueryProductScopeList",
        "ProductScopeName": m_ProductScopeSelector_ProductScopeNameQuery,
        "PageIndex": PageIndex,
        "PageSize": m_ProductScopeSelector_PageSize
    }, function (data) {

        var htmlData = "";
        $("#divProductScopeSelector .SelectorBody .SelectorContent .list tr:gt(0)").remove();

        $(data).find("ProductScope").each(function () {

            var jqProductScope = $(this);
            var ProductScopeID = jqProductScope.attr("ProductScopeID");
            var ProductScopeName = jqProductScope.attr("ProductScopeName");
            var Inclusion = jqProductScope.attr("Inclusion");
            var Exclusion = jqProductScope.attr("Exclusion");
            var Remark = jqProductScope.attr("Remark");

            htmlData += "<tr id='trProductScope" + ProductScopeID + "'>" +
                            "<td>" + ProductScopeName + "</td>" +
                            "<td>" + Inclusion + "</td>" +
                            "<td>" + Exclusion + "</td>" +
                            "<td>" + Remark + "</td>" +
                            "<td><a href='#none' onclick='ProductScopeSelector_OnSelectProductScope(this," + ProductScopeID + ");return true;'>选择</a></td>" +
                        "</tr>";
        });
        $(htmlData).insertAfter("#divProductScopeSelector .SelectorBody .SelectorContent .list tr:eq(0)");

        //m_ProductScopeSelector_PageCount = $(data).find("PageCount").text();
        //m_ProductScopeSelector_CurrentPageIndex = PageIndex;
        //$("#divProductScopeSelectorPager").pager({ pagenumber: m_ProductScopeSelector_CurrentPageIndex, pagecount: m_ProductScopeSelector_PageCount, buttonClickCallback: ProductScopeSelectorPageClick });
    });
}