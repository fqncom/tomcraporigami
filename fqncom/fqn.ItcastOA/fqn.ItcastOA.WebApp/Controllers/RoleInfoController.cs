using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class RoleInfoController : BaseController
    {
        //
        // GET: /RoleInfo/

        IBll.IRoleInfoBll RoleInfoBll { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取分页用户数据
        public ActionResult GetRoleInfoList()
        {
            int pageIndex = Convert.ToInt32(Request["page"] ?? "1");
            int pageSize = Convert.ToInt32(Request["rows"] ?? "5");
            int rowCount = 0;
            
            var roleInfoList = RoleInfoBll.SelectPageEntities<int>(r=>true,r=>r.ID,(pageIndex-1)*pageSize,pageSize,out rowCount,false);
            var temp = (from r in roleInfoList
                        select new
                        {
                            ID = r.ID,
                            RName = r.RoleName,
                            Remark = r.Remark,
                            Sort=  r.Sort,
                            SubTime = r.SubTime
                        }).ToList();

            return Json(new { rows = temp, total = rowCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加角色信息
        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = (short)DeleteFlag.Normal;
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            RoleInfoBll.AddEntity(roleInfo);
            return Content("success");
        }
        #endregion

        #region 获取要修改的数据
        public ActionResult ShowRoleInfo()
        {
            int id = int.Parse(Request["id"]);
            int rowCount = 0;
            var roleInfo = RoleInfoBll.SelectEntities<RoleInfo>(r => r.ID == id,out rowCount).FirstOrDefault();
            ViewData.Model = roleInfo;
            return View();
        }
        #endregion

        public ActionResult EditRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            RoleInfoBll.UpdateEntity(roleInfo);
            return Content("success");
        }

    }
}
