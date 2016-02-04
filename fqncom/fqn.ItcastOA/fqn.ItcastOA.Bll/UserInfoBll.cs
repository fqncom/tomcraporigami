using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.DalFactory;
using fqn.ItcastOA.IBll;
using fqn.ItcastOA.IDal;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;
using fqn.ItcastOA.Model.SearchModel;

namespace fqn.ItcastOA.Bll
{
    public partial class UserInfoBll : BaseBll<UserInfo>, IUserInfoBll//接口中有自定义的一些接口方法
    {

        //public override void SetCurrentDal()
        //{
        //    base.CurrentDal = base.GetDbSession.UserInfoDal;//base和this的区别
        //}

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntitiesLogical(List<int> list)
        {
            foreach (int uId in list)
            {
                int rowCount = 0;
                var user = base.GetDbSession.UserInfoDal.SelectEntities<UserInfo>(u => u.ID == uId, out rowCount).FirstOrDefault();
                user.DelFlag = (short)DeleteFlag.LogicalDeleted;
                base.GetDbSession.UserInfoDal.UpdateEntity(user);
            }
            return base.GetDbSession.SaveChanges() > 0;
        }

        public IQueryable<UserInfo> LoadSearchEntities(UserInfoParams pars, bool isAsc)
        {
            int rowCount = 0;
            var userInfoList =
                this.GetDbSession.UserInfoDal.SelectEntities<UserInfo>(u => u.DelFlag == (short)DeleteFlag.Normal, out rowCount);
            
            if (!string.IsNullOrEmpty(pars.UName))
            {
                userInfoList = userInfoList.Where<UserInfo>(u => u.UName.Contains(pars.UName));
            }
            if (!string.IsNullOrEmpty(pars.Remark))
            {
                userInfoList = userInfoList.Where<UserInfo>(u => u.Remark.Contains(pars.Remark));
            }
            pars.RowCount = userInfoList.Count();
            if (isAsc)
            {
                return userInfoList.OrderBy(u => u.ID).Skip(pars.RowSkip).Take(pars.RowTake);
            }
            return userInfoList.OrderByDescending(u => u.ID).Skip(pars.RowSkip).Take(pars.RowTake);
        }

        public bool UpdateUserRoleInfo(int userId, List<int> list)
        {
            int rowCount = 0;
            UserInfo userInfo =
                this.GetDbSession.UserInfoDal.SelectEntities<UserInfo>(u => u.ID == userId, out rowCount)
                    .FirstOrDefault();
            if (userInfo != null)
            {
                userInfo.RoleInfo.Clear();
                foreach (int roleId in list)
                {
                    RoleInfo roleInfo = this.GetDbSession.RoleInfoDal.SelectEntities<RoleInfo>(r => r.ID == roleId, out rowCount)
                         .FirstOrDefault();
                    userInfo.RoleInfo.Add(roleInfo);
                }
            }
            return this.GetDbSession.SaveChanges() > 0;
        }
    }
}
