using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    public class CargoInfo : BaseEntity, ISort
    {
        protected int _sortNo;
        /// <summary>
        /// 单据名称
        /// </summary>
        public virtual int SortNo
        {
            get { return _sortNo; }
            set { _sortNo = value; OnPropertyChanged(nameof(SortNo)); }
        }

        protected string _cargoName;
        /// <summary>
        /// 申领单据名称
        /// </summary>
        public virtual string CargoName
        {
            get { return _cargoName; }
            set { _cargoName = value; OnPropertyChanged(nameof(CargoName)); }
        }
    }

    public class ViewCargoInfo : CargoInfo
    {
        protected string _supplierName;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string SupplierName
        {
            get { return _supplierName; }
            set { _supplierName = value; OnPropertyChanged(nameof(SupplierName)); }
        }

        protected string _outputName;
        /// <summary>
        /// 退货库房
        /// </summary>
        public virtual string OutputName
        {
            get { return _outputName; }
            set { _outputName = value; OnPropertyChanged(nameof(OutputName)); }
        }

        protected string _operator;
        /// <summary>
        /// 退货人
        /// </summary>
        public virtual string Operator
        {
            get { return _operator; }
            set { _operator = value; OnPropertyChanged(nameof(Operator)); }
        }
    }
}
