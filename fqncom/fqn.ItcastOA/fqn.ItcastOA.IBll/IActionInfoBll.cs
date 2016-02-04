using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.IBll
{
    public partial interface IActionInfoBll : IBaseBll<ActionInfo>
    {

        bool UpdateActionRoleInfo(int actionId, List<int> list);
    }
}
