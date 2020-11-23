/**  版本信息模板在安装目录下，可自行修改。
* cart_instock.cs
*
* 功 能： N/A
* 类 名： cart_instock
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:07   N/A    初版
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
	/// cart_instock:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_instock
	{
		public cart_instock()
		{}
		#region Model
		private long _instockid;
		private string _sourceid;
		private string _instockno;
		private string _goodsid;
		private long? _instockqty;
		private long? _rackqty;
		private int? _instocktype;
		private int? _instockstatus;
		private string _cremanid;
		private string _cremanname;
		private string _sourcetype;
		private string _editmanid;
		private string _editmanname;
		private string _supplyname;
		private string _storehousename;
		private string _cabinetcode;
		private string _storehouseid;
		/// <summary>
		/// 
		/// </summary>
		public long instock_id
		{
			set{ _instockid=value;}
			get{return _instockid;}
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
		public string instock_no
		{
			set{ _instockno=value;}
			get{return _instockno;}
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
		public long? instock_qty
		{
			set{ _instockqty=value;}
			get{return _instockqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? rack_qty
		{
			set{ _rackqty=value;}
			get{return _rackqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? instock_type
		{
			set{ _instocktype=value;}
			get{return _instocktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? instock_status
		{
			set{ _instockstatus=value;}
			get{return _instockstatus;}
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
		/// <summary>
		/// 
		/// </summary>
		public string supply_name
		{
			set{ _supplyname=value;}
			get{return _supplyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string storehouse_no
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
		/// <summary>
		/// 
		/// </summary>
		public string storehouse_id
		{
			set{ _storehouseid=value;}
			get{return _storehouseid;}
		}
        public DateTime create_time { get; set; }

		public string plot_id { get; set; }
		public string plot_no { get; set; }
		public DateTime validity_date { get; set; }
		#endregion Model

	}
}

