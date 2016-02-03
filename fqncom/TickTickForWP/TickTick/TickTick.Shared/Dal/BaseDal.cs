using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using Windows.Storage;

namespace TickTick.Dal
{
    public class BaseDal<T> : IBaseDal<T> where T : class,new()
    {

        #region IBaseDal<T> 成员

        public async Task<SQLiteAsyncConnection> CreateTableAsync()
        {
            var conn = App.Connection;//new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
            //await conn.CreateTableAsync<T>();
            return conn;
        }

        public async Task<List<TResult>> ExecuteNonQuery<TResult>(string sql, params object[] paras) where TResult : class,new()
        {
            var conn = await CreateTableAsync();
            return await conn.QueryAsync<TResult>(sql, paras);
        }

        public async Task<AsyncTableQuery<T>> ExecuteAsyncQueryTable()
        {
            var conn = await CreateTableAsync();
            return conn.Table<T>();
        }

        public async Task<List<T>> ExecuteTable()
        {
            var result = await (await ExecuteAsyncQueryTable()).ToListAsync();
            return result;
            //var conn = await CreateTableAsync();
            //var queryResult = await conn.Table<T>().ToListAsync();
            //return queryResult;
        }

        public async Task<int> DeleteData(T t)
        {
            var conn = await CreateTableAsync();
            return await conn.DeleteAsync(t);
        }

        public async Task<int> InsertAsync(T t)
        {
            var conn = await CreateTableAsync();
            return await conn.InsertAsync(t);
        }

        public async Task<int> InsertAllAsync(List<T> t)
        {
            var conn = await CreateTableAsync();
            return await conn.InsertAllAsync(t);
        }

        public async Task<int> UpdateAsync(T t)
        {
            var conn = await CreateTableAsync();
            return await conn.UpdateAsync(t);
        }

        public async Task<int> UpdateAllAsync(List<T> t)
        {
            var conn = await CreateTableAsync();
            return await conn.UpdateAllAsync(t);
        }

        public async Task<int> DropTable()
        {
            var conn = await CreateTableAsync();
            return await conn.DropTableAsync<T>();
        }
        public async Task<List<T>> GetDataTableByExpression(Expression<Func<T, bool>> whereExpr, Expression<Func<T, object>> orderByExpr)
        {   
            var queryResult = await ExecuteAsyncQueryTable();
            if (whereExpr != null)
            {
                queryResult = queryResult.Where(whereExpr);
            }
            if (orderByExpr != null)
            {
                queryResult = queryResult.OrderBy(orderByExpr);
            }
            return await queryResult.ToListAsync();
        }
        #endregion

        #region 自定义成员
        //public async Task<List<T>> GetAllTasksByDelFlag(int delStatus)
        //{
        //    var queryResult = (await ExecuteTable()) as List<BaseEntity>;

        //    return (from q in queryResult
        //            where q.Deleted == delStatus
        //            select q).ToList() as List<T>;
        //}



        #endregion
    }
}
