using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.IDal;

namespace fqn.ItcastOA.DalFactory
{
    public static class DbSessionFactory
    {

        public static IDbSession CreateDbSession()
        {
            IDbSession db = CallContext.GetData("dbSession") as DbSession;
            if (db == null)
            {
                db = new DbSession();
                CallContext.SetData("dbSession", db);
            }
            return db;
        }
    }
}
