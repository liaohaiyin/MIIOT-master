using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.ORCart.Common
{
    public class PicModel : INotifyPropertyChanged
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
        private string _name;

        public string _Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("_Name");
            }
        }
        private string _code;

        public string _Code
        {
            get { return _code; }
            set { _code = value;
                OnPropertyChanged("_Code");
            }
        }
        private string _comName;

        public string _ComName
        {
            get { return _comName; }
            set { _comName = value;
                OnPropertyChanged("_ComName");
            }
        }
        private string _comAddr;

        public string _ComAddr
        {
            get { return _comAddr; }
            set { _comAddr = value;
                OnPropertyChanged("_ComAddr");
            }
        }

        private string _comPerson;

        public string _ComPerson
        {
            get { return _comPerson; }
            set { _comPerson = value;
                OnPropertyChanged("_ComPerson");
            }
        }

        private string _comProduct;

        public string _ComProduct
        {
            get { return _comProduct; }
            set { _comProduct = value;
                OnPropertyChanged("_ComProduct");
            }
        }
        private string _boss;

        public string _Boss
        {
            get { return _boss; }
            set { _boss = value;
                OnPropertyChanged("_Boss");
            }
        }
        private string _addr;

        public string _Addr
        {
            get { return _addr; }
            set { _addr = value;
                OnPropertyChanged("_Addr");
            }
        }

        private string _dept;

        public string _Dept
        {
            get { return _dept; }
            set { _dept = value;
                OnPropertyChanged("_Dept");
            }
        }
        private string _releaseDate;

        public string _ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value;
                OnPropertyChanged("_ReleaseDate");
            }
        }

        private string _activeDate;

        public string _ActiveDate
        {
            get { return _activeDate; }
            set { _activeDate = value;
                OnPropertyChanged("_ActiveDate");
            }
        }
        private string _time;

        public string time
        {
            get { return _time; }
            set { _time = value;
                OnPropertyChanged("time");
            }
        }

    }
}
