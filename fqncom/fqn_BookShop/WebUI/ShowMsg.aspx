<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="ShowMsg.aspx.cs" Inherits="MyBookShop.ShowMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script src="js/jquery-1.4.2.js"></script>
    <style type="text/css">
        .style1 {
            text-align: center;
        }
    </style>

    <script>
        $(function () {
            var timeCount = 15;
            $('#spTimeCount').html(timeCount);

            var timeId = setInterval(function () {
                timeCount--;
                $('#spTimeCount').html(timeCount);
                if (timeCount <= 0) {
                    clearInterval(timeId);
                    location.href = $('#aGotoUrl').attr('href');
                }
            }, 1000);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="form1" runat="server">
        <div>
            <table width="490" height="325" border="0" align="center" cellpadding="0" cellspacing="0" background="Images/showinfo.png">
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="text-align: center;">
                            <tr>
                                <td width="50">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td width="40">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="50">&nbsp;</td>
                                <td style="text-align: center"><%=this.Msg %></td>
                                <td width="40">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="50">&nbsp;</td>
                                <td><a id="aGotoUrl" href="<%=this.UrlAddress %>"><span id="spTimeCount"></span>秒后即将跳转到<%=this.UrlName %>，手动跳转请点击</a></td>
                                <td width="40">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="50" class="style1">&nbsp;</td>
                                <td style="text-align: center"></td>
                                <td width="40">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>


</asp:Content>
