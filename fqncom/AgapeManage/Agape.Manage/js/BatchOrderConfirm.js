var m_BatchReady = false;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;

    LoadDictItemList("OrderStatus,BatchStatus", OnBaseDictLoaded);
});


function CheckReady() {

    if (!g_DictReady) {
        alert("字典没有加载！");
        return false;
    }

    return true;
}

/****** 批次订单 ******/
function QueryBatchOrderList() {

    if (!CheckReady()) {
        return;
    }

    var OrderNo = $("#txtOrderNo").val();
    var OrderStatusSet = E_OrderStatus_Submit + "," + E_OrderStatus_Payed;

    $.post("BatchService.aspx",
        { "TransCode": "QueryBatchOrderList",
            "OrderNo": OrderNo,
            "OrderStatusSet": OrderStatusSet
        },
        function(data) {

            var htmlData = "";
            $("#tbBatchOrderList tr:gt(0)").remove();

            $(data).find("BatchOrder").each(function() {

                var jqOrder = $(this);
                var OrderID = jqOrder.attr("OrderID");
                var OrderNo = jqOrder.attr("OrderNo");
                var CreateTime = jqOrder.attr("CreateTime");
                var MemberName = jqOrder.attr("MemberName");
                var TotalQuantity = jqOrder.attr("TotalQuantity");
                var TotalAmount = jqOrder.attr("TotalAmount");
                var Status = jqOrder.attr("Status");
                Status = GetDictItemValue("OrderStatus", Status);

                htmlData +=
                    "<tr id='trOrder" + OrderID + "'>" +
                        "<td><input type='checkbox' value='" + OrderID + "' /></td>" +
                        "<td>" + OrderNo + "</td>" +
                        "<td>" + CreateTime + "</td>" +
                        "<td>" + MemberName + "</td>" +
                        "<td>" + TotalQuantity + "</td>" +
                        "<td>" + TotalAmount + "</td>" +
                        "<td>" + Status + "</td>" +
                        "<td>&nbsp;</td>" +
                    "</tr>";

            });
            $(htmlData).insertAfter("#tbBatchOrderList tr:eq(0)");

            alert("查询完成！");
        });
}

function BatchConfirmCheckedOrders() {

    if (!window.confirm("确认要对选中订单进行确认处理吗？")) {
        return false;
    }

    var OrderID = 0;
    $("#tbBatchOrderList input[type='checkbox']").each(function() {
        if (this.checked) {
            OrderID = $(this).val();
            BatchOrderConfirm(OrderID);
        }
    });
}

function BatchOrderConfirm(BatchOrderID) {

    if (BatchOrderID == 0) {
        alert("找不到订单号！");
        return;
    }

    var jqResultCell = $("#trOrder" + BatchOrderID + " td:eq(7)");
    jqResultCell.css("color", "blue").text("处理中");

    $.post("BatchService.aspx",
        { "TransCode": "BatchOrderConfirm",
            "OrderID": BatchOrderID
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                jqResultCell.css("color", "red").text(ReturnMessage);
                return false;
            }

            jqResultCell.css("color", "green").text("处理完成");
        });
}
