var g_IsLogin = false;
var g_OperatorID = 0;
var g_OperatorName = "";

$(document).ready(function() {

});

function SetRadioGroupValue(RadioName, Value) {

    $("input[name='" + RadioName + "'][value!='" + Value + "']").removeAttr("checked");
    $("input[name='" + RadioName + "'][value='" + Value + "']").attr("checked", "true");
}

function GetRadioGroupValue(RadioName) {

    return $("input[name='" + RadioName + "'][checked]").val();
}

function ResetRadioGroup(RadioName) {

    return $("input[name='" + RadioName + "']").removeAttr("checked"); ;
}

//小数两位截取函数
function ToAmount(varNumber) {
    if (varNumber.toFixed) {
        // Browser supports toFixed() method
        varNumber = varNumber.toFixed(2)
    } else {
        // Browser doesn’t support toFixed() method so use some other code
        var div = Math.pow(10, 2);
        varNumber = Math.round(varNumber * div) / div;
    }
    return varNumber;
}

function ToMoney(number) {
    number = number.replace(/\,/g, "");
    if (isNaN(number) || number == "") return "";
    number = Math.round(number * 100) / 100;
    if (number < 0)
        return '-' + ToDollars(Math.floor(Math.abs(number) - 0) + '') + ToCents(Math.abs(number) - 0);
    else
        return ToDollars(Math.floor(number - 0) + '') + ToCents(number - 0);
}

function ToDollars(number) {
    if (number.length <= 3)
        return (number == '' ? '0' : number);
    else {
        var mod = number.length % 3;
        var output = (mod == 0 ? '' : (number.substring(0, mod)));
        for (i = 0; i < Math.floor(number.length / 3); i++) {
            if ((mod == 0) && (i == 0))
                output += number.substring(mod + 3 * i, mod + 3 * i + 3);
            else
                output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
        }
        return (output);
    }
}
function ToCents(amount) {
    amount = Math.round(((amount) - Math.floor(amount)) * 100);
    return (amount < 10 ? '.0' + amount : '.' + amount);
}


