using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn_WebMVC.IDal;
using fqn_WebMVC.Model;

namespace fqn_WebMVC.Dal
{
    public partial class UsersDal : IUsersDal
    {
        private Book_ShopEntities bse = CommonHelper.CheckEntitiesExistOrCreate();

        public int Add(Model.Users obj)
        {
            bse.Users.Add(obj);
            bse.SaveChanges();
            return obj.Id;
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
            if (string.IsNullOrEmpty(whereStr))
            {
                var user = from u in bse.Users
                           where u.Id != 0
                           select u;
                return user.FirstOrDefault();
            }
            else
            {
                return bse.Users.SqlQuery("select * from Users " + whereStr).FirstOrDefault();
            }

        }
    }
}
