using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    public class LibraryInfo : BaseEntity, ISort
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

        protected string _libraryName;
        /// <summary>
        /// 申领单据名称
        /// </summary>
        public virtual string LibraryName
        {
            get { return _libraryName; }
            set { _libraryName = value; OnPropertyChanged(nameof(LibraryName)); }
        }
    }

    public class ViewLibraryInfo : LibraryInfo
    {
        protected string _logoutName;
        /// <summary>
        /// 申退科室
        /// </summary>
        public virtual string LogoutName
        {
            get { return _logoutName; }
            set { _logoutName = value; OnPropertyChanged(nameof(LogoutName)); }
        }

        protected string _outputName;
        /// <summary>
        /// 退库库房
        /// </summary>
        public virtual string OutputName
        {
            get { return _outputName; }
            set { _outputName = value; OnPropertyChanged(nameof(OutputName)); }
        }

        protected string _storeName;
        /// <summary>
        /// 入库库房
        /// </summary>
        public virtual string StoreName
        {
            get { return _storeName; }
            set { _storeName = value; OnPropertyChanged(nameof(StoreName)); }
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
