<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="fqn_FirstMVC" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div>
        <% var userInfoList = ViewData["UserInfo"] as List<Users>; %>

        <% if (userInfoList.Count <= 0)
           {
               return;
           }
           else
           {
               
        %>
        <table>
            <tr>
                <th>用户编号</th>
                <th>用户名</th>
                <th>密码</th>
                <th>注册时间</th>
                <th>详情</th>
                <th>删除</th>
            </tr>
            <% foreach (var user in userInfoList)
               {
            %>
            <tr>
                <td><%=user.autoId %></td>
                <td><%=user.loginId %></td>
                <td><%=user.loginPwd %></td>
                <td><%=user.LastLoginTime %></td>
                <td><a href="/UserInfo/ShowUserInfo/<%=user.autoId %>">详细</a></td>
                <td><a href ="/UserInfo/DeleteUserInfo/<%=user.autoId %>">删除</a></td>
            </tr>

            <%
               } %>
        </table>
        <%} %>
    </div>
</body>
</html>
