using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.SearchModel;

namespace fqn.ItcastOA.IBll
{
    public partial interface IUserInfoBll : IBaseBll<UserInfo>
    {
        bool DeleteEntitiesLogical(List<int> list);
        IQueryable<UserInfo> LoadSearchEntities(UserInfoParams pars, bool isAsc);

        bool UpdateUserRoleInfo(int userId, List<int> list);
    }
}
