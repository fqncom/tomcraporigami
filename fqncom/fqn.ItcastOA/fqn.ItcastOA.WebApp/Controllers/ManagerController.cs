using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;
using Spring.Expressions.Parser.antlr.debug;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class ManagerController : BaseController
    {
        //
        // GET: /Manager/

        private IBll.IUserInfoBll UserInfoBll { get; set; }

        #region 登入成功，显示后台主界面
        public ActionResult Index()
        {
            if (Request.Cookies["SessionId"] != null)
            {
                string sessionId = Request.Cookies["SessionId"].Value;
                UserInfo user = Common.SerializerHelper.SerializerToObject<UserInfo>(Common.MemCacheHelper.Get(sessionId).ToString());
                ViewBag.Model = user;
            }
            return View();
        }
        #endregion

        #region 注销
        //[MyCommonFilter(Msg = "注销成功")]//可以使用过滤器
        public ActionResult UserLogOut()
        {
            HttpCookie cookie = Request.Cookies["sessionId"];
            if (cookie != null)
            {
                cookie.Expires = System.DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return Redirect("/Login/Index");
            //return null;
        }
        #endregion

        #region 显示WIn7界面
        public ActionResult MainIndex()
        {
            if (Request.Cookies["SessionId"] != null)
            {
                string sessionId = Request.Cookies["SessionId"].Value;
                UserInfo user = Common.SerializerHelper.SerializerToObject<UserInfo>(Common.MemCacheHelper.Get(sessionId).ToString());
                ViewBag.Model = user;
            }
            return View();
        }
        #endregion

        #region 加载用户权限

        public ActionResult LoadUserAction()
        {
            int rowCount = 0;
            UserInfo userInfo =
                UserInfoBll.SelectEntities<UserInfo>(u => u.ID == LoginUserInfo.ID, out rowCount).FirstOrDefault();
            //加载用户==角色==权限中用户所有的权限
            var user_role_act = (from u in userInfo.RoleInfo
                                 from a in u.ActionInfo
                                 select a).ToList();
            //加载用户==权限中用户所有特殊权限
            var r_user_act = (from u in userInfo.R_UserInfo_ActionInfo
                              select u.ActionInfo).ToList();
            //加载用户==权限中用户所有的禁用权限
            var r_user_act_forbid = (from u in userInfo.R_UserInfo_ActionInfo
                                     where u.IsPass == false
                                     select u.ActionInfoID).ToList();
            //将两个集合放入一个集合
            user_role_act.AddRange(r_user_act);
            //将用户所有权限中禁用的权限去除,得到用户真正的权限信息,,注意删除标识和去重
            var real_user_act = (from a in user_role_act
                                 where !r_user_act_forbid.Contains(a.ID) && a.DelFlag == (short)DeleteFlag.Normal
                                 select a).Distinct().ToList();
            //将的到的权限信息转化为前台数据进行输出
            var actions = (from a in real_user_act
                           select new
                           {
                               icon = a.MenuIcon,
                               title = a.ActionInfoName,
                               url = a.Url
                           }).ToList();
            return Json(actions, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
