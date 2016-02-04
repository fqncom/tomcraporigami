using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.IDal;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.Dal
{
    public class BaseSqlServerDal<T> : IBaseDal<T> where T : class,new()
    {

        private fqn_OAEntities Db = DbContextFactory.GetEfContextInstance();

        public T AddEntity(T t)
        {
            Db.Set<T>().Add(t);
            Db.SaveChanges();
            return t;
        }

        public bool DeleteEntity(T t)
        {
            Db.Entry<T>(t).State = System.Data.EntityState.Deleted;
            return true;
        }

        public bool UpdateEntity(T t)
        {
            Db.Entry<T>(t).State = System.Data.EntityState.Modified;
            return true;
        }

        public IQueryable<T> SelectEntities<TS>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, out int rowCount)
        {
            var temp = Db.Set<T>().Where<T>(whereLambda);
            rowCount = temp.Count();
            return temp;
        }

        public IQueryable<T> SelectPageEntities<TS>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TS>> orderByLambda, int rowSkip, int rowTake, out int rowCount,bool isAsc)
        {
            var temp = Db.Set<T>().Where<T>(whereLambda);
            rowCount = temp.Count();
            if (isAsc)
            {
                return temp.OrderBy<T, TS>(orderByLambda).Skip(rowSkip).Take(rowTake);
            }
            return temp.OrderByDescending<T, TS>(orderByLambda).Skip(rowSkip).Take(rowTake);
        }


        public int ExecuteNonQuerySql(string sql, params object[] obj)
        {
            return Db.Database.ExecuteSqlCommand(sql,obj);
        }

        public IEnumerable<T> ExecuteQuerySql<T>(string sql, params object[] obj)
        {
            return Db.Database.SqlQuery<T>(sql,obj);
        }
    }
}
