using System;
namespace MIIOT.Model
{
	/// <summary>
	/// pub_store_in:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class pub_store_in:BaseNotify
	{
		public pub_store_in()
		{ }
		#region Model
		private long _id;
		private long _organ_id;
		private int _source_type = 0;
		private long _source_id = 0;
		private string _source_no;
		private string _origin_id;
		private long _storehouse_id = 0;
		private int _status = 0;
		private int _is_delete = 0;
		private long _creater_id = 0;
		private DateTime _gmt_create = DateTime.Now;
		private long _modifier_id = 0;
		private DateTime _gmt_modified = DateTime.Now;
		private int _result_status = 0;
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
		public long storehouse_id
		{
			set { _storehouse_id = value; }
			get { return _storehouse_id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int status
		{
			set { _status = value; }
			get { return _status; }
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
		public int result_status
		{
			set { _result_status = value; }
			get { return _result_status; }
		}
        #endregion Model
        public long out_storehouse_id { get; set; }
        private bool _isSelected;

        public bool IsSelected
		{
            get { return _isSelected; }
            set { _isSelected = value;
				OnPropertyChanged("IsSelected");
			}
        }

    }
}

