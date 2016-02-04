var m_RowCount;
var m_PageCount;
var m_CurrentPageIndex;
var m_PageSize;

$(document).ready(function() {

    m_CurrentPageIndex = 1;
    m_PageSize = 15;

    QueryArticleCategoryList();

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


/****** 文章分类 ******/
function QueryArticleCategoryList() {

    $.post("InformationService.aspx",
    { "TransCode": "QueryArticleCategoryList"
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var jqSelect = $("#sltArticleCategory");

        $(data).find("ArticleCategory").each(function() {
            var ArticleCategoryID = $("ArticleCategoryID", this).text();
            var ArticleCategoryName = $("ArticleCategoryName", this).text();
            jqSelect.append("<option value='" + ArticleCategoryID + "'>" + ArticleCategoryName + "</option>");
        });
    });
}


/****** 文章 ******/
function OnQueryArticleList() {

    QueryArticleCount();
}

function QueryArticleCount() {

    $.post("InformationService.aspx",
    { "TransCode": "QueryArticleCount"
    }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }
        m_RowCount = new Number($(data).find("RowCount").text());

        m_PageCount = parseInt(m_RowCount / m_PageSize);
        if (m_RowCount % m_PageSize > 0) m_PageCount++;

        QueryArticleList(m_CurrentPageIndex);
    });
}

PageClick = function(pageclickednumber) {

    QueryArticleList(pageclickednumber);
}

function QueryArticleList(pageclickednumber) {

    $.post("InformationService.aspx", { "TransCode": "QueryArticleList", "ArticleCategoryID": 0, "PageIndex": pageclickednumber, "PageSize": m_PageSize }, function(data) {

        var htmlData = "";
        $("#tbArticleList tr:gt(0)").remove();

        $(data).find("Article").each(function() {

            var $Article = $(this);
            var ArticleID = $Article.attr("ArticleID");
            var ArticleTitle = $Article.attr("ArticleTitle");
            var ArticleCategoryName = $Article.attr("ArticleCategoryName");
            var IssueDateTime = $Article.attr("IssueDateTime");
            var IssueOperatorName = $Article.attr("IssueOperatorName");
            var HitCount = $Article.attr("HitCount");
            var ReviewCount = $Article.attr("ReviewCount");
            var ScoreLevel = $Article.attr("ScoreLevel");

            htmlData += "<tr>" +
                            "<td><input type='checkbox' value='" + ArticleID + "' /></td>" +
                            "<td>" + ArticleCategoryName + "</td>" +
                            "<td>" + ArticleTitle + "</td>" +
                            "<td>" + IssueDateTime + "</td>" +
                            "<td>" + IssueOperatorName + "</td>" +
                            "<td>" + HitCount + "</td>" +
                            "<td>" + ReviewCount + "</td>" +
                            "<td>" + ScoreLevel + "</td>" +
                            "<td><a href='#none' onclick='OnEditArticle(" + ArticleID + ");return true;'>修改</a>|<a href='#none' onclick='OnDeleteArticle(" + ArticleID + ");return true;'>删除</a></td>" +
                        "</tr>";

        });
        $(htmlData).insertAfter("#tbArticleList tr:eq(0)");

        m_CurrentPageIndex = pageclickednumber;
        $("#pager").pager({ pagenumber: m_CurrentPageIndex, pagecount: m_PageCount, buttonClickCallback: PageClick });

        alert("查询成功！");
    });
}

function OnNewArticle() {

    $("#hdnArticleID").val(0);
    $("#txtArticleTitle").val("");
    $("#sltArticleCategory").val("");
    $("#txtArticleContent").val("");
    tinyMCE.get("txtArticleContent").setContent("");

    SwitchDiv("ArticleEditor");
}

function OnEditArticle(ArticleID) {

    $.post("InformationService.aspx", { "TransCode": "QueryArticle", "ArticleID": ArticleID }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        var ArticleID = $(data).find("Article ArticleID").text();
        var ArticleTitle = $(data).find("Article ArticleTitle").text();
        var ArticleCategoryID = $(data).find("Article ArticleCategoryID").text();
        var ArticleContent = $(data).find("Article ArticleContent").text();
        ArticleContent = unescape(ArticleContent);

        $("#hdnArticleID").val(ArticleID);
        $("#txtArticleTitle").val(ArticleTitle);
        $("#sltArticleCategory").val(ArticleCategoryID);
        tinyMCE.get("txtArticleContent").setContent(ArticleContent);

        SwitchDiv("ArticleEditor");
    });
}

function OnSaveArticle() {

    if (!window.confirm("确认要保存吗？")) {
        return false;
    }

    var ArticleID = $("#hdnArticleID").val();
    var ArticleTitle = $("#txtArticleTitle").val();
    var ArticleCategoryID = $("#sltArticleCategory").val();
    var ArticleContent = tinyMCE.get("txtArticleContent").getContent();
    ArticleContent = escape(ArticleContent);

    $.post("InformationService.aspx",
        { "TransCode": "SaveArticle",
            "ArticleID": ArticleID,
            "ArticleTitle": ArticleTitle,
            "ArticleCategoryID": ArticleCategoryID,
            "ArticleContent": ArticleContent
        },
        function(data) {

            var ReturnCode = $(data).find("ReturnCode").text();
            var ReturnMessage = $(data).find("ReturnMessage").text();
            if (ReturnCode != "0000") {

                alert(ReturnMessage);
                return false;
            }

            ArticleID = $(data).find("ArticleID").text();
            $("#hdnArticleID").val(ArticleID);

            alert("保存成功！");
        });
}

function OnDeleteArticle(ArticleID) {

    if (!window.confirm("确认要删除吗？")) {
        return false;
    }

    $.post("InformationService.aspx", { "TransCode": "DeleteArticle", "ArticleID": ArticleID }, function(data) {

        var ReturnCode = $(data).find("ReturnCode").text();
        var ReturnMessage = $(data).find("ReturnMessage").text();
        if (ReturnCode != "0000") {

            alert(ReturnMessage);
            return false;
        }

        QueryArticleList();
    });
}

function OnBackToArticleList() {

    SwitchDiv("ArticleList");
}

function SwitchDiv(DivName) {

    if (DivName == "ArticleList") {

        $("#divArticleList").show();
        $("#divArticleEditor").hide();

    }
    else if (DivName == "ArticleEditor") {

        $("#divArticleEditor").show();
        $("#divArticleList").hide();
    }
}


/****** 功能 ******/
