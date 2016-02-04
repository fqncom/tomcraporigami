using System;
namespace MyBookShop.Model
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class UserStates
	{
		public UserStates()
		{}
		#region Model
		private int _id;
		private string _name;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		#endregion Model

	}
}

