BatchSelector_Append();

$(document).ready(function () {

    LoadDictItemList("BatchStatus", OnBaseDictLoaded);
    BatchSelector_Init();
});


/****** 商品下拉选择框处理 ******/
function BatchSelector_OnSelectBatch(obj, BatchID) {

    var jqRow = $(obj).parent().parent();
    var BatchNo = jqRow.children("td:eq(0)").text();

    $("#txtBatchNo").val(BatchNo);
    $("#hdnBatchID").val(BatchID);

    BatchSelector_Hide();
}


/****** 批次订单商品 ******/
var jqBatchProductList = null;

function QueryBatchProductList() {

    var BatchID = $("#hdnBatchID").val();
    if (BatchID == 0) {
        alert("请选择批次！");
        return;
    }

    $.post("BatchService.aspx",
        { "TransCode": "QueryBatchProductList",
            "BatchID": BatchID
        },
        function (data) {

            var htmlData = "";
            jqBatchProductList = $(data);

            $("#tbBatchProductList tr:gt(0)").remove();

            $(data).find("BatchProduct").each(function () {

                var jqProduct = $(this);
                var ProductSpecID = jqProduct.attr("ProductSpecID");
                var ProductNo = jqProduct.attr("ProductNo");
                var ProductName = jqProduct.attr("ProductName");
                var ProductUnit = jqProduct.attr("ProductUnit");
                var ProductSpec = jqProduct.attr("ProductSpec");
                var TotalQuantity = jqProduct.attr("TotalQuantity");
                var TotalAmount = jqProduct.attr("TotalAmount");
                TotalAmount = ToMoney(TotalAmount);

                htmlData += "<tr>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + TotalQuantity + "</td>" +
                            "<td>" + TotalAmount + "</td>" +
                        "</tr>";

            });
            $(htmlData).insertAfter("#tbBatchProductList tr:eq(0)");

            alert("查询完成！");
        });
}

function BatchOrderPick() {

    if (!window.confirm("确认要对批次订单进行确认处理吗？")) {
        return false;
    }

    var BatchID = $("#hdnBatchID").val();
    if (BatchID == 0) {
        alert("找不到批次号！");
        return;
    }

    $.post("BatchService.aspx",
        { "TransCode": "BatchOrderPick",
            "BatchID": BatchID
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            alert("处理成功！");
        });
}

