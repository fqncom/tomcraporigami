<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="MyBookShop.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">

    <%--<link type="text/css" href="Css/SWFU_default.css" rel="stylesheet" />--%>
   <%-- <link href="Css/jquery-ui-1.8.16.custom.css" rel="stylesheet" />--%>
    <link href="Css/imgareaselect-default.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="js/swfupload.js"></script>
    <script type="text/javascript" src="js/handlers.js"></script>
  <%--  <script type="text/javascript" src="js/ui/jquery-ui-1.8.2.custom.js"></script>--%>
    <script type="text/javascript" src="js/jquery.imgareaselect.min.js"></script>


    <script type="text/javascript">
        var swfu;
        window.onload = function () {
            swfu = new SWFUpload({
                // Backend Settings
                upload_url: "/ashx/UpLoadFile.ashx",
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>",
                    'TransCode': 'UpLoadImage'
                },

                // File Upload Settings
                file_size_limit: "2 MB",//不得大于4M。微软.net框架默认。防止上传大文件的攻击，占用请求
                file_types: "*.jpg;*.png",
                file_types_description: "JPG Images;PNG",
                file_upload_limit: 0,    // Zero means unlimited

                //  Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: MyUpLoadSuccess,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_image_url: "images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonPlaceholder",
                button_width: 160,
                button_height: 22,
                button_text: '<span class="button">上传文件<span class="buttonSmall">(2 MB)</span></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,

                // Flash Settings
                flash_url: "Images/swfupload.swf",	// Relative to this file
                flash9_url: "Images/swfupload_fp9.swf",	// Relative to this file

                custom_settings: {
                    upload_target: "divFileProgressContainer"
                },

                // Debug Settings
                debug: false
            });
        }

        $(function () {
            //$('#divSmallCut').hide();
            //$("#divSmallCut").draggable({ containment: "#divShowBigPic", scroll: false }).resizable({
            //    containment: "#divShowBigPic"
            //});


            $('#btnCutPic').click(function () {
                var pic = $('#selectbanner').attr('src');
                var x, y, width, height;
                $.post(
                 "/ashx/UpLoadFile.ashx",
                 {
                     TransCode: 'GetSmallPic',
                     x: $('#selectbanner').data('x'),
                     y: $('#selectbanner').data('y'),
                     width: $('#selectbanner').data('width'),
                     height: $('#selectbanner').data('height'),
                     pic: pic
                 },
                 function (data) {
                     if (data) {
                         $('#imgSmall').attr('src', data);
                     }
                 }
                );
            });

        });
        function MyUpLoadSuccess(file, serverData) {
            var data = serverData.split(',');
            if (data[0] == "success") {
                //$('#divShowBigPic').css('background-image', 'url(' + data[1] + ')').css('width', data[2]).css('height', data[3]);
                $('#selectbanner').attr('src', data[1]).imgAreaSelect({
                    selectionColor: 'blue', x1: 0, y1: 0, x2: 950,

                    selectionOpacity: 0.2, onSelectEnd: preview
                });
                
                //$('#divSmallCut').show();
                //$('#imgSmall').
            }

        }

        function preview(img, selection) {

            $('#selectbanner').data('x', selection.x1);

            $('#selectbanner').data('y', selection.y1);

            $('#selectbanner').data('width', selection.width);

            $('#selectbanner').data('height', selection.height);
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="divUserImage">

        <form id="form1" runat="server">

            <div id="content">

                <div id="swfu_container" style="margin: 0px 10px;">
                    <div>
                        <span id="spanButtonPlaceholder"></span>
                    </div>
                    <input id="btnCutPic" type="button" name="name" value="裁剪图片" />
                    <div id="divFileProgressContainer" style="height: 75px;">
                        <!--显示大图-->
                        <div id="divShowBigPic">
                            <%-- <div id="divSmallCut" class="draggable ui-widget-content" style="border: 1px solid red; height: 150px; width: 150px"></div>--%>
                            <img id="selectbanner" src="/pic/banner.jpg" />
                        </div>
                        <img id="imgSmall" src="#" alt="截取" />
                    </div>
                </div>
            </div>
        </form>
    </div>

</asp:Content>
