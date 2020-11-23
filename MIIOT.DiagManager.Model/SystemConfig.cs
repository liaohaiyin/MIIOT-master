using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model
{
    public class SystemConfig : BaseEntity, ISort
    {
        

        protected int _groupSortNo;

        public virtual int GroupSortNo
        {
            get { return _groupSortNo; }
            set { _groupSortNo = value; OnPropertyChanged(nameof(GroupSortNo)); }
        }

        protected int _sortNo;

        public virtual int SortNo
        {
            get { return _sortNo; }
            set { _sortNo = value; OnPropertyChanged(nameof(SortNo)); }
        }


        protected string _groupName;

        public virtual string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; OnPropertyChanged(nameof(GroupName)); }
        }

        

        protected string _name;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }


        protected string _key;

        public virtual string Key
        {
            get { return _key; }
            set { _key = value; OnPropertyChanged(nameof(Key)); }
        }

        protected string _value;

        public virtual string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(nameof(Value)); }
        }

        protected enumConfigValueType _valueType;

        public virtual enumConfigValueType ValueType
        {
            get { return _valueType; }
            set { _valueType = value; OnPropertyChanged(nameof(ValueType)); }
        }

        protected string _ex1;

        public virtual string Ex1
        {
            get { return _ex1; }
            set { _ex1 = value; OnPropertyChanged(nameof(Ex1)); }
        }

        protected string _ex2;

        public virtual string Ex2
        {
            get { return _ex2; }
            set { _ex2 = value; OnPropertyChanged(nameof(Ex2)); }
        }

        protected string _ex3;

        public virtual string Ex3
        {
            get { return _ex3; }
            set { _ex3 = value; OnPropertyChanged(nameof(Ex3)); }
        }

        protected string _data;

        public virtual string Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(nameof(Data)); }
        }

        protected bool _isReadOnly;

        public virtual bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; OnPropertyChanged(nameof(IsReadOnly)); }
        }

        protected string _cultrueKey;

        public virtual string CultrueKey
        {
            get { return _cultrueKey; }
            set { _cultrueKey = value; OnPropertyChanged(nameof(CultrueKey)); }
        }


        protected string _remark;

        public virtual string Remark
        {
            get { return _remark; }
            set { _remark = value; OnPropertyChanged(nameof(Remark)); }
        }
    }


    

    public class EnumConfigItem : BaseVm
    {
        protected string _enumName;

        public virtual string EnumName
        {
            get { return _enumName; }
            set { _enumName = value; OnPropertyChanged(nameof(EnumName)); }
        }

        protected string _enumValue;

        public virtual string EnumValue
        {
            get { return _enumValue; }
            set { _enumValue = value; OnPropertyChanged(nameof(EnumValue)); }
        }

    }
}
