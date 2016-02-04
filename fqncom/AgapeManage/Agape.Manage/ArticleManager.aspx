<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticleManager.aspx.cs"
    Inherits="ArticleManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/ArticleManager.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/ArticleManager.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>咨询管理</span>&nbsp;&gt;&nbsp;<span>文章管理</span></div>
    <div id="divArticleList">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" value="查询" onclick="OnQueryArticleList();" style="width: 70px;" /></li>
                <li>
                    <input type="button" value="新 建" onclick="OnNewArticle();" style="width: 70px;" /></li>
            </ul>
        </div>
        <table id="tbArticleList" cellpadding="0" cellspacing="0" class="list">
            <tr>
                <th width="5%">
                    选择
                </th>
                <th width="10%">
                    文章类型
                </th>
                <th width="30%">
                    文章标题
                </th>
                <th width="15%">
                    发布时间
                </th>
                <th width="10%">
                    发布者
                </th>
                <th width="5%">
                    点击数
                </th>
                <th width="5%">
                    评论数
                </th>
                <th width="10%">
                    评价等级
                </th>
                <th width="10%">
                    操作
                </th>
            </tr>
        </table>
        <div id="pager" class="admin-pager">
        </div>
    </div>
    <div id="divArticleEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" id="btnSaveArticle" value="保存" onclick="OnSaveArticle();" style="width: 80px;" /></li>
                <li>
                    <input type="button" id="btnBackToArticleList" value="返回" onclick="OnBackToArticleList();"
                        style="width: 80px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnArticleID" value="0" />
        <table style="width: 900px;">
            <tr>
                <td style="text-align: right;">
                    文章标题
                </td>
                <td style="text-align: left;" colspan="3">
                    <input type="text" id="txtArticleTitle" style="width: 600px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right;">
                    文章分类
                </td>
                <td style="text-align: left;">
                    <select id="sltArticleCategory" style="width: 100px;">
                    </select>
                </td>
                <td style="width: 100px; text-align: right;">
                </td>
                <td style="text-align: left; width: 300px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;">
                    简介
                </td>
                <td style="text-align: left;" colspan="3">
                    <textarea id="txtArticleContent" rows="50" cols="120" style="width: 800px; height: 800px;"
                        class="tinymce"></textarea>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
