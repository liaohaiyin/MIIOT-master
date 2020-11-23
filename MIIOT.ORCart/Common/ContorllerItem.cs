using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MIIOT.ORCart
{
    /// <summary>
    /// 侧边栏选择
    /// </summary>
    public class ContorllerItem : INotifyPropertyChanged
    {

        private string _name;
        private object _content;
        private bool _showMenu;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement;
        private Thickness _marginRequirement = new Thickness(0);

        public ContorllerItem(string name, IMenuMsgSend menuMsgSend, bool showMenu = true)
        {
            _name = name;
            Content = menuMsgSend;
            _showMenu = showMenu;
            MenuMsgSend = menuMsgSend;
        }
        public bool ShowMenu
        {
            get { return _showMenu; }
            set { this.MutateVerbose(ref _showMenu, value, RaisePropertyChanged()); }
        }
        public string Name
        {
            get { return _name; }
            set { this.MutateVerbose(ref _name, value, RaisePropertyChanged()); }
        }
        public object Content
        {
            get { return _content; }
            set {
                this.MutateVerbose(ref _content, value, RaisePropertyChanged()); }
        }
        private sys_menu _sysMenu;

        public sys_menu SysMenu
        {
            get { return _sysMenu; }
            set { this.MutateVerbose(ref _sysMenu, value, RaisePropertyChanged()); }
        }

        private IMenuMsgSend _MenuMsgSend;

        public IMenuMsgSend MenuMsgSend
        {
            get { return _MenuMsgSend; }
            set {
                
                this.MutateVerbose(ref _MenuMsgSend, value, RaisePropertyChanged()); }
        }
        
        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get { return _horizontalScrollBarVisibilityRequirement; }
            set { this.MutateVerbose(ref _horizontalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get { return _verticalScrollBarVisibilityRequirement; }
            set { this.MutateVerbose(ref _verticalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        }

        public Thickness MarginRequirement
        {
            get { return _marginRequirement; }
            set { this.MutateVerbose(ref _marginRequirement, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        private List<ContorllerItem> _ChildItems=new List<ContorllerItem>();

        public List<ContorllerItem> ChildItems
        {
            get { return _ChildItems; }
            set {  this.MutateVerbose(ref _ChildItems, value, RaisePropertyChanged()); }
        }

    }

    public static class NotifyPropertyChangedExtension
    {
        public static void MutateVerbose<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs> raise, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
        }
    }



}
