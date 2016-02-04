using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fqn.MVC_EF.FactoryBll;
using fqn.MVC_EF.IBll;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.WepApp.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        private readonly IProvinceBll bll = AbstractFactory.CreateProvinceBllInstance();
        public ActionResult Index()
        {
            int pageIndex = Convert.ToInt32(Request["pageIndex"] ?? "1");
            int pageSize = 5;
            int pageCount = Convert.ToInt32(Math.Ceiling(1.0 * bll.SelectCount() / pageSize));
            ViewData.Model = bll.SelectList((pageIndex - 1) * pageSize, pageSize);
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageCount"] = pageCount;
            //ViewBag.PageIndex = pageIndex;//编译时确定类型
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewData.Model = bll.Select(id);
            return View();
        }
        [HttpPost]
        public ActionResult Detail(S_Province model)
        {
            if (bll.Update(model))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("操作失败");
            }
        }

        public ActionResult Update(S_Province model)
        {
            //S_Province model = new S_Province();
            //model.ProvinceID = id;
            //model.ProvinceName = Request["ProvinceName"] ?? "默认";
            //model.S_City.Add(new S_City() { CityID = 1 });
            if (bll.Update(model))
            {
                return RedirectToAction("Detail");
            }
            else
            {
                return Content("操作失败");
            }
        }

        public ActionResult Delete(int id)
        {
            if (bll.Delete(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("操作失败");
            }
        }

        public ActionResult Add(S_Province model)
        {
            if (bll.Add(model))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("操作失败");
            }
        }

    }
}
