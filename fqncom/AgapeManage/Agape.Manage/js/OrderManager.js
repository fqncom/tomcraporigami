var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

BatchSelector_Append();

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;

    LoadDictItemList("OrderStatus,BatchStatus", OnBaseDictLoaded);
    BatchSelector_Init();
});


/****** 商品下拉选择框处理 ******/
function BatchSelector_OnSelectBatch(obj, BatchID) {

    var jqRow = $(obj).parent().parent();
    var BatchNo = jqRow.children("td:eq(0)").text();

    $("#txtBatchNo").val(BatchNo);
    $("#hdnBatchID").val(BatchID);

    BatchSelector_Hide();
}


/****** 事件响应 ******/
function OnQueryOrderList() {

    if (!g_DictReady) {

        alert("字典还未加载！");
        return;
    }

    QueryOrderCount();
}


/****** 订单列表处理 ******/
function QueryOrderCount() {

    var OrderNo = $("#txtOrderNo").val();
    var BatchID = $("#hdnBatchID").val();

    $.post("BatchService.aspx",
    {
        "TransCode": "QueryBatchOrderCount",
        "OrderNo": OrderNo,
        "BatchID": BatchID
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

        QueryOrderList(m_CurrentPageIndex);
    });
}

function QueryOrderList(pageclickednumber) {

    var OrderNo = $("#txtOrderNo").val();
    var BatchID = $("#hdnBatchID").val();

    $.post("BatchService.aspx",
    { "TransCode": "QueryBatchOrderList",
        "OrderNo": OrderNo,
        "BatchID": BatchID,
        "PageIndex": pageclickednumber,
        "PageSize": m_PageSize
    }, function(data) {

        var htmlData = "";
        $("#tbOrderList tr:gt(0)").remove();

        $(data).find("BatchOrder").each(function() {

            var jqOrder = $(this);
            var OrderID = jqOrder.attr("OrderID");
            var BatchNo = jqOrder.attr("BatchNo");
            var OrderNo = jqOrder.attr("OrderNo");
            var CreateTime = jqOrder.attr("CreateTime");
            var MemberName = jqOrder.attr("MemberName");
            var RealName = jqOrder.attr("RealName");
            var TotalQuantity = jqOrder.attr("TotalQuantity");
            var TotalAmount = jqOrder.attr("TotalAmount");
            var Status = jqOrder.attr("Status");
            var BatchStatus = jqOrder.attr("BatchStatus");
            Status = Status + "-" + GetDictItemValue("OrderStatus", Status);
            BatchStatus = BatchStatus + "-" + GetDictItemValue("BatchStatus", BatchStatus);
            if (RealName == "") RealName = "[" + MemberName + "]";

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + OrderID + "' /></td>" +
                            "<td>" + BatchNo + "</td>" +
                            "<td>" + OrderNo + "</td>" +
                            "<td>" + CreateTime + "</td>" +
                            "<td>" + RealName + "</td>" +
                            "<td>" + TotalQuantity + "</td>" +
                            "<td>" + TotalAmount + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td>" + BatchStatus + "</td>" +
                            "<td><a href='#none' onclick='OnViewOrder(" + OrderID + ");return true;'>查看</a>|<a href='#none' onclick='OnDeleteOrder(" + OrderID + ");return true;'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbOrderList tr:eq(0)");

        m_CurrentPageIndex = pageclickednumber;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}

PageClick = function(pageclickednumber) {

    QueryOrderList(pageclickednumber);
}


/****** 订单明细处理 ******/
function OnViewOrder(BatchOrderID) {

    $.post("SalesService.aspx",
    {
        "TransCode": "QueryOrderAndItems",
        "OrderID": BatchOrderID
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var jqOrder = $(data).find("Order");
        var OrderNo = jqOrder.find("OrderNo").text();
        var BarCode = jqOrder.find("BarCode").text();
        var MemberName = jqOrder.find("MemberName").text();
        var RealName = jqOrder.find("RealName").text();
        var CreateTime = jqOrder.find("CreateTime").text();
        var TotalQuantity = jqOrder.find("TotalQuantity").text();
        var TotalAmount = jqOrder.find("TotalAmount").text();
        var ReceiverName = jqOrder.find("ReceiverName").text();
        var MobilePhone = jqOrder.find("MobilePhone").text();
        var Province = jqOrder.find("Province").text();
        var City = jqOrder.find("City").text();
        var District = jqOrder.find("District").text();
        var AddressDetail = jqOrder.find("AddressDetail").text();
        var FullAddress = GetAreaName(Province) + GetAreaName(City) + GetAreaName(District) + AddressDetail;
        if (RealName != "") MemberName = RealName + "[" + MemberName + "]";

        $("#divOrder span.OrderNo").text(OrderNo);
        $("#divOrder span.BarCode").text(BarCode);
        $("#divOrder span.MemberName").text(MemberName);
        $("#divOrder span.CreateTime").text(CreateTime);
        $("#divOrder span.TotalQuantity").text(TotalQuantity);
        $("#divOrder span.TotalAmount").text(TotalAmount);
        $("#divOrder span.ReceiverName").text(ReceiverName);
        $("#divOrder span.MobilePhone").text(MobilePhone);
        $("#divOrder span.FullAddress").text(FullAddress);

        var htmlData = "";
        $("#tbOrderItem tr:gt(0)").remove();

        $(data).find("OrderItem").each(function() {

            var jqProduct = $(this);
            var ProductSpecID = jqProduct.find("ProductSpecID").text();
            var ProductNo = jqProduct.find("ProductNo").text();
            var ProductName = jqProduct.find("ProductName").text();
            var ProductUnit = jqProduct.find("ProductUnit").text();
            var ProductSpec = jqProduct.find("ProductSpec").text();
            var Price = jqProduct.find("Price").text();
            var Quantity = jqProduct.find("Quantity").text();
            var Amount = jqProduct.find("Amount").text();
            Price = ToMoney(Price);
            Amount = ToMoney(Amount);

            htmlData += "<tr>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + Price + "</td>" +
                            "<td>" + Quantity + "</td>" +
                            "<td>" + Amount + "</td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbOrderItem tr:eq(0)");

        $("#hdnOrderID").val(BatchOrderID);
        SwitchDiv("OrderView");
    });
}

function BackToOrderList() {

    SwitchDiv("OrderList");
}

/****** 功能函数 ******/
function SwitchDiv(DivName) {

    if (DivName == "OrderList") {

        $("#divOrderList").show();
        $("#divOrderView").hide();
    }
    else if (DivName == "OrderView") {

        $("#divOrderView").show();
        $("#divOrderList").hide();
    }
}