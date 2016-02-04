<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MyBookShop.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">

    <style>
        .commentBox {
            padding: 3px;
            background: #ffe;
            border: 1px solid #999;
            margin-bottom: 12px;
            word-wrap: break-word;
            zoom: 1;
            position: relative;
            z-index: 1;
            background-image: url(http://static.ule.com/mstore/user_800107446/store_7915/images/20150105/98992fe905a3bcba.jpg);
        }
    </style>
    <script src="/js/jquery-1.7.1.js"></script>
    <script src="/js/jquery-ui-1.8.2.custom.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Repeater ID="Repeater1" runat="server">

        <ItemTemplate>
            <tr>
                <td>
                    <table>
                        <tbody>
                            <tr>
                                <td rowspan="2"><a href="<%#MyBookShop.Common.CommonTools.GetDirectoryPath(Eval("PublishDate")) %><%#Eval("ISBN") %>.html">
                                    <img
                                        id="ctl00_cphContent_dl_Books_ctl01_imgBook"
                                        style="CURSOR: pointer" height="121"
                                        alt="" hspace="4"
                                        src="<%#Eval("ISBN","Images/BookCovers/{0}.jpg") %>" width="95"></a>
                                </td>
                                <td style="FONT-SIZE: small; COLOR: red" width="650"><a
                                    class="booktitle" id="link_prd_name"
                                    href="<%#MyBookShop.Common.CommonTools.GetDirectoryPath(Eval("PublishDate")) %><%#Eval("ISBN") %>.html" target="_blank"
                                    name="link_prd_name"><%#Eval("Title") %></a>
                                </td>
                            </tr>
                            <tr>
                                <td align="left"><span
                                    style="FONT-SIZE: 12px; LINE-HEIGHT: 20px"><%#Eval("Author") %></span><br>
                                    <br>
                                    <span
                                        style="FONT-SIZE: 12px; LINE-HEIGHT: 20px"><%#CutString(Eval("ContentDescription").ToString(),150) + "..."%>

                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2"><span
                                    style="FONT-WEIGHT: bold; FONT-SIZE: 13px; LINE-HEIGHT: 20px"><%#Eval("UnitPrice","{0:0.00}") %></span> </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </ItemTemplate>
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
    </asp:Repeater>

    <form runat="server">
        <div class="contentstyle" style="MARGIN: 20px 0px; TEXT-ALIGN: left">

            <input type="hidden" name="hidePageIndex" value="<%=CurrentPageIndex %>" />
            <%--  简单上下页           第 <%=CurrentPageIndex %> 页 共 <%=CurrentPageCount %> 页
        <input name="PrePage" class="btnChangePage" type="submit" value="上一页">

            <input name="NextPage" class="btnChangePage" type="submit" value="下一页">--%>
            <span><%=PageBarString %></span>
        </div>
        
    </form>
</asp:Content>
