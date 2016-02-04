using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using MyBookShop.Model;

namespace MyBookShop.ashx
{
    /// <summary>
    /// DealWithBookComment 的摘要说明
    /// </summary>
    public class DealWithBookComment : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string transCode = context.Request["TransCode"] ?? "";
            if (transCode == "")
            {
                return;
            }
            //声明object类型的集合，存储改变之后的日期
            int bookId = Convert.ToInt32(context.Request["BookId"] ?? "-1");
            switch (transCode)
            {
                case "GetAllComment":
                    List<Model.BookComment> list = GetAllCommentByBookId(bookId);
                    List<object> newList = new List<object>();
                    foreach (Model.BookComment bookComment in list)
                    {
                        newList.Add(new
                        {
                            CreateDateTime = Common.CommonTools.ChangeTimeSpanToString(System.DateTime.Now - bookComment.CreateDateTime),
                            Msg = Common.CommonTools.UBBDecode(bookComment.Msg)
                            //ParentId = bookComment.parentId,
                            //FloorIndexId = bookComment.floorIndexId
                        });
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    context.Response.Write(js.Serialize(newList));
                    break;
                case "AddComment":
                    string msg = context.Request["Msg"] ?? "";
                    //对评论内容进行校验
                    msg = CheckCommentIsLegal(bookId, msg);
                    context.Response.Write(msg);
                    break;
                case "AddBookIntoCart":
                    msg = AddBookIntoCart(bookId);
                    context.Response.Write(msg);
                    break;
                default:
                    break;
            }
        }

        //添加进购物车
        private string AddBookIntoCart(int bookId)
        {
            //判断用户是否登入
            Model.Users user = Common.CommonTools.CheckIsLoginOrNot();
            if (user != null)//登入状态下将数据保存到数据库
            {
                BLL.CartBll bll = new BLL.CartBll();
                Model.Cart cart = bll.GetModel(bookId, user.Id);

                if (cart != null)//先检测购物车中是否存在相同的商品
                {
                    cart.Count++;//存在则自增1
                    bll.Update2(cart);//更新
                    return "已永久更新你的购物信息";
                }
                else//不存在同样的商品则新增
                {
                    cart = new Model.Cart();//为空则创建
                    cart.Book.Id = bookId;
                    cart.Count = 1;
                    cart.User.Id = user.Id;
                    bll.Add2(cart);//新增
                    return "已永久新增你的购物信息";
                }
            }
            else//未登入状态下，保存到cookie
            {
                //先判断cookie是否有值
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ShoppingCart"];
                if (cookie == null)
                {
                    cookie = new HttpCookie("ShoppingCart");
                    cookie.Values.Add(bookId.ToString(), bookId + "|" + 1);
                    cookie.Expires = System.DateTime.Now.AddDays(2);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    return "已临时保存你的购物信息";//,<a href='javascript:LoginClick();'>永久保存？</a>
                }
                else
                {
                    bool flag = true;
                    foreach (string item in cookie.Values)
                    {
                        string itemValue = cookie.Values[item];
                        if (string.IsNullOrEmpty(itemValue))
                        {
                            break;
                        }
                        string[] itemValues = itemValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        if (itemValues[0] == bookId.ToString())//如果cookie中存在，则数量增加
                        {
                            int num = Convert.ToInt32(itemValues[1]) + 1;
                            cookie.Values.Remove(bookId.ToString());
                            cookie.Values.Add(bookId.ToString(), bookId + "|" + num);
                            cookie.Expires = System.DateTime.Now.AddDays(2);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                            flag = false;
                            break;
                        }
                    }
                    if (flag)//遍历之后都不存在相同产品，则新增一条
                    {
                        cookie.Values.Add(bookId.ToString(), bookId + "|" + 1);
                        cookie.Expires = System.DateTime.Now.AddDays(2);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    return "已临时更新你的购物信息";//,<a href='javascript:LoginClick();'>永久保存？</a>
                }
            }
        }

        //检测用户评论是否合法
        private string CheckCommentIsLegal(int bookId, string msg)
        {
            BLL.Articel_WordsBll bll = new BLL.Articel_WordsBll();
            if (bll.CheckIsForbidWord(msg))
            {
                return "failed:文本有敏感词";
            }
            if (bll.CheckIsModWord(msg))
            {
                if (AddComment(bookId, msg))
                {
                    return "success:文本待审查";
                }
                return "failed:出现了未知错误";
            }
            msg = bll.CheckIsReplaceWord(msg);
            if (AddComment(bookId, msg))
            {
                return "success:评论成功";
            }
            return "failed:出现了未知错误";
        }

        //添加评论
        private bool AddComment(int bookId, string msg)
        {
            Model.BookComment bookComment = new Model.BookComment();
            bookComment.CreateDateTime = System.DateTime.Now;
            bookComment.Msg = msg;
            bookComment.BookId = bookId;
            BLL.BookCommentBll bll = new BLL.BookCommentBll();
            return bll.Add(bookComment) > 0;
        }

        //根据书的Id获取评论
        private List<Model.BookComment> GetAllCommentByBookId(int bookId)
        {
            return new BLL.BookCommentBll().GetModelList("BookId =" + bookId);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}