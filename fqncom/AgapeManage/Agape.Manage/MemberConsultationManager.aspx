<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberConsultationManager.aspx.cs"
    Inherits="MemberConsultationManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Common.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.thickbox.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="css/AdminCommon.css" />
    <link rel="stylesheet" type="text/css" href="css/MemberConsultationManager.css" />

    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/jquery.pager.js" type="text/javascript"></script>

    <script src="js/jquery.thickbox.js" type="text/javascript"></script>

    <script src="js/jquery.treeview.js" type="text/javascript"></script>

    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/tiny_mce/tiny_mce.js"></script>

    <script src="js/Global.js" type="text/javascript"></script>

    <script src="js/MemberConsultationManager.js" type="text/javascript"></script>

</head>
<body>
    <div class="admin-header">
        <span>管理中心</span>&nbsp;&gt;&nbsp;<span>会员管理</span>&nbsp;&gt;&nbsp;<span>咨询管理</span></div>
    <div id="divMemberConsultationList">
        <div class="toolbar">
            <ul>
                <li><span>会员名</span><input type="text" id="txtQueryMemberName" style="width: 150px;" /></li>
                <li><span>会员姓名</span><input type="text" id="txtQueryRealName" style="width: 150px;" /></li>
                <li><span>商品编号</span><input type="text" id="txtQueryProductNo" style="width: 150px;" /></li>
                <li><span>商品名称</span><input type="text" id="txtQueryProductName" style="width: 150px;" /></li>
                <li>
                    <input type="button" value="查询会员咨询" onclick="OnQueryMemberConsultationList();" style="width: 90px;
                        margin-left: 20px;" /></li>
            </ul>
        </div>
        <table id="tbMemberConsultationList" class="list" cellpadding="0" cellspacing="0">
            <tr>
                <th width="10%">
                    会员名
                </th>
                <th width="10%">
                    会员姓名
                </th>
                <th width="10%">
                    商品编号
                </th>
                <th width="10%">
                    商品名称
                </th>
                <th width="10%">
                    咨询内容
                </th>
                <th width="10%">
                    咨询时间
                </th>
                <th width="10%">
                    回答内容
                </th>
                <th width="10%">
                    回答时间
                </th>
                <th width="10%">
                    状态
                </th>
                <th width="10%">
                    操作
                </th>
            </tr>
        </table>
    </div>
    <div id="divMemberConsultationEditor" style="display: none;">
        <div class="toolbar">
            <ul>
                <li>
                    <input type="button" value="返回" onclick="BackToMemberConsultationList();" style="width: 70px;" /></li>
            </ul>
        </div>
        <input type="hidden" id="hdnMemberConsultationID" value="0" />
        <div id="divMemberConsultation">
            <div>
                <ul>
                    <li><span class="title">商品编号</span><span class="content ProductNo"></span></li>
                    <li><span class="title">商品名称</span><span class="content ProductName"></span></li>
                    <li><span class="title">会员名</span><span class="content MemberName"></span></li>
                    <li><span class="title">会员姓名</span><span class="content RealName"></span></li>
                    <li><span class="title">咨询内容</span><span class="content Question"></span></li>
                    <li><span class="title">咨询时间</span><span class="content QuestionTime"></span></li>
                    <li style="height: auto;"><span class="title">商品名称</span><textarea id="txtAnswer"
                        cols="100" rows="5"></textarea></li>
                    <li><span class="title"></span>
                        <input type="button" id="btnAnswerMemberConsultation" onclick="OnAnswerMemberConsultation();"
                            value="答复会员咨询" style="width: 100px;" /></li>
                </ul>
            </div>
        </div>
    </div>
</body>
</html>
