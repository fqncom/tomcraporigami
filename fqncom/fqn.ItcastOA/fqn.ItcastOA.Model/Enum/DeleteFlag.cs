using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.ItcastOA.Model.Enum
{
    public enum DeleteFlag
    {
        Normal = 0,//未删除
        LogicalDeleted = 1,//逻辑删除
        PhysicalDeleted = 2//彻底删除1
    }
}
