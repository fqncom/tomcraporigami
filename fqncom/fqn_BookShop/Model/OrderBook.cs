using System;
namespace MyBookShop.Model
{
	/// <summary>
	/// OrderBook:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderBook
	{
		public OrderBook()
		{}
		#region Model
		private int _id;
		private string _orderid;
		private int _bookid;
		private int _quantity;
		private decimal _unitprice;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BookID
		{
			set{ _bookid=value;}
			get{return _bookid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal UnitPrice
		{
			set{ _unitprice=value;}
			get{return _unitprice;}
		}
		#endregion Model

	}
}

