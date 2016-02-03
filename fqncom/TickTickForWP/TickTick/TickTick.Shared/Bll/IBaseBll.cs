using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TickTick.Bll
{
    public interface IBaseBll<T> where T : class,new()
    {
        /// <summary>
        /// 基本查询
        /// </summary>
        /// <returns>返回查询的结果</returns>
        //Task<List<T>> ExecuteNonQuery(string sql, params object[] paras);

       
        /// <summary>
        /// 返回指定表的所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ExecuteTable();

        /// <summary>
        /// 直接删除传入的项
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> DeleteData(T t);

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

        Task<int> DropTable();
    }
}
