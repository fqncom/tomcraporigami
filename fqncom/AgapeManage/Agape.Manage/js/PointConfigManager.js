var m_bChangedFlag = false;

ProductCategorySelector_Append();
ProductSelector_Append();

$(document).ready(function() {

    InitTab();
    ProductCategorySelector_Init();
    ProductSelector_Init();

    OnQueryPointConfig();
    QueryProductCategoryPointConfigList();
    QueryProductPointConfigList();
});

/****** 选项卡 ******/
function InitTab() {

    var jqMenuItemList = $(".tabmenu ul li");

    jqMenuItemList.click(function() {

        $(this).addClass("selected")                    //当前<li>元素高亮
				   .siblings().removeClass("selected"); //去掉其他同辈<li>元素的高亮

        var index = jqMenuItemList.index(this);         // 获取当前点击的<li>元素 在 全部li元素中的索引。
        $(".tabbox > div.tabitem")   	            //选取子节点。不选取子节点的话，会引起错误。如果里面还有div 
					.eq(index).show()                   //显示 <li>元素对应的<div>元素
					.siblings().hide();                 //隐藏其他几个同辈的<div>元素
    });

    jqMenuItemList.hover(function() {
        $(this).addClass("hover");
    }, function() {
        $(this).removeClass("hover");
    });
}

function OnQueryPointConfigList() {

    SwitchDiv("PointConfigList");

    QueryPointConfigList();
    
    alert("查询完成！");
}


/****** 积分配置处理 ******/
function OnQueryPointConfig() {

    $.post("ConfigService.aspx",
    { "TransCode": "QueryPointConfig"
    },
    function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var PointValue = $(data).find("PointConfig:first").attr("PointValue");
        var MinAmountToPoint = $(data).find("PointConfig:first").attr("MinAmountToPoint");

        $("#txtPointValue").val(PointValue);
        $("#txtMinAmountToPoint").val(MinAmountToPoint);
    });
}

function OnSavePointConfig() {

    var PointValue = $("#txtPointValue").val();
    if (PointValue == "") {
        alert("请输入积分值！");
        return;
    }

    var MinAmountToPoint = $("#txtMinAmountToPoint").val();
    if (MinAmountToPoint == "") {
        alert("请输入最小消费积分金额！");
        return;
    }

    if (!window.confirm("确认要保存限购商品吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SavePointConfig",
        "PointValue": PointValue,
        "MinAmountToPoint": MinAmountToPoint
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存积分配置成功！");
    });
}


/****** 商品类型下拉选择框处理 ******/
function OnSelectProductCategory(ProductCategoryID) {

    var ProductCategoryName = GetProductCategoryName(ProductCategoryID);
    $("#txtProductCategory").val(ProductCategoryID + " - " + ProductCategoryName);
    $("#hdnProductCategoryID").val(ProductCategoryID);

    ProductCategorySelector_Hide();
}


/****** 商品下拉选择框处理 ******/
function ProductSelector_OnSelectProduct(obj, ProductID) {

    var jqRow = $(obj).parent().parent();
    var ProductNo = jqRow.children("td:eq(0)").text();
    var ProductName = jqRow.children("td:eq(1)").text();
    
    $("#txtProductName").val(ProductNo + " - " + ProductName);
    $("#hdnProductID").val(ProductID);

    ProductSelector_Hide();
}


