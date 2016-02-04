using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fqn_FirstMVC.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/
        nononodeleteImportantEntities nse = new nononodeleteImportantEntities();
        public ActionResult Index()
        {

            var userInfoList = nse.Users.Where<Users>(u => true).ToList();
            ViewData["UserInfo"] = userInfoList;

            return View(); //return View("Index");
        }

        public ActionResult ShowUserInfo(int id)
        {
            var userInfo = nse.Users.FirstOrDefault(u => u.autoId == id);//nse.Users.Where(u => u.autoId == id).FirstOrDefault();
            ViewData["UserInfo"] = userInfo;
            return View();
        }

        public ActionResult DeleteUserInfo(int id)
        {
            var userInfo = nse.Users.FirstOrDefault(u => u.autoId == id);
            nse.Entry<Users>(userInfo).State = System.Data.EntityState.Deleted;
            nse.SaveChanges();
            return RedirectToAction("Index");
            //return this.Index();
        }
    }
}
