using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.DalFactory;
using fqn.ItcastOA.IBll;
using fqn.ItcastOA.IDal;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.Bll
{
    public abstract class BaseBll<T> : IBaseBll<T> where T : class,new()
    {

        public BaseBll()
        {
            this.SetCurrentDal();
        }

        public IDbSession GetDbSession
        {
            get { return DbSessionFactory.CreateDbSession(); }
        }

        public IBaseDal<T> CurrentDal { get; set; }

        public abstract void SetCurrentDal();

        public T AddEntity(T t)
        {
            t = this.CurrentDal.AddEntity(t);
            this.GetDbSession.SaveChanges();
            return t;
        }

        public bool DeleteEntity(T t)
        {
            this.CurrentDal.DeleteEntity(t);
            return this.GetDbSession.SaveChanges() > 0;
        }

        public bool UpdateEntity(T t)
        {
            this.CurrentDal.UpdateEntity(t);
            return GetDbSession.SaveChanges() > 0;
        }

        public IQueryable<T> SelectEntities<TS>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            out int rowCount)
        {
            return this.CurrentDal.SelectEntities<TS>(whereLambda, out rowCount);
        }

        public IQueryable<T> SelectPageEntities<TS>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, TS>> orderByLambda, int rowSkip, int rowTake, out int rowCount,
            bool isAsc)
        {
            return this.CurrentDal.SelectPageEntities(whereLambda, orderByLambda, rowSkip, rowTake, out rowCount, isAsc);
        }


        public int ExecuteNonQuerySql(string sql, params object[] obj)
        {
            return this.CurrentDal.ExecuteNonQuerySql(sql, obj);
        }

        public IEnumerable<T> ExecuteQuerySql<T>(string sql, params object[] obj)
        {
            return this.CurrentDal.ExecuteQuerySql<T>(sql, obj);
        }
    }
}
