using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn_AbstractFactoryDemo.Model;

namespace fqn_AbstractFactoryDemo.IDal
{
   public  interface IUserInfoDal
   {

       List<UserInfo> GetUserList();
   }
}
