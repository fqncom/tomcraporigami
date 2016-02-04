using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using fqn.ItcastOA.IBll;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;
using Spring.Context;
using Spring.Context.Support;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected UserInfo LoginUserInfo { get; set; }

        /// <summary>
        /// 全局过滤器，检测用户是否处于登入状态，同时检测用户的权限是否是菜单权限还是只是普通权限
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookieSession = filterContext.HttpContext.Request.Cookies["SessionId"];

            if (cookieSession != null)
            {
                #region 由于删除了membercache程序，所以直接在这里进入了。
                //return;
                #endregion
                object obj = Common.MemCacheHelper.Get(cookieSession.Value);
                if (obj != null)
                {
                    UserInfo userInfoTemp = Common.SerializerHelper.SerializerToObject<UserInfo>(obj.ToString());
                    Common.MemCacheHelper.Set(cookieSession.Value, obj, DateTime.Now.AddMinutes(20));
                    this.LoginUserInfo = userInfoTemp;
                    //return;
                    if (this.LoginUserInfo.UName == "itcast")
                    {
                        return;//留下一个后门，方便开发，之后交接产品的时候记得删除
                    }
                    //根据用户输入的控制器名称和方法名获取action对象
                    string url = Request.Url.AbsolutePath.ToString();
                    string method = Request.HttpMethod;
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    IUserInfoBll userInfoBll = (IUserInfoBll)ctx.GetObject("IUserInfoBll");
                    IActionInfoBll actionInfoBll = (IActionInfoBll)ctx.GetObject("IActionInfoBll");
                    int rowCount = 0;
                    UserInfo userInfo =
                        userInfoBll.SelectEntities<UserInfo>(u => u.ID == userInfoTemp.ID, out rowCount)
                            .FirstOrDefault();

                    ActionInfo action = actionInfoBll.SelectEntities<ActionInfo>(a => a.Url == url.ToLower() && a.HttpMethod == method && a.DelFlag == (short)DeleteFlag.Normal, out rowCount).FirstOrDefault();

                    if (action == null)
                    {
                        filterContext.Result = Redirect("/Error.html");
                        return;
                    }
                    //判断用户的特殊权限的操作等级
                    var userAction = (from a in userInfo.R_UserInfo_ActionInfo
                                      where a.IsPass == true && a.ID == action.ID
                                      select a).FirstOrDefault();
                    if (userAction != null)
                    {
                        return;
                    }
                    //判断用户的角色权限的操作等级
                    var userRoleAction = (from r in userInfo.RoleInfo
                                          from a in r.ActionInfo
                                          where a.DelFlag == (short)DeleteFlag.Normal && a.ID == action.ID
                                          select a).Count();
                    if (userRoleAction < 1)
                    {
                        filterContext.Result = Redirect("/Error.html");
                        return;
                    }
                    return;
                }
            }
            filterContext.Result = Redirect("/Login/Index");//如果用户状态失效，那么将过滤器的Result属性设置为跳转ActionResult，这样直接在MVC运行的过程中，当执行完过滤器之后，会去判断过滤器的这个属性是否有值，如果有值则，直接返回这个值，之后的方法不再执行。达到过滤的效果。而如果Result属性为Null，则表示过滤完了之后一切正常，继续执行之后的方法。
        }
    }
}