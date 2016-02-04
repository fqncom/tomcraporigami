$(document).ready(function() {

});

function OnQueryExchangeProductList() {

    QueryExchangeProductList();
    
    alert("查询完成！");
}


/****** 兑换商品列表处理 ******/
function QueryExchangeProductList() {

    SwitchDiv("ExchangeProductList");

    var ProductNo = $("#txtQueryProductNo").val();
    var ProductName = $("#txtQueryProductName").val();

    $.post("ConfigService.aspx",
    { "TransCode": "QueryExchangeProductList",
        "ProductNo": ProductNo,
        "ProductName": ProductName
    },
    function(data) {

        var htmlData = "";
        $("#tbExchangeProductList tr:gt(0)").remove();

        $(data).find("ExchangeProduct").each(function() {

            var jqExchangeProduct = $(this);
            var ProductID = jqExchangeProduct.attr("ProductID");
            var ProductNo = jqExchangeProduct.attr("ProductNo");
            var ProductName = jqExchangeProduct.attr("ProductName");
            var ProductSpec = jqExchangeProduct.attr("ProductSpec");
            var ProductUnit = jqExchangeProduct.attr("ProductUnit");
            var PointValue = jqExchangeProduct.attr("PointValue");

            htmlData += "<tr id='trExchangeProduct" + ProductID + "'>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + PointValue + "</td>" +
                            "<td>" +
                            "   <a href='#none' onclick='DeleteExchangeProduct(" + ProductID + ");return true;'>删除</a>" +
                            "</td>" +
                        "</tr>";

        });
        
        $(htmlData).insertAfter("#tbExchangeProductList tr:eq(0)");
    });
}

function OnNewExchangeProduct() {

    ResetExchangeProductEditor();
    
    SwitchDiv("ExchangeProductEditor");
}


/****** 兑换商品处理 ******/
function OnQueryProduct() {

    var ProductNo = $("#txtProductNo").val();
    var BarCode = $("#txtBarCode").val();

    $.post("ConfigService.aspx",
    { "TransCode": "QueryProduct",
        "ProductNo": ProductNo,
        "BarCode": BarCode
    },
    function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            ResetExchangeProductEditor();

            alert(ReturnMessage);
            return false;
        }

        var ProductID = $(data).find("Product ProductID").text();
        var ProductNo = $(data).find("Product ProductNo").text();
        var ProductName = $(data).find("Product ProductName").text();
        var ProductSpec = $(data).find("ProductSpec ProductSpec").text();
        var ProductUnit = $(data).find("Product ProductUnit").text();
        var ProductSpecID = $(data).find("ProductSpec ProductSpecID").text();
        var BarCode = $(data).find("Product BarCode").text();

        $("#hdnProductID").val(ProductID);
        $("#txtProductNo").val(ProductNo);
        $("#txtProductName").val(ProductName);
        $("#txtProductSpec").val(ProductSpec);
        $("#txtProductUnit").val(ProductUnit);
        $("#txtBarCode").val(BarCode);

        alert("查询商品成功！");
    });
}

function OnSaveExchangeProduct() {

    var ProductID = $("#hdnProductID").val();
    if (ProductID == 0) {
        alert("请选择商品！");
        return;
    }

    var PointValue = $("#txtPointValue").val();
    if (PointValue == "") {
        alert("请输入兑换积分值！");
        return;
    }

    if (!window.confirm("确认要保存兑换商品吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveExchangeProduct",
        "ProductID": ProductID,
        "PointValue": PointValue
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存兑换商品成功！");
        
        ResetExchangeProductEditor();
    });
}

function DeleteExchangeProduct(ProductID) {

    if (!window.confirm("确认要删除兑换商品吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteExchangeProduct",
        "ProductID": ProductID
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除兑换商品成功！");

        QueryExchangeProductList();
    });
}

function ResetExchangeProductEditor() {

    $("#hdnExchangeProductID").val(0);
    $("#hdnProductID").val(0);
    $("#txtProductNo").val("");
    $("#txtProductName").val("");
    $("#txtProductSpec").val("");
    $("#txtProductUnit").val("");
    $("#txtBarCode").val("");
    $("#txtPointValue").val("0");
}

function BackToExchangeProductList() {

    SwitchDiv("ExchangeProductList");
}


/****** 功能函数 ******/
function SwitchDiv(DivName) {

    if (DivName == "ExchangeProductList") {

        $("#divExchangeProductList").show();
        $("#divExchangeProductEditor").hide();
    }
    else if (DivName == "ExchangeProductEditor") {

        $("#divExchangeProductEditor").show();
        $("#divExchangeProductList").hide();
    }
}
