<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MyBookShop.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script src="js/jquery-1.4.2.js"></script>
    <style>
        #btnRegister {
            background-image: url("../Images/注册按钮.png");
            width: 229px;
            height: 78px;
        }

        .labEmptyError {
            display: none;
            font-family: "宋体";
            font-size: 12px;
            color: red;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $txtLoginId = $('#txtLoginId');
            $txtName = $('#txtName');
            $txtLoginPwd = $('#txtLoginPwd');
            $txtLoginPwd2 = $('#txtLoginPwd2');
            $txtMail = $('#txtMail');
            $txtAddress = $('#txtAddress');
            $txtPhone = $('#txtPhone');
            $txtVCode = $('#txtVCode');

            //页面初始化，加载验证码
            GenerateVCode();

            //用户名失去焦点，判断用户名是否存在
            $txtLoginId.blur(function () {
                if ($txtLoginId.val() == '') {
                    $txtLoginId.siblings('.labEmptyError').show();
                    return;
                }
                $txtLoginId.siblings('.labEmptyError').hide();
                $.post(
                    'Register.aspx',
                    {
                        TransCode: 'CheckIsExistName',
                        LoginId: $(this).val()
                    },
                    function (data) {
                        if (data == 'success') {//===success存在
                            $txtLoginId.siblings('.labEmptyError').first().html('该用户名已被注册！').show();
                        } else {
                            $txtLoginId.siblings('.labEmptyError').first().html('恭喜！该用户名还未被注册').show();
                        }
                    }
                );
            });

            //判断两次密码是否一致
            $txtLoginPwd2.blur(function () {
                if ($(this).val() != $txtLoginPwd.val()) {
                    $(this).siblings('.labEmptyError').html('两次输入的密码不一致').show();
                } else {
                    $(this).siblings('.labEmptyError').html('√').show();
                }
            });

            //点击图片更换验证码
            $('#imgVCode').click(GenerateVCode);

            //点击提交进行空值判断,然后验证验证码
            $('#btnRegister').click(function () {
                if (CheckEmpty() > 0) { //空值检测
                    alert('页面存在空值，请检查输入');
                    GenerateVCode();
                    return;
                }
                CheckVCode(); //验证验证码,验证码正确，插入数据

            });
        });

        //插入数据，跳转页面
        function AddNewUserInfo() {
            $.post(
                'Register.aspx',
                {
                    TransCode: 'AddNewUserInfo',
                    Address: $txtAddress.val(),
                    LoginId: $txtLoginId.val(),
                    LoginPwd: $txtLoginPwd.val(),
                    Mail: $txtMail.val(),
                    UserName: $txtName.val(),
                    Phone: $txtPhone.val()
                },
                function (data) {
                    if (data == 'success') {
                        location.href = 'ShowMsg.aspx?TransCode=registerSuccess';
                    } else {
                        location.href = 'ShowMsg.aspx?TransCode=registerFailed';
                    }
                }
            );
        }

        //验证验证码
        function CheckVCode() {
            $.post(
                'Register.aspx',
                {
                    TransCode: 'CheckVCode',
                    VCode: $txtVCode.val()
                },
                function (data) {
                    if (data == 'success') {
                        $txtVCode.siblings('.labEmptyError').first().html('验证码正确').show();

                        //插入数据，跳转页面
                        AddNewUserInfo();
                    } else {
                        $txtVCode.siblings('.labEmptyError').first().html('验证码错误').show();
                        GenerateVCode();//生成新的验证码
                    }
                }
            );
        }

        //生成验证码
        function GenerateVCode() {
            $('#imgVCode').attr('src', '../ashx/ValidateCode.ashx?changeId=' + Math.random());
        }

        //空值检测
        function CheckEmpty() {
            var emptyCount = 0;
            if ($txtLoginId.val() == '') {
                $txtLoginId.siblings('.labEmptyError').show();
                emptyCount++;
            }
            if ($txtName.val() == '') {
                $txtName.siblings('.labEmptyError').show();
                emptyCount++;
            }
            if ($txtLoginPwd.val() == '') {
                $txtLoginPwd.siblings('.labEmptyError').show();
                emptyCount++;
            }
            if ($txtLoginPwd2.val() == '') {
                $txtLoginPwd2.siblings('.labEmptyError').show();
                emptyCount++;
            }
            if ($txtMail.val() == '') {
                $txtMail.siblings('.labEmptyError').show();
                emptyCount++;
            }
            if ($txtAddress.val() == '') {
                $txtAddress.siblings('.labEmptyError').show();
                emptyCount++;
            }
            if ($txtPhone.val() == '') {
                $txtPhone.siblings('.labEmptyError').show();
                emptyCount++;
            }
            if ($txtVCode.val() == '') {
                $txtVCode.siblings('.labEmptyError').show();
                emptyCount++;
            }
            return emptyCount;
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="font-size: small">
        <table width="80%" height="22" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 10px">
                    <img src="../Images/az-tan-top-left-round-corner.gif" width="10" height="28" /></td>
                <td bgcolor="#DDDDCC"><span class="STYLE1">注册新用户</span></td>
                <td width="10">
                    <img src="../Images/az-tan-top-right-round-corner.gif" width="10" height="28" /></td>
            </tr>
        </table>


        <table width="80%" height="22" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
                <td>
                    <div align="center">
                        <table height="61" cellpadding="0" cellspacing="0" style="height: 332px">
                            <tr>
                                <td height="33" colspan="6">
                                    <p class="STYLE2" style="text-align: center">注册新帐户方便又容易</p>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" align="center" valign="top" style="height: 26px">用户名</td>
                                <td valign="top" width="37%" align="left" style="height: 26px">
                                    <input id="txtLoginId" type="text" name="loginId" value="" /><br />
                                    <label class="labEmptyError">用户名不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" height="26" align="center" valign="top">真实姓名：</td>
                                <td valign="top" width="37%" align="left">
                                    <input id="txtName" type="text" name="userName" value="" /><br />
                                    <label class="labEmptyError">真实姓名不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" height="26" align="center" valign="top">密码：</td>
                                <td valign="top" width="37%" align="left">
                                    <input id="txtLoginPwd" type="password" name="loginPwd" value="" /><br />
                                    <label class="labEmptyError">密码不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" height="26" align="center" valign="top">确认密码：</td>
                                <td valign="top" width="37%" align="left">
                                    <input id="txtLoginPwd2" type="password" name="loginPwd2" value="" /><br />
                                    <label class="labEmptyError">确认密码不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" height="26" align="center" valign="top">Email：</td>
                                <td valign="top" width="37%" align="left">
                                    <input id="txtMail" type="text" name="mail" value="" /><br />
                                    <label class="labEmptyError">Email不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" height="26" align="center" valign="top">地址：</td>
                                <td valign="top" width="37%" align="left">
                                    <input id="txtAddress" type="text" name="address" value="" /><br />
                                    <label class="labEmptyError">地址不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" height="26" align="center" valign="top">手机：</td>
                                <td valign="top" width="37%" align="left">
                                    <input id="txtPhone" type="text" name="phone" value="" /><br />
                                    <label class="labEmptyError">手机不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td width="24%" height="26" align="center" valign="top">验证码：</td>
                                <td valign="top" width="37%" align="left">
                                    <input id="txtVCode" type="text" name="vCode" value="" />
                                    <img id="imgVCode" src="" alt="" title="点击更换" /><br />
                                    <label class="labEmptyError">验证码不能为空</label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <input id="btnRegister" type="button" name="name" value="" /></td>
                            </tr>
                        </table>
                        <div class="STYLE5">---------------------------------------------------------</div>
                    </div>
                </td>
                <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
            </tr>
        </table>

        <table width="80%" height="3" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="3" bgcolor="#DDDDCC">
                    <img src="../Images/touming.gif" width="27" height="9" /></td>
            </tr>
        </table>
    </div>

</asp:Content>
