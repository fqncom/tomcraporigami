using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn_WebMVC.IBll
{
    public interface IBaseBll<T> where T :class,new()
    {
        /// <summary>
        /// 根据条件进行增加
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        int Add(T obj);

        /// <summary>
        /// 根据条件进行删除
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        int Delete(string whereStr);

        /// <summary>
        /// 根据条件进行更新
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        int Update(T obj, string whereStr);

        /// <summary>
        /// 根据条件进行选择
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        T Select(string whereStr);
    }
}
