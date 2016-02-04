using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using MyBookShop.Model;
namespace MyBookShop.BLL
{
	/// <summary>
	/// SysFunBll
	/// </summary>
	public partial class SysFunBll
	{
		private readonly MyBookShop.DAL.SysFunDal dal=new MyBookShop.DAL.SysFunDal();
		public SysFunBll()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int NodeId)
		{
			return dal.Exists(NodeId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(MyBookShop.Model.SysFun model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyBookShop.Model.SysFun model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int NodeId)
		{
			
			return dal.Delete(NodeId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string NodeIdlist )
		{
			return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(NodeIdlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyBookShop.Model.SysFun GetModel(int NodeId)
		{
			
			return dal.GetModel(NodeId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public MyBookShop.Model.SysFun GetModelByCache(int NodeId)
		{
			
			string CacheKey = "SysFunModel-" + NodeId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(NodeId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MyBookShop.Model.SysFun)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyBookShop.Model.SysFun> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyBookShop.Model.SysFun> DataTableToList(DataTable dt)
		{
			List<MyBookShop.Model.SysFun> modelList = new List<MyBookShop.Model.SysFun>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MyBookShop.Model.SysFun model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

