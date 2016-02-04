var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;

});

function OnQuery() {

    QueryProductStockCount();
}

/****** 商品库存 ******/
function QueryProductStockCount() {

    $.post("InventoryService.aspx", { "TransCode": "QueryProductStockCount" }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_RowCount = new Number($(data).find("RowCount").text());

        m_PageCount = parseInt(m_RowCount / m_PageSize);
        if (m_RowCount % m_PageSize > 0) m_PageCount++;

        QueryProductStockList(m_CurrentPageIndex);
    });
}

PageClick = function(pageclickednumber) {

    QueryProductStockList(pageclickednumber);
}

function QueryProductStockList(pageclickednumber) {

    $.post("InventoryService.aspx",
    { "TransCode": "QueryProductStockList",
        "PageIndex": pageclickednumber,
        "PageSize": m_PageSize
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_PageCount = $(data).find("PageCount").text();
        m_RowCount = $(data).find("RowCount").text();

        var htmlData = "";
        $("#tbProductStockList tr:gt(0)").remove();

        $(data).find("ProductStock").each(function() {

            var jqProductStock = $(this);
            var ProductStockID = jqProductStock.find("ProductStockID").text();
            var ProductID = jqProductStock.find("ProductID").text();
            var ProductNo = jqProductStock.find("ProductNo").text();
            var ProductName = jqProductStock.find("ProductName").text();
            var ProductUnit = jqProductStock.find("ProductUnit").text();
            var ProductSpecID = jqProductStock.find("ProductSpecID").text();
            var ProductSpec = jqProductStock.find("ProductSpec").text();
            var WarehouseID = jqProductStock.find("WarehouseID").text();
            var WarehouseName = jqProductStock.find("WarehouseName").text();
            var Quantity = jqProductStock.find("Quantity").text();
            var FrozenQuantity = jqProductStock.find("FrozenQuantity").text();
            var RemainQuantity = jqProductStock.find("RemainQuantity").text();

            htmlData += "<tr>" +
                            "<td>" + WarehouseName + "</td>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + Quantity + "</td>" +
                            "<td>" + FrozenQuantity + "</td>" +
                            "<td>" + RemainQuantity + "</td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbProductStockList tr:eq(0)");

        m_CurrentPageIndex = pageclickednumber;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}