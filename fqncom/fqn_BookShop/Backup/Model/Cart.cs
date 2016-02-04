using System;
namespace MyBookShop.Model
{
	/// <summary>
	/// Cart:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Cart
	{
		public Cart()
		{}
		#region Model
		private int _id;
		private int _userid;
		private int _bookid;
		private int _count;
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
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BookId
		{
			set{ _bookid=value;}
			get{return _bookid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		#endregion Model

	}
}

