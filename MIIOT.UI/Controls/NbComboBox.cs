using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MIIOT.UI.Controls
{
    public class NbComboBox : ComboBox
    {
        static NbComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbComboBox), new FrameworkPropertyMetadata(typeof(NbComboBox)));
        }

        private TextBox _searchTextBox;
        public NbComboBox()
        {
            //this.SetResourceReference(NbComboBox.StyleProperty, nameof(NbComboBox));
            this.IsTextSearchEnabled = false;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(NbComboBox), new PropertyMetadata(string.Empty));


        /// <summary>
        /// 下拉框显示搜索框
        /// </summary>
        public Visibility SearchTextBoxVisilibity
        {
            get { return (Visibility)GetValue(SearchTextBoxVisilibityProperty); }
            set { SetValue(SearchTextBoxVisilibityProperty, value); }
        }
        public static readonly DependencyProperty SearchTextBoxVisilibityProperty =
            DependencyProperty.Register("SearchTextBoxVisilibity", typeof(Visibility), typeof(NbComboBox), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// 设置空选项按钮是否显示
        /// </summary>
        public Visibility SelectedEmptyButtonVisilibity
        {
            get { return (Visibility)GetValue(SelectedEmptyButtonVisilibityProperty); }
            set { SetValue(SelectedEmptyButtonVisilibityProperty, value); }
        }
        public static readonly DependencyProperty SelectedEmptyButtonVisilibityProperty =
            DependencyProperty.Register("SelectedEmptyButtonVisilibity", typeof(Visibility), typeof(NbComboBox), new PropertyMetadata(Visibility.Collapsed));

        public IEnumerable SearchItems
        {
            get
           {
                return (IList)GetValue(SearchItemsProperty);
            }
            set
            {
                ItemsSource = value;
                SetValue(SearchItemsProperty, value);
            }
        }

        public static readonly DependencyProperty SearchItemsProperty =
            DependencyProperty.Register("SearchItems", typeof(IList), typeof(NbComboBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SearchItemsPropertyCallBack));

        public static void SearchItemsPropertyCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            property.SetValue(SearchItemsProperty, args.NewValue);
            if (args.NewValue == null)
                return;
            var list = (IList)args.NewValue;
            if (list.Count > 100)
            {
                var ret = new List<object>();
                for (int i = 0; i < 100; i++)
                {
                    ret.Add(list[i]);
                }
                (property as NbComboBox).ItemsSource = ret;
            }
            else
            {
                (property as NbComboBox).ItemsSource = list;
            }
        }


        protected override void OnDropDownOpened(EventArgs e)
        {
            if (this.SearchTextBoxVisilibity == Visibility.Visible && this.IsEditable == false && this._searchTextBox == null)
            {
                this._searchTextBox = this.GetTemplateChild("searchTxt") as TextBox;
                this._searchTextBox.KeyUp += _searchTextBox_KeyUp;
            }
            base.OnDropDownOpened(e);
        }


        private void _searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                this._searchTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                return;
            }
            if (this.SearchTextBoxVisilibity == Visibility.Visible &&
                this.IsEditable == false &&
                this.SearchItems != null)
            {
                var ret = new List<object>();
                var txt = this._searchTextBox.Text;
                foreach (var item in this.SearchItems)
                {
                    if (item == null)
                        continue;
                    var val = item.ToString();
                    if (!string.IsNullOrWhiteSpace(DisplayMemberPath))
                    {
                        try
                        {
                            var displayProp = item.GetType().GetProperty(DisplayMemberPath);
                            if (displayProp != null)
                                val = displayProp.GetValue(item)?.ToString();
                        }
                        catch { }
                    }

                    if (val?.IndexOf(txt.Trim(), StringComparison.OrdinalIgnoreCase) > -1)
                        ret.Add(item);
                    if (ret.Count > 100)
                        break;
                }
                if (this.SelectedEmptyButtonVisilibity != Visibility.Visible &&
                    this.SelectedItem != null &&
                    ret.Count == 0)
                {
                    ret.Add(this.SelectedItem);
                }

                this.ItemsSource = ret;
                if (ret.Count > 0)
                    this.SelectedIndex = 0;
                else
                    this.SelectedItem = null;
                this._searchTextBox.Focus();
            }
        }

        public ICommand SelectedEmptyCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    this.SelectedIndex = -1;
                    this.IsDropDownOpen = false;
                });
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NbComboBoxItem();
        }
    }


    public class NbComboBoxItem : ComboBoxItem
    {
        static NbComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbComboBoxItem), new FrameworkPropertyMetadata(typeof(NbComboBoxItem)));
        }
        public NbComboBoxItem()
        {
            //this.SetResourceReference(NbComboBoxItem.StyleProperty, nameof(NbComboBoxItem));
        }
    }
}
