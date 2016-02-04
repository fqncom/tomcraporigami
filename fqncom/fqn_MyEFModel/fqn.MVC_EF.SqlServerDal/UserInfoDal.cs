using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.MVC_EF.CommonDal;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.SqlServerDal
{
    public class UserInfoDal : IUserInfoDal
    {
        private fqnModelContainer db = CommonHelper.CheckDbModel2AndGet();
        public int Add(UserInfo t)
        {
            db.UserInfo.Add(t);
            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(UserInfo t)
        {
            throw new NotImplementedException();
        }

        public UserInfo Select(int id)
        {
            throw new NotImplementedException();
        }

        //以下代码随便写的，就是为了练习一下那几个方法
        public List<UserInfo> SelectList(int rowSkip, int rowTake)
        {
            var userInfoList = (from u in db.UserInfo
                                where u.Id > 0
                                select u).Where<UserInfo>(u => u.Id > 0).OrderBy<UserInfo, int>(u => u.Id).Skip(rowSkip).Take(rowTake).AsEnumerable().ToList();
            return userInfoList;
        }

        public int SelectCount()
        {
            return db.UserInfo.Count(u => u.Id > 0);
        }
    }
}