function OnPrint() {

    if (jqBatchProductList == null) {
        alert("请先查询！");
        return;
    }

    var objExcel, xlsBook, xlsSheet;
    var TitleIndex = 3;

    try {
        objExcel = new ActiveXObject("Excel.Application"); //创建AX对象objExcel
        xlsBook = objExcel.Workbooks.Add;                   //获取workbook对象
        xlsSheet = xlsBook.Worksheets(1);                  //创建xlsSheet
    }
    catch (err) {
        var txt = "启用Excel控件失败。\n错误描述: " + err.description + "\n";
        alert(txt);
    }

    var BatchNo = $("#txtBatchNo").val();

    //页面设置
    objExcel.ActiveSheet.PageSetup.LeftMargin = objExcel.InchesToPoints(0.4);
    objExcel.ActiveSheet.PageSetup.RightMargin = objExcel.InchesToPoints(0.4);
    objExcel.ActiveSheet.PageSetup.TopMargin = objExcel.InchesToPoints(0.4);
    objExcel.ActiveSheet.PageSetup.BottomMargin = objExcel.InchesToPoints(0.4);
    objExcel.ActiveSheet.PageSetup.HeaderMargin = objExcel.InchesToPoints(0);
    objExcel.ActiveSheet.PageSetup.FooterMargin = objExcel.InchesToPoints(0);
    objExcel.ActiveSheet.PageSetup.CenterHorizontally = 1;

    xlsSheet.Rows.Font.Size = 9;  //设置文字大小
    xlsSheet.Rows.Font.Name = "宋体"; //设置字体

    // 标题
    xlsSheet.Range(xlsSheet.Cells(1, 1), xlsSheet.Cells(1, 6)).MergeCells = true;
    xlsSheet.Cells(1, 1).HorizontalAlignment = 3;
    xlsSheet.Cells(1, 1).VerticalAlignment = 2;
    xlsSheet.Cells(1, 1).Font.Size = 12;
    xlsSheet.Cells(1, 1).Font.Bold = true;
    xlsSheet.Cells(1, 1).RowHeight = 22;
    xlsSheet.Cells(1, 1).Value = "订单拣货商品列表";

    // 批次号等信息
    xlsSheet.Range(xlsSheet.Cells(2, 1), xlsSheet.Cells(2, 6)).MergeCells = true;
    xlsSheet.Cells(2, 1).HorizontalAlignment = 2;
    xlsSheet.Cells(2, 1).VerticalAlignment = 2;
    xlsSheet.Cells(2, 1).Font.Size = 9;
    xlsSheet.Cells(2, 1).RowHeight = 15;
    xlsSheet.Cells(2, 1).Value = "批次号：" + BatchNo;

    // 设置列宽度
    xlsSheet.Columns(1).ColumnWidth = 10;
    xlsSheet.Columns(2).ColumnWidth = 35;
    xlsSheet.Columns(3).ColumnWidth = 10;
    xlsSheet.Columns(4).ColumnWidth = 15;
    xlsSheet.Columns(5).ColumnWidth = 10;
    xlsSheet.Columns(6).ColumnWidth = 10;

    // 表头
    xlsSheet.Cells(TitleIndex, 1).Value = "商品编号";
    xlsSheet.Cells(TitleIndex, 1).HorizontalAlignment = 3;
    xlsSheet.Cells(TitleIndex, 2).Value = "商品名称";
    xlsSheet.Cells(TitleIndex, 2).HorizontalAlignment = 3;
    xlsSheet.Cells(TitleIndex, 3).Value = "商品单位";
    xlsSheet.Cells(TitleIndex, 3).HorizontalAlignment = 3;
    xlsSheet.Cells(TitleIndex, 4).Value = "商品规格";
    xlsSheet.Cells(TitleIndex, 4).HorizontalAlignment = 3;
    xlsSheet.Cells(TitleIndex, 5).Value = "总数量";
    xlsSheet.Cells(TitleIndex, 5).HorizontalAlignment = 3;
    xlsSheet.Cells(TitleIndex, 6).Value = "总金额";
    xlsSheet.Cells(TitleIndex, 6).HorizontalAlignment = 3;
    xlsSheet.Rows(TitleIndex).RowHeight = 15; 

    // 导出数据
    var nRowIndex = TitleIndex;
    jqBatchProductList.find("BatchProduct").each(function () {

        nRowIndex++;

        var jqProduct = $(this);
        var ProductNo = jqProduct.attr("ProductNo");
        var ProductName = jqProduct.attr("ProductName");
        var ProductUnit = jqProduct.attr("ProductUnit");
        var ProductSpec = jqProduct.attr("ProductSpec");
        var TotalQuantity = jqProduct.attr("TotalQuantity");
        var TotalAmount = jqProduct.attr("TotalAmount");
        TotalAmount = ToMoney(TotalAmount);

        xlsSheet.Cells(nRowIndex, 1).Value = ProductNo;
        xlsSheet.Cells(nRowIndex, 2).Value = ProductName;
        xlsSheet.Cells(nRowIndex, 3).Value = ProductUnit;
        xlsSheet.Cells(nRowIndex, 3).HorizontalAlignment = 3; 
        xlsSheet.Cells(nRowIndex, 4).Value = ProductSpec;
        xlsSheet.Cells(nRowIndex, 5).Value = TotalQuantity;
        xlsSheet.Cells(nRowIndex, 6).Value = TotalAmount;
        xlsSheet.Cells(nRowIndex, 6).NumberFormatLocal = "0.00_ ";
        xlsSheet.Rows(nRowIndex).RowHeight = 15; 
    });

    // 设置数据样式
    xlsSheet.Range(xlsSheet.Cells(TitleIndex, 1), xlsSheet.Cells(nRowIndex, 6)).Borders.Weight = 2;

    try {
        // 检查文件夹是否存在
        var fso = new ActiveXObject("Scripting.FileSystemObject");

        var filefolder = "c:\\AgapePrint";
        if (!fso.FolderExists(filefolder)) {
            fso.CreateFolder(filefolder);
        }

        filefolder = "c:\\AgapePrint\\ProductList";
        if (!fso.FolderExists(filefolder)) {
            fso.CreateFolder(filefolder);
        }

        // 保存文件
        var filename = filefolder + "\\ProductList_" + new Date().getTime() + ".xls";
        objExcel.ActiveSheet.SaveAs(filename);
    }
    catch (err) {
        var txt = "保存打印缓存文件失败。\n错误描述: " + err.description + "\n";
        alert(txt);
    }

    // 打印文件
    if (window.confirm("打印缓存文件创建完毕,是否确认打印？")) {

        objExcel.ActiveSheet.PrintOut;
    }

    xlsSheet.Close(savechanges = false); 
    objExcel.ActiveDocument.Close;
    objExcel.Quit;
    objExcel = null;

    CleanupTimerID = window.setInterval("Cleanup();", 1000);
}

var CleanupTimerID = "";

function Cleanup() {
    window.clearInterval(CleanupTimerID);
    CollectGarbage();
}
