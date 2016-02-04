$(document).ready(function() {

});


/****** 商品入库单 ******/
function QueryStockInList() {

    SwitchDiv("StockInList");

    var StockInNo = $("txtQueryStockInNo").val();
    var FromDate = $("txtQueryFromDate").val();
    var ToDate = $("txtQueryToDate").val();

    $.post("InventoryService.aspx",
    { "TransCode": "QueryStockInList",
        "StockInNo": StockInNo,
        "FromDate": FromDate,
        "ToDate": ToDate
    },
    function(data) {

        var htmlData = "";
        $("#tbStockInList tr:gt(0)").remove();

        $(data).find("StockIn").each(function() {

            var jqStockIn = $(this);
            var StockInID = jqStockIn.attr("StockInID");
            var StockInNo = jqStockIn.attr("StockInNo");
            var StockInReason = jqStockIn.attr("StockInReason");
            var TotalQuantity = jqStockIn.attr("TotalQuantity");
            var TotalAmount = jqStockIn.attr("TotalAmount");
            var CreateTime = jqStockIn.attr("CreateTime");
            var Status = jqStockIn.attr("Status");

            htmlData += "<tr id='trStockIn" + StockInID + "'>" +
                            "<td>" + StockInNo + "</td>" +
                            "<td>" + CreateTime + "</td>" +
                            "<td>" + StockInReason + "</td>" +
                            "<td>" + TotalQuantity + "</td>" +
                            "<td>" + TotalAmount + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td><a href='#none' onclick='ViewStockIn(" + StockInID + ");return true;'>查看</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbStockInList tr:eq(0)");

        alert("查询完成！");
    });
}

function NewStockIn() {

    $("#hdnStockInID").val(0);
    $("#txtStockInNo").val("自动生成");
    $("#txtTotalQuantity").val("");
    $("#txtTotalAmount").val("");

    ResetProductQueryGroupBox();
    $("#tbStockInItemList tr:gt(0)").remove();

    SetStockInItemMode("New");
    SwitchDiv("StockInEditor");
}

function ViewStockIn(StockInID) {

    $.post("InventoryService.aspx", { "TransCode": "QueryStockInAndItems", "StockInID": StockInID }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var jqStockIn = $(data).find("StockIn");
        var StockInNo = jqStockIn.find("StockInNo").text();
        var TotalQuantity = jqStockIn.find("TotalQuantity").text();
        var TotalAmount = jqStockIn.find("TotalAmount").text();

        $("#hdnStockInID").val(StockInID);
        $("#txtStockInNo").val(StockInNo);
        $("#txtTotalQuantity").val(TotalQuantity);
        $("#txtTotalAmount").val(TotalAmount);

        var htmlData = "";
        $("#tbStockInItemList tr:gt(0)").remove();

        $(data).find("StockInItem").each(function() {

            var jqStockInItem = $(this);
            var ProductID = jqStockInItem.find("ProductID").text();
            var ProductSpecID = jqStockInItem.find("ProductSpecID").text();
            var ProductNo = jqStockInItem.find("ProductNo").text();
            var ProductName = jqStockInItem.find("ProductName").text();
            var ProductUnit = jqStockInItem.find("ProductUnit").text();
            var ProductSpec = jqStockInItem.find("ProductSpec").text();
            var BarCode = jqStockInItem.find("BarCode").text();
            var Price = jqStockInItem.find("Price").text();
            var Quantity = jqStockInItem.find("Quantity").text();
            var Amount = jqStockInItem.find("Amount").text();

            if (BarCode == "") BarCode = "&nbsp;";
            if (ProductSpec == "") ProductSpec = "&nbsp;";

            htmlData +=
                "<tr id='trProductSpec" + ProductSpecID + "'>" +
                    "<td><input type='checkbox' value='" + ProductSpecID + "' /></td>" +
                    "<td>" + ProductNo + "</td>" +
                    "<td>" + ProductName + "</td>" +
                    "<td>" + ProductUnit + "</td>" +
                    "<td>" + ProductSpec + "</td>" +
                    "<td>" + BarCode + "</td>" +
                    "<td>" + Price + "</td>" +
                    "<td>" + Quantity + "</td>" +
                    "<td>" + Amount + "</td>" +
                    "<td class='hide'>" + ProductID + "</td>" +
                    "<td class='hide'>" + ProductSpecID + "</td>" +
                "</tr>";
        });
        $(htmlData).insertAfter("#tbStockInItemList tr:eq(0)");

        SetStockInItemMode("View");
        SwitchDiv("StockInEditor");
    });
}

function SubmitStockIn() {

    if (!window.confirm("确认提交商品入库单吗？")) {
        return false;
    }

    var TotalQuantity = $("#txtTotalQuantity").val();
    var TotalAmount = $("#txtTotalAmount").val();

    var StockInItemListXml = "<?xml version='1.0' encoding='gb2312'?><StockInItems>";
    $("#tbStockInItemList tr:gt(0)").each(function() {

        var Price = $(this).find("td:eq(6)").text();
        var Quantity = $(this).find("td:eq(7)").text();
        var Amount = $(this).find("td:eq(8)").text();
        var ProductID = $(this).find("td:eq(9)").text();
        var ProductSpecID = $(this).find("td:eq(10)").text();

        StockInItemListXml +=
            "<StockInItem>" +
                "<ProductID>" + ProductID + "</ProductID>" +
                "<ProductSpecID>" + ProductSpecID + "</ProductSpecID>" +
                "<Price>" + Price + "</Price>" +
                "<Quantity>" + Quantity + "</Quantity>" +
                "<Amount>" + Amount + "</Amount>" +
            "</StockInItem>";
    });
    StockInItemListXml += "</StockInItems>";

    var encodeStockInItemListXml = escape(StockInItemListXml);

    $.post("InventoryService.aspx",
        { "TransCode": "SubmitStockIn",
            "TotalQuantity": TotalQuantity,
            "TotalAmount": TotalAmount,
            "StockInItemListXml": encodeStockInItemListXml
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            alert("处理成功！");
            NewStockIn();
        });
}

function BackToStockInList() {

    SwitchDiv("StockInList");
}


/****** 商品处理 ******/
function ResetProductQueryGroupBox() {

    $("#txtQueryProductNo").val("");
    $("#txtQueryBarCode").val("");
    $("#txtQuantity").val(1);
    
    $("#hdnProductID").val(0);
    $("#hdnProductSpecID").val(0);
    $("#txtProductNo").val("");
    $("#txtProductName").val("");
    $("#txtProductUnit").val("");
    $("#txtProductSpec").val("");
    $("#txtBarCode").val("");
    $("#txtSalesPrice").val("");
}

function QueryProduct() {

    var ProductNo = $("#txtQueryProductNo").val();
    var BarCode = $("#txtQueryBarCode").val();

    $.post("BasicService.aspx",
    { "TransCode": "QueryProduct",
        "ProductNo": ProductNo,
        "BarCode": BarCode
    },
    function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            $("#txtProductID").val("");
            $("#txtProductNo").val("");
            $("#txtProductName").val("");
            $("#txtProductUnit").val("");
            $("#txtProductSpec").val("");
            $("#txtBarCode").val("");
            $("#txtSalesPrice").val("");

            alert(ReturnMessage);
            return false;
        }

        var ProductID = $(data).find("Product ProductID").text();
        var ProductNo = $(data).find("Product ProductNo").text();
        var ProductName = $(data).find("Product ProductName").text();
        var ProductUnit = $(data).find("Product ProductUnit").text();
        var ProductSpecID = $(data).find("ProductSpec ProductSpecID").text();
        var ProductSpec = $(data).find("ProductSpec ProductSpec").text();
        var BarCode = $(data).find("Product BarCode").text();
        var SalesPrice = $(data).find("Product SalesPrice").text();

        $("#txtProductID").val(ProductID);
        $("#txtProductNo").val(ProductNo);
        $("#txtProductName").val(ProductName);
        $("#txtProductUnit").val(ProductUnit);
        $("#txtProductSpec").val(ProductSpec);
        $("#txtBarCode").val(BarCode);
        $("#txtSalesPrice").val(SalesPrice);
        $("#hdnProductID").val(ProductID);
        $("#hdnProductSpecID").val(ProductSpecID);

        alert("查询商品成功！");
    });
}

