using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Cache;
using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.Bll;
using fqn.ItcastOA.Common;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;
using fqn.ItcastOA.Model.SearchModel;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        private IBll.IUserInfoBll UserInfoBll { get; set; }
        private IBll.IRoleInfoBll RoleInfoBll { get; set; }

        private IBll.IActionInfoBll ActionInfoBll { get; set; }
        private IBll.IR_UserInfo_ActionInfoBll R_UserInfo_ActionInfoBll { get; set; }


        public ActionResult Index()
        {
            return View();
        }

        #region 获取分页用户数据
        public ActionResult GetUserInfoList()
        {
            int pageIndex = Convert.ToInt32(Request["page"] ?? "1");
            int pageSize = Convert.ToInt32(Request["rows"] ?? "5");
            int rowCount = 0;
            UserInfoParams pars = new UserInfoParams()
            {
                UName = Request["UName"] ?? "",
                Remark = Request["Remark"] ?? "",
                RowSkip = (pageIndex - 1) * pageSize,
                RowTake = pageSize,
                RowCount = rowCount
            };


            var userInfoList = UserInfoBll.LoadSearchEntities(pars, false);
            //var userInfoList = bll.SelectPageEntities<int>(u => u.DelFlag == (short)DeleteFlag.Normal, u => u.ID, (pageIndex - 1) * pageSize, pageSize, out rowCount, false);
            var temp = (from u in userInfoList
                        select new
                        {
                            ID = u.ID,
                            UName = u.UName,
                            UPwd = u.UPwd,
                            Remark = u.Remark,
                            SubTime = u.SubTime
                        }).ToList();

            return Json(new { rows = temp, total = pars.RowCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 逻辑删除用户
        public ActionResult DeleteUserInfo()
        {
            string deleteId = Request["DeleteId"] ?? "";
            if (deleteId == "")
            {
                return null;
            }
            string[] delIds = deleteId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> listId = new List<int>();
            foreach (string delId in delIds)
            {
                listId.Add(Convert.ToInt32(delId));
            }
            return Content(UserInfoBll.DeleteEntitiesLogical(listId) ? "success" : "failed");
        }
        #endregion

        #region 新增用户
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.SubTime = System.DateTime.Now;
            userInfo.ModifiedOn = System.DateTime.Now;
            userInfo.DelFlag = (short)DeleteFlag.Normal;
            return Content(UserInfoBll.AddEntity(userInfo) != null ? "success" : "failed");
        }
        #endregion

        #region 查询一个用户

        public ActionResult GetUserInfo()
        {
            int id = Convert.ToInt32(Request["Id"] ?? "-1");
            int rowCount = 0;
            UserInfo user = UserInfoBll.SelectEntities<UserInfo>(u => u.ID == id, out rowCount).FirstOrDefault();
            if (user != null)
            {
                return Json(new
                {
                    ID = user.ID,
                    UName = user.UName,
                    UPwd = user.UPwd,
                    Remark = user.Remark,
                    Sort = user.Sort,
                    SubTime = user.SubTime
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("failed");
            }
        }

        #endregion

        #region 编辑用户
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            userInfo.DelFlag = (short)DeleteFlag.Normal;
            userInfo.ModifiedOn = System.DateTime.Now;
            return Content(UserInfoBll.UpdateEntity(userInfo) ? "success" : "failed");
        }

        #endregion

        #region 测试错误日志记录

        public ActionResult TestLogRecord()
        {
            int a = 10;
            int b = 1;
            b--;
            return Content((a / b).ToString());
        }

        #endregion

        #region 查看用户角色

        public ActionResult GetRUserRoleInfo()
        {
            int id = Convert.ToInt32(Request["id"] ?? "-1");
            int rowCount = 0;
            UserInfo user = UserInfoBll.SelectEntities<UserInfo>(u => u.ID == id, out rowCount).FirstOrDefault();
            if (user != null)
            {
                ViewBag.UserInfo = user;
                List<RoleInfo> roleInfoList = RoleInfoBll.SelectEntities<RoleInfo>(r => r.DelFlag == (short)DeleteFlag.Normal, out rowCount).ToList();
                List<int> userRoleIdList = (from ur in user.RoleInfo
                                            select ur.ID).ToList();

                ViewBag.RoleInfoList = roleInfoList;
                ViewBag.UserRoleIdList = userRoleIdList;
                return View();
            }

            return Content("页面找不到了");

        }

        #endregion

        #region 编辑用户角色

        public ActionResult EditRUserRoleInfo()
        {
            int userId = Convert.ToInt32(Request["UserId"] ?? "-1");
            string[] allKeys = Request.Form.AllKeys;
            List<int> roleIdList = new List<int>();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("ch_"))
                {
                    string k = key.Replace("ch_", "");
                    roleIdList.Add(Convert.ToInt32(k));
                }
            }
            return Content(UserInfoBll.UpdateUserRoleInfo(userId, roleIdList) ? "success" : "failed");
        }

        #endregion

        #region 显示用户的特殊权限

        public ActionResult ShowUserActionInfo()
        {
            int userId = Convert.ToInt32(Request["userId"] ?? "-1");
            int rowCount = 0;
            var userInfo = UserInfoBll.SelectEntities<UserInfo>(u => u.ID == userId && u.DelFlag == (short)DeleteFlag.Normal, out rowCount).FirstOrDefault();
            if (userInfo != null)
            {
                ViewBag.UserInfo = userInfo;
                ViewBag.UserActionInfoList = (from u in userInfo.R_UserInfo_ActionInfo
                                              select u).ToList();
                ViewBag.ActionInfoList = ActionInfoBll.SelectEntities<ActionInfo>(a => a.DelFlag == (short)DeleteFlag.Normal,
                    out rowCount).ToList();
                return View();
            }
            return Content("something wrong");
        }

        #endregion

        #region 为用户指定特殊权限

        public ActionResult SetUserInfoActionInfo()
        {

            int userId = Convert.ToInt32(Request["userId"] ?? "-1");
            int actionId = Convert.ToInt32(Request["actionId"] ?? "-1");
            bool isPass = Request["IsPass"] == "true";
            int rowCount = 0;
            UserInfo userInfo = UserInfoBll.SelectEntities<UserInfo>(u => u.ID == userId && u.DelFlag == (short)DeleteFlag.Normal,
                 out rowCount).FirstOrDefault();
            if (userInfo != null)
            {
                R_UserInfo_ActionInfo userAction = (from ru in userInfo.R_UserInfo_ActionInfo
                                                    where ru.ActionInfoID == actionId
                                                    select ru).FirstOrDefault();

                if (userAction != null)
                {
                    userAction.IsPass = isPass;
                    R_UserInfo_ActionInfoBll.UpdateEntity(userAction);
                }
                else
                {
                    userAction = new R_UserInfo_ActionInfo();
                    userAction.ActionInfoID = actionId;
                    userAction.IsPass = isPass;
                    userAction.UserInfoID = userInfo.ID;
                    R_UserInfo_ActionInfoBll.AddEntity(userAction);
                }
                return Content("success");
            }

            return Content("failed");
        }

        #endregion

        #region 清除用户权限

        public ActionResult ClearUserActionInfo()
        {
            int userId = Convert.ToInt32(Request["userId"] ?? "-1");
            int actionId = Convert.ToInt32(Request["actionId"] ?? "-1");
            int rowCount = 0;
            var r_userInfo_ActionInfo =
                R_UserInfo_ActionInfoBll.SelectEntities<R_UserInfo_ActionInfo>(
                    ru => ru.UserInfoID == userId && ru.ActionInfoID == actionId, out rowCount).FirstOrDefault();
            return Content(R_UserInfo_ActionInfoBll.DeleteEntity(r_userInfo_ActionInfo) ? "success" : "failed");
        }

        #endregion

       

    }
}
