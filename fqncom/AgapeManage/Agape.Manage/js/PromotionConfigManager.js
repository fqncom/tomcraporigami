ProductScopeSelector_Append();
ProductSelector_Append();

$(document).ready(function () {

    ProductScopeSelector_Init();
    ProductSelector_Init();

    LoadDictItemList("EPromotionConditionMode,EPromotionImplementOrderMode,EPromotionImplementOrderItemMode,EPromotionTarget", DictStateLoaded);
});

function DictStateLoaded() {

    BindSelect("sltConditionTarget", "EPromotionTarget", "0", "不指定");
    BindSelect("sltConditionMode", "EPromotionConditionMode", "0", "不指定");

    BindSelect("sltImplementTarget", "EPromotionTarget", "", "");

    OnConditionModeChanged();
    OnImplementTargetChanged();
    OnImplementModeChanged();

    QueryPromotionRuleList();
}

function OnConditionModeChanged() {

    $("#txtConditionParameter1").val("");

    var ConditionMode = $("#sltConditionMode").val();
    if (ConditionMode == E_PromotionConditionMode_OverQuantity) {

        $("#txtConditionParameter1").removeAttr("readonly");
        $("span.ConditionParameter1").text("超过数量值");
    }
    else if (ConditionMode == E_PromotionConditionMode_OverAmount) {

        $("#txtConditionParameter1").removeAttr("readonly");
        $("span.ConditionParameter1").text("超过金额值");
    }
    else {

        $("#txtConditionParameter1").attr("readonly", "readonly");
        $("span.ConditionParameter1").text("无需输入");
    }
}

function OnImplementTargetChanged() {

    var ImplementTarget = $("#sltImplementTarget").val();

    if (ImplementTarget == E_PromotionTarget_Order) BindSelect("sltImplementMode", "EPromotionImplementOrderMode", "", "");
    else if (ImplementTarget == E_PromotionTarget_OrderItem) BindSelect("sltImplementMode", "EPromotionImplementOrderItemMode", "", "");

    OnImplementModeChanged();
}

function OnImplementModeChanged() {

    $("#txtImplementParameter1").val("");
    $("#txtImplementParameter2").val("");

    var ImplementTarget = $("#sltImplementTarget").val();
    var ImplementMode = $("#sltImplementMode").val();

    var DisplayFlag1 = true;
    var DisplayFlag2 = false;
    var Title1 = "";
    var Title2 = "";

    if (ImplementTarget == E_PromotionTarget_Order) {

        if (ImplementMode == E_PromotionImplementOrderMode_CutAmount) {

            Title1 = "优惠金额";
        }
        else if (ImplementMode == E_PromotionImplementOrderMode_Agio) {

            Title1 = "折扣率";
        }
        else if (ImplementMode == E_PromotionImplementOrderMode_MoreThenAgio) {

            Title1 = "折扣率";
        }
        else if (ImplementMode == E_PromotionImplementOrderMode_Largess) {

            Title1 = "赠品编号";
        }
    }
    else {

        if (ImplementMode == E_PromotionImplementOrderItemMode_MoreThenAgio) {

            Title1 = "折扣率";
        }
        else if (ImplementMode == E_PromotionImplementOrderItemMode_MoreThenCutPrice) {

            Title1 = "减少金额";
        }
        else if (ImplementMode == E_PromotionImplementOrderItemMode_MoreThenBargainPrice) {

            Title1 = "特价";
        }
        else if (ImplementMode == E_PromotionImplementOrderItemMode_Agio) {

            Title1 = "折扣率";
        }
        else if (ImplementMode == E_PromotionImplementOrderItemMode_CutPrice) {

            Title1 = "减少价格";
        }
        else if (ImplementMode == E_PromotionImplementOrderItemMode_BargainPrice) {

            Title1 = "特价";
        }
        else if (ImplementMode == E_PromotionImplementOrderItemMode_SomeThenFree) {

            DisplayFlag2 = true;
            Title1 = "满";
            Title2 = "送";
        }
        else if (ImplementMode == E_PromotionImplementOrderItemMode_Largess) {

            Title1 = "赠品编号";
        }
    }

    if (DisplayFlag1) $("li.ImplementParameter1").show();
    else $("li.ImplementParameter1").hide();

    if (DisplayFlag2) $("li.ImplementParameter2").show();
    else $("li.ImplementParameter2").hide();

    $("li.ImplementParameter1 span").text(Title1);
    $("li.ImplementParameter2 span").text(Title2);
}


