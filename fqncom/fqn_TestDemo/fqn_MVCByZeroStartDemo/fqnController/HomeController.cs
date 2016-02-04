using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data.SqlClient;
using System.Web.Routing;

namespace fqn_MVCByZeroStartDemo.fqnController
{
    public class HomeController : Controller
    {
        #region IController 成员

        //public void Execute(System.Web.Routing.RequestContext requestContext)
        //{
        //    requestContext.HttpContext.Response.Write("this is awesome");
        //}

        #endregion


        public ActionResult BegForAction()
        {
            int rowStart = 5;
            int rowEnd = 5;
            using (SqlConnection conn = new SqlConnection("server =(local);database=book_shop;uid=sa;pwd=123;"))
            {
                var list = conn.Query<Books>("select * from Books").Skip(rowStart).Take(rowEnd);

                ViewBag.BooksList = list;
            }
            return View();
        }
    }
}