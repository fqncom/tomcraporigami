var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;
});


function OnQuery() {

    QueryProductStockChangeCount();
}

/****** 商品库存 ******/
function QueryProductStockChangeCount() {

    $.post("InventoryService.aspx", { "TransCode": "QueryProductStockChangeCount" }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_RowCount = new Number($(data).find("RowCount").text());

        m_PageCount = parseInt(m_RowCount / m_PageSize);
        if (m_RowCount % m_PageSize > 0) m_PageCount++;

        QueryProductStockChangeList(m_CurrentPageIndex);
    });
}

PageClick = function(pageclickednumber) {

    QueryProductStockChangeList(pageclickednumber);
}

function QueryProductStockChangeList(pageclickednumber) {

    $.post("InventoryService.aspx",
    { "TransCode": "QueryProductStockChangeList",
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
        $("#tbProductStockChangeList tr:gt(0)").remove();

        $(data).find("ProductStockChange").each(function() {

            var jqProductStockChange = $(this);
            var ProductID = jqProductStockChange.find("ProductID").text();
            var ProductNo = jqProductStockChange.find("ProductNo").text();
            var ProductName = jqProductStockChange.find("ProductName").text();
            var ProductUnit = jqProductStockChange.find("ProductUnit").text();
            var ProductSpecID = jqProductStockChange.find("ProductSpecID").text();
            var ProductSpec = jqProductStockChange.find("ProductSpec").text();
            var WarehouseID = jqProductStockChange.find("WarehouseID").text();
            var WarehouseName = jqProductStockChange.find("WarehouseName").text();
            var ChangeDate = jqProductStockChange.find("ChangeDate").text();
            var ChangeType = jqProductStockChange.find("ChangeType").text();
            var ChangeQuantity = jqProductStockChange.find("ChangeQuantity").text();
            var OperatorName = jqProductStockChange.find("OperatorName").text();
            var Status = jqProductStockChange.find("Status").text();

            htmlData += "<tr>" +
                            "<td>" + WarehouseName + "</td>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + ChangeDate + "</td>" +
                            "<td>" + ChangeType + "</td>" +
                            "<td>" + ChangeQuantity + "</td>" +
                            "<td>" + OperatorName + "</td>" +
                            "<td>" + Status + "</td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbProductStockChangeList tr:eq(0)");

        m_CurrentPageIndex = pageclickednumber;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}