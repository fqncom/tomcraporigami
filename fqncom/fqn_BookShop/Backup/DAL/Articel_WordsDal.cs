using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace MyBookShop.DAL
{
	/// <summary>
	/// 数据访问类:Articel_WordsDal
	/// </summary>
	public partial class Articel_WordsDal
	{
		public Articel_WordsDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "Articel_Words"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Articel_Words");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(MyBookShop.Model.Articel_Words model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Articel_Words(");
			strSql.Append("WordPattern,IsForbid,IsMod,ReplaceWord)");
			strSql.Append(" values (");
			strSql.Append("@WordPattern,@IsForbid,@IsMod,@ReplaceWord)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@WordPattern", SqlDbType.NVarChar,50),
					new SqlParameter("@IsForbid", SqlDbType.Bit,1),
					new SqlParameter("@IsMod", SqlDbType.Bit,1),
					new SqlParameter("@ReplaceWord", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.WordPattern;
			parameters[1].Value = model.IsForbid;
			parameters[2].Value = model.IsMod;
			parameters[3].Value = model.ReplaceWord;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(MyBookShop.Model.Articel_Words model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Articel_Words set ");
			strSql.Append("WordPattern=@WordPattern,");
			strSql.Append("IsForbid=@IsForbid,");
			strSql.Append("IsMod=@IsMod,");
			strSql.Append("ReplaceWord=@ReplaceWord");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@WordPattern", SqlDbType.NVarChar,50),
					new SqlParameter("@IsForbid", SqlDbType.Bit,1),
					new SqlParameter("@IsMod", SqlDbType.Bit,1),
					new SqlParameter("@ReplaceWord", SqlDbType.NVarChar,50),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.WordPattern;
			parameters[1].Value = model.IsForbid;
			parameters[2].Value = model.IsMod;
			parameters[3].Value = model.ReplaceWord;
			parameters[4].Value = model.Id;

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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Articel_Words ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Articel_Words ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public MyBookShop.Model.Articel_Words GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,WordPattern,IsForbid,IsMod,ReplaceWord from Articel_Words ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			MyBookShop.Model.Articel_Words model=new MyBookShop.Model.Articel_Words();
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
		public MyBookShop.Model.Articel_Words DataRowToModel(DataRow row)
		{
			MyBookShop.Model.Articel_Words model=new MyBookShop.Model.Articel_Words();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["WordPattern"]!=null)
				{
					model.WordPattern=row["WordPattern"].ToString();
				}
				if(row["IsForbid"]!=null && row["IsForbid"].ToString()!="")
				{
					if((row["IsForbid"].ToString()=="1")||(row["IsForbid"].ToString().ToLower()=="true"))
					{
						model.IsForbid=true;
					}
					else
					{
						model.IsForbid=false;
					}
				}
				if(row["IsMod"]!=null && row["IsMod"].ToString()!="")
				{
					if((row["IsMod"].ToString()=="1")||(row["IsMod"].ToString().ToLower()=="true"))
					{
						model.IsMod=true;
					}
					else
					{
						model.IsMod=false;
					}
				}
				if(row["ReplaceWord"]!=null)
				{
					model.ReplaceWord=row["ReplaceWord"].ToString();
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
			strSql.Append("select Id,WordPattern,IsForbid,IsMod,ReplaceWord ");
			strSql.Append(" FROM Articel_Words ");
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
			strSql.Append(" Id,WordPattern,IsForbid,IsMod,ReplaceWord ");
			strSql.Append(" FROM Articel_Words ");
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
			strSql.Append("select count(1) FROM Articel_Words ");
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
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from Articel_Words T ");
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
			parameters[0].Value = "Articel_Words";
			parameters[1].Value = "Id";
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

