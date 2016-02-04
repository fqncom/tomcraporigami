using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.ItcastOA.IDal
{
   public partial interface IDbSession
    {
       int SaveChanges();
    }
}
