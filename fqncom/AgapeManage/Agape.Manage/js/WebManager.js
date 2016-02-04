function OnUpdateTopProductList() {

    if (!window.confirm("确认要更新商品排行榜列表吗？")) {
        return;
    }

    $.post("WebManagerService.aspx",
    { "TransCode": "UpdateTopProductList"
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        alert("执行成功！");
    });
}

function OnUpdateAllProductCategoryFullPath() {

    if (!window.confirm("确认要更新所有商品类型全路径吗？")) {
        return;
    }

    $.post("WebManagerService.aspx",
    { "TransCode": "UpdateAllProductCategoryFullPath"
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        alert("执行成功！");
    });
}

function OnUpdateAllProductWordKey() {

    if (!window.confirm("确认要更新所有商品检索词吗？")) {
        return;
    }

    $.post("WebManagerService.aspx",
    { "TransCode": "UpdateAllProductWordKey"
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        alert("执行成功！");
    });
}