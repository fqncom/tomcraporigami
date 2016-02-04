<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyBookShop.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script src="js/jquery-1.4.2.js"></script>

    <style>
        #btnLogin {
            background-image: url('../Images/登录按钮.png');
            width: 229px;
            height: 78px;
        }
    </style>
    <script>
        $(function () {
            $txtLoginId = $('#txtLoginId');
            $txtLoginPwd = $('#txtLoginPwd');

            $txtLoginId.focus(function () {
                ClearInput($(this));
            }).blur(function () {
                CheckEmpty($(this));
            });

            $txtLoginPwd.focus(function () {
                ClearInput($(this));
            }).blur(function () {
                CheckEmpty($(this));
            });

            $('#btnLogin').click(function () {
                CheckEmpty($txtLoginId);
                CheckEmpty($txtLoginPwd);
                if ($txtLoginId.val() == '不能为空' || $txtLoginPwd.val() == '不能为空') {
                    alert('不能为空');
                    return;
                }
                $.post(
                    'Login.aspx',
                    {
                        TransCode: 'UserLogin',
                        LoginId: $txtLoginId.val(),
                        LoginPwd: $txtLoginPwd.val(),
                        RememberMe: $('#chkRememberMe').attr('checked')
                    },
                        function (data) {
                            if (data == 'success') {
                                var returnUrl = $('#txtReturnUrl').val();
                                if (returnUrl != '') {
                                    location.href = returnUrl;
                                } else {
                                    location.href = 'Index.aspx';//在后台进行状态保持操作。在index页面直接拿状态保持
                                }
                            } else {
                                alert('登入失败');
                            }
                        }
                );
            });

        });

        //清空文本框
        function ClearInput($Obj) {
            if ($Obj.val() == '不能为空') {
                $Obj.css({ 'color': '', 'font-style': '' });
                $Obj.val('');
            }
        }

        //空值检测
        function CheckEmpty($Obj) {
            if ($Obj.val() == '') {
                $Obj.css({ 'color': 'red', 'font-style': 'italic' });
                $Obj.val('不能为空');
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="txtReturnUrl" type="hidden" name="ReturnUrl" value="<%=ReturnUrl %>" />
    <table width="60%" height="22" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="tblfirst">
        <tr>
            <td width="10">
                <img src="/Images/az-tan-top-left-round-corner.gif" width="10" height="28" /></td>
            <td bgcolor="#DDDDCC"><span style="font-family: '黑体'; font-size: 16px; color: #660000;">登录网上书店</span></td>
            <td width="10">
                <img src="/Images/az-tan-top-right-round-corner.gif" width="10" height="28" /></td>
        </tr>
    </table>
    <table width="60%" height="22" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="tblsecend">
        <tr>
            <td bgcolor="#DDDDCC" style="width: 2px">&nbsp;</td>
            <td>
                <div align="center">
                    <table height="61" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="33" colspan="6">
                                <p style="font-size: 14px; font-weight: bold; color: #FF9900; text-align: center;">已注册用户请从这里登录</p>
                            </td>
                        </tr>
                        <tr>
                            <td width="24%" height="26" rowspan="2" align="right" valign="top"><strong>用户名：</strong></td>
                            <td valign="top" width="37%">
                                <input id="txtLoginId" type="text" name="loginId" value="<%=this.LoginIdCookie %>" /></td>
                        </tr>
                    </table>
                    <table height="61" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="1" colspan="2"></td>
                        </tr>
                        <tr>
                            <td width="24%" height="26" rowspan="3" align="right" valign="top"><strong>密　码：</strong></td>
                            <td valign="top" width="37%">
                                <input id="txtLoginPwd" type="password" name="loginPwd" value="" /></td>
                        </tr>

                        <tr>
                            <td width="37%">
                                <input id="chkRememberMe" type="checkbox" name="rememberMe" checked="checked" />&nbsp;记住我</td>
                        </tr>

                    </table>
                    <div>
                    </div>
                    <div>
                        <div align="center">
                            <input id="btnLogin" type="submit" name="name" value="" />
                        </div>
                    </div>
                </div>
            </td>
            <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
        </tr>
    </table>
    <table width="60%" height="3" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="tblthird">
        <tr align="center">
            <td height="3" bgcolor="#DDDDCC">&nbsp;
            </td>
        </tr>
    </table>

</asp:Content>
