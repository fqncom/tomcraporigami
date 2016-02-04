using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using fqn_WebMVC.Bll;
using fqn_WebMVC.FactoryBll;
using fqn_WebMVC.IBll;
using fqn_WebMVC.Model;

namespace fqn_WebMVC.WebApp
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Object> list = new List<Object>();
            IBooksBll booksBll = AbstractFactory.CreateBooksInstance();
            Books books = booksBll.Select("");
            //list.Add(books);

            IUsersBll usersBll = AbstractFactory.CreateUsersInstance();
            Users user = usersBll.Select("");
            list.Add(user);

            this.GridView1.DataSource = list;
            this.GridView1.DataBind();

        }
    }
}