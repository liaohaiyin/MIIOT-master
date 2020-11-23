using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    /// <summary>
    /// 申领详细数据
    /// </summary>
    public class LibraryDetail : BaseEntity
    {
        protected int _lid;
        /// <summary>
        /// 单据ID
        /// </summary>
        public virtual int LID
        {
            get { return _lid; }
            set { _lid = value; OnPropertyChanged(nameof(LID)); }
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

        protected int _outputNum;
        /// <summary>
        /// 验收数量
        /// </summary>
        public virtual int OutputNum
        {
            get { return _outputNum; }
            set { _outputNum = value; OnPropertyChanged(nameof(OutputNum)); }
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

        protected string _libraryName;
        /// <summary>
        /// 单据编号
        /// </summary>
        public virtual string LibraryName
        {
            get { return _libraryName; }
            set { _libraryName = value; OnPropertyChanged(nameof(LibraryName)); }
        }
    }

    public class ViewLibraryDetail : LibraryDetail
    {

    }
}
