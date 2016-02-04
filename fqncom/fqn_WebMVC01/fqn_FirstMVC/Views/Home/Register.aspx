<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Register</title>
</head>
<body>
    <div>
        <form action="/Home/DealWithRegister" method="post">
            用户名：<input type="text" name="loginId" value="" /><br />
            密码：<input type="password" name="loginPwd" value="" /><br />
            <input type="submit" name="name" value="注册" />
        </form>
    </div>
</body>
</html>
