using System.ComponentModel;

namespace MIIOT.UI
{
    public abstract class NbPageVm : INotifyPropertyChanged
    {

        protected bool _isWaiting;

        public virtual bool IsWaiting
        {
            get { return _isWaiting; }
            set { _isWaiting = value; OnPropertyChanged(nameof(IsWaiting)); }
        }

        protected string _tipText;

        public virtual string TipText
        {
            get { return _tipText; }
            set { _tipText = value; OnPropertyChanged(nameof(TipText)); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void Init();


        protected object _tag;

        public virtual object Tag
        {
            get { return _tag; }
            set { _tag = value; OnPropertyChanged(nameof(Tag)); }
        }

    }
}
