<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<S_Province>>" %>

<%@ Import Namespace="fqn.MVC_EF.IDal" %>
<%@ Import Namespace="fqn.MVC_EF.Common" %>

<!doctype html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
    <link href="../../Content/PageBar.css" rel="stylesheet" />
    <link href="../../Content/tableStyle.css" rel="stylesheet" />
    <style type="text/css">
        div {
            text-align: center;
        }
        table {
             margin: 0 auto;
         }
    </style>

    <script type="text/javascript">
        window.onload = function () {
            var linkDeleteList = document.getElementsByClassName('linkDelete');
            for (var i = 0; i < linkDeleteList.length; i++) {
                linkDeleteList[i].onclick = function () {
                    if (!confirm('确认删除？')) {
                        return false;
                    }
                };
            }
        }
    </script>
</head>
<body>
    <div>
        <%--<%List<S_Province> provinceList = ViewData["ProvinceList"] as List<S_Province>; %>--%>
            <% List<S_Province> provinceList = ViewData.Model; %>

<%if (provinceList.Count > 0)
  {%>
       <table>
            <tr>
                <td>省Id</td>
                <td>省名</td>
                <td>详细</td>
                <td>删除</td>
            </tr>
     <% foreach (S_Province province in provinceList)
        {%>
          <tr>
              <td><%=province.ProvinceID %></td>
                <td><%=province.ProvinceName %></td>
                <td><a href="/Default/Detail/<%=province.ProvinceID %>">详细</a></td>
                <td><a class="linkDelete" href="/Default/Delete/<%=province.ProvinceID %>">删除</a></td>
            </tr>
     <% }
      %>
</table>
 <% }
  else
  {%>
      <span>不存在数据</span>
 <% }%>
     <br/>
       <div>
           <span class="page_nav"><%=CommonHelper.GetPageBar(Convert.ToInt32(ViewData["PageIndex"]),Convert.ToInt32(ViewData["PageCount"])) %></span>
       </div>
    </div>
</body>
</html>
