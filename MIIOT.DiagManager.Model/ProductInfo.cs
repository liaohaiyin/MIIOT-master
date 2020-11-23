using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    public class ProductInfo : BaseEntity
    {
        protected int _bid;
        /// <summary>
        /// 单据ID
        /// </summary>
        public virtual int BID
        {
            get { return _bid; }
            set { _bid = value; OnPropertyChanged(nameof(BID)); }
        }

        protected string _productNo;
        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual string ProductNo
        {
            get { return _productNo; }
            set { _productNo = value; OnPropertyChanged(nameof(ProductNo)); }
        }

        protected string _productName;
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string ProductName
        {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        protected string _productType;
        /// <summary>
        /// 商品规格
        /// </summary>
        public virtual string ProductType
        {
            get { return _productType; }
            set { _productType = value; OnPropertyChanged(nameof(ProductType)); }
        }

        protected string _factoryName;
        /// <summary>
        /// 商品规格
        /// </summary>
        public virtual string FactoryName
        {
            get { return _factoryName; }
            set { _factoryName = value; OnPropertyChanged(nameof(FactoryName)); }
        }

        protected string _unit;
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit
        {
            get { return _unit; }
            set { _unit = value; OnPropertyChanged(nameof(Unit)); }
        }

        protected int _verifyNum;
        /// <summary>
        /// 验收数量
        /// </summary>
        public virtual int VerifyNum
        {
            get { return _verifyNum; }
            set { _verifyNum = value; OnPropertyChanged(nameof(VerifyNum)); }
        }

        protected int _checkNum;
        /// <summary>
        /// 复核数量
        /// </summary>
        public virtual int CheckNum
        {
            get { return _checkNum; }
            set { _checkNum = value; OnPropertyChanged(nameof(CheckNum)); }
        }

        protected float _price;
        /// <summary>
        /// 价格
        /// </summary>
        public virtual float Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        protected int _batchNo;
        /// <summary>
        /// 批号
        /// </summary>
        public virtual int BatchNo
        {
            get { return _batchNo; }
            set { _batchNo = value; OnPropertyChanged(nameof(BatchNo)); }
        }

        protected DateTime _createDate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public virtual DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; OnPropertyChanged(nameof(CreateDate)); }
        }

        protected DateTime _verifyDate;
        /// <summary>
        /// 有效期
        /// </summary>
        public virtual DateTime VerifyDate
        {
            get { return _verifyDate; }
            set { _verifyDate = value; OnPropertyChanged(nameof(VerifyDate)); }
        }

        protected string _billsName;
        /// <summary>
        /// 单号
        /// </summary>
        public virtual string BillsName
        {
            get { return _billsName; }
            set { _billsName = value; OnPropertyChanged(nameof(BillsName)); }
        }        
    }

    /// <summary>
    /// 产品视图字段
    /// </summary>
    public class ViewProfuctInfo : ProductInfo
    {

    }
}
