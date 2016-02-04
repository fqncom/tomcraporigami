BatchSelector_Append();

$(document).ready(function() {

    LoadDictItemList("BatchStatus", OnBaseDictLoaded);
    BatchSelector_Init();
});


function CheckReady() {

    if (!g_DictReady) {
        alert("字典没有加载！");
        return false;
    }

    return true;
}


/****** 商品下拉选择框处理 ******/
function BatchSelector_OnSelectBatch(obj, BatchID) {

    var jqRow = $(obj).parent().parent();
    var BatchNo = jqRow.children("td:eq(0)").text();

    $("#txtBatchNo").val(BatchNo);
    $("#hdnBatchID").val(BatchID);

    BatchSelector_Hide();
}


/****** 批次订单 ******/
function QueryBatchOrderList() {

    if (!CheckReady()) {
        return;
    }

    var BatchID = $("#hdnBatchID").val();
    if (BatchID == 0) {
        alert("请选择批次！");
        return;
    }

    $.post("BatchService.aspx",
    { "TransCode": "QueryBatchOrderList",
        "BatchID": BatchID,
        "BatchStatus": E_BatchStatus_OrderPack
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
             var BatchStatus = jqOrder.attr("BatchStatus");
             BatchStatus = GetDictItemValue("BatchStatus", BatchStatus);

             htmlData += "<tr id='trOrder" + OrderID + "'>" +
                            "<td><input type='checkbox' value='" + OrderID + "' /></td>" +
                            "<td>" + OrderNo + "</td>" +
                            "<td>" + CreateTime + "</td>" +
                            "<td>" + MemberName + "</td>" +
                            "<td>" + TotalQuantity + "</td>" +
                            "<td>" + TotalAmount + "</td>" +
                            "<td>" + BatchStatus + "</td>" +
                            "<td>&nbsp;</td>" +
                        "</tr>";

         });
         $(htmlData).insertAfter("#tbBatchOrderList tr:eq(0)");

         alert("查询完成！");
     });
}

function BatchDeliverCheckedOrders() {

    if (!window.confirm("确认要对选中订单进行配送处理吗？")) {
        return false;
    }

    var OrderID = 0;
    $("#tbBatchOrderList input[type='checkbox']").each(function() {
        if (this.checked) {
            OrderID = $(this).val();
            BatchOrderDeliver(OrderID);
        }
    });  
}

function BatchOrderDeliver(BatchOrderID) {

    if (BatchOrderID == 0) {
        alert("找不到订单号！");
        return;
    }

    var jqResultCell = $("#trOrder" + BatchOrderID + " td:eq(7)");
    jqResultCell.css("color", "green").text("处理中");

    $.post("BatchService.aspx",
        { "TransCode": "BatchOrderDeliver",
            "OrderID": BatchOrderID
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                jqResultCell.css("color", "red").text(ReturnMessage);
                return false;
            }

            $("#trOrder" + BatchOrderID).remove();
        });
}


/****** 功能函数 ******/