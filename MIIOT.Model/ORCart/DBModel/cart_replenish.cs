/**  版本信息模板在安装目录下，可自行修改。
* cart_replenishdtl.cs
*
* 功 能： N/A
* 类 名： cart_replenishdtl
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:09   N/A    初版
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
	/// cart_replenish:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_replenish: BaseRecord
    {
		public cart_replenish()
		{}
		#region Model
		private int _id;
		private int _deleflag=0;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
        }

        private string _replenish_no;
        public string replenish_no
		{
			set{ _replenish_no = value;}
			get{return _replenish_no; }
		}


        private string _cart_id;
        public string cart_id
		{
			set{ _cart_id = value;}
			get{return _cart_id; }
		}
        private string _cart_name;

        public string cart_name
        {
            get { return _cart_name; }
            set { _cart_name = value; }
        }

        private int _replenish_status;

        public int replenish_status
		{
            get { return _replenish_status; }
            set { _replenish_status = value; }
        }

        private string _out_storehouse_id;

        public string out_storehouse_id
		{
            get { return _out_storehouse_id; }
            set { _out_storehouse_id = value; }
        }
        private string _out_storehouse_name;

        public string out_storehouse_name
		{
            get { return _out_storehouse_name; }
            set { _out_storehouse_name = value; }
        }
        public string in_storehouse_id { get; set; }
        public string in_storehouse_name { get; set; }
        private DateTime _outstore_time;

        public DateTime outstore_time
		{
            get { return _outstore_time; }
            set { _outstore_time = value; }
        }
        private string _replenish_id;

        public string replenish_id
		{
            get { return _replenish_id; }
            set { _replenish_id = value; }
        }
        private DateTime _replenish_time;

        public DateTime replenish_time
		{
            get { return _replenish_time; }
            set { _replenish_time = value; }
        }
        private string _replenish_name;

        public string replenish_name
		{
            get { return _replenish_name; }
            set { _replenish_name = value; }
        }
        private string _creman_id;

        public string creman_id
		{
            get { return _creman_id; }
            set { _creman_id = value; }
        }
        private string _creman_name;

        public string creman_name
		{
            get { return _creman_name; }
            set { _creman_name = value; }
        }
        private string _memo;

        public string memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int deleflag
		{
			set{ _deleflag=value;}
			get{return _deleflag;}
		}
		
	
		#endregion Model

	}
}

