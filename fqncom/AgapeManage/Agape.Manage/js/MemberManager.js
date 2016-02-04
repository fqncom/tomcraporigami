var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;

    LoadDictItemList("MemberStatus,Gender", OnBaseDictLoaded);
});

function OnQuery() {

    QueryMemberCount();
}

/****** 会员 ******/
function QueryMemberCount() {

    var MemberNo = $("#txtMemberNo").val();
    var MemberName = $("#txtMemberName").val();

    $.post("MemberService.aspx",
    {
        "TransCode": "QueryMemberCount",
        "MemberNo": MemberNo,
        "MemberName": MemberName
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_RowCount = new Number($(data).find("RowCount").text());

        m_PageCount = parseInt(m_RowCount / m_PageSize);
        if (m_RowCount % m_PageSize > 0) m_PageCount++;

        QueryMemberList(m_CurrentPageIndex);
    });
}

PageClick = function(pageclickednumber) {

    QueryMemberList(pageclickednumber);
}

function QueryMemberList(pageclickednumber) {

    var MemberNo = $("#txtMemberNo").val();
    var MemberName = $("#txtMemberName").val();

    $.post("MemberService.aspx",
    {
        "TransCode": "QueryMemberList",
        "MemberNo": MemberNo,
        "MemberName": MemberName,
        "PageIndex": pageclickednumber,
        "PageSize": m_PageSize
    }, function(data) {

        var htmlData = "";
        $("#tbMemberList tr:gt(0)").remove();

        $(data).find("Member").each(function() {

            var jqMember = $(this);
            var MemberID = jqMember.attr("MemberID");
            var MemberNo = jqMember.attr("MemberNo");
            var MemberName = jqMember.attr("MemberName");
            var RealName = jqMember.attr("RealName");
            var Gender = jqMember.attr("Gender");
            var Age = jqMember.attr("Age");
            var Birthday = jqMember.attr("Birthday");
            var MobilePhone = jqMember.attr("MobilePhone");
            var Email = jqMember.attr("Email");
            var PointValue = jqMember.attr("PointValue");
            var ExchangedPointValue = jqMember.attr("ExchangedPointValue");
            var CreateTime = jqMember.attr("CreateTime");
            var Status = jqMember.attr("Status");
            Status = GetDictItemValue("MemberStatus", Status);
            Gender = GetDictItemValue("Gender", Gender);

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + MemberID + "' /></td>" +
                            "<td>" + MemberNo + "</td>" +
                            "<td>" + MemberName + "</td>" +
                            "<td>" + RealName + "</td>" +
                            "<td>" + Gender + "</td>" +
                            "<td>" + Age + "</td>" +
                            "<td>" + Birthday + "</td>" +
                            "<td>" + MobilePhone + "</td>" +
                            "<td>" + Email + "</td>" +
                            "<td>" + PointValue + "</td>" +
                            "<td>" + ExchangedPointValue + "</td>" +
                            "<td>" + CreateTime + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td><a href='#none' onclick='OnEditMember(" + MemberID + ");return true;'>修改</a>|<a href='#none' onclick='OnDeleteMember(" + MemberID + ");return true;'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbMemberList tr:eq(0)");

        m_CurrentPageIndex = pageclickednumber;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}