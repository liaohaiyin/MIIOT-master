using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{
    public class ModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        private bool _IsHandOrder=false;
        public bool IsHandOrder
        {
            get { return _IsHandOrder; }
            set
            {
                _IsHandOrder = value;
                OnPropertyChanged("IsHandOrder");
            }
        }
    }
    public class CheckInfo : ModelBase
    {
     
        private int _qty;

        public int Qty
        {
            get { return _qty; }
            set
            {
                _qty = value;
                OnPropertyChanged("Qty");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private int _CheckQty;

        public int CheckQty
        {
            get { return _CheckQty; }
            set
            {
                _CheckQty = value;
                OnPropertyChanged("CheckQty");
            }
        }
        private bool _IsNew;

        public bool IsNew
        {
            get { return _IsNew; }
            set
            {
                _IsNew = value;
                OnPropertyChanged("IsNew");
            }
        }
    }
}
