using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    public class ApplyInfo : BaseEntity, ISort
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

        protected string _applyName;
        /// <summary>
        /// 申领单据名称
        /// </summary>
        public virtual string ApplyName
        {
            get { return _applyName; }
            set { _applyName = value; OnPropertyChanged(nameof(ApplyName)); }
        }
    }

    public class ViewApplyInfo : ApplyInfo
    {
        protected string _officeName;
        /// <summary>
        /// 申领科室
        /// </summary>
        public virtual string OfficeName
        {
            get { return _officeName; }
            set { _officeName = value; OnPropertyChanged(nameof(OfficeName)); }
        }

        protected string _storeName;
        /// <summary>
        /// 申领库房
        /// </summary>
        public virtual string StoreName
        {
            get { return _storeName; }
            set { _storeName = value; OnPropertyChanged(nameof(StoreName)); }
        }

        protected string _outputName;
        /// <summary>
        /// 出库库房
        /// </summary>
        public virtual string OutputName
        {
            get { return _outputName; }
            set { _outputName = value; OnPropertyChanged(nameof(OutputName)); }
        }

        protected string _operator;
        /// <summary>
        /// 验收人
        /// </summary>
        public virtual string Operator
        {
            get { return _operator; }
            set { _operator = value; OnPropertyChanged(nameof(Operator)); }
        }
    }
}
