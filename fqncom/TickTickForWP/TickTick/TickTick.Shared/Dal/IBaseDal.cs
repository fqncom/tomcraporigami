using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TickTick.Dal
{
    public interface IBaseDal<T> where T : class,new()
    {

        /// <summary>
        /// 创建数据表，存在则不创建而是打开
        /// </summary>
        /// <returns>返回连接对象</returns>
        Task<SQLiteAsyncConnection> CreateTableAsync();

        /// <summary>
        /// 基本查询
        /// </summary>
        /// <returns>返回查询的结果</returns>
        Task<List<TResult>> ExecuteNonQuery<TResult>(string sql, params object[] paras) where TResult : class,new();

        /// <summary>
        /// 返回指定表的asyncTableQuery数据，方便进行where语句的使用
        /// </summary>
        /// <returns></returns>
        Task<AsyncTableQuery<T>> ExecuteAsyncQueryTable();

        /// <summary>
        /// 返回指定表的所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ExecuteTable();

        /// <summary>
        /// 根据具体的需求，返回具体的内容
        /// </summary>
        /// <param name="whereExpr"></param>
        /// <param name="orderByExpr"></param>
        /// <returns></returns>
        Task<List<T>> GetDataTableByExpression(Expression<Func<T, bool>> whereExpr, Expression<Func<T, object>> orderByExpr);

        /// <summary>
        /// 直接删除传入的项
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> DeleteData(T t);

        #region 暂时废弃的数据库插入方法
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="t">插入数据类型</param>
        /// <returns>返回受影响的行数</returns>
        Task<int> InsertAsync(T t);

        /// <summary>
        /// 一次插入多条插入数据
        /// </summary>
        /// <param name="t">插入数据类型</param>
        /// <returns>返回受影响的行数</returns>
        Task<int> InsertAllAsync(List<T> t);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回受影响的行数</returns>
        Task<int> UpdateAsync(T t);

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回受影响的行数</returns>
        Task<int> UpdateAllAsync(List<T> t);
        #endregion

        Task<int> DropTable();
    }
}
