var m_ProductNoQuery;
var m_ProductNameQuery;
var m_EditOperatorID;
var m_AuditOperatorID;
var m_FromEditDate;
var m_ToEditDate;
var m_ProductTempStatus;
var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;
var m_OperatorReady = false;

$(document).ready(function () {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

    SwitchPage("ProductList");

    LoadDictItemList("EProductTempStatus", OnDictLoaded);

    InitOperatorSelect();
});

function OnDictLoaded() {

    OnBaseDictLoaded();

    BindSelect("sltStatus", "EProductTempStatus", "0", "请选择");
}

function CheckReady() {

    if (!g_DictReady) {
        alert("字典没有加载！");
        return false;
    }

    if (!m_OperatorReady) {
        alert("操作员没有加载！");
        return false;
    }
    return true;
}

function InitOperatorSelect() {

    $.post("BasicService.aspx",
    { "TransCode": "QueryOperatorList"
    },
    function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        if (ReturnCode != "0000") {

            var ReturnMessage = $(data).find("ReturnMessage").text();
            alert(ReturnMessage);
            return false;
        }

        var jqEditOperatorSelect = $("#sltEditOperator");
        var jqAuditOperatorSelect = $("#sltAuditOperator");

        jqEditOperatorSelect.append("<option value='0'>请选择</option>");
        jqAuditOperatorSelect.append("<option value='0'>请选择</option>");

        $(data).find("Operator").each(function () {

            var jqOperator = $(this);
            OperatorID = jqOperator.attr("OperatorID");
            OperatorName = jqOperator.attr("OperatorName");
            html = "<option value='" + OperatorID + "'>" + OperatorName + "</option>";
            jqEditOperatorSelect.append(html);
            jqAuditOperatorSelect.append(html);

        });

        m_OperatorReady = true;
    });
}

/****** 商品 ******/
function OnQueryProductTempList() {

    if (!CheckReady()) {
        alert("页面还未完全载入！");
        return;
    }

    m_ProductNoQuery = $("#txtProductNoQuery").val();
    m_ProductNameQuery = $("#txtProductNameQuery").val();
    m_EditOperatorID = $("#sltEditOperator").val();
    m_AuditOperatorID = $("#sltAuditOperator").val();
    m_FromEditDate = $("#txtFromEditDate").val();
    m_ToEditDate = $("#txtToEditDate").val();
    m_ProductTempStatus = $("#sltStatus").val();

    QueryProductTempCount();
}

function QueryProductTempCount() {

    $.post("ProductService.aspx",
    {
        "TransCode": "QueryProductTempCount",
        "ProductNo": m_ProductNoQuery,
        "ProductName": m_ProductNameQuery,
        "EditOperatorID": m_EditOperatorID,
        "AuditOperatorID": m_AuditOperatorID,
        "FromEditDate": m_FromEditDate,
        "ToEditDate": m_ToEditDate,
        "Status": m_ProductTempStatus
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

        QueryProductTempList(m_CurrentPageIndex);
    });
}

PageClick = function (pageclickednumber) {

    QueryProductTempList(pageclickednumber);
}

