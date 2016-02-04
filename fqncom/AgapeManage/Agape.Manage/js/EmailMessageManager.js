var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

});


/****** 文章 ******/
function OnQueryEmailMessageList() {

    QueryEmailMessageList(1);
}

function QueryEmailMessageList(pageclickednumber) {

    var Title = $("#txtTitle").val();
    var Receiver = $("#txtReceiver").val();
    var FromDate = $("#txtFromDate").val();
    var ToDate = $("#txtToDate").val();

    ShowLoadingBox();

    $.post("BasicService.aspx",
    {
        "TransCode": "QueryEmailMessageList",
        "Title": Title,
        "Receiver": Receiver,
        "FromDate": FromDate,
        "ToDate": ToDate,
        "PageIndex": pageclickednumber,
        "PageSize": m_PageSize
    }, function (data) {

        var htmlData = "";
        $("#tbEmailMessageList tr:gt(0)").remove();

        $(data).find("EmailMessage").each(function () {

            var jqEmailMessage = $(this);
            var EmailMessageID = jqEmailMessage.attr("EmailMessageID");
            var EmailMessageCategoryName = "";
            var Title = jqEmailMessage.attr("Title");
            var IsBodyHtml = jqEmailMessage.attr("IsBodyHtml");
            var SendDateTime = jqEmailMessage.attr("SendDateTime");
            var Status = jqEmailMessage.attr("Status");
            IsBodyHtml = IsBodyHtml == 1 ? "是" : "";

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + EmailMessageID + "' /></td>" +
                            "<td>" + EmailMessageCategoryName + "</td>" +
                            "<td>" + Title + "</td>" +
                            "<td>" + IsBodyHtml + "</td>" +
                            "<td>" + SendDateTime + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td><a href='#none' onclick='OnViewEmailMessage(" + EmailMessageID + ");return true;'>查看</a>|<a href='#none' onclick='OnDeleteEmailMessage(" + EmailMessageID + ");return true;'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbEmailMessageList tr:eq(0)");

        m_CurrentPageIndex = pageclickednumber;
        m_PageCount = $(data).find("PageCount").text();

        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });

        HideLoadingBox();
    });
}

PageClick = function(pageclickednumber) {

    QueryEmailMessageList(pageclickednumber);
}

function OnNewEmailMessage() {

    $("#txtEmailMessageTitle").val("");
    $("#txtReceiverList").val("");
    $("#txtEmailMessageBody").val("");

    SwitchDiv("EmailMessageEditor");
}

function OnSubmitEmailMessage() {

    var Title = $("#txtEmailMessageTitle").val();
    if (Title == "") {
        alert("请填写邮件标题！");
        return;
    }

    var ReceiverList = $("#txtReceiverList").val();
    if (ReceiverList == "") {
        alert("请填写邮件收件人列表！");
        return;
    }

    var IsBodyHtml = $("#ckbIsBodyHtml").attr("checked") == "checked" ? 1 : 0;

    var Body = $("#txtEmailMessageBody").val();
    if (Body == "") {
        alert("请填写邮件主体！");
        return;
    }
    Body = escape(Body);

    if (!window.confirm("确认要提交吗？")) {
        return false;
    }

    ShowLoadingBox();

    $.post("BasicService.aspx",
        { "TransCode": "SubmitEmailMessage",
            "Title": Title,
            "ReceiverList": ReceiverList,
            "IsBodyHtml": IsBodyHtml,
            "Body": Body
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


function OnBackToEmailMessageList() {

    SwitchDiv("EmailMessageList");
}

function SwitchDiv(DivName) {

    if (DivName == "EmailMessageList") {

        $("#divEmailMessageList").show();
        $("#divEmailMessageEditor").hide();

    }
    else if (DivName == "EmailMessageEditor") {

        $("#divEmailMessageEditor").show();
        $("#divEmailMessageList").hide();
    }
}


/****** 功能 ******/
