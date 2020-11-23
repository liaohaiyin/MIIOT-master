using MIIOT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MIIOT.Control
{
    public class BaseConnectControl : UserControl, INotifyPropertyChanged
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
        public delegate void DelDelete(BaseConnectControl control);
        public event DelDelete OnDelete;
        private MacPara macPara = new MacPara();

        public MacPara MacPara
        {
            get { return macPara; }
            set
            {
                macPara = value;
                OnPropertyChanged("MacPara");
            }
        }
        private ObservableCollection<SysPara> _sysParas = new ObservableCollection<SysPara>();

        public ObservableCollection<SysPara> sysParas
        {
            get { return _sysParas; }
            set
            {
                _sysParas = value;
                OnPropertyChanged("sysParas");
            }
        }
        public BaseConnectControl()
        {


        }
        public void DoDelete()
        {
            OnDelete?.Invoke(this);
        }
        public virtual void UpdateStatus(bool isConnect)
        { 
        
        }
    }
}
