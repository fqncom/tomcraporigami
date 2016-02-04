using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.Common;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.WebApp.Controllers
{
    //[MyCommonFilter(Msg = "this is awesome")]//自定义的类过滤器
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        private IBll.IUserInfoBll UserInfoBll { get; set; }
        //[MyCommonFilter(Msg = "this is awesome")]//自定义的方法过滤器
        public ActionResult Index()
        {
            HttpCookie cpName = Request.Cookies["cpUName"];
            HttpCookie cpPwd = Request.Cookies["cpUPwd"];
            if (cpName != null && cpPwd != null)
            {
                ViewData["cpUName"] = cpName.Value;
                ViewData["cpUPwd"] = cpPwd.Value;
            }
            return View();
        }

        #region 获取验证码
        public ActionResult GetValidateCode()
        {
            Common.ValidateHelper validate = new ValidateHelper();
            string validateCode = validate.CreateValidateCode(4);
            byte[] vCodeBytes = validate.CreateValidateGraphic(validateCode);
            Session["vCode"] = validateCode;
            return File(vCodeBytes, "image/jpeg");
        }
        #endregion

        #region 检测用户账号
        
        public ActionResult CheckLoginInfo()
        {
            //判断用户是否登入状态
            string vCode = Session["vCode"].ToString() ?? "";
            ////暂停使用验证码
            //if (!string.IsNullOrEmpty(vCode) && vCode.Equals(Request["vCode"]))
            //{
            string name = Request["LoginCode"] ?? "";
            string pwd = Request["LoginPwd"] ?? "";
            int rowCount = 0;
            UserInfo userInfo = UserInfoBll.SelectEntities<UserInfo>(u => u.UName == name && u.UPwd == pwd, out rowCount).FirstOrDefault();
            if (userInfo != null)
            {
                //Session["LoginUserInfo"] = userInfo;
                string sessionId = Guid.NewGuid().ToString();
                Common.MemCacheHelper.Set(sessionId, Common.SerializerHelper.SerializerToString(userInfo), DateTime.Now.AddDays(3));
                HttpCookie cookieSession = new HttpCookie("SessionId", sessionId);
                Response.Cookies.Add(cookieSession);
                if (Request["remmberMe"] != null)
                {
                    HttpCookie cpName = new HttpCookie("cpUName", userInfo.UName);
                    cpName.Expires = System.DateTime.Now.AddDays(3);
                    HttpCookie cpPwd = new HttpCookie("cpUPwd", userInfo.UPwd);
                    cpPwd.Expires = System.DateTime.Now.AddDays(3);

                    Response.Cookies.Add(cpName);
                    Response.Cookies.Add(cpPwd);
                }
                return Content("success:登入成功");
            }
            //Session["vCode"] = null;
            return Content("falied:账户密码错误");
            //}
            //else
            //{
            //    //Session["vCode"] = null;
            //    return Content("failed:验证码错误");
            //}
        }
        #endregion


    }
}
