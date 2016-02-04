using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.ItcastOA.IDal
{
    public interface IBaseDal<T> where T : class,new()
    {
        T AddEntity(T t);

        bool DeleteEntity(T t);

        bool UpdateEntity(T t);

        IQueryable<T> SelectEntities<TS>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, out int rowCount);

        IQueryable<T> SelectPageEntities<TS>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, TS>> orderByLambda, int rowSkip, int rowTake, out int rowCount, bool isAsc);

        int ExecuteNonQuerySql(string sql, params object[] obj);

        IEnumerable<T> ExecuteQuerySql<T>(string sql, params object[] obj);

    }
}
