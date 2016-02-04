
var height = window.screen.height - 250;   
var width = window.screen.width;   
 var leftW = 300;   
 if(width>1200){   
    leftW = 500;   
 }else if(width>1000){   
    leftW = 350;   
 }else {   
    leftW = 100;   
 }

 var _html =
     "<div id='loading' class='LoadingBox' style='height:" + height + "px;'>" +
     "  <div class='LoadingTitle' style='left:" + leftW + "px;'>" +
     "      正在加载，请等待..." +
     "  </div>" +
     "</div>";

 var _html2 =
    "div id='divLoadingBackground' class='LoadingBackground' style='display: none;'></div> " +
    "<div id='divLoadingProgressBar' class='LoadingProgressBar' style='display: none;'>数据加载中，请稍等...</div>";

 document.write(_html2);
    
 window.onload = function(){   
    //var _mask = document.getElementById('loading');
    //_mask.parentNode.removeChild(_mask);
 }

 function ShowLoadingBox() {
     var ajaxbg = $("#divLoadingBackground,#divLoadingProgressBar");
     ajaxbg.show();
 }

 function HideLoadingBox() {
     var ajaxbg = $("#divLoadingBackground,#divLoadingProgressBar");
     ajaxbg.hide();
 }
