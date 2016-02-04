$(document).ready(function() {

});

function OnQueryMemberConsultationList() {

    QueryMemberConsultationList();

    alert("查询完成！");
}


/****** 兑换商品列表处理 ******/
var m_jqMemberConsultationList;

function QueryMemberConsultationList() {

    SwitchDiv("MemberConsultationList");

    var QueryMemberName = $("#txtQueryMemberName").val();
    var QueryRealName = $("#txtQueryRealName").val();
    var QueryProductNo = $("#txtQueryProductNo").val();
    var QueryProductName = $("#txtQueryProductName").val();

    $.post("MemberService.aspx",
    { "TransCode": "QueryMemberConsultationList",
        "MemberName": QueryMemberName,
        "RealName": QueryRealName,
        "ProductNo": QueryProductNo,
        "ProductName": QueryProductName
    },
    function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        var htmlData = "";
        $("#tbMemberConsultationList tr:gt(0)").remove();
        m_jqMemberConsultationList = $(data).find("Response");

        $(data).find("MemberConsultation").each(function() {

            var jqMemberConsultation = $(this);
            var MemberConsultationID = jqMemberConsultation.attr("MemberConsultationID");
            var MemberName = jqMemberConsultation.attr("MemberName");
            var RealName = jqMemberConsultation.attr("RealName");
            var ProductID = jqMemberConsultation.attr("ProductID");
            var ProductNo = jqMemberConsultation.attr("ProductNo");
            var ProductName = jqMemberConsultation.attr("ProductName");
            var Question = jqMemberConsultation.attr("Question");
            var QuestionTime = jqMemberConsultation.attr("QuestionTime");
            var Answer = jqMemberConsultation.attr("Answer");
            var AnswerTime = jqMemberConsultation.attr("AnswerTime");
            var Status = jqMemberConsultation.attr("Status");

            htmlData += "<tr id='trMemberConsultation" + MemberConsultationID + "'>" +
                            "<td>" + MemberName + "</td>" +
                            "<td>" + RealName + "</td>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + Question + "</td>" +
                            "<td>" + QuestionTime + "</td>" +
                            "<td>" + Answer + "</td>" +
                            "<td>" + AnswerTime + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td>" +
                            "   <a href='#none' onclick='OnEditMemberConsultation(" + MemberConsultationID + ");return true;'>编辑</a>&nbsp;|&nbsp;" +
                            "<a href='#none' onclick='OnDeleteMemberConsultation(" + MemberConsultationID + ");return true;'>删除</a>" +
                            "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbMemberConsultationList tr:eq(0)");
    });
}

function OnEditMemberConsultation(MemberConsultationID) {

    var jqMemberConsultation = $("MemberConsultation[MemberConsultationID='" + MemberConsultationID + "']", m_jqMemberConsultationList);

    var Status = jqMemberConsultation.attr("Status");
    if (Status != E_MemberConsultationStatus_Question) {
        alert("会员咨询状态[" + Status + "]不能答复");
        return;
    }

    var MemberName = jqMemberConsultation.attr("MemberName");
    var RealName = jqMemberConsultation.attr("RealName");
    var ProductNo = jqMemberConsultation.attr("ProductNo");
    var ProductName = jqMemberConsultation.attr("ProductName");
    var Question = jqMemberConsultation.attr("Question");
    var QuestionTime = jqMemberConsultation.attr("QuestionTime");
    
    $("#hdnMemberConsultationID").val(MemberConsultationID);
    $("#divMemberConsultation span.MemberName").text(MemberName);
    $("#divMemberConsultation span.RealName").text(RealName);
    $("#divMemberConsultation span.ProductNo").text(ProductNo);
    $("#divMemberConsultation span.ProductName").text(ProductName);
    $("#divMemberConsultation span.Question").text(Question);
    $("#divMemberConsultation span.QuestionTime").text(QuestionTime);
    $("#txtAnswer").val("");

    SwitchDiv("MemberConsultationEditor");
}

function OnAnswerMemberConsultation() {

    var MemberConsultationID = $("#hdnMemberConsultationID").val();
    if (MemberConsultationID == 0) {
        alert("请选择会员咨询！");
        return;
    }

    var Answer = $("#txtAnswer").val();
    if (Answer == "") {
        alert("请输入答复内容！");
        return;
    }

    if (!window.confirm("确认要答复会员咨询吗？")) {
        return;
    }

    $.post("MemberService.aspx",
    { "TransCode": "AnswerMemberConsultation",
        "MemberConsultationID": MemberConsultationID,
        "Answer": Answer
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("答复会员咨询成功！");

        QueryMemberConsultationList();
        
        BackToMemberConsultationList();
    });
}

function OnDeleteMemberConsultation(MemberConsultationID) {

    if (!window.confirm("确认要删除会员咨询吗？")) {
        return;
    }

    $.post("MemberService.aspx",
    { "TransCode": "DeleteMemberConsultation",
        "MemberConsultationID": MemberConsultationID
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除会员咨询成功！");

        QueryMemberConsultationList();
    });
}

function ResetMemberConsultationEditor() {

    $("#hdnMemberConsultationID").val(0);
    $("#divMemberConsultation span.MemberName").text("");
    $("#divMemberConsultation span.RealName").text("");
    $("#divMemberConsultation span.ProductNo").text("");
    $("#divMemberConsultation span.ProductName").text("");
    $("#divMemberConsultation span.Question").text("");
    $("#divMemberConsultation span.QuestionTime").text("");
    $("#txtAnswer").val("");
}

function BackToMemberConsultationList() {

    SwitchDiv("MemberConsultationList");
}


/****** 功能函数 ******/
function SwitchDiv(DivName) {

    if (DivName == "MemberConsultationList") {

        $("#divMemberConsultationList").show();
        $("#divMemberConsultationEditor").hide();
    }
    else if (DivName == "MemberConsultationEditor") {

        $("#divMemberConsultationEditor").show();
        $("#divMemberConsultationList").hide();
    }
}
