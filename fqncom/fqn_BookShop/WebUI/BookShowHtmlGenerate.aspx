<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="BookShowHtmlGenerate.aspx.cs" Inherits="MyBookShop.BookShowHtmlGenerate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">

    <script src="js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btnGenerateHtml').click(function () {
                $.post(
                    'BookShowHtmlGenerate.aspx',
                    {
                        TransCode: 'GenerateHtml'
                    },
                    function (data) {
                        if (data == 'success') {
                            alert('操作成功');
                        } else {
                            alert('操作失败');
                        }
                    }
                );
            });

            $('#btnSubmitCode').click(function () {
                var $txtAreaSensitiveCode = $('#txtAreaSensitiveCode');
                if ($txtAreaSensitiveCode.val() == null) {
                    alert('请不要填写空值');
                } else {
                    $.post(
                        'BookShowHtmlGenerate.aspx',
                        {
                            TransCode: 'AddSensitiveCode',
                            SensitiveCode: $txtAreaSensitiveCode.val()
                        },
                        function (data) {
                            if (data == 'success') {
                                alert('操作成功');
                            } else {
                                alert('操作失败');
                            }
                        }
                    );
                }

            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input id="btnGenerateHtml" type="button" name="name" value="点击生成前十个静态Html页面" />

    <div>
        <textarea id="txtAreaSensitiveCode" rows="20" cols="50"></textarea>
        <input id="btnSubmitCode" type="button" name="name" value="确认提交敏感词" />
    </div>
</asp:Content>
