var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

    InitTinyMCE();
});

function InitTinyMCE() {

    tinyMCE.init({
        // General options
        mode: "textareas",
        theme: "advanced",
        plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist",

        // Theme options
        theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak,restoredraft",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,

        // Example content CSS (should be your site CSS)
        content_css: "tiny_mce/css/content.css",
        language: "cn",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "tiny_mce/lists/template_list.js",
        external_link_list_url: "tiny_mce/lists/link_list.js",
        external_image_list_url: "tiny_mce/lists/image_list.js",
        media_external_list_url: "tiny_mce/lists/media_list.js",

        // Style formats
        style_formats: [
			{ title: 'Bold text', inline: 'b' },
			{ title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
			{ title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
			{ title: 'Example 1', inline: 'span', classes: 'example1' },
			{ title: 'Example 2', inline: 'span', classes: 'example2' },
			{ title: 'Table styles' },
			{ title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
		],

        // Replace values for the template plugin
        template_replace_values: {
            username: "Some User",
            staffid: "991234"
        }
    });
}


function OnHelpItemSelected() {

    var HelpCode = $("#sltHelpItem").val();

    QueryHelpItem();
}

function QueryHelpItem() {

    var HelpCode = $("#sltHelpItem").val();
    ShowLoadingBox();

    $.post("BasicService.aspx",
    {
        "TransCode": "QueryHelpItem",
        "HelpCode": HelpCode
    }, function (data) {

        HideLoadingBox();

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        HelpContent = $(data).find("HelpContent").text();
        HelpContent = unescape(HelpContent);
        tinyMCE.get("txtHelpContent").setContent(HelpContent);
    });
}

function OnSaveHelpItem() {

    if (!window.confirm("确认要保存吗？")) {
        return false;
    }

    var HelpCode = $("#sltHelpItem").val();
    var HelpContent = tinyMCE.get("txtHelpContent").getContent();
    HelpContent = escape(HelpContent);

    $.post("BasicService.aspx",
        { "TransCode": "SaveHelpItem",
            "HelpCode": HelpCode,
            "HelpContent": HelpContent
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            alert("保存成功！");
        });
}


/****** 功能 ******/
