<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FindMyPassword.aspx.cs" Inherits="MyBookShop.FindMyPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script src="js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btnGetPwd').click(FindMyPassword);
        });

        function FindMyPassword() {
            $.post(
                'FindMyPassword.aspx',
                {
                    TransCode: 'FindMyPassword',
                    LoginId: $('#txtLoginId').val(),
                    LoginMail: $('#txtLoginMail').val()
                },
                function (data) {
                    if (data == 'success') {
                        window.location.href = 'ShowMsg.aspx?TransCode=findMyPassword';
                    } else {
                        $('#txtMsg').val('输入有误！').show();

                    }
                }
            );
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div>
        登录用户名：<input id="txtLoginId" type="text" name="loginId" value="" />
        登入邮箱：<input id="txtLoginMail" type="text" name="loginMail" value="" />
        <input id="btnGetPwd" type="button" name="name" value="找回密码" />
        <input id="txtMsg" type="hidden" name="name" value="" />
    </div>

</asp:Content>