/****** 促销规则处理 ******/
function QueryPromotionRuleList() {

    $.post("ConfigService.aspx",
    { "TransCode": "QueryPromotionRuleList"
    },
    function (data) {

        var htmlData = "";
        $("#tbPromotionRuleList tr:gt(0)").remove();

        $(data).find("PromotionRule").each(function () {

            var jqPromotionRule = $(this);
            var PromotionRuleID = jqPromotionRule.attr("PromotionRuleID");
            var PromotionRuleName = jqPromotionRule.attr("PromotionRuleName");
            var ConditionTarget = jqPromotionRule.attr("ConditionTarget");
            var ConditionMode = jqPromotionRule.attr("ConditionMode");
            var ConditionProductScope = jqPromotionRule.attr("ConditionProductScope");
            var ConditionParameterList = jqPromotionRule.attr("ConditionParameterList");
            var ImplementTarget = jqPromotionRule.attr("ImplementTarget");
            var ImplementMode = jqPromotionRule.attr("ImplementMode");
            var ImplementProductScope = jqPromotionRule.attr("ImplementProductScope");
            var ImplementParameterList = jqPromotionRule.attr("ImplementParameterList");
            var ImplementMaxQuantity = jqPromotionRule.attr("ImplementMaxQuantity");
            var BeginDate = jqPromotionRule.attr("BeginDate");
            var EndDate = jqPromotionRule.attr("EndDate");
            var OperateTime = jqPromotionRule.attr("OperateTime");
            var OperatorName = jqPromotionRule.attr("OperatorName");
            var Status = jqPromotionRule.attr("Status");

            var ConditionTargetText, ConditionModeText, ImplementTargetText, ImplementModeText;

            if (ConditionTarget == 0) ConditionTargetText = "";
            else ConditionTargetText = ConditionTarget + "-" + GetDictItemValue("EPromotionTarget", ConditionTarget);

            if (ConditionMode == 0) ConditionModeText = "";
            else ConditionModeText = ConditionMode + "-" + GetDictItemValue("EPromotionConditionMode", ConditionMode);

            if (ImplementTarget == 0) ImplementTargetText = "";
            else ImplementTargetText = ImplementTarget + "-" + GetDictItemValue("EPromotionTarget", ImplementTarget);

            if (ImplementMode == 0) ImplementModeText = "";
            else {
                if (ImplementTarget == E_PromotionTarget_Order) ImplementModeText = ImplementMode + "-" + GetDictItemValue("EPromotionImplementOrderMode", ImplementMode);
                else ImplementModeText = ImplementMode + "-" + GetDictItemValue("EPromotionImplementOrderItemMode", ImplementMode);
            }

            htmlData += "<tr id='trPromotionRule" + PromotionRuleID + "'>" +
                            "<td class='Operation'>" +
                            "   <a href='#none' onclick='OnDeletePromotionRule(" + PromotionRuleID + ");return true;'>删除</a>" +
                            "</td>" +
                            "<td>" + PromotionRuleName + "</td>" +
                            "<td>" + ConditionTargetText + "</td>" +
                            "<td>" + ConditionModeText + "</td>" +
                            "<td>" + ConditionParameterList + "</td>" +
                            "<td>" + ConditionProductScope + "</td>" +
                            "<td>" + ImplementTargetText + "</td>" +
                            "<td>" + ImplementModeText + "</td>" +
                            "<td>" + ImplementParameterList + "</td>" +
                            "<td>" + ImplementProductScope + "</td>" +
                            "<td>" + ImplementMaxQuantity + "</td>" +
                            "<td>" + BeginDate + "</td>" +
                            "<td>" + EndDate + "</td>" +
                            "<td>" + OperateTime + "</td>" +
                            "<td>" + OperatorName + "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbPromotionRuleList tr:eq(0)");
    });
}

function OnSavePromotionRule() {

    var PromotionRuleID = $("#hdnPromotionRuleID").val();

    var PromotionRuleName = $("#txtPromotionRuleName").val();
    if (PromotionRuleName == "") {
        alert("请输入促销规则名称！");
        return;
    }

    var ConditionTarget = $("#sltConditionTarget").val();

    var ConditionMode = $("#sltConditionMode").val();

    var ConditionParameterList = "";
    var ConditionParameter1 = $("#txtConditionParameter1").val();
    if (ConditionMode > 0 && ConditionParameter1 != "") ConditionParameterList += "Parameter1=" + ConditionParameter1;

    var ConditionProductScopeID = $("#hdnConditionProductScopeID").val();

    var ImplementTarget = $("#sltImplementTarget").val();

    var ImplementMode = $("#sltImplementMode").val();

    var ImplementParameterList = "";
    var ImplementParameter1 = $("#txtImplementParameter1").val();
    if (ImplementParameter1 != "") ImplementParameterList += "Parameter1=" + ImplementParameter1;

    var ImplementParameter2 = $("#txtImplementParameter2").val();
    if (ImplementParameter2 != "") ImplementParameterList += ",Parameter2=" + ImplementParameter2;

    var ImplementProductScopeID = $("#hdnImplementProductScopeID").val();

    var ImplementMaxQuantity = $("#txtImplementMaxQuantity").val();

    var BeginDate = $("#txtBeginDate").val();
    if (BeginDate == "") {
        alert("请输入开始日期！");
        return;
    }

    var EndDate = $("#txtEndDate").val();
    if (EndDate == "") {
        alert("请输入结束日期！");
        return;
    }

    if (!window.confirm("确认要保存促销规则吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SavePromotionRule",
        "PromotionRuleID": PromotionRuleID,
        "PromotionRuleName": PromotionRuleName,
        "ConditionTarget": ConditionTarget,
        "ConditionMode": ConditionMode,
        "ConditionParameterList": ConditionParameterList,
        "ConditionProductScopeID": ConditionProductScopeID,
        "ImplementTarget": ImplementTarget,
        "ImplementMode": ImplementMode,
        "ImplementParameterList": ImplementParameterList,
        "ImplementProductScopeID": ImplementProductScopeID,
        "ImplementMaxQuantity": ImplementMaxQuantity,
        "BeginDate": BeginDate,
        "EndDate": EndDate
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存促销规则成功！");

        ClearPromotionRuleEditor();

        QueryPromotionRuleList();
    });
}

function OnDeletePromotionRule(PromotionRuleID) {

    if (!window.confirm("确认要删除促销规则吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeletePromotionRule",
        "PromotionRuleID": PromotionRuleID
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除促销规则成功！");

        QueryPromotionRuleList();
    });
}

function ClearPromotionRuleEditor() {

    $("#hdnPromotionRuleID").val(0);
    $("#txtPromotionRuleName").val("");
    $("#txtConditionParameter1").val("");
    $("#txtConditionProductScopeName").val("");
    $("#hdnConditionProductScopeID").val(0);
    $("#txtImplementParameter1").val("");
    $("#txtImplementParameter2").val("");
    $("#txtImplementProductScopeName").val("");
    $("#hdnImplementProductScopeID").val(0);
    $("#txtImplementMaxQuantity").val(0);
    $("#txtBeginDate").val("");
    $("#txtEndDate").val("");
}


/****** 商品范围下拉选择框处理 ******/
function ProductScopeSelector_OnSelectProductScope(obj, ProductScopeID) {

    var jqRow = $(obj).parent().parent();
    var ProductScopeName = jqRow.children("td:eq(0)").text();
    var Inclusion = jqRow.children("td:eq(1)").text();
    var Exclusion = jqRow.children("td:eq(2)").text();

    var jqBinder = $(m_ProductScopeSelector_Binder);
    var BinderID = jqBinder.attr("id");

    if (BinderID == "txtConditionProductScopeName") {

        $("#txtConditionProductScopeName").val(ProductScopeName);
        $("#hdnConditionProductScopeID").val(ProductScopeID);
    }
    else if (BinderID == "txtImplementProductScopeName") {

        $("#txtImplementProductScopeName").val(ProductScopeName);
        $("#hdnImplementProductScopeID").val(ProductScopeID);
    }

    ProductScopeSelector_Hide();
}


/****** 商品下拉选择框处理 ******/
function ProductSelector_OnSelectProduct(obj, ProductID) {

    var jqRow = $(obj).parent().parent();
    var ProductNo = jqRow.children("td:eq(0)").text();
    var ProductName = jqRow.children("td:eq(1)").text();

    $("#txtProductAgio_ProductName").val(ProductNo + " - " + ProductName);
    $("#hdnProductAgio_ProductID").val(ProductID);

    ProductSelector_Hide();
}
