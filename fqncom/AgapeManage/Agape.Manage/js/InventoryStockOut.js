$(document).ready(function() {

});


/****** 商品出库单 ******/
function QueryStockOutList() {

    SwitchDiv("StockOutList");

    var StockOutNo = $("txtQueryStockOutNo").val();
    var FromDate = $("txtQueryFromDate").val();
    var ToDate = $("txtQueryToDate").val();

    $.post("InventoryService.aspx",
    { "TransCode": "QueryStockOutList",
        "StockOutNo": StockOutNo,
        "FromDate": FromDate,
        "ToDate": ToDate
    },
    function(data) {

        var htmlData = "";
        $("#tbStockOutList tr:gt(0)").remove();

        $(data).find("StockOut").each(function() {

            var jqStockOut = $(this);
            var StockOutID = jqStockOut.attr("StockOutID");
            var StockOutNo = jqStockOut.attr("StockOutNo");
            var StockOutReason = jqStockOut.attr("StockOutReason");
            var TotalQuantity = jqStockOut.attr("TotalQuantity");
            var TotalAmount = jqStockOut.attr("TotalAmount");
            var CreateTime = jqStockOut.attr("CreateTime");
            var Status = jqStockOut.attr("Status");

            htmlData += "<tr id='trStockOut" + StockOutID + "'>" +
                            "<td>" + StockOutNo + "</td>" +
                            "<td>" + CreateTime + "</td>" +
                            "<td>" + StockOutReason + "</td>" +
                            "<td>" + TotalQuantity + "</td>" +
                            "<td>" + TotalAmount + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td><a href='#none' onclick='ViewStockOut(" + StockOutID + ");return true;'>查看</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbStockOutList tr:eq(0)");

        alert("查询完成！");
    });
}

function NewStockOut() {

    $("#hdnStockOutID").val(0);
    $("#txtStockOutNo").val("自动生成");
    $("#txtTotalQuantity").val("");
    $("#txtTotalAmount").val("");

    ResetProductQueryGroupBox();
    $("#tbStockOutItemList tr:gt(0)").remove();

    SetStockOutItemMode("New");
    SwitchDiv("StockOutEditor");
}

function ViewStockOut(StockOutID) {

    $.post("InventoryService.aspx", { "TransCode": "QueryStockOutAndItems", "StockOutID": StockOutID }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var jqStockOut = $(data).find("StockOut");
        var StockOutNo = jqStockOut.find("StockOutNo").text();
        var TotalQuantity = jqStockOut.find("TotalQuantity").text();
        var TotalAmount = jqStockOut.find("TotalAmount").text();

        $("#hdnStockOutID").val(StockOutID);
        $("#txtStockOutNo").val(StockOutNo);
        $("#txtTotalQuantity").val(TotalQuantity);
        $("#txtTotalAmount").val(TotalAmount);

        var htmlData = "";
        $("#tbStockOutItemList tr:gt(0)").remove();

        $(data).find("StockOutItem").each(function() {

            var jqStockOutItem = $(this);
            var ProductID = jqStockOutItem.find("ProductID").text();
            var ProductSpecID = jqStockOutItem.find("ProductSpecID").text();
            var ProductNo = jqStockOutItem.find("ProductNo").text();
            var ProductName = jqStockOutItem.find("ProductName").text();
            var ProductUnit = jqStockOutItem.find("ProductUnit").text();
            var ProductSpec = jqStockOutItem.find("ProductSpec").text();
            var BarCode = jqStockOutItem.find("BarCode").text();
            var Price = jqStockOutItem.find("Price").text();
            var Quantity = jqStockOutItem.find("Quantity").text();
            var Amount = jqStockOutItem.find("Amount").text();

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
        $(htmlData).insertAfter("#tbStockOutItemList tr:eq(0)");

        SetStockOutItemMode("View");
        SwitchDiv("StockOutEditor");
    });
}

function SubmitStockOut() {

    if (!window.confirm("确认提交商品出库单吗？")) {
        return false;
    }

    var TotalQuantity = $("#txtTotalQuantity").val();
    var TotalAmount = $("#txtTotalAmount").val();

    var StockOutItemListXml = "<?xml version='1.0' encoding='gb2312'?><StockOutItems>";
    $("#tbStockOutItemList tr:gt(0)").each(function() {

        var Price = $(this).find("td:eq(6)").text();
        var Quantity = $(this).find("td:eq(7)").text();
        var Amount = $(this).find("td:eq(8)").text();
        var ProductID = $(this).find("td:eq(9)").text();
        var ProductSpecID = $(this).find("td:eq(10)").text();

        StockOutItemListXml +=
            "<StockOutItem>" +
                "<ProductID>" + ProductID + "</ProductID>" +
                "<ProductSpecID>" + ProductSpecID + "</ProductSpecID>" +
                "<Price>" + Price + "</Price>" +
                "<Quantity>" + Quantity + "</Quantity>" +
                "<Amount>" + Amount + "</Amount>" +
            "</StockOutItem>";
    });
    StockOutItemListXml += "</StockOutItems>";

    var encodeStockOutItemListXml = escape(StockOutItemListXml);

    $.post("InventoryService.aspx",
        { "TransCode": "SubmitStockOut",
            "TotalQuantity": TotalQuantity,
            "TotalAmount": TotalAmount,
            "StockOutItemListXml": encodeStockOutItemListXml
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            alert("处理成功！");
            NewStockOut();
        });
}

function BackToStockOutList() {

    SwitchDiv("StockOutList");
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

    $("#tbStockOutItemList").append(htmlData);

    UpdateTotalData();
}

function UpdateTotalData() {

    var TotalQuantity = 0, TotalAmount = 0;
    $("#tbStockOutItemList tr:gt(0)").each(function() {
        var Quantity = $(this).find("td:eq(7)").text();
        var Amount = $(this).find("td:eq(8)").text();

        TotalQuantity += parseInt(Quantity);
        TotalAmount += parseInt(Amount);
    });

    $("#txtTotalQuantity").val(TotalQuantity);
    $("#txtTotalAmount").val(TotalAmount);
}


/****** 功能函数 ******/
function SetStockOutItemMode(ItemMode) {

    if (ItemMode == "New") {

        $("#fstSelectProduct").show();
        $("#divStockOutItemToolBar").show();
        $("#btnSubmitStockOut").show();
        $("#btnCancelStockOut").hide();
    }
    else {
    
        $("#fstSelectProduct").hide();
        $("#divStockOutItemToolBar").hide();
        $("#btnSubmitStockOut").hide();
        $("#btnCancelStockOut").show();
    }
}

function SwitchDiv(DivName) {

    if (DivName == "StockOutList") {

        $("#divStockOutList").show();
        $("#divStockOutEditor").hide();
    }
    else if (DivName == "StockOutEditor") {

        $("#divStockOutEditor").show();
        $("#divStockOutList").hide();
    }
}
