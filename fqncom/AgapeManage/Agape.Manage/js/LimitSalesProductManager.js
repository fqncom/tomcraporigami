var m_bChangedFlag = false;

ProductSelector_Append();

$(document).ready(function () {

    ProductSelector_Init();

    QueryLimitSalesProductList();
});

function OnQueryLimitSalesProductList() {

    SwitchDiv("LimitSalesProductList");

    QueryLimitSalesProductList();
    
    alert("查询完成！");
}


/****** 兑换商品列表处理 ******/
var m_jqLimitSalesProductList;

function QueryLimitSalesProductList() {

    var ProductNo = $("#txtQueryProductNo").val();
    var ProductName = $("#txtQueryProductName").val();

    $.post("ConfigService.aspx",
    { "TransCode": "QueryLimitSalesProductList",
        "ProductNo": ProductNo,
        "ProductName": ProductName
    },
    function(data) {

        var htmlData = "";
        $("#tbLimitSalesProductList tr:gt(0)").remove();
        m_jqLimitSalesProductList = $(data).find("Response");

        $(data).find("LimitSalesProduct").each(function() {

            var jqLimitSalesProduct = $(this);
            var LimitSalesProductID = jqLimitSalesProduct.attr("LimitSalesProductID");
            var ProductID = jqLimitSalesProduct.attr("ProductID");
            var ProductNo = jqLimitSalesProduct.attr("ProductNo");
            var ProductName = jqLimitSalesProduct.attr("ProductName");
            var ProductSpec = jqLimitSalesProduct.attr("ProductSpec");
            var ProductUnit = jqLimitSalesProduct.attr("ProductUnit");
            var SalesPrice = jqLimitSalesProduct.attr("SalesPrice");
            var LimitSalesPrice = jqLimitSalesProduct.attr("LimitSalesPrice");
            var Agio = jqLimitSalesProduct.attr("Agio");
            var Title = jqLimitSalesProduct.attr("Title");
            var RecommendOrder = jqLimitSalesProduct.attr("RecommendOrder");
            var BeginTime = jqLimitSalesProduct.attr("BeginTime");
            var EndTime = jqLimitSalesProduct.attr("EndTime");
            var WeightValue = jqLimitSalesProduct.attr("WeightValue");
            SalesPrice = ToMoney(SalesPrice);
            LimitSalesPrice = ToMoney(LimitSalesPrice);

            htmlData += "<tr id='trLimitSalesProduct" + LimitSalesProductID + "'>" +
                            "<td class='Operation'>" +
                            "   <a href='#none' onclick='OnEditLimitSalesProduct(" + LimitSalesProductID + ");return true;'>修改</a>" +
                            "   <a href='#none' onclick='OnDeleteLimitSalesProduct(" + LimitSalesProductID + ");return true;'>删除</a>" +
                            "</td>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + SalesPrice + "</td>" +
                            "<td>" + LimitSalesPrice + "</td>" +
                            "<td>" + Agio + "</td>" +
                            "<td>" + Title + "</td>" +
                            "<td>" + RecommendOrder + "</td>" +
                            "<td>" + WeightValue + "</td>" +
                            "<td>" + BeginTime + "</td>" +
                            "<td>" + EndTime + "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbLimitSalesProductList tr:eq(0)");

        m_bChangedFlag = false;
    });
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

            ResetLimitSalesProductEditor();

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
        var SalesPrice = $(data).find("Product SalesPrice").text();
        SalesPrice = ToMoney(SalesPrice);

        $("#hdnProductID").val(ProductID);
        $("#txtProductNo").val(ProductNo);
        $("#txtProductName").val(ProductName);
        $("#txtProductSpec").val(ProductSpec);
        $("#txtProductUnit").val(ProductUnit);
        $("#txtBarCode").val(BarCode);
        $("#txtSalesPrice").val(SalesPrice);

        alert("查询商品成功！");
    });
}

function OnNewLimitSalesProduct() {

    ResetLimitSalesProductEditor();
    
    SwitchDiv("LimitSalesProductEditor");
}

function OnEditLimitSalesProduct(LimitSalesProductID) {

    var jqLimitSalesProduct = $("LimitSalesProduct[LimitSalesProductID='" + LimitSalesProductID + "']", m_jqLimitSalesProductList);

    var ProductID = jqLimitSalesProduct.attr("ProductID");
    var ProductNo = jqLimitSalesProduct.attr("ProductNo");
    var ProductName = jqLimitSalesProduct.attr("ProductName");
    var ProductSpec = jqLimitSalesProduct.attr("ProductSpec");
    var ProductUnit = jqLimitSalesProduct.attr("ProductUnit");
    var SalesPrice = jqLimitSalesProduct.attr("SalesPrice");
    var LimitSalesPrice = jqLimitSalesProduct.attr("LimitSalesPrice");
    var Title = jqLimitSalesProduct.attr("Title");
    var RecommendOrder = jqLimitSalesProduct.attr("RecommendOrder");
    var RecommendTitle = jqLimitSalesProduct.attr("RecommendTitle");
    var RecommendBrief = jqLimitSalesProduct.attr("RecommendBrief");
    var WeightValue = jqLimitSalesProduct.attr("WeightValue");
    var BeginTime = jqLimitSalesProduct.attr("BeginTime");
    var EndTime = jqLimitSalesProduct.attr("EndTime");
    SalesPrice = ToMoney(SalesPrice);
    LimitSalesPrice = ToMoney(LimitSalesPrice);

    $("#hdnLimitSalesProductID").val(LimitSalesProductID);
    $("#hdnProductID").val(ProductID);
    $("#txtProductNo").val(ProductNo);
    $("#txtProductName").val(ProductName);
    $("#txtProductSpec").val(ProductSpec);
    $("#txtProductUnit").val(ProductUnit);
    $("#txtBarCode").val("");
    $("#txtSalesPrice").val(SalesPrice);
    $("#txtLimitSalesPrice").val(LimitSalesPrice);
    $("#txtTitle").val(Title);
    $("#txtRecommendOrder").val(RecommendOrder);
    $("#txtRecommendTitle").val(RecommendTitle);
    $("#txtRecommendBrief").val(RecommendBrief);
    $("#txtWeightValue").val(WeightValue);
    $("#txtBeginTime").val(BeginTime);
    $("#txtEndTime").val(EndTime);

    SwitchDiv("LimitSalesProductEditor");
}

