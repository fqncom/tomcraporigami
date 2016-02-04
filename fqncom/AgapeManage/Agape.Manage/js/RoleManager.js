var m_RoleNoQuery;
var m_RoleNameQuery;
var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

RoleCategorySelector_Append();

$(document).ready(function () {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

    SwitchPage("RoleList");

    QueryRoleBrandList();
    RoleCategorySelector_Init();
});


/****** 角色信息 ******/
function OnQueryRoleList() {
    
    m_RoleNoQuery = $("#txtRoleNoQuery").val();
    m_RoleNameQuery = $("#txtRoleNameQuery").val();

    QueryRoleCount();
}

function QueryRoleCount() {

    $.post("BasicService.aspx",
    {
        "TransCode": "QueryRoleCount",
        "RoleNo": m_RoleNoQuery,
        "RoleName": m_RoleNameQuery
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

        QueryRoleList(m_CurrentPageIndex);
    });
}

PageClick = function (pageclickednumber) {

    QueryRoleList(pageclickednumber);
}

function QueryRoleList(PageIndex) {

    $.post("BasicService.aspx",
    {
        "TransCode": "QueryRoleList",
        "RoleNo": m_RoleNoQuery,
        "RoleName": m_RoleNameQuery,
        "RoleCategory": 0,
        "PageIndex": PageIndex,
        "PageSize": m_PageSize
    }, function (data) {

        var htmlData = "";
        $("#tbRoleList tr:gt(0)").remove();

        $(data).find("Role").each(function () {

            var jqRole = $(this);
            //var RoleID = jqRole.attr("RoleID");
            var RoleCode = jqRole.attr("RoleCode");
            var RoleName = jqRole.attr("RoleName");
            var RoleStatus = jqRole.attr("Status");
            var RoleRemark = jqRole.attr("Remark");

            htmlData += "<tr>" +
                            "<td>" + RoleCode + "</td>" +
                            "<td>" + RoleName + "</td>" +
                            "<td>" + RoleStatus + "</td>" +
                            "<td>" + RoleRemark + "</td>" +
                            "<td><a href=\"#none\" onclick=\"OnEditRole('" + RoleCode + "');return true;\">修改</a>|<a href=\"#none\" onclick=\"OnDeleteRole('" + RoleCode + "');return true;\">删除</a></td>" +
                        "</tr>";
        });
        $(htmlData).insertAfter("#tbRoleList tr:eq(0)");

        m_CurrentPageIndex = PageIndex;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });
    });
}


function OnNewRole() {

    //$("#hdnRoleID").val(0);
    $("#txtRoleCode").val("");
    $("#txtRoleName").val("");
    $("#txtRoleStatus").val("");
    $("#txtRoleRemark").val("");

    SwitchPage("RoleEditor");
}


function OnEditRole(RoleCode) {
    alert("d");
    $.post("BasicService.aspx", { "TransCode": "QueryRole", "RoleCode": RoleCode }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var RoleCode = $(data).find("Role RoleCode").text();
        var RoleName = $(data).find("Role RoleName").text();
        var RoleStatus = $(data).find("Role RoleStatus").text();
        var RoleRemark = $(data).find("Role RoleRemark").text();


        //$("#hdnRoleID").val(RoleID);
        $("#txtRoleCode").val(RoleCode);
        $("#txtRoleName").val(RoleName);
        $("#txtRoleStatus").val(RoleStatus);
        $("#txtRoleRemark").val(RoleRemark);

        SwitchPage("RoleEditor");

    });
}


function OnSaveRole() {

    if (!window.confirm("确认要保存吗？")) {
        return false;
    }

    //var RoleID = $("#hdnRoleID").val();
    var RoleCode = $("#txtRoleCode").val();
    var RoleName = $("#txtRoleName").val();
    var RoleStatus = $("#txtRoleStatus").val();
    var RoleRemark = $("#txtRoleRemark").val();

    $.post("BasicService.aspx",
        {
            "TransCode": "SaveRole",
            //"RoleID": RoleID,
            "RoleCode": RoleCode,
            "RoleName": RoleName,
            "Status": RoleStatus,
            "Remark": RoleRemark
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

                QueryRoleList();
        
            alert("保存成功！");
        });
}

function OnDeleteRole(RoleCode) {

    if (!window.confirm("确认要删除吗？")) {
        return false;
    }

    $.post("BasicService.aspx", { "TransCode": "DeleteRole", "RoleCode": RoleCode }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        QueryRoleList();
    });
}



/****** 全局功能 ******/
function OnBackToRoleList() {

    SwitchPage("RoleList");
}


function SwitchPage(DivName) {

    if (DivName == "RoleList") {

        $("#divRoleList").show();
        $("#divRoleEditor").hide();

    }
    else if (DivName == "RoleEditor") {

        $("#divRoleEditor").show();
        $("#divRoleList").hide();
    }
}
