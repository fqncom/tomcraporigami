var m_RowCount = 0;
var m_CurrentRowIndex = 0;

$(document).ready(function() {

});

function OnReadFile() {

    var strFilePath = $("#fileProductFilePath").val();
    //var strFilePath = "D:\\test.xls";
    
    var oXL = new ActiveXObject("Excel.Application");
    try {
        var oWB = oXL.Workbooks.open(strFilePath);
    }
    catch (e) {
        alert('打开文件失败！');
    }

    var oSheet = oWB.ActiveSheet;

    var StartRowIndex = 3;
    var ProductID = 0;
    var htmlData = "";
    m_RowCount = 0;
    $("#tbProductList tr:gt(0)").remove();

    for (var i = StartRowIndex; i <= oSheet.usedrange.rows.count; i++) {

        var ProductNo = $.trim(oSheet.Cells(i, 1).value);
        if (ProductNo == null || ProductNo == "") {
            break;
        }
        
        var ProductName = $.trim(oSheet.Cells(i, 2).value);
        var ProductSpec = $.trim(oSheet.Cells(i, 3).value);
        var ProductUnit = $.trim(oSheet.Cells(i, 4).value);
        var BarCode = $.trim(oSheet.Cells(i, 5).value);
        var ProductCategory = $.trim(oSheet.Cells(i, 6).value);
        var ProductBrand = $.trim(oSheet.Cells(i, 7).value);
        var MarketPrice = $.trim(oSheet.Cells(i, 8).value);
        var SalesPrice = $.trim(oSheet.Cells(i, 9).value);

        if (ProductSpec == null) ProductSpec = "";       
        if (BarCode == null) BarCode = "";
        if (SalesPrice == null) SalesPrice = 0;
        if (MarketPrice == null) MarketPrice = 0;
        
        htmlData += "<tr>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + BarCode + "</td>" +
                            "<td>" + ProductCategory + "</td>" +
                            "<td>" + ProductBrand + "</td>" +
                            "<td>" + MarketPrice + "</td>" +
                            "<td>" + SalesPrice + "</td>" +
                            "<td>未导入</td>" +
                        "</tr>";
        m_RowCount++;

    }
    $(htmlData).insertAfter("#tbProductList tr:eq(0)");

    oSheet = null;
    oWB.close();
    oXL = null;
}

function OnImportProduct() {

    if (!window.confirm("确认要保存吗？")) {
        return false;
    }

    if (m_RowCount > 0) {
        m_CurrentRowIndex = 0;
        OnSaveProduct();
    }
}

function OnSaveProduct() {

    if (m_CurrentRowIndex >= m_RowCount || m_RowCount < 0) {

        alert("导入完成！");
        return;
    }

    var trIndex = m_CurrentRowIndex + 1;
    var jRow = $("#tbProductList tr:eq(" + trIndex + ")");

    var Status = $("td:eq(9)", jRow).text();
    if (Status == "导入成功") {
    
        m_CurrentRowIndex++;
        OnSaveProduct();
        return;
    }
    
    var ProductNo = $("td:eq(0)", jRow).text();
    var ProductName = $("td:eq(1)", jRow).text();
    var ProductSpec = $("td:eq(2)", jRow).text();
    var ProductUnit = $("td:eq(3)", jRow).text();
    var BarCode = $("td:eq(4)", jRow).text();
    var ProductCategory = $("td:eq(5)", jRow).text();
    var ProductBrand = $("td:eq(6)", jRow).text();
    var MarketPrice = $("td:eq(7)", jRow).text();
    var SalesPrice = $("td:eq(8)", jRow).text();

    $("td:eq(9)", jRow).css("color", "blue").text("开始导入");
    
    $.post("ProductService.aspx",
        { "TransCode": "ImportProduct",
            "ProductID": 0,
            "ProductNo": ProductNo,
            "ProductName": ProductName,
            "ProductSpec": ProductSpec,
            "ProductUnit": ProductUnit,
            "BarCode": BarCode,
            "ProductCategory": ProductCategory,
            "ProductBrand": ProductBrand,
            "MarketPrice": MarketPrice,
            "SalesPrice": SalesPrice,
            "PurchasePrice": 0,
            "FitAge": "",
            "Description": ""
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                $("td:eq(9)", jRow).css("color", "red").text(ReturnMessage);
                
                if (!window.confirm("是否继续导入？")) {
                    return false;
                }
            }
            else {

                ProductID = $(data).find("ProductID").text();
                $("td:eq(9)", jRow).css("color", "green").text("导入成功");
            }


            m_CurrentRowIndex++;
            OnSaveProduct();
        });
}