using MIIOT.Model.ORCart.SPDModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/7/31 17:06:43
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart
{

	/// <summary>
	/// pub_room:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class pub_room : BaseNotify
	{
		public pub_room()
		{ }
		#region Model
		private int _id;
		private string _room_code;
		private string _room_name;
		private string _room_type;
		private string _meno;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int id
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string room_code
		{
			set { _room_code = value; }
			get { return _room_code; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string room_name
		{
			set { _room_name = value; }
			get { return _room_name; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string room_type
		{
			set { _room_type = value; }
			get { return _room_type; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string meno
		{
			set { _meno = value; }
			get { return _meno; }
		}
        public int status { get; set; }
        #endregion Model
        private ObservableCollection<spd_surgeryinfo> _pub_Surgeries = new ObservableCollection<spd_surgeryinfo>();

		public ObservableCollection<spd_surgeryinfo> pub_Surgeries
		{
			get { return _pub_Surgeries; }
			set
			{
				_pub_Surgeries = value;
				OnPropertyChanged("pub_Surgeries");
			}
		}
	}

}
