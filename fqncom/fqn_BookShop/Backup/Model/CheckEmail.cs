using System;
namespace MyBookShop.Model
{
	/// <summary>
	/// CheckEmail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CheckEmail
	{
		public CheckEmail()
		{}
		#region Model
		private int _id;
		private bool _actived;
		private string _activecode;
		private int? _userid;
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
		public bool Actived
		{
			set{ _actived=value;}
			get{return _actived;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActiveCode
		{
			set{ _activecode=value;}
			get{return _activecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		#endregion Model

	}
}

