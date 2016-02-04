using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShop.BLL
{
    public partial class BooksBll
    {
        /// <summary>
        /// 根据分页信息，返回查询结果
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Model.Books> GetModelListByPage(int pageIndex, int pageSize, int categoryId)
        {
            int pageStart = (pageIndex - 1) * pageSize + 1;
            int pageEnd = pageIndex * pageSize;
            return this.DataTableToList(dal.GetModelListByPage(pageStart, pageEnd, categoryId).Tables[0]);
        }

        /// <summary>
        /// 根据类型Id获取总数
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetPageCountByCategoryId(int categoryId, int pageSize)
        {
            int rowCount = Convert.ToInt32(dal.GetModelListCountByCategoryId(categoryId));
            return Convert.ToInt32(Math.Ceiling(rowCount * 1.0 / pageSize));
        }
    }
}
