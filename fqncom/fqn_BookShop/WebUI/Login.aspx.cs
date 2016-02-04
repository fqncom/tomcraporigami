using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBookShop
{
    public partial class Login : Common.CheckSession
    {
        protected string LoginIdCookie { get; set; }
        protected string ReturnUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["loginId"];
            if (cookie != null)
            {
                this.LoginIdCookie = cookie.Value;
            }
            this.ReturnUrl = Request["ReturnUrl"] ?? "Index.aspx";
            string transCode = Request["TransCode"] ?? "";
            if (transCode == "")
            {
                return;
            }
            string loginId = Request["LoginId"] ?? this.LoginIdCookie;
            string loginPwd = Request["loginPwd"] ?? "-1";
            //string rememberMe = Request["RememberMe"] ?? "false";
            string msg = string.Empty;
            switch (transCode)
            {
                case "UserLogin":
                    BLL.CommonBll.CheckUserLoginPwdAndGetCartCookie(loginId, loginPwd, cookie, out msg);

                    #region 以下封装在方法里了

                    //BLL.UsersBll bll = new BLL.UsersBll();
                    //Model.Users user = new BLL.UsersBll().GetModelByLoginId(loginId);
                    //object realPwd = bll.GetUserLoginPwdByLoginId(loginId);
                    //if (user != null)
                    //{
                    //    msg = Common.CommonTools.GetMd5String(Common.CommonTools.GetMd5String(loginPwd)).Equals(user.LoginPwd) ? "success" : "failed";
                    //}

                    //if (msg == "success")
                    //{
                    //    if (rememberMe != "false")//记住我选项
                    //    {
                    //        HttpCookie cookie1 = new HttpCookie("loginId", loginId);
                    //        cookie1.Value = loginId;
                    //        cookie1.Expires = DateTime.Now.AddDays(10);
                    //        Response.Cookies.Add(cookie1);
                    //    }
                    //    else
                    //    {
                    //        if (cookie != null)
                    //        {
                    //            cookie.Expires = DateTime.Now.AddDays(-1);
                    //            Response.Cookies.Add(cookie);
                    //        }
                    //    }
                    //    //Model.Users user = new BLL.UsersBll().GetModelByLoginId(loginId);
                    //    Session["userInfo"] = user;
                    //    //同时检测用户cookie中有没有购物车信息，有就将数据写入数据库
                    //    //HttpCookie cookieCart = Request.Cookies["ShoppingCart"];
                    //    //if (cookieCart != null)
                    //    //{
                    //    //    int userId = bll.GetModelByLoginId(loginId).Id;
                    //    //    foreach (string item in cookieCart.Values)
                    //    //    {
                    //    //        string itemValue = cookieCart[item].ToString();
                    //    //        string[] cartInfo = itemValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    //    //        BLL.CartBll cartBll = new BLL.CartBll();
                    //    //        Model.Cart cart = cartBll.GetModel(Convert.ToInt32(cartInfo[0]), user.Id);
                    //    //        if (cart == null)
                    //    //        {
                    //    //            cart = new Model.Cart();
                    //    //            cart.Book.Id = Convert.ToInt32(cartInfo[0]);
                    //    //            cart.Count = Convert.ToInt32(cartInfo[1]);
                    //    //            cart.User.Id = userId;
                    //    //            cartBll.Add2(cart);//调用新的增加方法。对象属性变了
                    //    //        }
                    //    //        else
                    //    //        {
                    //    //            cart.Count += Convert.ToInt32(cartInfo[1]);
                    //    //            cartBll.Update2(cart);
                    //    //        }
                    //    //    }
                    //    //    //清空cookie
                    //    //    cookieCart.Expires = System.DateTime.Now.AddDays(-1);
                    //    //    Response.Cookies.Add(cookieCart);
                    //    //}
                    //}
                    #endregion

                    break;
            }
            Response.Write(msg);
            Response.End();
        }

    }
}