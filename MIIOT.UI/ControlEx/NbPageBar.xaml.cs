using MIIOT.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MIIOT.UI.ControlEx
{
    /// <summary>
    /// PageBar.xaml 的交互逻辑
    /// </summary>
    public partial class NbPageBar : UserControl
    {
        public NbPageBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            set
            {
                this.GoPageNoTxt.MaxValue = value;
                SetValue(TotalPagesProperty, value);
            }
        }

        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register("TotalPages", typeof(int), typeof(NbPageBar), new PropertyMetadata(0));



        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalItems
        {
            get { return (int)GetValue(TotalItemsProperty); }
            set { SetValue(TotalItemsProperty, value); }
        }

        public static readonly DependencyProperty TotalItemsProperty =
            DependencyProperty.Register("TotalItems", typeof(int), typeof(NbPageBar), new PropertyMetadata(0));

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo
        {
            get { return (int)GetValue(PageNoProperty); }
            set { SetValue(PageNoProperty, value); }
        }

        public static readonly DependencyProperty PageNoProperty =
            DependencyProperty.Register("PageNo", typeof(int), typeof(NbPageBar), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PageNoPropertyCallBack));

        public static void PageNoPropertyCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            property.SetValue(PageNoProperty, args.NewValue);
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(NbPageBar), new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PageSizePropertyCallBack));

        public static void PageSizePropertyCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            property.SetValue(PageSizeProperty, args.NewValue);
        }


        /// <summary>
        /// 跳转页码
        /// </summary>
        public int GoPageNo
        {
            get { return (int)GetValue(GoPageNoProperty); }
            set { SetValue(GoPageNoProperty, value); }
        }

        public static readonly DependencyProperty GoPageNoProperty =
            DependencyProperty.Register("GoPageNo", typeof(int), typeof(NbPageBar), new PropertyMetadata(1));

        /// <summary>
        /// 翻页命令
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(NbPageBar));


        public event EventHandler<PageNoChangedEventArgs> PageNoChanged;

        public virtual void OnPageNoChanged()
        {
            var args = new PageNoChangedEventArgs(this.PageNo, this.PageSize);
            PageNoChanged?.Invoke(this, args);
            if (this.Command?.CanExecute(args) == true)
            {
                this.Command.Execute(args);
            }
        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageNo != this.TotalPages)
            {
                this.PageNo = this.TotalPages;
                OnPageNoChanged();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageNo < this.TotalPages)
            {
                this.PageNo += 1;
                OnPageNoChanged();
            }
        }

        private void UpPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageNo > 1)
            {
                this.PageNo -= 1;
                OnPageNoChanged();
            }
        }

        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageNo != 1)
            {
                this.PageNo = 1;
                OnPageNoChanged();
            }
        }

        private void GoPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.GoPageNo > this.TotalPages)
                this.GoPageNo = this.TotalPages;
            else if (this.GoPageNo < 1)
                this.GoPageNo = 1;
            this.PageNo = this.GoPageNo;
            OnPageNoChanged();
        }


        private void PageSizeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = this.PageSizeCb.SelectedItem as NbComboBoxItem;
            if (item != null)
            {
                this.PageSize = int.Parse(item.Tag.ToString());

                if (this.TotalItems > 0)
                {
                    var totalPages = (int)Math.Ceiling((double)this.TotalItems / (double)this.PageSize);
                    if (this.PageNo > totalPages)
                    {
                        this.PageNo = totalPages;
                    }
                }

                OnPageNoChanged();
            }
        }
    }


    public class PageNoChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        public PageNoChangedEventArgs(int pageNo, int pageSize)
        {
            this.PageNo = pageNo;
            this.PageSize = pageSize;
        }
    }
}
