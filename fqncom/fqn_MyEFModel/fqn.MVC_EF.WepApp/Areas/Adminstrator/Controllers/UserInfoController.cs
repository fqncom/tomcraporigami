using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using fqn.MVC_EF.Bll;
using fqn.MVC_EF.Common;
using fqn.MVC_EF.FactoryBll;
using fqn.MVC_EF.IBll;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.WepApp.Areas.Adminstrator.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /Adminstrator/UserInfo/
        private IUserInfoBll bll = AbstractFactory.CreateUserInfoBllInstance();

        public ActionResult Index()
        {
            //ViewData.Model = bll.SelectList(10, 10);
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(int pageIndex)
        {
            List<UserInfo> list = bll.SelectList(pageIndex, 10);
            //System.Web.Script.Serialization.JavaScriptSerializer js = new JavaScriptSerializer();
            //string s = js.Serialize(list);
            return Json(list);
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Create()
        {
            return Content("hahah");
        }

    }
}
