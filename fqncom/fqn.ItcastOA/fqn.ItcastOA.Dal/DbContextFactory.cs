using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.Dal
{
    public static class DbContextFactory
    {
        public static fqn_OAEntities GetEfContextInstance()
        {
            fqn_OAEntities db = CallContext.GetData("fqn_Db") as fqn_OAEntities;
            if (db == null)
            {
                db = new fqn_OAEntities();
                CallContext.SetData("fqn_Db", db);
            }
            return db;
        }

    }
}