/****** 商品类型积分配置处理 ******/
function QueryProductCategoryPointConfigList() {

    $.post("ConfigService.aspx",
    { "TransCode": "QueryProductCategoryPointConfigList"
    },
    function(data) {

        var htmlData = "";
        $("#tbProductCategoryPointConfigList tr:gt(0)").remove();

        $(data).find("ProductCategoryPointConfig").each(function() {

            var jqProductCategoryPointConfig = $(this);
            var ProductCategoryID = jqProductCategoryPointConfig.attr("ProductCategoryID");
            var ProductCategoryName = jqProductCategoryPointConfig.attr("ProductCategoryName");
            var PointMode = jqProductCategoryPointConfig.attr("PointMode");
            var PointValue = jqProductCategoryPointConfig.attr("PointValue");
            
            htmlData += "<tr id='trProductCategoryPointConfig" + ProductCategoryID + "'>" +
                            "<td>" + ProductCategoryName + "</td>" +
                            "<td>" + PointMode + "</td>" +
                            "<td>" + PointValue + "</td>" +
                            "<td>" +
                            "   <a href='#none' onclick='OnDeleteProductCategoryPointConfig(" + ProductCategoryID + ");return true;'>删除</a>" +
                            "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbProductCategoryPointConfigList tr:eq(0)");
    });
}

function OnSaveProductCategoryPointConfig() {

    var ProductCategoryID = $("#hdnProductCategoryID").val();
    if (ProductCategoryID == 0 || ProductCategoryID == "") {
        alert("请选择商品类型！");
        return;
    }

    var PointValue = $("#txtProductCategoryPointValue").val();
    if (PointValue == "") {
        alert("请输入积分值！");
        return;
    }

    if (!window.confirm("确认要保存商品类型积分配置吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveProductCategoryPointConfig",
        "ProductCategoryID": ProductCategoryID,
        "PointValue": PointValue
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存商品类型积分配置成功！");
        
        ClearProductCategoryPointConfigEditor();
        
        QueryProductCategoryPointConfigList();
    });
}

function OnDeleteProductCategoryPointConfig(ProductCategoryID) {

    if (!window.confirm("确认要删除商品类型积分配置吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteProductCategoryPointConfig",
        "ProductCategoryID": ProductCategoryID
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除商品类型积分配置成功！");

        QueryProductCategoryPointConfigList();
    });
}

function ClearProductCategoryPointConfigEditor() {

    $("#txtProductCategory").val("");
    $("#hdnProductCategoryID").val(0);
    $("#txtProductCategoryPointValue").val("");
}


/****** 商品积分配置处理 ******/
function QueryProductPointConfigList() {

    $.post("ConfigService.aspx",
    { "TransCode": "QueryProductPointConfigList"
    },
    function(data) {

        var htmlData = "";
        $("#tbProductPointConfigList tr:gt(0)").remove();

        $(data).find("ProductPointConfig").each(function() {

            var jqProductPointConfig = $(this);
            var ProductID = jqProductPointConfig.attr("ProductID");
            var ProductNo = jqProductPointConfig.attr("ProductNo");
            var ProductName = jqProductPointConfig.attr("ProductName");
            var ProductSpec = jqProductPointConfig.attr("ProductSpec");
            var ProductUnit = jqProductPointConfig.attr("ProductUnit");
            var PointMode = jqProductPointConfig.attr("PointMode");
            var PointValue = jqProductPointConfig.attr("PointValue");

            htmlData += "<tr id='trProductPointConfig" + ProductID + "'>" +
                            "<td>" + ProductNo + "</td>" +
                            "<td>" + ProductName + "</td>" +
                            "<td>" + ProductSpec + "</td>" +
                            "<td>" + ProductUnit + "</td>" +
                            "<td>" + PointMode + "</td>" +
                            "<td>" + PointValue + "</td>" +
                            "<td>" +
                            "   <a href='#none' onclick='OnDeleteProductPointConfig(" + ProductID + ");return true;'>删除</a>" +
                            "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbProductPointConfigList tr:eq(0)");
    });
}

function OnSaveProductPointConfig() {

    var ProductID = $("#hdnProductID").val();
    if (ProductID == 0 || ProductID == "") {
        alert("请选择商品！");
        return;
    }

    var PointValue = $("#txtProductPointValue").val();
    if (PointValue == "") {
        alert("请输入积分值！");
        return;
    }

    if (!window.confirm("确认要保存商品积分配置吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveProductPointConfig",
        "ProductID": ProductID,
        "PointValue": PointValue
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存商品积分配置成功！");

        ClearProductPointConfigEditor();

        QueryProductPointConfigList();
    });
}

function OnDeleteProductPointConfig(ProductID) {

    if (!window.confirm("确认要删除商品积分配置吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteProductPointConfig",
        "ProductID": ProductID
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除商品积分配置成功！");

        QueryProductPointConfigList();
    });
}

function ClearProductPointConfigEditor() {

    $("#txtProductName").val("");
    $("#hdnProductID").val(0);
    $("#txtProductPointValue").val("");
}