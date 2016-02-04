using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace MyBookShop.DAL
{
	/// <summary>
	/// 数据访问类:OrdersDal
	/// </summary>
	public partial class OrdersDal
	{
		public OrdersDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrderId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Orders");
			strSql.Append(" where OrderId=@OrderId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.NVarChar,50)			};
			parameters[0].Value = OrderId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(MyBookShop.Model.Orders model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Orders(");
			strSql.Append("OrderId,OrderDate,UserId,TotalPrice,PostAddress,state)");
			strSql.Append(" values (");
			strSql.Append("@OrderId,@OrderDate,@UserId,@TotalPrice,@PostAddress,@state)");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@PostAddress", SqlDbType.NVarChar,255),
					new SqlParameter("@state", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderId;
			parameters[1].Value = model.OrderDate;
			parameters[2].Value = model.UserId;
			parameters[3].Value = model.TotalPrice;
			parameters[4].Value = model.PostAddress;
			parameters[5].Value = model.state;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyBookShop.Model.Orders model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Orders set ");
			strSql.Append("OrderDate=@OrderDate,");
			strSql.Append("UserId=@UserId,");
			strSql.Append("TotalPrice=@TotalPrice,");
			strSql.Append("PostAddress=@PostAddress,");
			strSql.Append("state=@state");
			strSql.Append(" where OrderId=@OrderId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@PostAddress", SqlDbType.NVarChar,255),
					new SqlParameter("@state", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.OrderDate;
			parameters[1].Value = model.UserId;
			parameters[2].Value = model.TotalPrice;
			parameters[3].Value = model.PostAddress;
			parameters[4].Value = model.state;
			parameters[5].Value = model.OrderId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string OrderId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Orders ");
			strSql.Append(" where OrderId=@OrderId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.NVarChar,50)			};
			parameters[0].Value = OrderId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string OrderIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Orders ");
			strSql.Append(" where OrderId in ("+OrderIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyBookShop.Model.Orders GetModel(string OrderId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 OrderId,OrderDate,UserId,TotalPrice,PostAddress,state from Orders ");
			strSql.Append(" where OrderId=@OrderId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.NVarChar,50)			};
			parameters[0].Value = OrderId;

			MyBookShop.Model.Orders model=new MyBookShop.Model.Orders();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyBookShop.Model.Orders DataRowToModel(DataRow row)
		{
			MyBookShop.Model.Orders model=new MyBookShop.Model.Orders();
			if (row != null)
			{
				if(row["OrderId"]!=null)
				{
					model.OrderId=row["OrderId"].ToString();
				}
				if(row["OrderDate"]!=null && row["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(row["OrderDate"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["TotalPrice"]!=null && row["TotalPrice"].ToString()!="")
				{
					model.TotalPrice=decimal.Parse(row["TotalPrice"].ToString());
				}
				if(row["PostAddress"]!=null)
				{
					model.PostAddress=row["PostAddress"].ToString();
				}
				if(row["state"]!=null && row["state"].ToString()!="")
				{
					model.state=int.Parse(row["state"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderId,OrderDate,UserId,TotalPrice,PostAddress,state ");
			strSql.Append(" FROM Orders ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" OrderId,OrderDate,UserId,TotalPrice,PostAddress,state ");
			strSql.Append(" FROM Orders ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Orders ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.OrderId desc");
			}
			strSql.Append(")AS Row, T.*  from Orders T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Orders";
			parameters[1].Value = "OrderId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

