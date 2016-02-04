<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChangeMyPassword.aspx.cs" Inherits="MyBookShop.ChangeMyPassword" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form action="/" method="post">
        <input type="hidden" name="LoginId" value="<%=this.LoginId %>" />
        新的密码：<input type="text" name="newPassword" value="" />
        确认密码：<input type="text" name="newPasswordAgain" value="" />
        <input type="submit" name="name" value="确认修改" />
    </form>
</asp:Content>
