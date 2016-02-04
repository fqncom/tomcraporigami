using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyBookShop.BLL
{
    public partial class CommonBll
    {
        /// <summary>
        /// 根据用户名和密码，登入验证，并且拿cookie的值
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="loginPwd">登入密码</param>
        /// <param name="cookie">cookie</param>
        /// <param name="msg">返回信息</param>
        public static void CheckUserLoginPwdAndGetCartCookie(string loginId, string loginPwd, HttpCookie cookie, out string msg)
        {
            string rememberMe = HttpContext.Current.Request["RememberMe"] ?? "false";

            BLL.UsersBll bll = new BLL.UsersBll();
            Model.Users user = new MyBookShop.BLL.UsersBll().GetModelByLoginId(loginId);
            msg = string.Empty;
            if (user != null)
            {
                msg = Common.CommonTools.GetMd5String(Common.CommonTools.GetMd5String(loginPwd)).Equals(user.LoginPwd) ? "success" : "failed";
            }

            if (msg == "success")
            {
                if (rememberMe != "false")//记住我选项
                {
                    HttpCookie cookie1 = new HttpCookie("loginId", loginId);
                    cookie1.Value = loginId;
                    cookie1.Expires = DateTime.Now.AddDays(10);
                    HttpContext.Current.Response.Cookies.Add(cookie1);
                }
                else
                {
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                }
                //Model.Users user = new BLL.UsersBll().GetModelByLoginId(loginId);
                HttpContext.Current.Session["userInfo"] = user;
                //同时检测用户cookie中有没有购物车信息，有就将数据写入数据库
                HttpCookie cookieCart = HttpContext.Current.Request.Cookies["ShoppingCart"];
                if (cookieCart != null)
                {
                    int userId = bll.GetModelByLoginId(loginId).Id;
                    foreach (string item in cookieCart.Values)
                    {
                        string itemValue = cookieCart[item].ToString();
                        string[] cartInfo = itemValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        BLL.CartBll cartBll = new BLL.CartBll();
                        Model.Cart cart = cartBll.GetModel(Convert.ToInt32(cartInfo[0]), user.Id);
                        if (cart == null)
                        {
                            cart = new Model.Cart();
                            cart.Book.Id = Convert.ToInt32(cartInfo[0]);
                            cart.Count = Convert.ToInt32(cartInfo[1]);
                            cart.User.Id = userId;
                            cartBll.Add2(cart);//调用新的增加方法。对象属性变了
                        }
                        else
                        {
                            cart.Count += Convert.ToInt32(cartInfo[1]);
                            cartBll.Update2(cart);
                        }
                    }
                    //清空cookie
                    cookieCart.Expires = System.DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookieCart);
                }
            }
        }
    }
}