function AddProduct() {

    var ProductID = $("#txtProductID").val();
    if (ProductID == "") {
        alert("请选择商品！");
        return;
    }

    var Quantity = $("#txtQuantity").val();
    if (Quantity == "") {
        alert("请输入商品数量！");
        return;
    }

    var ProductID = $("#hdnProductID").val();
    var ProductSpecID = $("#hdnProductSpecID").val();
    var ProductNo = $("#txtProductNo").val();
    var ProductName = $("#txtProductName").val();
    var ProductUnit = $("#txtProductUnit").val();
    var ProductSpec = $("#txtProductSpec").val();
    var BarCode = $("#txtBarCode").val();
    var SalesPrice = $("#txtSalesPrice").val();
    var Amount = parseInt(SalesPrice) * parseInt(Quantity);

    if (BarCode == "") BarCode = "&nbsp;";
    if (ProductSpec == "") ProductSpec = "&nbsp;";

    var htmlData =
        "<tr id='trProductSpec" + ProductSpecID + "'>" +
            "<td><input type='checkbox' value='" + ProductSpecID + "' /></td>" +
            "<td>" + ProductNo + "</td>" +
            "<td>" + ProductName + "</td>" +
            "<td>" + ProductUnit + "</td>" +
            "<td>" + ProductSpec + "</td>" +
            "<td>" + BarCode + "</td>" +
            "<td>" + SalesPrice + "</td>" +
            "<td>" + Quantity + "</td>" +
            "<td>" + Amount + "</td>" +
            "<td class='hide'>" + ProductID + "</td>" +
            "<td class='hide'>" + ProductSpecID + "</td>" +
        "</tr>";

    $("#tbStockInItemList").append(htmlData);

    UpdateTotalData();
}

function UpdateTotalData() {

    var TotalQuantity = 0, TotalAmount = 0;
    $("#tbStockInItemList tr:gt(0)").each(function() {
        var Quantity = $(this).find("td:eq(7)").text();
        var Amount = $(this).find("td:eq(8)").text();

        TotalQuantity += parseInt(Quantity);
        TotalAmount += parseInt(Amount);
    });

    $("#txtTotalQuantity").val(TotalQuantity);
    $("#txtTotalAmount").val(TotalAmount);
}


/****** 功能函数 ******/
function SetStockInItemMode(ItemMode) {

    if (ItemMode == "New") {

        $("#fstSelectProduct").show();
        $("#divStockInItemToolBar").show();
        $("#btnSubmitStockIn").show();
        $("#btnCancelStockIn").hide();
    }
    else {
    
        $("#fstSelectProduct").hide();
        $("#divStockInItemToolBar").hide();
        $("#btnSubmitStockIn").hide();
        $("#btnCancelStockIn").show();
    }
}

function SwitchDiv(DivName) {

    if (DivName == "StockInList") {

        $("#divStockInList").show();
        $("#divStockInEditor").hide();
    }
    else if (DivName == "StockInEditor") {

        $("#divStockInEditor").show();
        $("#divStockInList").hide();
    }
}
