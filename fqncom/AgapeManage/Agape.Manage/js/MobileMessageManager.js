var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

});


/****** 文章 ******/
function OnQueryMobileMessageList() {

    QueryMobileMessageList(1);
}

function QueryMobileMessageList(pageclickednumber) {

    ShowLoadingBox();

    var MobilePhone = $("#txtMobilePhone").val();
    var MessageContent = $("#txtMessageContent").val();
    var FromDate = $("#txtFromDate").val();
    var ToDate = $("#txtToDate").val();

    $.post("BasicService.aspx",
    {
        "TransCode": "QueryMobileMessageList",
        "MobilePhone": MobilePhone,
        "MessageContent": MessageContent,
        "FromDate": FromDate,
        "ToDate": ToDate,
        "PageIndex": pageclickednumber,
        "PageSize": m_PageSize
    }, function (data) {

        var htmlData = "";
        $("#tbMobileMessageList tr:gt(0)").remove();

        $(data).find("MobileMessage").each(function () {

            var jqMobileMessage = $(this);
            var MobileMessageID = jqMobileMessage.attr("MobileMessageID");
            var MobileMessageCategoryName = "";
            var MobilePhone = jqMobileMessage.attr("MobilePhone");
            var MessageContent = jqMobileMessage.attr("MessageContent");
            var SendDateTime = jqMobileMessage.attr("SendDateTime");
            var ReturnCode = jqMobileMessage.attr("ReturnCode");
            var Status = jqMobileMessage.attr("Status");

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + MobileMessageID + "' /></td>" +
                            "<td>" + MobileMessageCategoryName + "</td>" +
                            "<td>" + MobilePhone + "</td>" +
                            "<td>" + MessageContent + "</td>" +
                            "<td>" + SendDateTime + "</td>" +
                            "<td>" + ReturnCode + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbMobileMessageList tr:eq(0)");

        m_CurrentPageIndex = pageclickednumber;
        m_PageCount = $(data).find("PageCount").text();

        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });

        HideLoadingBox();
    });
}

PageClick = function(pageclickednumber) {

    QueryMobileMessageList(pageclickednumber);
}

function OnNewMobileMessage() {

    $("#txtReceiverList").val("");
    $("#txtMessageContent").val("");

    SwitchDiv("MobileMessageEditor");
}

function OnSubmitMobileMessage() {

    var ReceiverList = $("#txtReceiverList").val();
    if (ReceiverList == "") {
        alert("请填写短信收件人列表！");
        return;
    }

    var MessageContent = $("#txtMessageContent").val();
    if (MessageContent == "") {
        alert("请填写短信内容！");
        return;
    }
    MessageContent = escape(MessageContent);

    if (!window.confirm("确认要提交吗？")) {
        return false;
    }

    ShowLoadingBox();

    $.post("BasicService.aspx",
        { "TransCode": "SubmitMobileMessage",
            "ReceiverList": ReceiverList,
            "MessageContent": MessageContent
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            HideLoadingBox();

            alert("提交成功！");
        });
}


function OnBackToMobileMessageList() {

    SwitchDiv("MobileMessageList");
}

function SwitchDiv(DivName) {

    if (DivName == "MobileMessageList") {

        $("#divMobileMessageList").show();
        $("#divMobileMessageEditor").hide();

    }
    else if (DivName == "MobileMessageEditor") {

        $("#divMobileMessageEditor").show();
        $("#divMobileMessageList").hide();
    }
}


/****** 功能 ******/
