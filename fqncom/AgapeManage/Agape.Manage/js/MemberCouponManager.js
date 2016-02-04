var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;

    LoadDictItemList("EMemberCouponStatus", OnBaseDictLoaded);

});

function OnQuery() {

    QueryMemberCouponList(1);
}

/****** 会员优惠劵处理 ******/
function QueryMemberCouponList(PageIndex) {

    var MemberNo = $("#txtMemberNo").val();
    var MemberName = $("#txtMemberName").val();
    var MemberCouponNo = $("#txtMemberCouponNo").val();
    var ParValue = $("#txtParValue").val();

    $.post("MemberService.aspx",
    {
        "TransCode": "QueryMemberCouponList",
        "MemberNo": MemberNo,
        "MemberName": MemberName,
        "MemberCouponNo": MemberCouponNo,
        "ParValue": ParValue,
        "PageIndex": PageIndex,
        "PageSize": m_PageSize
    }, function(data) {

        var htmlData = "";
        $("#tbMemberCouponList tr:gt(0)").remove();

        $(data).find("MemberCoupon").each(function() {

            var jqMemberCoupon = $(this);
            var MemberCouponID = jqMemberCoupon.attr("MemberCouponID");
            var MemberCouponNo = jqMemberCoupon.attr("MemberCouponNo");
            var MemberNo = jqMemberCoupon.attr("MemberNo");
            var MemberName = jqMemberCoupon.attr("MemberName");
            var RealName = jqMemberCoupon.attr("RealName");
            var ParValue = jqMemberCoupon.attr("ParValue");
            var RequiredAmount = jqMemberCoupon.attr("RequiredAmount");
            var BeginDate = jqMemberCoupon.attr("BeginDate");
            var EndDate = jqMemberCoupon.attr("EndDate");
            var ProductScopeName = jqMemberCoupon.attr("ProductScopeName");
            var Status = jqMemberCoupon.attr("Status");
            var ValidDateRange = BeginDate + "-" + EndDate;
            Status = Status + "-" + GetDictItemValue("EMemberCouponStatus", Status);

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + MemberCouponID + "' /></td>" +
                            "<td>" + MemberNo + "</td>" +
                            "<td>" + MemberName + "</td>" +
                            "<td>" + RealName + "</td>" +
                            "<td>" + MemberCouponNo + "</td>" +
                            "<td>" + ParValue + "</td>" +
                            "<td>" + RequiredAmount + "</td>" +
                            "<td>" + ValidDateRange + "</td>" +
                            "<td>" + ProductScopeName + "</td>" +
                            "<td>" + Status + "</td>" +
                            "<td></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbMemberCouponList tr:eq(0)");

        m_PageCount = $(data).find("PageCount").text();
        m_CurrentPageIndex = PageIndex;

        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}

PageClick = function(pageclickednumber) {

    QueryMemberCouponList(pageclickednumber);
}