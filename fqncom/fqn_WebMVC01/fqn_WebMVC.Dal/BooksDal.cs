using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using fqn_WebMVC.IDal;
using fqn_WebMVC.Model;

namespace fqn_WebMVC.Dal
{
    public partial class BooksDal : IBooksDal
    {
        private Book_ShopEntities bse = CommonHelper.CheckEntitiesExistOrCreate();
        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>返回刚插入数据的ID</returns>
        public int Add(Books obj)
        {
            bse.Books.Add(obj);
            bse.SaveChanges();
            return obj.Id;
        }

        public int Delete(string whereStr)
        {
            throw new NotImplementedException();
        }

        public int Update(Books obj, string whereStr)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public Books Select(string whereStr)
        {
            if (string.IsNullOrEmpty(whereStr))
            {
                var books = from u in bse.Books
                            where u.Id != 0
                            select u;
                foreach (Books books1 in books)
                {

                }
                return books.FirstOrDefault();
            }
            else
            {
                return bse.Books.SqlQuery("select * from Books " + whereStr).FirstOrDefault();
            }
        }
    }
}
