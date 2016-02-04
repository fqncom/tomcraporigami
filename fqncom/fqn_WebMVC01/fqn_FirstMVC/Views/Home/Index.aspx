<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div>
        This My First MVC Page;
        <!--找的是方法而不是直接找页面-->
        <a href="home/Register">进入注册页面</a>
        <a href="UserInfo/Index">进入用户详细</a>
    </div>
</body>
</html>
