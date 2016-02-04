using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fqn_FirstMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult DealWithRegister()
        {
            nononodeleteImportantEntities entities = new nononodeleteImportantEntities();
            string loginId = Request["loginId"] ?? "";
            string loginPwd = Request["loginPwd"] ?? "";
            Users user = new Users();
            user.loginId = loginId;
            user.loginPwd = loginPwd;
            user.LastLoginTime = System.DateTime.Now;
            user.ErrorCount = 0;
            entities.Users.Add(user);
            if (entities.SaveChanges() > 0)
            {
                return Content("注册成功");
            }
            else
            {
                return Content("注册失败");
            }
        }

    }
}
