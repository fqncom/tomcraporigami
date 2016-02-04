
$(document).ready(function() {

    InitTab();

    QueryCouponGrantAmountConfigList();
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


/****** 优惠劵发放金额区间配置处理 ******/
function QueryCouponGrantAmountConfigList() {

    $.post("ConfigService.aspx",
    { "TransCode": "QueryCouponGrantAmountConfigList"
    },
    function(data) {

        var htmlData = "";
        $("#tbCouponGrantAmountConfigList tr:gt(0)").remove();

        $(data).find("CouponGrantAmountConfig").each(function() {

            var jqCouponGrantAmountConfig = $(this);
            var CouponGrantAmountConfigID = jqCouponGrantAmountConfig.attr("CouponGrantAmountConfigID");
            var BeginAmount = jqCouponGrantAmountConfig.attr("BeginAmount");
            var EndAmount = jqCouponGrantAmountConfig.attr("EndAmount");
            var ParValue = jqCouponGrantAmountConfig.attr("ParValue");
            var Count = jqCouponGrantAmountConfig.attr("Count");
            var Status = jqCouponGrantAmountConfig.attr("Status");

            htmlData += "<tr id='trCouponGrantAmountConfig" + CouponGrantAmountConfigID + "'>" +
                            "<td>" + BeginAmount + "</td>" +
                            "<td>" + EndAmount + "</td>" +
                            "<td>" + ParValue + "</td>" +
                            "<td>" + Count + "</td>" +
                            "<td></td>" +
                            "<td class='Operation'>" +
                            "   <a href='#none' onclick='OnDeleteCouponGrantAmountConfig(" + CouponGrantAmountConfigID + ");return true;'>删除</a>" +
                            "</td>" +
                        "</tr>";

        });

        $(htmlData).insertAfter("#tbCouponGrantAmountConfigList tr:eq(0)");
    });
}

function OnSaveCouponGrantAmountConfig() {

    var CouponGrantAmountConfigID = $("#hdnCouponGrantAmountConfigID").val();

    var BeginAmount = $("#txtBeginAmount").val();
    if (BeginAmount == "") {
        alert("请输入开始金额！");
        return;
    }

    var EndAmount = $("#txtEndAmount").val();
    if (EndAmount == "") {
        alert("请输入结束金额！");
        return;
    }

    var ParValue = $("#txtParValue").val();
    if (ParValue == "") {
        alert("请输入每张面值！");
        return;
    }

    var Count = $("#txtCount").val();
    if (Count == "") {
        alert("请输入发放数量！");
        return;
    }

    if (!window.confirm("确认要保存商品优惠劵配置吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "SaveCouponGrantAmountConfig",
        "CouponGrantAmountConfigID": CouponGrantAmountConfigID,
        "BeginAmount": BeginAmount,
        "EndAmount": EndAmount,
        "ParValue": ParValue,
        "Count": Count
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("保存商品优惠劵配置成功！");

        ClearCouponGrantAmountConfigEditor();

        QueryCouponGrantAmountConfigList();
    });
}

function OnDeleteCouponGrantAmountConfig(CouponGrantAmountConfigID) {

    if (!window.confirm("确认要删除商品优惠劵配置吗？")) {
        return;
    }

    $.post("ConfigService.aspx",
    { "TransCode": "DeleteCouponGrantAmountConfig",
        "CouponGrantAmountConfigID": CouponGrantAmountConfigID
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {
            alert(ReturnMessage);
            return;
        }

        alert("删除商品优惠劵配置成功！");

        QueryCouponGrantAmountConfigList();
    });
}

function ClearCouponGrantAmountConfigEditor() {

    $("#hdnCouponGrantAmountConfigID").val(0);
    $("#txtBeginAmount").val("");
    $("#txtEndAmount").val("");
    $("#txtParValue").val("");
    $("#txtCount").val("");
}
