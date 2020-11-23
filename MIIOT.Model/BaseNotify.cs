using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{
    public class BaseNotify : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        #endregion

    }
    public class BaseRecord : BaseNotify
    {
        [Description("External")]
        public int index { get; set; }
        private bool _selected;
        [Description("External")]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        private bool _isNew;
        [Description("External")]
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                OnPropertyChanged("IsNew");
            }
        }
        private bool _isDelete;
        [Description("External")]
        public bool IsDelete
        {
            get { return _isDelete; }
            set
            {
                _isDelete = value;
                OnPropertyChanged("IsDelete");
            }
        }

    }
}
