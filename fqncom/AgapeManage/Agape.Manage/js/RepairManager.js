var m_BatchReady = false;

$(document).ready(function () {

    m_CurrentPageIndex = 1;
    m_PageSize = 10;

    LoadDictItemList("ERepairStatus,EProductAppearanceStatus,EProductPackageStatus", DictStateLoaded);
});

function DictStateLoaded() {

    OnBaseDictLoaded();

    BindSelect("sltRepairStatus", "ERepairStatus", "0", "全部");
}

function CheckReady() {

    if (!g_DictReady) {
        alert("字典没有加载！");
        return false;
    }

    return true;
}

/****** 返修 ******/
function QueryRepairList() {

    if (!CheckReady()) {
        return;
    }

    var RepairNo = $("#txtRepairNo").val();
    var FromDate = $("#txtFromDate").val();
    var ToDate = $("#txtToDate").val();
    var Status = $("#sltRepairStatus").val();

    $.post("SalesService.aspx",
        { "TransCode": "QueryRepairList",
            "RepairNo": RepairNo,
            "FromDate": FromDate,
            "ToDate": ToDate,
            "Status": Status
        },
        function (data) {

            var htmlData = "";
            $("#tbRepairList tr:gt(0)").remove();

            $(data).find("Repair").each(function () {

                var jqRepair = $(this);
                var RepairID = jqRepair.attr("RepairID");
                var RepairNo = jqRepair.attr("RepairNo");
                var ApplyTime = jqRepair.attr("ApplyTime");
                var MemberName = jqRepair.attr("MemberName");
                var RealName = jqRepair.attr("RealName");
                var ProductName = jqRepair.attr("ProductName");
                var ProblemContent = jqRepair.attr("ProblemContent");
                var AppearanceStatus = jqRepair.attr("AppearanceStatus");
                var PackageStatus = jqRepair.attr("PackageStatus");
                var Status = jqRepair.attr("Status");
                AppearanceStatus = GetDictItemValue("EProductAppearanceStatus", AppearanceStatus);
                PackageStatus = GetDictItemValue("EProductPackageStatus", PackageStatus);
                Status = Status + " - " + GetDictItemValue("ERepairStatus", Status);

                htmlData +=
                    "<tr id='trRepair" + RepairID + "'>" +
                        "<td><input type='checkbox' value='" + RepairID + "' /></td>" +
                        "<td>" + RepairNo + "</td>" +
                        "<td>" + ApplyTime + "</td>" +
                        "<td>" + MemberName + "</td>" +
                        "<td>" + ProductName + "</td>" +
                        "<td>" + ProblemContent + "</td>" +
                        "<td>" + AppearanceStatus + "," + PackageStatus + "</td>" +
                        "<td>" + Status + "</td>" +
                        "<td>&nbsp;</td>" +
                    "</tr>";

            });
            $(htmlData).insertAfter("#tbRepairList tr:eq(0)");

            alert("查询完成！");
        });
}

function OnVerifySuccess() {

    LoopUpdateRepairStatus(E_RepairStatus_VerifySuccess);
}

function OnVerifyFail() {

    LoopUpdateRepairStatus(E_RepairStatus_VerifyFail);
}

function OnProductRepair() {

    LoopUpdateRepairStatus(E_RepairStatus_ProductRepair);
}

function OnProductDelivery() {

    LoopUpdateRepairStatus(E_RepairStatus_ProductDelivery);
}

function OnRepairFinish() {

    LoopUpdateRepairStatus(E_RepairStatus_RepairFinish);
}

function OnCustomerConfirm() {

    LoopUpdateRepairStatus(E_RepairStatus_CustomerConfirm);
}

function LoopUpdateRepairStatus(RepairStatus) {

    if (!window.confirm("确认要对选中返修进行处理吗？")) {
        return false;
    }

    var RepairID = 0;
    $("#tbRepairList input[type='checkbox']").each(function () {
        if (this.checked) {
            RepairID = $(this).val();
            UpdateRepairStatus(RepairID, RepairStatus);
        }
    });
}

function UpdateRepairStatus(RepairID, RepairStatus) {

    if (RepairID == 0) {

        alert("找不到返修号！");
        return;
    }

    var jqResultCell = $("#trRepair" + RepairID + " td:eq(8)");
    jqResultCell.css("color", "blue").text("处理中");

    $.post("SalesService.aspx",
        { "TransCode": "UpdateRepairStatus",
            "RepairID": RepairID,
            "RepairStatus": RepairStatus
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                jqResultCell.css("color", "red").text(ReturnMessage);
                return false;
            }

            jqResultCell.css("color", "green").text("处理完成");
        });
}
