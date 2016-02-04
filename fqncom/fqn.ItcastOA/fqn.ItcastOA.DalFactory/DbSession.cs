using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Dal;
using fqn.ItcastOA.IDal;

namespace fqn.ItcastOA.DalFactory
{
    public partial class DbSession : IDbSession
    {

        public DbContext DbContext
        {
            get { return DbContextFactory.GetEfContextInstance(); }
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
    }
}
