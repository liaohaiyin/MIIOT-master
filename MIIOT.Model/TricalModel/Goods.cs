using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.Trical
{
    public class Goods: INotifyPropertyChanged
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
        public string GoodsCode { get; set; }
        public string GoodsName { get; set; }
        public string GoodsSpec { get; set; }
        public string Provider { get; set; }
        public string Unit { get; set; }
        private int _qty;

        public int Qty
        {
            get { return _qty; }
            set { _qty = value;
                OnPropertyChanged("Qty");
            }
        }

        public string Batch { get; set; }
        public DateTime ProducedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
