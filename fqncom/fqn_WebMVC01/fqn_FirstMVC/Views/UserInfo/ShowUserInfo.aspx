<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="fqn_FirstMVC" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>ShowUserInfo</title>
</head>
<body>
    <div>
        <% var userInfo = ViewData["UserInfo"] as Users; %>

        <span><%=userInfo.loginId %></span>
        <span><%=userInfo.loginPwd %></span>
    </div>
</body>
</html>
