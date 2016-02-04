<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<S_Province>" %>

<%@ Import Namespace="fqn.MVC_EF.IDal" %>

<!doctype html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Detail</title>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('btnUpdate').onclick = function () {
                document.getElementById('divShowInfo').style.display = 'none';
                document.getElementById('divShowUpdate').style.display = 'block';
            };
        }
    </script>
</head>
<body>
    <div>
        <%S_Province province = ViewData.Model; %>
            <div id="divShowInfo">
            编号：<span><%=province.ProvinceID %></span>
            省名：<span><%=province.ProvinceName %></span>
                  <input id="btnUpdate" type="button" name="name" value="修改" />
            </div>
            <form action="/Default/Detail" method="post">
                <div id="divShowUpdate" style="display: none;">
                    <input type="hidden" name="ProvinceID" value="<%=province.ProvinceID %>" />
                    <input type="text" name="ProvinceName" value="<%=province.ProvinceName %>" />
                    <input type="submit" name="name" value="提交修改" />
                </div>
            </form>
    </div>
</body>
</html>
