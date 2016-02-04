var m_OperatorNoQuery;
var m_OperatorNameQuery;
var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function () {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

    SwitchPage("OperatorList");
});


/****** 操作员信息 ******/
function OnQueryOperatorList() {

    m_OperatorNoQuery = $("#txtOperatorNoQuery").val();
    m_OperatorNameQuery = $("#txtOperatorNameQuery").val();

    QueryOperatorCount();
}

function QueryOperatorCount() {

    $.post("BasicService.aspx",
    {
        "TransCode": "QueryOperatorCount",
        "OperatorNo": m_OperatorNoQuery,
        "OperatorName": m_OperatorNameQuery
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_RowCount = new Number($(data).find("RowCount").text());

        m_PageCount = parseInt(m_RowCount / m_PageSize);
        if (m_RowCount % m_PageSize > 0) m_PageCount++;

        QueryOperatorList(m_CurrentPageIndex);
    });
}

PageClick = function (pageclickednumber) {

    QueryOperatorList(pageclickednumber);
}

function QueryOperatorList(PageIndex) {

    $.post("BasicService.aspx",
    {
        "TransCode": "QueryOperatorList",
        "OperatorNo": m_OperatorNoQuery,
        "OperatorName": m_OperatorNameQuery,
        "OperatorCategory": 0,
        "PageIndex": PageIndex,
        "PageSize": m_PageSize
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        if (ReturnCode != "0000") {

            var ReturnMessage = $(data).find("ReturnMessage").text();
            alert(ReturnMessage);
            return false;
        }

        var htmlData = "";
        $("#tbOperatorList tr:gt(0)").remove();

        $(data).find("Operator").each(function () {

            var jqOperator = $(this);
            var OperatorID = jqOperator.attr("OperatorID");
            var OperatorNo = jqOperator.attr("OperatorNo");
            var OperatorName = jqOperator.attr("OperatorName");

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + OperatorID + "' /></td>" +
                            "<td>" + OperatorNo + "</td>" +
                            "<td>" + OperatorName + "</td>" +
                            "<td><a href='#none' onclick='OnEditOperator(" + OperatorID + ");return true;'>修改</a>|<a href='#none' onclick='OnDeleteOperator(" + OperatorID + ");return true;'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbOperatorList tr:eq(0)");

        m_CurrentPageIndex = PageIndex;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}

function OnNewOperator() {

    $("#hdnOperatorID").val(0);
    $("#txtOperatorNo").val("");
    $("#txtOperatorName").val("");
    $("#txtOperatorPassword").val("");

    SwitchPage("OperatorEditor");
}

function OnEditOperator(OperatorID) {

    $.post("BasicService.aspx", { "TransCode": "QueryOperator", "OperatorID": OperatorID }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var OperatorNo = $(data).find("Operator OperatorNo").text();
        var OperatorName = $(data).find("Operator OperatorName").text();

        $("#hdnOperatorID").val(OperatorID);
        $("#txtOperatorNo").val(OperatorNo);
        $("#txtOperatorName").val(OperatorName);
        $("#txtOperatorPassword").val("");

        SwitchPage("OperatorEditor");

    });
}

function OnSaveOperator() {

    if (!window.confirm("确认要保存吗？")) {
        return false;
    }

    var OperatorID = $("#hdnOperatorID").val();
    var OperatorNo = $("#txtOperatorNo").val();
    var OperatorName = $("#txtOperatorName").val();
    var OperatorPassword = $("#txtOperatorPassword").val();

    $.post("BasicService.aspx",
        {
            "TransCode": "SaveOperator",
            "OperatorID": OperatorID,
            "OperatorNo": OperatorNo,
            "OperatorName": OperatorName,
            "Password": OperatorPassword
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            QueryOperatorList();

            alert("保存成功！");
        });
}

function OnDeleteOperator(OperatorID) {

    if (!window.confirm("确认要删除吗？")) {
        return false;
    }

    $.post("BasicService.aspx", { "TransCode": "DeleteOperator", "OperatorID": OperatorID }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        QueryOperatorList();
    });
}



/****** 全局功能 ******/
function OnBackToOperatorList() {

    SwitchPage("OperatorList");
}


function SwitchPage(DivName) {

    if (DivName == "OperatorList") {

        $("#divOperatorList").show();
        $("#divOperatorEditor").hide();

    }
    else if (DivName == "OperatorEditor") {

        $("#divOperatorEditor").show();
        $("#divOperatorList").hide();
    }
}
