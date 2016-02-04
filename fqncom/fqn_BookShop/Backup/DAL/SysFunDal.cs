using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace MyBookShop.DAL
{
	/// <summary>
	/// 数据访问类:SysFunDal
	/// </summary>
	public partial class SysFunDal
	{
		public SysFunDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("NodeId", "SysFun"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int NodeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SysFun");
			strSql.Append(" where NodeId=@NodeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@NodeId", SqlDbType.Int,4)			};
			parameters[0].Value = NodeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(MyBookShop.Model.SysFun model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SysFun(");
			strSql.Append("NodeId,DisplayName,NodeURL,DisplayOrder,ParentNodeId)");
			strSql.Append(" values (");
			strSql.Append("@NodeId,@DisplayName,@NodeURL,@DisplayOrder,@ParentNodeId)");
			SqlParameter[] parameters = {
					new SqlParameter("@NodeId", SqlDbType.Int,4),
					new SqlParameter("@DisplayName", SqlDbType.NVarChar,50),
					new SqlParameter("@NodeURL", SqlDbType.NVarChar,50),
					new SqlParameter("@DisplayOrder", SqlDbType.Int,4),
					new SqlParameter("@ParentNodeId", SqlDbType.Int,4)};
			parameters[0].Value = model.NodeId;
			parameters[1].Value = model.DisplayName;
			parameters[2].Value = model.NodeURL;
			parameters[3].Value = model.DisplayOrder;
			parameters[4].Value = model.ParentNodeId;

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
		public bool Update(MyBookShop.Model.SysFun model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SysFun set ");
			strSql.Append("DisplayName=@DisplayName,");
			strSql.Append("NodeURL=@NodeURL,");
			strSql.Append("DisplayOrder=@DisplayOrder,");
			strSql.Append("ParentNodeId=@ParentNodeId");
			strSql.Append(" where NodeId=@NodeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@DisplayName", SqlDbType.NVarChar,50),
					new SqlParameter("@NodeURL", SqlDbType.NVarChar,50),
					new SqlParameter("@DisplayOrder", SqlDbType.Int,4),
					new SqlParameter("@ParentNodeId", SqlDbType.Int,4),
					new SqlParameter("@NodeId", SqlDbType.Int,4)};
			parameters[0].Value = model.DisplayName;
			parameters[1].Value = model.NodeURL;
			parameters[2].Value = model.DisplayOrder;
			parameters[3].Value = model.ParentNodeId;
			parameters[4].Value = model.NodeId;

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
		public bool Delete(int NodeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SysFun ");
			strSql.Append(" where NodeId=@NodeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@NodeId", SqlDbType.Int,4)			};
			parameters[0].Value = NodeId;

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
		public bool DeleteList(string NodeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SysFun ");
			strSql.Append(" where NodeId in ("+NodeIdlist + ")  ");
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
		public MyBookShop.Model.SysFun GetModel(int NodeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 NodeId,DisplayName,NodeURL,DisplayOrder,ParentNodeId from SysFun ");
			strSql.Append(" where NodeId=@NodeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@NodeId", SqlDbType.Int,4)			};
			parameters[0].Value = NodeId;

			MyBookShop.Model.SysFun model=new MyBookShop.Model.SysFun();
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
		public MyBookShop.Model.SysFun DataRowToModel(DataRow row)
		{
			MyBookShop.Model.SysFun model=new MyBookShop.Model.SysFun();
			if (row != null)
			{
				if(row["NodeId"]!=null && row["NodeId"].ToString()!="")
				{
					model.NodeId=int.Parse(row["NodeId"].ToString());
				}
				if(row["DisplayName"]!=null)
				{
					model.DisplayName=row["DisplayName"].ToString();
				}
				if(row["NodeURL"]!=null)
				{
					model.NodeURL=row["NodeURL"].ToString();
				}
				if(row["DisplayOrder"]!=null && row["DisplayOrder"].ToString()!="")
				{
					model.DisplayOrder=int.Parse(row["DisplayOrder"].ToString());
				}
				if(row["ParentNodeId"]!=null && row["ParentNodeId"].ToString()!="")
				{
					model.ParentNodeId=int.Parse(row["ParentNodeId"].ToString());
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
			strSql.Append("select NodeId,DisplayName,NodeURL,DisplayOrder,ParentNodeId ");
			strSql.Append(" FROM SysFun ");
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
			strSql.Append(" NodeId,DisplayName,NodeURL,DisplayOrder,ParentNodeId ");
			strSql.Append(" FROM SysFun ");
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
			strSql.Append("select count(1) FROM SysFun ");
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
				strSql.Append("order by T.NodeId desc");
			}
			strSql.Append(")AS Row, T.*  from SysFun T ");
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
			parameters[0].Value = "SysFun";
			parameters[1].Value = "NodeId";
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

