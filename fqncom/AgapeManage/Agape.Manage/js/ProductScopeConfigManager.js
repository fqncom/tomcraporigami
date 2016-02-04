ProductSelector_Append();
ProductCategorySelector_Append();
ProductBrandSelector_Append();

$(document).ready(function () {

    ProductSelector_Init();
    ProductCategorySelector_Init();
    ProductBrandSelector_Init();
    QueryProductScopeList();

    LoadDictItemList("EProductScopeItemMode", DictStateLoaded);
});

function DictStateLoaded() {

    BindSelect("sltProductScopeItemMode", "EProductScopeItemMode", "", "");
}


/****** 商品范围处理 ******/
function QueryProductScopeList() {

    $.post("ConfigService.aspx",
    {
        "TransCode": "QueryProductScopeList"
    },
    function (data) {

        var htmlData = "";
        $("#tbProductScopeList tr:gt(0)").remove();

        $(data).find("ProductScope").each(function () {

            var jqProductScope = $(this);
            var ProductScopeID = jqProductScope.attr("ProductScopeID");
            var ProductScopeName = jqProductScope.attr("ProductScopeName");
            var Inclusion = jqProductScope.attr("Inclusion");
            var Exclusion = jqProductScope.attr("Exclusion");
            var Remark = jqProductScope.attr("Remark");

            htmlData += "<tr id='trProductScope" + ProductScopeID + "'>" +
                            "<td>" + ProductScopeName + "</td>" +
                            "<td>" + Inclusion + "</td>" +
                            "<td>" + Exclusion + "</td>" +
                            "<td>" + Remark + "</td>" +
                            "<td class='Operation'>" +
                            "   <a href='#none' onclick='OnDeleteProductScope(" + ProductScopeID + ");return true;'>删除</a>" +
                            "   <a href='#none' onclick='OnEditProductScopeItemList(" + ProductScopeID + ");return true;'>编辑明细</a>" +
                            "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbProductScopeList tr:eq(0)");
    });
}

function OnSaveProductScope() {

    var ProductScopeID = $("#hdnProductScopeID").val();

    var ProductScopeName = $("#txtProductScopeName").val();
    if (ProductScopeName == "") {
        alert("请选择商品范围名称！");
        return;
    }

    var Remark = $("#txtRemark").val();

    if (!window.confirm("确认要保存商品范围吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveProductScope",
        "ProductScopeID": ProductScopeID,
        "ProductScopeName": ProductScopeName,
        "Remark": Remark
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存商品范围成功！");

        ClearProductScopeEditor();

        QueryProductScopeList();
    });
}

function OnDeleteProductScope(ProductScopeID) {

    if (!window.confirm("确认要删除商品范围及其明细吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteProductScope",
        "ProductScopeID": ProductScopeID
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除商品范围成功！");

        QueryProductScopeList();
    });
}

function ClearProductScopeEditor() {

    $("#hdnProductScopeID").val(0);
    $("#txtProductScopeName").val("");
    $("#txtRemark").val("");
}

function OnEditProductScopeItemList(ProductScopeID) {

    $("#hdnProductScopeID").val(ProductScopeID);

    QueryProductScopeItemList();

    $("#divProductScope").hide();
    $("#divProductScopeItem").show();
}


/****** 商品范围明细处理 ******/
function QueryProductScopeItemList() {

    var ProductScopeID = $("#hdnProductScopeID").val();

    $.post("ConfigService.aspx",
    { "TransCode": "QueryProductScopeItemList",
        "ProductScopeID": ProductScopeID
    },
    function (data) {

        var htmlData = "";
        $("#tbProductScopeItemList tr:gt(0)").remove();

        $(data).find("ProductScopeItem").each(function () {

            var jqProductScopeItem = $(this);
            var ProductScopeItemID = jqProductScopeItem.attr("ProductScopeItemID");
            var ProductScopeItemMode = jqProductScopeItem.attr("ProductScopeItemMode");
            var ProductName = jqProductScopeItem.attr("ProductName");
            var ProductCategoryName = jqProductScopeItem.attr("ProductCategoryName");
            var ProductCategoryFullName = jqProductScopeItem.attr("ProductCategoryFullName");
            var ProductBrandName = jqProductScopeItem.attr("ProductBrandName");
            ProductScopeItemMode = GetDictItemValue("EProductScopeItemMode", ProductScopeItemMode);

            htmlData += "<tr id='trProductScopeItem" + ProductScopeItemID + "'>" +
                            "<td>" + ProductScopeItemMode + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductCategoryFullName + "</td>" +
                            "<td>" + ProductBrandName + "</td>" +
                            "<td class='Operation'>" +
                            "   <a href='#none' onclick='OnDeleteProductScopeItem(" + ProductScopeItemID + ");return true;'>删除</a>" +
                            "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbProductScopeItemList tr:eq(0)");
    });
}

function OnSaveProductScopeItem() {

    var ProductScopeItemID = $("#hdnProductScopeItemID").val();
    var ProductScopeID = $("#hdnProductScopeID").val();

    var ProductScopeItemMode = $("#sltProductScopeItemMode").val();
    if (ProductScopeItemMode == "") {
        alert("请选择范围方式！");
        return;
    }

    var ProductID = $("#hdnProductID").val();
    var ProductCategoryID = $("#hdnProductCategoryID").val();
    var ProductBrandID = $("#hdnProductBrandID").val();

    if (!window.confirm("确认要保存商品范围配置吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveProductScopeItem",
        "ProductScopeItemID": ProductScopeItemID,
        "ProductScopeID": ProductScopeID,
        "ProductScopeItemMode": ProductScopeItemMode,
        "ProductID": ProductID,
        "ProductCategoryID": ProductCategoryID,
        "ProductBrandID": ProductBrandID
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存商品范围明细成功！");

        ClearProductScopeItemEditor();

        QueryProductScopeItemList();
    });
}

function OnBackToProductScope() {

    $("#divProductScope").show();
    $("#divProductScopeItem").hide();
}

function OnDeleteProductScopeItem(ProductScopeItemID) {

    if (!window.confirm("确认要删除商品范围明细吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteProductScopeItem",
        "ProductScopeItemID": ProductScopeItemID
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除商品范围明细成功！");

        QueryProductScopeItemList();
    });
}

function ClearProductScopeItemEditor() {

    $("#hdnProductScopeItemID").val(0);

    ResetProductScopeItemEditor();
}

function ResetProductScopeItemEditor() {

    $("#hdnProductID").val(0);
    $("#txtProductName").val("");
    $("#hdnProductCategoryID").val(0);
    $("#txtProductCategoryName").val("");
    $("#hdnProductBrandID").val(0);
    $("#txtProductBrandName").val("");
}


/****** 商品类型下拉选择框处理 ******/
function OnSelectProductCategory(ProductCategoryID) {

    var ProductCategoryName = GetProductCategoryName(ProductCategoryID);
    $("#txtProductCategoryName").val(ProductCategoryID + " - " + ProductCategoryName);
    $("#hdnProductCategoryID").val(ProductCategoryID);

    ProductCategorySelector_Hide();
}


/****** 商品下拉选择框处理 ******/
function ProductSelector_OnSelectProduct(obj, ProductID) {

    var jqRow = $(obj).parent().parent();
    var ProductNo = jqRow.children("td:eq(0)").text();
    var ProductName = jqRow.children("td:eq(1)").text();

    $("#txtProductName").val(ProductNo + " - " + ProductName);
    $("#hdnProductID").val(ProductID);

    ProductSelector_Hide();
}


/****** 商品品牌下拉选择框处理 ******/
function ProductBrandSelector_OnSelectProductBrand(obj, ProductBrandID) {

    var jqRow = $(obj).parent().parent();
    var ProductBrandNo = jqRow.children("td:eq(0)").text();
    var ProductBrandName = jqRow.children("td:eq(1)").text();

    $("#txtProductBrandName").val(ProductBrandNo + " - " + ProductBrandName);
    $("#hdnProductBrandID").val(ProductBrandID);

    ProductBrandSelector_Hide();
}
