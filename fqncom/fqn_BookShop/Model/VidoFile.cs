using System;
namespace MyBookShop.Model
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class VidoFile
	{
		public VidoFile()
		{}
		#region Model
		private int _id;
		private string _title;
		private string _fivpath;
		private string _status;
		private string _fileext;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FivPath
		{
			set{ _fivpath=value;}
			get{return _fivpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileExt
		{
			set{ _fileext=value;}
			get{return _fileext;}
		}
		#endregion Model

	}
}

