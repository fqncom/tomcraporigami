using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn_AbstractFactoryDemo.IDal;
using fqn_AbstractFactoryDemo.Model;

namespace fqn_AbstractFactoryDemo.Bll
{
    public class UserInfoBll
    {
        private IUserInfoDal userInfoDal = Factory.UserInfoFactory.GetUserInfoDal();

        public List<UserInfo> GetUserInfoList()
        {
            return userInfoDal.GetUserList();
        }

    }
}
