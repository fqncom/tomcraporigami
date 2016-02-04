using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.IBll;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.Bll
{
    public partial class ActionInfoBll : BaseBll<ActionInfo>, IActionInfoBll
    {

        public bool UpdateActionRoleInfo(int actionId, List<int> list)
        {
            int rowCount = 0;
            ActionInfo actionInfo =
                this.GetDbSession.ActionInfoDal.SelectEntities<ActionInfo>(a => a.ID == actionId, out rowCount)
                    .FirstOrDefault();
            if (actionInfo != null)
            {
                actionInfo.RoleInfo.Clear();
                foreach (int roleId in list)
                {
                    RoleInfo roleInfo = this.GetDbSession.RoleInfoDal.SelectEntities<RoleInfo>(r => r.ID == roleId, out rowCount)
                         .FirstOrDefault();
                    actionInfo.RoleInfo.Add(roleInfo);
                }
            }
            return this.GetDbSession.SaveChanges() > 0;
        }
    }
}
