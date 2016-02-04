using System;
namespace MyBookShop.Model
{
	/// <summary>
	/// Orders:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Orders
	{
		public Orders()
		{}
		#region Model
		private string _orderid;
		private DateTime _orderdate;
		private int _userid;
		private decimal _totalprice;
		private string _postaddress;
		private int _state=0;
		/// <summary>
		/// 
		/// </summary>
		public string OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime OrderDate
		{
			set{ _orderdate=value;}
			get{return _orderdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal TotalPrice
		{
			set{ _totalprice=value;}
			get{return _totalprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PostAddress
		{
			set{ _postaddress=value;}
			get{return _postaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int state
		{
			set{ _state=value;}
			get{return _state;}
		}
		#endregion Model

	}
}

