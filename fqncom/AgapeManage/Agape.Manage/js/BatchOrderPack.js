BatchSelector_Append();

$(document).ready(function () {

    LoadDictItemList("BatchStatus", OnBaseDictLoaded);
    BatchSelector_Init();
});


function CheckReady() {

    if (!g_DictReady) {
        alert("字典没有加载！");
        return false;
    }

    return true;
}


/****** 商品下拉选择框处理 ******/
function BatchSelector_OnSelectBatch(obj, BatchID) {

    var jqRow = $(obj).parent().parent();
    var BatchNo = jqRow.children("td:eq(0)").text();

    $("#txtBatchNo").val(BatchNo);
    $("#hdnBatchID").val(BatchID);

    BatchSelector_Hide();
}


/****** 批次订单 ******/
function QueryBatchOrderList() {

    if (!CheckReady()) {
        return;
    }

    var BatchID = $("#hdnBatchID").val();
    if (BatchID == 0) {
        alert("请选择批次！");
        return;
    }

    SwitchDiv("BatchOrderList");

    $.post("BatchService.aspx",
    { "TransCode": "QueryBatchOrderList",
        "BatchID": BatchID,
        "BatchStatus": E_BatchStatus_OrderPick
    },
    function (data) {

        var htmlData = "";
        $("#tbBatchOrderList tr:gt(0)").remove();

        $(data).find("BatchOrder").each(function () {

            var jqOrder = $(this);
            var OrderID = jqOrder.attr("OrderID");
            var OrderNo = jqOrder.attr("OrderNo");
            var CreateTime = jqOrder.attr("CreateTime");
            var MemberName = jqOrder.attr("MemberName");
            var TotalQuantity = jqOrder.attr("TotalQuantity");
            var TotalPayAmount = jqOrder.attr("TotalPayAmount");
            var BatchStatus = jqOrder.attr("BatchStatus");
            BatchStatus = GetDictItemValue("BatchStatus", BatchStatus);
            TotalPayAmount = ToMoney(TotalPayAmount);

            htmlData += "<tr id='trOrder" + OrderID + "'>" +
                            "<td>" + OrderNo + "</td>" +
                            "<td>" + CreateTime + "</td>" +
                            "<td>" + MemberName + "</td>" +
                            "<td>" + TotalQuantity + "</td>" +
                            "<td>" + TotalPayAmount + "</td>" +
                            "<td>" + BatchStatus + "</td>" +
                            "<td><a href='#none' onclick='EditBatchOrder(" + OrderID + ");return true;'>开始打包</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbBatchOrderList tr:eq(0)");

        alert("查询完成！");
    });
}

var m_jqOrder;

function EditBatchOrder(BatchOrderID) {

    $.post("SalesService.aspx",
    {
        "TransCode": "QueryOrderAndItems",
        "OrderID": BatchOrderID
    }, function (data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        m_jqOrder = $(data);

        var jqOrder = $(data).find("Order");
        var OrderNo = jqOrder.find("OrderNo").text();
        var CreateTime = jqOrder.find("CreateTime").text();
        var RealName = jqOrder.find("RealName").text();
        var TotalQuantity = jqOrder.find("TotalQuantity").text();
        var TotalPayAmount = jqOrder.find("TotalPayAmount").text();
        var Province = jqOrder.find("Province").text();
        var City = jqOrder.find("City").text();
        var District = jqOrder.find("District").text();
        var AddressDetail = jqOrder.find("AddressDetail").text();
        var Address = GetAreaName(Province) + GetAreaName(City) + GetAreaName(District) + AddressDetail;
        TotalPayAmount = ToMoney(TotalPayAmount);

        $("#hdnOrderID").val(BatchOrderID);
        $("#txtOrderNo").val(OrderNo);
        $("#txtCreateTime").val(CreateTime);
        $("#txtRealName").val(RealName);
        $("#txtAddress").val(Address);
        $("#txtTotalQuantity").val(TotalQuantity);
        $("#txtTotalAmount").val(TotalPayAmount);

        var htmlData = "";
        $("#tbBatchOrderItemList tr:gt(0)").remove();

        $(data).find("OrderItem").each(function () {

            var jqProduct = $(this);
            var ProductSpecID = jqProduct.find("ProductSpecID").text();
            var ProductNo = jqProduct.find("ProductNo").text();
            var ProductName = jqProduct.find("ProductName").text();
            var ProductUnit = jqProduct.find("ProductUnit").text();
            var ProductSpec = jqProduct.find("ProductSpec").text();
            var Quantity = jqProduct.find("Quantity").text();
            var Amount = jqProduct.find("Amount").text();
            Amount = ToMoney(Amount);

            htmlData += "<tr>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + Quantity + "</td>" +
                            "<td>" + Amount + "</td>" +
                            "<td>&nbsp;</td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbBatchOrderItemList tr:eq(0)");

        $("#hdnBatchOrderID").val(BatchOrderID);
        SwitchDiv("BatchOrderEditor");
    });
}

function BatchOrderPack() {

    if (!window.confirm("确认要对该订单进行打包处理吗？")) {
        return false;
    }

    var BatchOrderID = $("#hdnBatchOrderID").val();
    if (BatchOrderID == 0) {
        alert("找不到订单号！");
        return;
    }

    var BarCode = $("#txtBarCode").val();
    if (BarCode == "") {
        alert("请输入条形码！");
        return;
    }

    $.post("BatchService.aspx",
        { "TransCode": "BatchOrderPack",
            "OrderID": BatchOrderID,
            "BarCode": BarCode
        },
        function (data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            alert("处理成功！");

            $("#trOrder" + BatchOrderID).remove();
            BackToBatcOrderList();
        });
}

function BackToBatcOrderList() {

    SwitchDiv("BatchOrderList");
}


/****** 功能函数 ******/
function SwitchDiv(DivName) {

    if (DivName == "BatchOrderList") {

        $("#divBatchOrderList").show();
        $("#divBatchOrderEditor").hide();
    }
    else if (DivName == "BatchOrderEditor") {

        $("#divBatchOrderEditor").show();
        $("#divBatchOrderList").hide();
    }
}


function OnPrint() {

    if (m_jqOrder == null) {
        alert("请先查询！");
        return;
    }

    var nRowIndex = 0, nTitleIndex, nOrderIndex;
    var objExcel, xlsBook, xlsSheet;

    try {
        objExcel = new ActiveXObject("Excel.Application"); //创建AX对象objExcel
        xlsBook = objExcel.Workbooks.Add;                   //获取workbook对象
        xlsSheet = xlsBook.Worksheets(1);                  //创建xlsSheet
    }
    catch (err) {
        var txt = "启用Excel控件失败。\n错误描述: " + err.description + "\n";
        alert(txt);
    }

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
    nRowIndex = 1;
    xlsSheet.Rows(nRowIndex).RowHeight = 22;
    xlsSheet.Range(xlsSheet.Cells(nRowIndex, 1), xlsSheet.Cells(nRowIndex, 7)).MergeCells = true;
    xlsSheet.Cells(nRowIndex, 1).HorizontalAlignment = 3;
    xlsSheet.Cells(nRowIndex, 1).VerticalAlignment = 2;
    xlsSheet.Cells(nRowIndex, 1).Font.Size = 12;
    xlsSheet.Cells(nRowIndex, 1).Font.Bold = true;
    xlsSheet.Cells(nRowIndex, 1).Value = "爱家贝母婴用品网上商城订单";

    var jqOrder = m_jqOrder.find("Order");
    var OrderNo = $("#txtOrderNo").val();
    var CreateTime = $("#txtCreateTime").val();
    var RealName = $("#txtRealName").val();
    var Address = $("#txtAddress").val();
    var TotalQuantity = $("#txtTotalQuantity").val();
    var TotalPayAmount = $("#txtTotalAmount").val();

    nRowIndex++;
    nOrderIndex = nRowIndex;
    xlsSheet.Rows(nRowIndex).RowHeight = 15;
    xlsSheet.Range(xlsSheet.Cells(nRowIndex, 6), xlsSheet.Cells(nRowIndex, 7)).MergeCells = true;
    xlsSheet.Cells(nRowIndex, 1).Value = "订单编号";
    xlsSheet.Cells(nRowIndex, 2).Value = OrderNo;
    xlsSheet.Cells(nRowIndex, 3).Value = "客户姓名";
    xlsSheet.Cells(nRowIndex, 4).Value = RealName;
    xlsSheet.Cells(nRowIndex, 5).Value = "下单时间";
    xlsSheet.Cells(nRowIndex, 6).Value = CreateTime;
    xlsSheet.Cells(nRowIndex, 6).HorizontalAlignment = 2;

    nRowIndex++;
    xlsSheet.Rows(nRowIndex).RowHeight = 15;
    xlsSheet.Range(xlsSheet.Cells(nRowIndex, 6), xlsSheet.Cells(nRowIndex, 7)).MergeCells = true;
    xlsSheet.Cells(nRowIndex, 1).Value = "送货地址";
    xlsSheet.Cells(nRowIndex, 2).Value = Address;
    xlsSheet.Cells(nRowIndex, 3).Value = "收件人";
    xlsSheet.Cells(nRowIndex, 4).Value = RealName;

    nRowIndex++;
    xlsSheet.Rows(nRowIndex).RowHeight = 15;
    xlsSheet.Cells(nRowIndex, 1).Value = "总数量";
    xlsSheet.Cells(nRowIndex, 2).Value = TotalQuantity;
    xlsSheet.Cells(nRowIndex, 2).HorizontalAlignment = 2;
    xlsSheet.Cells(nRowIndex, 3).Value = "总金额";
    xlsSheet.Cells(nRowIndex, 4).Value = TotalPayAmount;
    xlsSheet.Cells(nRowIndex, 4).NumberFormatLocal = "0.00_ ";
    xlsSheet.Cells(nRowIndex, 4).HorizontalAlignment = 2;

    // 设置列宽度
    xlsSheet.Columns(1).ColumnWidth = 10;
    xlsSheet.Columns(2).ColumnWidth = 30;
    xlsSheet.Columns(3).ColumnWidth = 10;
    xlsSheet.Columns(4).ColumnWidth = 10;
    xlsSheet.Columns(5).ColumnWidth = 10;
    xlsSheet.Columns(6).ColumnWidth = 10;
    xlsSheet.Columns(7).ColumnWidth = 10;

    // 表头
    nRowIndex++;
    nTitleIndex = nRowIndex;
    xlsSheet.Rows(nRowIndex).RowHeight = 15; 
    xlsSheet.Cells(nRowIndex, 1).Value = "商品编号";
    xlsSheet.Cells(nRowIndex, 1).HorizontalAlignment = 3;
    xlsSheet.Cells(nRowIndex, 2).Value = "商品名称";
    xlsSheet.Cells(nRowIndex, 2).HorizontalAlignment = 3;
    xlsSheet.Cells(nRowIndex, 3).Value = "商品单位";
    xlsSheet.Cells(nRowIndex, 3).HorizontalAlignment = 3;
    xlsSheet.Cells(nRowIndex, 4).Value = "商品规格";
    xlsSheet.Cells(nRowIndex, 4).HorizontalAlignment = 3;
    xlsSheet.Cells(nRowIndex, 5).Value = "数量";
    xlsSheet.Cells(nRowIndex, 5).HorizontalAlignment = 3;
    xlsSheet.Cells(nRowIndex, 6).Value = "价格";
    xlsSheet.Cells(nRowIndex, 6).HorizontalAlignment = 3;
    xlsSheet.Cells(nRowIndex, 7).Value = "金额";
    xlsSheet.Cells(nRowIndex, 7).HorizontalAlignment = 3;

    // 导出数据
    m_jqOrder.find("OrderItem").each(function () {

        nRowIndex++;

        var jqProduct = $(this);
        var ProductNo = jqProduct.find("ProductNo").text();
        var ProductName = jqProduct.find("ProductName").text();
        var ProductUnit = jqProduct.find("ProductUnit").text();
        var ProductSpec = jqProduct.find("ProductSpec").text();
        var Price = jqProduct.find("Price").text();
        var Quantity = jqProduct.find("Quantity").text();
        var Amount = jqProduct.find("Amount").text();

        xlsSheet.Cells(nRowIndex, 1).Value = ProductNo;
        xlsSheet.Cells(nRowIndex, 2).Value = ProductName;
        xlsSheet.Cells(nRowIndex, 3).Value = ProductUnit;
        xlsSheet.Cells(nRowIndex, 3).HorizontalAlignment = 3;
        xlsSheet.Cells(nRowIndex, 4).Value = ProductSpec;
        xlsSheet.Cells(nRowIndex, 5).Value = Quantity;
        xlsSheet.Cells(nRowIndex, 5).HorizontalAlignment = 3;
        xlsSheet.Cells(nRowIndex, 6).Value = Price;
        xlsSheet.Cells(nRowIndex, 7).Value = Amount;
        xlsSheet.Cells(nRowIndex, 7).NumberFormatLocal = "0.00_ ";
        xlsSheet.Rows(nRowIndex).RowHeight = 15;
    });

    // 设置数据样式
    xlsSheet.Range(xlsSheet.Cells(nOrderIndex, 1), xlsSheet.Cells(nRowIndex, 7)).Borders.Weight = 2;

    try {
        // 检查文件夹是否存在
        var fso = new ActiveXObject("Scripting.FileSystemObject");

        var filefolder = "c:\\AgapePrint";
        if (!fso.FolderExists(filefolder)) {
            fso.CreateFolder(filefolder);
        }

        filefolder = "c:\\AgapePrint\\Order";
        if (!fso.FolderExists(filefolder)) {
            fso.CreateFolder(filefolder);
        }

        // 保存文件
        var filename = filefolder + "\\Order_" + new Date().getTime() + ".xls";
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

