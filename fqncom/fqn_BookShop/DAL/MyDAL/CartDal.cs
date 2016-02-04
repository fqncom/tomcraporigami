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
    public partial class CartDal
    {
        UsersDal userDal = new UsersDal();
        BooksDal bookDal = new BooksDal();

        ///// <summary>
        ///// 得到一个对象实体,重载
        ///// </summary>
        //public Model.Cart GetModel(int bookId,int userId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select Id,UserId,BookId,Count from Cart where BookId = @BookId and UserId = @UserId");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@BookId", SqlDbType.Int,4),
        //            new SqlParameter("@UserId", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = bookId;
        //    parameters[1].Value = userId;

        //    Model.Cart model = new Model.Cart();
        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return DataRowToModel(ds.Tables[0].Rows[0]);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Cart GetModel2(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserId,BookId,Count from Cart ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            Model.Cart model = new Model.Cart();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    int UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                    model.User = userDal.GetModel(UserId);
                }
                if (ds.Tables[0].Rows[0]["BookId"].ToString() != "")
                {
                    int BookId = int.Parse(ds.Tables[0].Rows[0]["BookId"].ToString());
                    model.Book = bookDal.GetModel(BookId);
                }
                if (ds.Tables[0].Rows[0]["Count"].ToString() != "")
                {
                    model.Count = int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add2(MyBookShop.Model.Cart model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Cart(");
            strSql.Append("UserId,BookId,Count)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@BookId,@Count)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", SqlDbType.Int, 4),
                new SqlParameter("@BookId", SqlDbType.Int, 4),
                new SqlParameter("@Count", SqlDbType.Int, 4)
            };
            parameters[0].Value = model.User.Id;
            parameters[1].Value = model.Book.Id;
            parameters[2].Value = model.Count;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update2(MyBookShop.Model.Cart model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Cart set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("BookId=@BookId,");
            strSql.Append("Count=@Count");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@BookId", SqlDbType.Int,4),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.User.Id;
            parameters[1].Value = model.Book.Id;
            parameters[2].Value = model.Count;
            parameters[3].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.Cart GetModel(int bookId, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserId,BookId,Count from Cart ");
            strSql.Append(" where UserId=@UserId and  BookId=@BookId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                          new SqlParameter("@BookId", SqlDbType.Int,4)              
                                        };
            parameters[0].Value = userId;
            parameters[1].Value = bookId;

            Model.Cart model = new Model.Cart();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    int UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                    model.User = userDal.GetModel(UserId);
                }
                if (ds.Tables[0].Rows[0]["BookId"].ToString() != "")
                {
                    int BookId = int.Parse(ds.Tables[0].Rows[0]["BookId"].ToString());
                    model.Book = bookDal.GetModel(BookId);
                }
                if (ds.Tables[0].Rows[0]["Count"].ToString() != "")
                {
                    model.Count = int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
