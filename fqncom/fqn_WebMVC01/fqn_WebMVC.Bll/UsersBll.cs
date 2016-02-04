using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn_WebMVC.IBll;
using fqn_WebMVC.IDal;
using fqn_WebMVC.Model;

namespace fqn_WebMVC.Bll
{
    public partial class UsersBll : IUsersBll
    {
        private IUsersDal usersDal = FactoryDal.AbstractFactory.CreateUsersInstance();
        public int Add(Model.Users obj)
        {
            return usersDal.Add(obj);
        }

        public int Delete(string whereStr)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.Users obj, string whereStr)
        {
            throw new NotImplementedException();
        }

        public Users Select(string whereStr)
        {
            return usersDal.Select("");
        }
    }
}
