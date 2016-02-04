using System;
namespace MyBookShop.Model
{
	
		/// <summary>
	/// BookComment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BookComment
	{
		public BookComment()
		{}
		#region Model
		private int _id;
		private string _msg;
		private DateTime _createdatetime;
		private int _bookid;
		private int? _parentid;
		private int? _floorindexid;
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
		public string Msg
		{
			set{ _msg=value;}
			get{return _msg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDateTime
		{
			set{ _createdatetime=value;}
			get{return _createdatetime;}
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
		public int? parentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? floorIndexId
		{
			set{ _floorindexid=value;}
			get{return _floorindexid;}
		}
		#endregion Model

	}
}

