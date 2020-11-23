using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
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
        #region 属性
        private ObservableCollection<ChatUserModel> _ContactList;

        public ObservableCollection<ChatUserModel> ContactList
        {
            get { return _ContactList; }
            set
            {
                _ContactList = value;
                OnPropertyChanged("ContactList");
            }
        }

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            var TotalContactList = new List<ChatUserModel>();
            ChatUserModel group = new ChatUserModel();
            group.UserName = "Flutter开发组";
            group.Header = "pack://application:,,,/Resources/Img/Header/头像1.png";
            group.Describe = "寻找适合你的UI";
            TotalContactList.Add(group);

            ChatUserModel Group1 = new ChatUserModel();
            Group1.UserName = "AduSkin开发组";
            Group1.Header = "pack://application:,,,/Resources/Img/Header/头像2.png";
            Group1.Describe = "开发者论坛";
            TotalContactList.Add(Group1);

            ChatUserModel item = new ChatUserModel();
            item.UserName = "AduSkin";
            item.Header = "pack://application:,,,/Resources/Img/Header/头像3.png";
            item.Describe = "追求极致，永臻完美";
            TotalContactList.Add(item);

            ChatUserModel item1 = new ChatUserModel();
            item1.UserName = "千百度";
            item1.Header = "pack://application:,,,/Resources/Img/Header/头像4.png";
            item1.Describe = "追求极致，永臻完美";
            TotalContactList.Add(item1);

            ChatUserModel item2 = new ChatUserModel();
            item2.UserName = "万里独行";
            item2.Header = "pack://application:,,,/Resources/Img/Header/头像5.png";
            item2.Describe = "追求极致，永臻完美";
            TotalContactList.Add(item2);

            ChatUserModel item3 = new ChatUserModel();
            item3.UserName = "一个人";
            item3.Header = "pack://application:,,,/Resources/Img/Header/头像6.png";
            item3.Describe = "追求极致，永臻完美";
            TotalContactList.Add(item3);

            ChatUserModel item4 = new ChatUserModel();
            item4.UserName = "AduMusic";
            item4.Header = "pack://application:,,,/Resources/Img/Header/头像1.png";
            item4.Describe = "追求极致，永臻完美";
            TotalContactList.Add(item4);

            ChatUserModel item6 = new ChatUserModel();
            item6.UserName = "往事如风";
            item6.Header = "pack://application:,,,/Resources/Img/Header/头像2.png";
            item6.Describe = "美滋滋";
            TotalContactList.Add(item6);

            var index = 0;
            for (int i = 0; i < 100; i++)
            {
                ChatUserModel item7 = new ChatUserModel();
                item7.UserName = "美滋滋";
                item7.Header = "pack://application:,,,/Resources/Img/Header/头像" + (index += 1) + ".png";
                item7.Describe = "不需要太多";
                TotalContactList.Add(item7);
                if (index >= 6)
                {
                    index = 0;
                }
            }
            ContactList = new ObservableCollection<ChatUserModel>(TotalContactList.Skip((1 - 1) * 10).Take(10).ToList());
        }
    }
    public class ChatUserModel
    {
        public string SortID { get; set; }

        public int Id { get; set; }

        public string Header { get; set; } = "pack://application:,,,/Resources/Img/Header/头像.png";

        public string UserName { get; set; } = "欢迎使用 AduChat";

        public string Describe { get; set; } = "追求极致，永臻完美";

        //public ContactType ContactType { get; set; } = ContactType.Single;

        public bool IsChecked { get; set; }
    }
}
