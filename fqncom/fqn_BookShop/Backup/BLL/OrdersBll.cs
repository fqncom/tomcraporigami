using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using MyBookShop.Model;
namespace MyBookShop.BLL
{
	/// <summary>
	/// OrdersBll
	/// </summary>
	public partial class OrdersBll
	{
		private readonly MyBookShop.DAL.OrdersDal dal=new MyBookShop.DAL.OrdersDal();
		public OrdersBll()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrderId)
		{
			return dal.Exists(OrderId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(MyBookShop.Model.Orders model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyBookShop.Model.Orders model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string OrderId)
		{
			
			return dal.Delete(OrderId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string OrderIdlist )
		{
			return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(OrderIdlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyBookShop.Model.Orders GetModel(string OrderId)
		{
			
			return dal.GetModel(OrderId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public MyBookShop.Model.Orders GetModelByCache(string OrderId)
		{
			
			string CacheKey = "OrdersModel-" + OrderId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(OrderId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MyBookShop.Model.Orders)objModel;
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
		public List<MyBookShop.Model.Orders> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyBookShop.Model.Orders> DataTableToList(DataTable dt)
		{
			List<MyBookShop.Model.Orders> modelList = new List<MyBookShop.Model.Orders>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MyBookShop.Model.Orders model;
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

