ProductScopeSelector_Append();

$(document).ready(function () {

    ProductScopeSelector_Init();

    QueryProductHintList();
});


/****** 商品提示处理 ******/
function QueryProductHintList() {

    $.post("ConfigService.aspx",
    { "TransCode": "QueryProductHintList"
    },
    function (data) {

        var htmlData = "";
        $("#tbProductHintList tr:gt(0)").remove();

        $(data).find("ProductHint").each(function () {

            var jqProductHint = $(this);
            var ProductHintID = jqProductHint.attr("ProductHintID");
            var ProductScope = jqProductHint.attr("ProductScope");
            var HintImageCode = jqProductHint.attr("HintImageCode");
            var HintTitle = jqProductHint.attr("HintTitle");

            htmlData += "<tr id='trProductHint" + ProductHintID + "'>" +
                            "<td class='Operation'>" +
                            "   <a href='#none' onclick='OnDeleteProductHint(" + ProductHintID + ");return true;'>删除</a>" +
                            "</td>" +
                            "<td>" + ProductScope + "</td>" +
                            "<td>" + HintImageCode + "</td>" +
                            "<td>" + HintTitle + "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbProductHintList tr:eq(0)");
    });
}

function OnSaveProductHint() {

    var ProductHintID = $("#hdnProductHintID").val();
    var ProductScopeID = $("#hdnProductScopeID").val();
    var HintImageCode = $("#txtHintImageCode").val();
    var HintTitle = $("#txtHintTitle").val();

    if (!window.confirm("确认要保存商品提示吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveProductHint",
        "ProductHintID": ProductHintID,
        "ProductScopeID": ProductScopeID,
        "HintImageCode": HintImageCode,
        "HintTitle": HintTitle
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存商品提示成功！");

        ClearProductHintEditor();

        QueryProductHintList();
    });
}

function OnDeleteProductHint(ProductHintID) {

    if (!window.confirm("确认要删除商品提示吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteProductHint",
        "ProductHintID": ProductHintID
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除商品提示成功！");

        QueryProductHintList();
    });
}

function ClearProductHintEditor() {

    $("#hdnProductHintID").val(0);
    $("#txtConditionProductScopeName").val("");
    $("#hdnConditionProductScopeID").val(0);
    $("#txtHintImageCode").val("");
    $("#txtHintTitle").val("");
}


/****** 商品范围下拉选择框处理 ******/
function ProductScopeSelector_OnSelectProductScope(obj, ProductScopeID) {

    var jqRow = $(obj).parent().parent();
    var ProductScopeName = jqRow.children("td:eq(0)").text();
    var Inclusion = jqRow.children("td:eq(1)").text();
    var Exclusion = jqRow.children("td:eq(2)").text();

    var jqBinder = $(m_ProductScopeSelector_Binder);
    var BinderID = jqBinder.attr("id");

    if (BinderID == "txtProductScopeName") {

        $("#txtProductScopeName").val(ProductScopeName);
        $("#hdnProductScopeID").val(ProductScopeID);
    }

    ProductScopeSelector_Hide();
}
