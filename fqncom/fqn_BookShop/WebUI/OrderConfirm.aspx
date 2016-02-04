<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="OrderConfirm.aspx.cs" Inherits="MyBookShop.OrderConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">


    <script type="text/javascript">
        $(document).ready(
            function () {

                $(".m_r").css("width", "710px");
                $(".m_r").css("float", "none");
            }
        );

        function OnSubmit() {
            Validate();
            if (isValid == false) {
                $("#spanMsg").text("请检查收货信息是否填写完整!");
                return false;
            }
            $("#hdType").val("1");
        }

        var isValid = true;
        function Validate() {
            isValid = true;
            $("#userinfo input[type=text]").each(
               function () {

                   if ($(this).val().length <= 0) {
                       isValid = false;
                       $($(this).next()).attr("style", "display:inline");
                   }
               }
            );
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="border: solid 4px #aacded; width: 710px">
        <div style="background: #AACDED; text-align: left">
            <h2>填写核对订单信息</h2>
        </div>
        <div align="left">
            <h2>收货人信息:</h2>
            <div>
                <table style="width: 100%" id="userinfo">
                    <tbody>
                        <tr>
                            <td style="WIDTH: 100px" align="right">姓名：</td>
                            <td style="text-align: left">
                                <input type="text" name="txtName" size="50" value="" /><img src="images/cha.ico" style="display: none" width="15" height="15" /></td>
                        </tr>
                        <tr>
                            <td align="right">地址：</td>
                            <td style="text-align: left">
                                <input type="text" name="txtAddress" size="50" value="" /><img src="images/cha.ico" style="display: none" width="15" height="15" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">电话：</td>
                            <td style="text-align: left">
                                <input type="text" name="txtPhone" size="50" value="" /><img src="images/cha.ico" style="display: none" width="15" height="15" /></td>
                        </tr>
                        <tr>
                            <td align="right">邮编：</td>
                            <td style="text-align: left">
                                <input type="text" name="txtPostCode" size="50" value="" /><img src="images/cha.ico" style="display: none" width="15" height="15" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <hr style="border-style: dashed; width: 100%; border-color: #ccc" />
            <div align="left">
                <h2>请选择支付方式:</h2>
                <div>
                    <!--支付方式-->
                    <table style="WIDTH: 100%" datasrc="">
                        <tbody>
                            <tr valign="middle">
                                <td style="TEXT-ALIGN: right; WIDTH: 80px">支付方式：</td>
                                <td style="vertical-align: middle; text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt="" src="Images/y_zfb.gif" /><input name="zfPay" type="radio" value="zfb" checked="checked" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                                        alt="" src="Images/unionpay.gif" /><input name="zfPay"
                                            type="radio" value="wyzx" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <!--支付方式-->
            </div>
            <hr style="border-style: dashed; width: 100%; border-color: #ccc" />
            <div align="left">
                <!--订单确定-->
                <h2>商品清单:</h2>
                <div>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>

                            <table cellspacing="0" cellpadding="1" width="98%" border="1">
                                <tbody>
                                    <tr class="align_Center Thead">
                                        <td width="10%">图书编号</td>
                                        <td>商品名称</td>
                                        <td width="10%">单价</td>
                                        <td width="8%">数量</td>
                                    </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="align_Center">
                                <td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 5px"></td>
                                <td class="align_Left"><a onmouseover="" onmouseout="" onclick="" href='' target="_blank"></a>
                                </td>
                                <td><span class="price">￥</span></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <!--订单确定-->
            <div align="right" style="margin-right: 20px">
                <!--总价格显示-->
                <h2>你需要支付的总价格为:<span class="price">￥</span></h2>
                <br />
                <input id="btnGoPay" type="submit" onclick="return OnSubmit()" value="" style="background: url('/Images/gopay.jpg') no-repeat center; width: 95px; height: 32px" name="btnGoPay" />
                <span id="spanMsg" style="color: Red"></span>
            </div>
        </div>
    </div>
    <input name="hdType" type="hidden" value="0" id="hdType" />
</asp:Content>


