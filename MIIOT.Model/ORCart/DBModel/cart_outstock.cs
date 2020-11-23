/**  版本信息模板在安装目录下，可自行修改。
* cart_outstock.cs
*
* 功 能： N/A
* 类 名： cart_outstock
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:08   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace MIIOT.Model
{
	/// <summary>
	/// cart_outstock:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_outstock
	{
		public cart_outstock()
		{}
		#region Model
		private long _oustockid;
		private string _sourceid;
		private string _oustockno;
		private string _goodsid;
		private long? _outstockqty;
		private long? _factoutstockqty;
		private int? _outstockstatus;
		private string _cremanid;
		private string _cremanname;
		private string _sourcetype;
		private string _editmanid;
		private string _editmanname;
		private string _storehousename;
		private string _cabinetcode;
		/// <summary>
		/// 
		/// </summary>
		public long outstock_id
		{
			set{ _oustockid=value;}
			get{return _oustockid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string source_id
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outstock_no
		{
			set{ _oustockno=value;}
			get{return _oustockno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goods_id
		{
			set{ _goodsid=value;}
			get{return _goodsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? outstock_qty
		{
			set{ _outstockqty=value;}
			get{return _outstockqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? factoutstock_qty
		{
			set{ _factoutstockqty=value;}
			get{return _factoutstockqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? outstock_status
		{
			set{ _outstockstatus=value;}
			get{return _outstockstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string creman_id
		{
			set{ _cremanid=value;}
			get{return _cremanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string creman_name
		{
			set{ _cremanname=value;}
			get{return _cremanname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string source_type
		{
			set{ _sourcetype=value;}
			get{return _sourcetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string editman_id
		{
			set{ _editmanid=value;}
			get{return _editmanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string editman_name
		{
			set{ _editmanname=value;}
			get{return _editmanname;}
		}
        public string storehouse_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouse_name
		{
			set{ _storehousename=value;}
			get{return _storehousename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cart_code
		{
			set{ _cabinetcode=value;}
			get{return _cabinetcode;}
		}
        public DateTime create_time { get; set; }

		public string plot_id { get; set; }
		public string plot_no { get; set; }
		public DateTime validity_date { get; set; }
		#endregion Model

	}
}