function OnSaveLimitSalesProduct() {

    var LimitSalesProductID = $("#hdnLimitSalesProductID").val();
    
    var ProductID = $("#hdnProductID").val();
    if (ProductID == 0) {
        alert("请选择商品！");
        return;
    }

    var LimitSalesPrice = $("#txtLimitSalesPrice").val();
    if (LimitSalesPrice == "") {
        alert("请输入限购价格！");
        return;
    }

    var Title = $("#txtTitle").val();
    var RecommendOrder = $("#txtRecommendOrder").val();
    var RecommendTitle = $("#txtRecommendTitle").val();
    var RecommendBrief = $("#txtRecommendBrief").val();
    var WeightValue = $("#txtWeightValue").val();

    var BeginTime = $("#txtBeginTime").val();
    if (BeginTime == "") {
        alert("请输入开始时间！");
        return;
    }

    var EndTime = $("#txtEndTime").val();
    if (EndTime == "") {
        alert("请输入结束时间！");
        return;
    }

    if (!window.confirm("确认要保存限购商品吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveLimitSalesProduct",
        "LimitSalesProductID": LimitSalesProductID,
        "ProductID": ProductID,
        "LimitSalesPrice": LimitSalesPrice,
        "Title": Title,
        "RecommendOrder": RecommendOrder,
        "RecommendTitle": RecommendTitle,
        "RecommendBrief": RecommendBrief,
        "WeightValue": WeightValue,
        "BeginTime": BeginTime,
        "EndTime": EndTime
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        m_bChangedFlag = true;
        alert("保存限购商品成功！");

        if (LimitSalesProductID == 0) {
         
            ResetLimitSalesProductEditor();
        }
    });
}

function OnDeleteLimitSalesProduct(LimitSalesProductID) {

    if (!window.confirm("确认要删除限购商品吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteLimitSalesProduct",
        "LimitSalesProductID": LimitSalesProductID
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除限购商品成功！");

        QueryLimitSalesProductList();
    });
}

function ResetLimitSalesProductEditor() {

    $("#hdnLimitSalesProductID").val(0);
    $("#hdnProductID").val(0);
    $("#txtProductNo").val("");
    $("#txtProductName").val("");
    $("#txtProductSpec").val("");
    $("#txtProductUnit").val("");
    $("#txtBarCode").val("");
    $("#txtSalesPrice").val("");
    $("#txtTitle").val("");
    $("#txtRecommendOrder").val("0");
    $("#txtRecommendTitle").val("");
    $("#txtRecommendBrief").val("");
    $("#txtWeightValue").val("0");
    $("#txtSalesPrice").val("");
    $("#txtLimitSalesPrice").val("");

    var now = new Date();
    var strNowTime = now.getFullYear() + "-" + (now.getMonth() + 1) + "-" + now.getDate() + " " + now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds();
    $("#txtBeginTime").val(strNowTime);
    $("#txtEndTime").val(strNowTime);
}

function BackToLimitSalesProductList() {

    SwitchDiv("LimitSalesProductList");
}


/****** 商品下拉选择框处理 ******/
function ProductSelector_OnSelectProduct(obj, ProductID) {

    var jqRow = $(obj).parent().parent();
    var ProductNo = jqRow.children("td:eq(0)").text();
    var ProductName = jqRow.children("td:eq(1)").text();
    var ProductUnit = jqRow.children("td:eq(4)").text();
    var SalesPrice = jqRow.children("td:eq(5)").text();

    $("#txtProductName").val(ProductNo + " - " + ProductName);
    $("#txtProductUnit").val(ProductUnit);
    $("#txtSalesPrice").val(SalesPrice);
    $("#hdnProductID").val(ProductID);

    ProductSelector_Hide();
}


/****** 功能函数 ******/
function SwitchDiv(DivName) {

    if (DivName == "LimitSalesProductList") {

        $("#divLimitSalesProductList").show();
        $("#divLimitSalesProductEditor").hide();

        if (m_bChangedFlag) {
        
            QueryLimitSalesProductList();
        }
    }
    else if (DivName == "LimitSalesProductEditor") {

        $("#divLimitSalesProductEditor").show();
        $("#divLimitSalesProductList").hide();
    }
}