function QueryProductTempList(PageIndex) {

    $.post("ProductService.aspx",
    {
        "TransCode": "QueryProductTempList",
        "ProductNo": m_ProductNoQuery,
        "ProductName": m_ProductNameQuery,
        "ProductCategory": 0,
        "EditOperatorID": m_EditOperatorID,
        "AuditOperatorID": m_AuditOperatorID,
        "FromEditDate": m_FromEditDate,
        "ToEditDate": m_ToEditDate,
        "Status": m_ProductTempStatus,
        "PageIndex": PageIndex,
        "PageSize": m_PageSize
    }, function (data) {

        var htmlData = "";
        $("#tbProductList tr:gt(0)").remove();

        $(data).find("ProductTemp").each(function () {

            var jqProduct = $(this);
            var ProductTempID = jqProduct.attr("ProductTempID");
            var ProductID = jqProduct.attr("ProductID");
            var ProductNo = jqProduct.attr("ProductNo");
            var ProductName = jqProduct.attr("ProductName");
            var ProductCategoryName = jqProduct.attr("ProductCategoryName");
            var ProductBrandName = jqProduct.attr("ProductBrandName");
            var ProductUnit = jqProduct.attr("ProductUnit");
            var SalesPrice = jqProduct.attr("SalesPrice");
            var EditOperatorName = jqProduct.attr("EditOperatorName");
            var AuditOperatorName = jqProduct.attr("AuditOperatorName");
            var EditTime = jqProduct.attr("EditTime");
            var AuditTime = jqProduct.attr("AuditTime");
            var Remark = jqProduct.attr("Remark");
            var Status = jqProduct.attr("Status");
            var StatusName = GetDictItemValue("EProductTempStatus", Status);

            htmlData += "<tr>" +
                            "<td><input type='checkbox' id='ProductID' value='" + ProductTempID + "' /></td>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductCategoryName + "</td>" +
                            "<td>" + ProductBrandName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + SalesPrice + "</td>" +
                            "<td>" + EditOperatorName + "</td>" +
                            "<td>" + EditTime + "</td>" +
                            "<td>" + AuditOperatorName + "</td>" +
                            "<td>" + AuditTime + "</td>" +
                            "<td>" + Remark + "</td>" +
                            "<td>" + StatusName + "</td>" +
                            "<td>" +
                                "<a Target='_blank' href='http://www.ibaby361.com/ProductTemp.aspx?ProductTempID=" + ProductTempID + "';return true;'>预览</a>";

            if (Status == 2) {
                htmlData += "|<a href='#none' onclick=OnAuditPassProduct(" + ProductTempID + ");return true;'>通过</a>" +
                                "|<a href='#none' onclick=OnReturnProduct(" + ProductTempID + ");return true;'>退回</a></td>";
            }
            htmlData +=
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbProductList tr:eq(0)");

        m_CurrentPageIndex = PageIndex;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });

        alert("查询完成！");
    });
}

function OnAuditPassProduct(ProductTempID) {

    if (!window.confirm("确认审核通过该商品编辑吗？")) {
        return false;
    }

    $.post("ProductService.aspx",
        {
            "TransCode": "AuditPassProduct",
            "ProductTempID": ProductTempID

        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            if (ProductID == 0) {
                ProductID = $(data).find("ProductID").text();
                $("#ProductID").val(ProductID);

            }
            alert("审核通过提交成功！");
        });
}

function OnReturnProduct(ProductTempID) {

    var Remark = window.prompt("请输入退回的原因：", "")
    if (Remark == null || Remark == "") {
        return;
    }

    $.post("ProductService.aspx",
       {
           "TransCode": "AuditBackProduct",
           "ProductTempID": ProductTempID,
           "Remark": Remark
       },
       function (data) {

           var ReturnCode = $(data).find("ReturnCode").text();
           var ReturnMessage = $(data).find("ReturnMessage").text();
           if (ReturnCode != "0000") {

               alert(ReturnMessage);
               return false;
           }

           if (ProductID == 0) {
               ProductID = $(data).find("ProductID").text();
               $("#ProductID").val(ProductID);

           }
           alert("审核退回提交成功！");
       });
}

function RefreshProductCache(ProductID) {

    $.post("ProductService.aspx",
       {
           "TransCode": "RefreshProductCache",
           "ProductID": ProductID

       },
       function (data) {

           var ReturnCode = $(data).find("ReturnCode").text();
           var ReturnMessage = $(data).find("ReturnMessage").text();
           if (ReturnCode != "0000") {

               alert(ReturnMessage);
               return false;
           }
       });
}


/****** 全局功能 ******/
function OnBackToProductList() {

    SwitchPage("ProductList");
}

function SwitchPage(PageName) {

    if (PageName == "ProductList") {

        $("#divProductList").show();
        $("#divProductEditor").hide();
        $("#btnBackToProductList").hide();

    }
    else if (PageName == "ProductEditor") {

        $("#divProductEditor").show();
        $("#divProductList").hide();
        $("#btnBackToProductList").show();

    }
}

function IsPageReady() {
    return m_ProductCategoryReady;
}

