using System;
namespace MIIOT.Model
{
	/// <summary>
	/// pub_store_in_dtl:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class pub_store_in_dtl
	{
		public pub_store_in_dtl()
		{ }
		#region Model
		private long _id;
		private long _organ_id = 0;
		private long _store_in_id;
		private int _source_type = 0;
		private long _source_id = 0;
		private long _source_dtl_id = 0;
		private string _source_no;
		private string _origin_id;
		private string _origin_dtl_id;
		private long _goods_id;
		private string _goods_no;
		private int _qty = 0;
		private long _lot_id = 0;
		private string _lot_no;
		private long _batch_id = 0;
		private int _is_delete = 0;
		private long _creater_id = 0;
		private DateTime _gmt_create = DateTime.Now;
		private long _modifier_id = 0;
		private DateTime _gmt_modified = DateTime.Now;
		private string _single_code;
		private string _back_reason;
		/// <summary>
		/// 
		/// </summary>
		public long id
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long organ_id
		{
			set { _organ_id = value; }
			get { return _organ_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long store_in_id
		{
			set { _store_in_id = value; }
			get { return _store_in_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int source_type
		{
			set { _source_type = value; }
			get { return _source_type; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long source_id
		{
			set { _source_id = value; }
			get { return _source_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long source_dtl_id
		{
			set { _source_dtl_id = value; }
			get { return _source_dtl_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string source_no
		{
			set { _source_no = value; }
			get { return _source_no; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string origin_id
		{
			set { _origin_id = value; }
			get { return _origin_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string origin_dtl_id
		{
			set { _origin_dtl_id = value; }
			get { return _origin_dtl_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long goods_id
		{
			set { _goods_id = value; }
			get { return _goods_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string goods_no
		{
			set { _goods_no = value; }
			get { return _goods_no; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int qty
		{
			set { _qty = value; }
			get { return _qty; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long lot_id
		{
			set { _lot_id = value; }
			get { return _lot_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string lot_no
		{
			set { _lot_no = value; }
			get { return _lot_no; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long batch_id
		{
			set { _batch_id = value; }
			get { return _batch_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int is_delete
		{
			set { _is_delete = value; }
			get { return _is_delete; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long creater_id
		{
			set { _creater_id = value; }
			get { return _creater_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime gmt_create
		{
			set { _gmt_create = value; }
			get { return _gmt_create; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long modifier_id
		{
			set { _modifier_id = value; }
			get { return _modifier_id; }
		}
		/// <summary>
		/// on update CURRENT_TIMESTAMP
		/// </summary>
		public DateTime gmt_modified
		{
			set { _gmt_modified = value; }
			get { return _gmt_modified; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string single_code
		{
			set { _single_code = value; }
			get { return _single_code; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string back_reason
		{
			set { _back_reason = value; }
			get { return _back_reason; }
		}
		#endregion Model

	}
}

