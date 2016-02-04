using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShop.BLL
{
    public partial class CartBll
    {
        private BLL.UsersBll usersBll = new UsersBll();
        private BooksBll booksBll = new BooksBll();

        public MyBookShop.Model.Cart GetModel(int bookId, int userId)
        {
            return dal.GetModel(bookId, userId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add2(MyBookShop.Model.Cart model)
        {
            return dal.Add2(model);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MyBookShop.Model.Cart> GetModelList2(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList2(ds.Tables[0]);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update2(MyBookShop.Model.Cart model)
        {
            return dal.Update2(model);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MyBookShop.Model.Cart GetModel2(int Id)
        {
            return dal.GetModel2(Id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MyBookShop.Model.Cart> DataTableToList2(DataTable dt)
        {
            List<MyBookShop.Model.Cart> modelList = new List<MyBookShop.Model.Cart>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MyBookShop.Model.Cart model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MyBookShop.Model.Cart();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["UserId"].ToString() != "")
                    {
                        int UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                        model.User = usersBll.GetModel(UserId);
                    }
                    if (dt.Rows[n]["BookId"].ToString() != "")
                    {
                        int BookId = int.Parse(dt.Rows[n]["BookId"].ToString());
                        model.Book = booksBll.GetModel(BookId);
                    }
                    if (dt.Rows[n]["Count"].ToString() != "")
                    {
                        model.Count = int.Parse(dt.Rows[n]["Count"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }


}
