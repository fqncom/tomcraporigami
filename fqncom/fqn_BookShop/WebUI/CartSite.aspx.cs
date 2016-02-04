using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyBookShop.BLL;
using MyBookShop.Model;

namespace MyBookShop
{
    public partial class Cart : System.Web.UI.Page
    {
        private BLL.CartBll cartBll = new BLL.CartBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            string transCode = Request["TransCode"] ?? "";
            if (transCode == "")
            {
                return;
            }
            string msg = string.Empty;
            int bookId = Convert.ToInt32(Request["BookId"] ?? "-1");
            int cartId = Convert.ToInt32(Request["CartId"] ?? "-1");
            int count = Convert.ToInt32(Request["Count"] ?? "-1");

            switch (transCode)
            {
                case "LoadAllCartInfo"://加载购物车中所有的信息
                    Model.Cart cart = new Model.Cart();
                    List<Model.Cart> listCart = new List<Model.Cart>();

                    LoadAllCartInfo(listCart);

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Response.Write(js.Serialize(listCart));
                    Response.End();
                    break;
                case "DeleteDataBaseCartInfo"://删除数据库中购物车信息
                    msg = DeleteDataBaseCartInfo(cartId);
                    break;
                case "DeleteCookieCartInfo"://删除cookie中购物车信息
                    msg = DeleteCookieCartInfo(bookId);
                    break;
                case "UpdateDataBaseCartInfo"://更新数据库中购物车信息
                    msg = UpdateDataBaseCartInfo(cartId, bookId, count);
                    break;
                case "UpdateCookieCartInfo"://更新cookie中购物车信息
                    msg = UpdateCookieCartInfo(bookId, count);
                    break;
            }
            Response.Write(msg);
            Response.End();

        }

        //更新cookie中购物车信息
        private string UpdateCookieCartInfo(int bookId, int count)
        {
            HttpCookie cookie = Request.Cookies["ShoppingCart"];
            if (cookie != null)
            {
                foreach (string item in cookie.Values)
                {
                    if (Convert.ToInt32(item) == bookId)
                    {
                        cookie.Values.Remove(item);//先删除再添加，更方便
                        cookie.Values.Add(bookId.ToString(), bookId + "|" + count);
                        Response.Cookies.Add(cookie);
                        return "cookie更新成功";
                    }
                }
                return "cookie中不存在该商品";
            }
            return "cookie为null";
        }

        //更新数据库中购物车信息
        private string UpdateDataBaseCartInfo(int cartId, int bookId, int count)
        {
            Model.Cart cart = cartBll.GetModel2(cartId);
            cart.Book.Id = bookId;
            cart.Count = count;
            return cartBll.Update2(cart) ? "数据库更新成功" : "数据库更新失败";
        }

        //删除cookie中购物车信息
        private string DeleteCookieCartInfo(int bookId)
        {
            HttpCookie cookie = Request.Cookies["ShoppingCart"];
            if (cookie != null)
            {
                foreach (string item in cookie.Values)
                {
                    if (Convert.ToInt32(item) == bookId)
                    {
                        cookie.Values.Remove(item);//==============多值cookie删除问题
                        Response.Cookies.Add(cookie);
                        return "cookie删除成功";
                    }
                }
                return "cookie中不存在该商品";
            }
            return "cookie为null";
        }

        //删除数据库中购物车信息
        private string DeleteDataBaseCartInfo(int cartId)
        {
            return cartBll.Delete(cartId) ? "数据库删除成功" : "数据库删除失败";
        }

        //加载购物车中所有的信息
        private void LoadAllCartInfo(List<Model.Cart> listCart)
        {
            if (Session["userInfo"] != null)
            {
                Model.Users user = Session["userInfo"] as Model.Users;
                //BLL.CartBll cartBll = new BLL.CartBll();
                listCart.AddRange(cartBll.GetModelList2("UserId = " + user.Id));
            }
            else
            {
                HttpCookie cookie = Request.Cookies["ShoppingCart"];
                if (cookie != null)
                {
                    BLL.BooksBll bookBll = new BooksBll();
                    foreach (string item in cookie.Values)
                    {
                        string itemValue = cookie.Values[item];
                        string[] cartInfo = itemValue.Split(new char[] { '|' },
                            StringSplitOptions.RemoveEmptyEntries);
                        listCart.Add(new Model.Cart()
                        {
                            Book = bookBll.GetModel(Convert.ToInt32(cartInfo[0])),
                            Count = Convert.ToInt32(cartInfo[1])
                        });
                    }
                }
            }
        }
    }
}