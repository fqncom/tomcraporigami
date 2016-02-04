using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.IBll;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class AuthorityController : BaseController
    {
        //
        // GET: /Authority/
        private IBll.IActionInfoBll ActionInfoBll { get; set; }
        private IBll.IRoleInfoBll RoleInfoBll { get; set; }

        /// <summary>
        /// 权限首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetActionInfoList()
        {
            int rowCount = 0;
            int pageIndex = Convert.ToInt32(Request["pageIndex"] ?? "1");
            int pageSize = Convert.ToInt32(Request["rows"] ?? "5");
            var actionInfoList = ActionInfoBll.SelectPageEntities(a => a.DelFlag == (short)DeleteFlag.Normal, a => a.ID,
                (pageIndex - 1) * pageSize, pageSize, out rowCount, false);
            var returnList = from a in actionInfoList
                             select new
                             {
                                 ID = a.ID,
                                 ActionInfoName = a.ActionInfoName,
                                 Sort = a.Sort,
                                 Url = a.Url,
                                 HttpMethod = a.HttpMethod,
                                 ActionTypeEnum = a.ActionTypeEnum,
                                 SubTime = a.SubTime,
                                 Remark = a.Remark
                             };


            return Json(new { rows = returnList, totalCount = rowCount });
        }

        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.Url = actionInfo.Url.ToLower();
            actionInfo.DelFlag = (short)DeleteFlag.Normal;
            actionInfo.ModifiedOn = System.DateTime.Now.ToString();
            actionInfo.SubTime = System.DateTime.Now;

            return Content(ActionInfoBll.AddEntity(actionInfo) != null ? "success" : "falied");
        }


        public ActionResult ShowActionInfo()
        {
            int id = Convert.ToInt32(Request["id"] ?? "-1");
            int rowCount = 0;
            var actionInfo = ActionInfoBll.SelectEntities<ActionInfo>(r => r.ID == id, out rowCount).FirstOrDefault();

            var result = new
            {
                ID = actionInfo.ID,
                ActionInfoName = actionInfo.ActionInfoName,
                HttpMethod = actionInfo.HttpMethod,
                MenuIcon = actionInfo.MenuIcon,
                ModifiedOn = actionInfo.ModifiedOn,
                Remark = actionInfo.Remark,
                ActionMethodName = actionInfo.ActionMethodName,
                Url = actionInfo.Url
            };
            return View();
        }

        public ActionResult ShowActionRoleInfo()
        {
            int actionId = Convert.ToInt32(Request["actionId"] ?? "-1");
            int rowCount = 0;
            var actionInfo = ActionInfoBll.SelectEntities<ActionInfo>(a => a.ID == actionId && a.DelFlag == (short)DeleteFlag.Normal, out rowCount).FirstOrDefault();
            if (actionInfo != null)
            {
                ViewBag.ActionInfo = actionInfo;
                ViewBag.RoleInfoList = RoleInfoBll.SelectEntities<RoleInfo>(r => r.DelFlag == (short)DeleteFlag.Normal,
                    out rowCount).ToList();
                ViewBag.RoleIdList = (from r in actionInfo.RoleInfo
                                      select r.ID).ToList();
                return View();
            }
            return Content("something wrong");
        }

        public ActionResult SetActionInfoRoleInfo()
        {
            int actionId = Convert.ToInt32(Request["hiddenActionId"]);
            string[] keys = Request.Form.AllKeys;
            int rowCount = 0;
            List<int> roleIdList = new List<int>();

            foreach (string key in keys)
            {
                if (key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    roleIdList.Add(Convert.ToInt32(k));
                }
            }
            return Content(ActionInfoBll.UpdateActionRoleInfo(actionId, roleIdList) ? "success" : "failed");


        }
    }
}
