var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;

    LoadDictItemList("BatchStatus", OnBaseDictLoaded);

});

function OnQueryBatchList() {

    QueryBatchCount();
}

/****** 批次处理 ******/
function QueryBatchCount() {

    var BatchNo = $("#txtBatchNo").val();
    var FromDate = $("#txtFromDate").val();
    var ToDate = $("#txtToDate").val();
    var HasOrder = document.getElementById("ckbHasOrder").checked;

    $.post("BatchService.aspx",
    {
        "TransCode": "QueryBatchCount",
        "BatchNo": BatchNo,
        "FromDate": FromDate,
        "ToDate": ToDate,
        "HasOrder": HasOrder
    },
    function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_RowCount = new Number($(data).find("RowCount").text());

        m_PageCount = parseInt(m_RowCount / m_PageSize);
        if (m_RowCount % m_PageSize > 0) m_PageCount++;

        QueryBatchList(m_CurrentPageIndex);
    });
}

function QueryBatchList(pageclickednumber) {

    var BatchNo = $("#txtBatchNo").val();
    var FromDate = $("#txtFromDate").val();
    var ToDate = $("#txtToDate").val();
    var HasOrder = document.getElementById("ckbHasOrder").checked;

    $.post("BatchService.aspx",
    { "TransCode": "QueryBatchList",
        "BatchNo": BatchNo,
        "FromDate": FromDate,
        "ToDate": ToDate,
        "HasOrder": HasOrder,
        "PageIndex": pageclickednumber,
        "PageSize": m_PageSize
    }, function(data) {

        var htmlData = "";
        $("#tbBatchList tr:gt(1)").remove();

        $(data).find("Batch").each(function() {

            var jqBatch = $(this);
            var BatchID = jqBatch.attr("BatchID");
            var BatchNo = jqBatch.attr("BatchNo");
            var BatchDate = jqBatch.attr("BatchDate");
            var BatchOrder = jqBatch.attr("BatchOrder");
            var FromTime = jqBatch.attr("FromTime");
            var ToTime = jqBatch.attr("ToTime");
            var ConfirmOrderCount = jqBatch.attr("ConfirmOrderCount");
            var ConfirmTotalQuantity = jqBatch.attr("ConfirmTotalQuantity");
            var ConfirmTotalAmount = jqBatch.attr("ConfirmTotalAmount");
            var PickOrderCount = jqBatch.attr("PickOrderCount");
            var PickTotalQuantity = jqBatch.attr("PickTotalQuantity");
            var PickTotalAmount = jqBatch.attr("PickTotalAmount");
            var PackOrderCount = jqBatch.attr("PackOrderCount");
            var PackTotalQuantity = jqBatch.attr("PackTotalQuantity");
            var PackTotalAmount = jqBatch.attr("PackTotalAmount");
            var DeliverOrderCount = jqBatch.attr("DeliverOrderCount");
            var DeliverTotalQuantity = jqBatch.attr("DeliverTotalQuantity");
            var DeliverTotalAmount = jqBatch.attr("DeliverTotalAmount");
            var FinishOrderCount = jqBatch.attr("FinishOrderCount");
            var FinishTotalQuantity = jqBatch.attr("FinishTotalQuantity");
            var FinishTotalAmount = jqBatch.attr("FinishTotalAmount");
            var Status = jqBatch.attr("Status");
            Status = Status + "-" + GetDictItemValue("BatchStatus", Status);

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + BatchID + "' /></td>" +
                            "<td>" + BatchNo + "</td>" +
                            "<td>" + BatchDate + "</td>" +
                            "<td>" + BatchOrder + "</td>" +
                            "<td>" + FromTime + "</td>" +
                            "<td>" + ToTime + "</td>" +
                            "<td>" + ConfirmOrderCount + "</td>" +
                            "<td>" + ConfirmTotalQuantity + "</td>" +
                            "<td>" + ConfirmTotalAmount + "</td>" +
                            "<td>" + PickOrderCount + "</td>" +
                            "<td>" + PickTotalQuantity + "</td>" +
                            "<td>" + PickTotalAmount + "</td>" +
                            "<td>" + PackOrderCount + "</td>" +
                            "<td>" + PackTotalQuantity + "</td>" +
                            "<td>" + PackTotalAmount + "</td>" +
                            "<td>" + DeliverOrderCount + "</td>" +
                            "<td>" + DeliverTotalQuantity + "</td>" +
                            "<td>" + DeliverTotalAmount + "</td>" +
                            "<td>" + FinishOrderCount + "</td>" +
                            "<td>" + FinishTotalQuantity + "</td>" +
                            "<td>" + FinishTotalAmount + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbBatchList tr:eq(1)");

        m_CurrentPageIndex = pageclickednumber;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}

PageClick = function(pageclickednumber) {

    QueryBatchList(pageclickednumber);
}