//   数字转换成大写金额函数   
function ToChineseAmount(numberValue) {
    var numberValue = new String(Math.round(numberValue * 100));   //   数字金额   
    var chineseValue = "";                     //   转换后的汉字金额   
    var String1 = "零壹贰叁肆伍陆柒捌玖";               //   汉字数字   
    var String2 = "万仟佰拾亿仟佰拾万仟佰拾元角分";           //   对应单位   
    var len = numberValue.length;                   //   numberValue   的字符串长度   
    var Ch1;                           //   数字的汉语读法   
    var Ch2;                           //   数字位的汉字读法   
    var nZero = 0;                         //   用来计算连续的零值的个数   
    var String3;                         //   指定位置的数值   
    if (len > 15) {
        alert("超出计算范围");
        return "";
    }
    if (numberValue == 0) {
        chineseValue = "零元整";
        return chineseValue;
    }

    String2 = String2.substr(String2.length - len, len);       //   取出对应位数的STRING2的值   
    for (var i = 0; i < len; i++) {
        String3 = parseInt(numberValue.substr(i, 1), 10);       //   取出需转换的某一位的值   
        if (i != (len - 3) && i != (len - 7) && i != (len - 11) && i != (len - 15)) {
            if (String3 == 0) {
                Ch1 = "";
                Ch2 = "";
                nZero = nZero + 1;
            }
            else if (String3 != 0 && nZero != 0) {
                Ch1 = "零" + String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
            else {
                Ch1 = String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
        }
        else {                             //   该位是万亿，亿，万，元位等关键位   
            if (String3 != 0 && nZero != 0) {
                Ch1 = "零" + String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
            else if (String3 != 0 && nZero == 0) {
                Ch1 = String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
            else if (String3 == 0 && nZero >= 3) {
                Ch1 = "";
                Ch2 = "";
                nZero = nZero + 1;
            }
            else {
                Ch1 = "";
                Ch2 = String2.substr(i, 1);
                nZero = nZero + 1;
            }
            if (i == (len - 11) || i == (len - 3)) {         //   如果该位是亿位或元位，则必须写上   
                Ch2 = String2.substr(i, 1);
            }
        }
        chineseValue = chineseValue + Ch1 + Ch2;
    }

    if (String3 == 0) {                       //   最后一位（分）为0时，加上“整”   
        chineseValue = chineseValue + "整";
    }

    return chineseValue;
}

// 加载等待框
var LoadingHtml =
    "<div id='divLoadingBackground' class='LoadingBackground' style='display: none;'></div> " +
    "<div id='divLoadingProgressBar' class='LoadingProgressBar' style='display: none;'>加载中，请稍等...</div>";

document.write(LoadingHtml);

function ShowLoadingBox() {
    var ajaxbg = $("#divLoadingBackground,#divLoadingProgressBar");
    ajaxbg.show();
}

function HideLoadingBox() {
    var ajaxbg = $("#divLoadingBackground,#divLoadingProgressBar");
    ajaxbg.hide();
}


/****** 功能 ******/
function GetAbsoluteLocationEx(element) {
    if (arguments.length != 1 || element == null) {
        return null;
    }
    var elmt = element;
    var offsetTop = elmt.offsetTop;
    var offsetLeft = elmt.offsetLeft;

    while (elmt = elmt.offsetParent) {
        // add this judge   
        if (elmt.style.position == 'absolute' || elmt.style.position == 'relative' || (elmt.style.overflow != 'visible' && elmt.style.overflow != '')) {
            break;
        }
        offsetTop += elmt.offsetTop;
        offsetLeft += elmt.offsetLeft;


    }

    //alert(offsetTop + "," + offsetLeft);
    return { Y: offsetTop, X: offsetLeft };

}


var E_BatchStatus_BatchActive = 1;
var E_BatchStatus_BatchCreated = 2;
var E_BatchStatus_OrderConfirm = 3;
var E_BatchStatus_OrderPick = 4;
var E_BatchStatus_OrderPack = 5;
var E_BatchStatus_OrderDeliver = 6;
var E_BatchStatus_OrderFinish = 7;

var E_OrderStatus_Created = 0;
var E_OrderStatus_Submit = 1;
var E_OrderStatus_Payed = 2;
var E_OrderStatus_Cancel = 3;
var E_OrderStatus_Failed = 4;
var E_OrderStatus_Frozen = 5;
var E_OrderStatus_Process = 6;
var E_OrderStatus_Deliver = 7;
var E_OrderStatus_Finish = 8;
var E_OrderStatus_Refund = 9;

var E_MemberInvoiceType_General = 1;
var E_MemberInvoiceType_ValueAddedTax = 2;

var E_MemberInvoiceHeaderType_Individual = 1;
var E_MemberInvoiceHeaderType_Company = 2;

var E_MemberInvoiceContent_Detail = 1;
var E_MemberInvoiceContent_Office = 2;
var E_MemberInvoiceContent_Computer = 3;
var E_MemberInvoiceContent_Expendable = 4;

var E_ShoppingType_Sales = 1;
var E_ShoppingType_Exchange = 2;

var E_MemberConsultationStatus_Question = 1;
var E_MemberConsultationStatus_Answer = 2;
var E_MemberConsultationStatus_Delete = 4;

var E_RepairStatus_SubmitRequest = 1;
var E_RepairStatus_CancelRequest = 2;
var E_RepairStatus_VerifySuccess = 3;
var E_RepairStatus_VerifyFail = 4;
var E_RepairStatus_ProductRepair = 5;
var E_RepairStatus_ProductDelivery = 6;
var E_RepairStatus_RepairFinish = 7;
var E_RepairStatus_CustomerConfirm = 8;


var E_PromotionTarget_Order = 1;
var E_PromotionTarget_OrderItem = 2;

var E_PromotionConditionMode_OverQuantity = 1;
var E_PromotionConditionMode_OverAmount = 2;

var E_PromotionImplementOrderMode_CutAmount = 1;
var E_PromotionImplementOrderMode_Agio = 2;
var E_PromotionImplementOrderMode_MoreThenAgio = 3;
var E_PromotionImplementOrderMode_Largess = 4;

var E_PromotionImplementOrderItemMode_Agio = 1;
var E_PromotionImplementOrderItemMode_CutPrice = 2;
var E_PromotionImplementOrderItemMode_BargainPrice = 3;
var E_PromotionImplementOrderItemMode_MoreThenAgio = 4;
var E_PromotionImplementOrderItemMode_MoreThenCutPrice = 5;
var E_PromotionImplementOrderItemMode_MoreThenBargainPrice = 6;
var E_PromotionImplementOrderItemMode_SomeThenFree = 7;
var E_PromotionImplementOrderItemMode_Largess = 8;


var E_PaymentDeliverType_CashOnDelivery = 1;
var E_PaymentDeliverType_OnlinePayment = 2;