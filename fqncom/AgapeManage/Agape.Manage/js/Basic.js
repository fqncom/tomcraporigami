var m_jqDictItemList;
var g_DictReady = false;

function GetDictItemList() {

}

function BindSelect(SelectID, DictCode, DefaultValue, DefaultText) {

    var jqDictItem, DictItemKey, DictItemValue, html;

    var jqSelect = $("#" + SelectID);

    jqSelect.empty();

    if (DefaultValue != "") {
    
        html = "<option value='" + DefaultValue + "'>" + DefaultText + "</option>";
        jqSelect.append(html);
    }

    m_jqDictItemList.find("Dict[DictCode='" + DictCode + "'] DictItem").each(function () {

        jqDictItem = $(this);
        DictItemKey = jqDictItem.attr("DictItemKey");
        DictItemValue = jqDictItem.attr("DictItemValue");
        html = "<option value='" + DictItemKey + "'>" + DictItemKey + " - " + DictItemValue + "</option>";
        jqSelect.append(html);
    });
}

function GetDictItemValue(DictCode, DictItemKey) {

    var jqDictItem = m_jqDictItemList.find("Dict[DictCode='" + DictCode + "'] DictItem[DictItemKey='" + DictItemKey + "']");
    if (jqDictItem == null || jqDictItem.length != 1) {
        return "";
    }

    var DictItemValue = jqDictItem.attr("DictItemValue");
    return DictItemValue;
}

function LoadDictItemList(DictCodeList, callback) {

    $.post("BasicService.aspx", { "TransCode": "QueryDictItemList", "DictCodeList": DictCodeList }, function(data) {

        m_jqDictItemList = $(data).find("DictList");

        callback();
    });

}

function OnBaseDictLoaded() {

    g_DictReady = true;
}