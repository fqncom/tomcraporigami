var AreaList =
    "[ \
            {Code:'010000',Name:'浙江省',ParentCode:'000000'}, \
            {Code:'020000',Name:'上海市',ParentCode:'000000'}, \
            {Code:'010100',Name:'杭州市',ParentCode:'010000'}, \
            {Code:'010200',Name:'宁波市',ParentCode:'010000'}, \
            {Code:'010300',Name:'温州市',ParentCode:'010000'}, \
            {Code:'010400',Name:'嘉兴市',ParentCode:'010000'}, \
            {Code:'010101',Name:'上城区',ParentCode:'010100'}, \
            {Code:'010102',Name:'下城区',ParentCode:'010100'}, \
            {Code:'010103',Name:'西湖区',ParentCode:'010100'}, \
            {Code:'010201',Name:'海曙区',ParentCode:'010200'}, \
            {Code:'010202',Name:'江东区',ParentCode:'010200'}, \
            {Code:'010203',Name:'江北区',ParentCode:'010200'}, \
            {Code:'010301',Name:'鹿城区',ParentCode:'010300'}, \
            {Code:'010302',Name:'龙港区',ParentCode:'010300'}, \
            {Code:'010401',Name:'南湖区',ParentCode:'010400'}, \
            {Code:'010402',Name:'秀洲区',ParentCode:'010400'}, \
            {Code:'010403',Name:'经济开发区',ParentCode:'010400'} \
        ]";

var AreaListJson = eval("(" + AreaList + ")");

function GetAreaName(AreaCode) {
    for (var i = 0; i < AreaListJson.length; i++) {
        if (AreaListJson[i].Code == AreaCode) {
            return AreaListJson[i].Name;
        }
    }
}

function BindAreaSelect(SelectName, ParentCode, SelectedValue) {

    var html;
    var $Select = $(SelectName);
    for (var i = 0; i < AreaListJson.length - 1; i++) {
        if (AreaListJson[i].ParentCode == ParentCode) {
            if (SelectedValue == AreaListJson[i].Code) {
                html = "<option value='" + AreaListJson[i].Code + "' selected>" + AreaListJson[i].Name + "</option>";
            }
            else {
                html = "<option value='" + AreaListJson[i].Code + "'>" + AreaListJson[i].Name + "</option>";
            }
            $Select.append(html);
        }
    }
}
