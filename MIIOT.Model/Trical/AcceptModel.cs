using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.Trical
{
   public class AcceptModel : INotifyPropertyChanged
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
        public int id { get; set; }
        public string AcceptCode { get; set; }
        private string _Hospital;

        public string Hospital
        {
            get { return _Hospital; }
            set { _Hospital = value;
                OnPropertyChanged("Hospital");
            }
        }
        private string _Provider;

        public string Provider
        {
            get { return _Provider; }
            set { _Provider = value;
                OnPropertyChanged("Provider");
            }
        }
        private string _StockName;

        public string StockName
        {
            get { return _StockName; }
            set { _StockName = value;
                OnPropertyChanged("StockName");
            }
        }
        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value;
                OnPropertyChanged("Status");
            }
        }
      

        public List<Goods> GoodsList { get; set; } = new List<Goods>();
    }
}
