using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.DBUtility;

namespace MyBookShop.DAL
{
    public partial class BooksDal
    {
        /// <summary>
        /// 根据类型获取书籍
        /// </summary>
        /// <param name="pageStart"></param>
        /// <param name="pageEnd"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public DataSet GetModelListByPage(int pageStart, int pageEnd, int categoryId)
        {
            string sql = "select * from (select ROW_NUMBER()over(Order by id)as rowId,* from Books {0}) as newBook where newBook.rowId between @pageStart and @pageEnd";
            sql = string.Format(sql, categoryId > 0 ? "where categoryId = @categoryId" : "");
            SqlParameter[] paras =
            {
                new SqlParameter("@pageStart",pageStart),
                new SqlParameter("@pageEnd",pageEnd),
                new SqlParameter("@categoryId",categoryId)
            };
            return DbHelperSQL.Query(sql, paras);
        }

        /// <summary>
        /// 根据类型Id获取总数
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public object GetModelListCountByCategoryId(int categoryId)
        {
            string sql = "select COUNT(*) from Books {0}";
            sql = string.Format(sql, categoryId > 0 ? "where categoryId = @categoryId" : "");
            return DbHelperSQL.GetSingle(sql, new SqlParameter("@categoryId", categoryId));
        }



        public void GetModelListByCategoryId(int categoryId)
        {

        }


    }
}
