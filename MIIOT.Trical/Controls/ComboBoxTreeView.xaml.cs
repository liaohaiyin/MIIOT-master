using MIIOT.Model.TricalModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Trical.Controls
{
    /// <summary>
    /// ComboBoxTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class ComboBoxTreeView : System.Windows.Controls.UserControl
    {
        public ComboBoxTreeView()
        {
            InitializeComponent();
            this.Loaded += MultiSelOrgTree_Loaded;
            this.orgTree.Loaded += orgTree_Loaded;
        }
        
   
        private bool _firstLoad = true;
        private log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ComboBoxTreeView));
        private ObservableCollection<MultiSelOrgTreeItemModel> _collection = new ObservableCollection<MultiSelOrgTreeItemModel>();
        private ObservableCollection<DepartmentModel> _selectedOrgs = new ObservableCollection<DepartmentModel>();
        private bool _inited = false;
        List<DepartmentModel> models = new List<DepartmentModel>();

        public IList<DepartmentModel> SelectedOrgs
        {
            get { return _selectedOrgs; }
        }

     

        private void MultiSelOrgTree_Loaded(object sender, RoutedEventArgs e)
        {
            if (_firstLoad)
            {
                _firstLoad = false;
                InitData(models);
            }
        }

        public void InitData(List<DepartmentModel> list)
        {
            if (list == null||list.Count==0) return;
            models = list;
            Task.Factory.StartNew(() =>
            {
                var info = list.FirstOrDefault(p => p.pid == "0"||p.pid==null);
                MultiSelOrgTreeItemModel root = new MultiSelOrgTreeItemModel();
                root.Info = info;
                root.Name = info.officeName;
                BuildTree(root, list);
                _collection.Add(root);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.orgTree.ItemsSource = root.Nodes;
                    _inited = true;
                }));
            });
        }

        private void orgTree_Loaded(object sender, RoutedEventArgs e)
        {
            ExpandInternal(this.orgTree);
        }

        private void BuildTree(MultiSelOrgTreeItemModel root, IList<DepartmentModel> orgs)
        {
            var children = orgs.Where(p => p.pid == root.Info.id.ToString());
            if (children != null && children.Count() > 0)
            {
                foreach (var item in children)
                {
                    MultiSelOrgTreeItemModel model = new MultiSelOrgTreeItemModel();
                    model.Info = item;
                    model.Name = item.officeName ;
                    model.IsChecked = false;
                    model.Parent = root;
                    BuildTree(model, orgs);
                    root.Nodes.Add(model);
                }
            }
            else
            {
                root.IsLeaf = true;
            }
        }

        /// <summary>
        /// 展开树节点
        /// </summary>
        /// <param name="targetItemContainer"></param>
        private void ExpandInternal(System.Windows.Controls.ItemsControl targetItemContainer)
        {
            try
            {
                if (targetItemContainer == null) return;
                if (targetItemContainer.Items == null) return;
                foreach (Object item in targetItemContainer.Items)
                {
                    System.Windows.Controls.TreeViewItem treeItem = targetItemContainer.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    var info = item as TreeNode;
                    if (treeItem == null || !treeItem.HasItems)
                    {
                        continue;
                    }
                    //if (info.Info == null)
                    //{
                    //    treeItem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ad7fa"));
                    //}
                    treeItem.IsExpanded = true;
                    //ExpandInternal(treeItem as ItemsControl);
                }
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("ExpandInternal error", ex);
            }
        }

        private void cxb_Node_UnChecked(object sender, RoutedEventArgs e) { }

        private void cxb_Node_Checked(object sender, RoutedEventArgs e) { }

        private void cxb_Node_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.CheckBox cbx = sender as System.Windows.Controls.CheckBox;
                var node = (sender as System.Windows.Controls.CheckBox).Tag as MultiSelOrgTreeItemModel;
                if (node != null && cbx.IsChecked != null)
                {
                    if (cbx.IsChecked.Value)
                    {
                        if (!_selectedOrgs.Contains(node.Info))
                        {
                            _selectedOrgs.Add(node.Info);
                            CheckChild(node, true);
                        }
                    }
                    else
                    {
                        if (_selectedOrgs.Contains(node.Info))
                        {
                            _selectedOrgs.Remove(node.Info);
                            CheckChild(node, false);
                        }
                    }
                    CheckParent(node);
                    btnSelected.Tag = string.Join(",", _selectedOrgs.ToList().ConvertAll(a => a.officeName));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CheckParent(MultiSelOrgTreeItemModel node)
        {
            if (node.Parent != null)
            {
                bool isCheck = true;
                foreach (MultiSelOrgTreeItemModel item in node.Parent.Nodes)
                {
                    if (!item.IsChecked)
                    {
                        isCheck = false;
                    }
                }
                if (isCheck)
                {
                    node.Parent.IsChecked = true;
                    if (!_selectedOrgs.Contains(node.Parent.Info))
                    {
                        _selectedOrgs.Insert(0, node.Parent.Info);
                    }
                }
                else
                {
                    node.Parent.IsChecked = false;
                    if (_selectedOrgs.Contains(node.Parent.Info))
                    {
                        _selectedOrgs.Remove(node.Parent.Info);
                    }
                }
                btnSelected.Tag = string.Join(",", _selectedOrgs.ToList().ConvertAll(a => a.officeName));
                if (node.Parent.Parent != null)
                {
                    CheckParent(node.Parent);
                }
            }
        }

        private void CheckChild(MultiSelOrgTreeItemModel node, bool isCheck)
        {
            if (node.Nodes.Count > 0)
            {
                if (isCheck)
                {
                    foreach (MultiSelOrgTreeItemModel item in node.Nodes)
                    {
                        item.IsChecked = true;
                        if (!_selectedOrgs.Contains(item.Info))
                        {
                            _selectedOrgs.Add(item.Info);
                        }
                        CheckChild(item, true);
                    }
                }
                else
                {
                    foreach (MultiSelOrgTreeItemModel item in node.Nodes)
                    {
                        item.IsChecked = false;
                        if (_selectedOrgs.Contains(item.Info))
                        {
                            _selectedOrgs.Remove(item.Info);
                        }
                        CheckChild(item, false);
                    }
                }
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            popup.PlacementTarget = sender as System.Windows.Controls.Button;
            popup.Placement = PlacementMode.Bottom;
            popup.IsOpen = true;
        }

        public void Select(List<string> orgIdList, ObservableCollection<MultiSelOrgTreeItemModel> Nodes = null)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (!_inited)
                {
                    System.Threading.Thread.Sleep(100);
                }
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (Nodes == null)
                    {
                        foreach (MultiSelOrgTreeItemModel item in _collection[0].Nodes)
                        {
                            if (orgIdList.Exists(a => a == item.Info.id.ToString()))
                            {
                                item.IsChecked = true;
                                if (!_selectedOrgs.Contains(item.Info))
                                {
                                    _selectedOrgs.Add(item.Info);
                                }
                            }
                            else
                            {
                                item.IsChecked = false;
                                if (_selectedOrgs.Contains(item.Info))
                                {
                                    _selectedOrgs.Remove(item.Info);
                                }
                            }
                            Select(orgIdList, item.Nodes);
                        }
                    }
                    else
                    {
                        foreach (MultiSelOrgTreeItemModel item in Nodes)
                        {
                            if (orgIdList.Exists(a => a == item.Info.id.ToString()))
                            {
                                item.IsChecked = true;
                                if (!_selectedOrgs.Contains(item.Info))
                                {
                                    _selectedOrgs.Add(item.Info);
                                }
                            }
                            else
                            {
                                item.IsChecked = false;
                                if (_selectedOrgs.Contains(item.Info))
                                {
                                    _selectedOrgs.Remove(item.Info);
                                }
                            }
                            Select(orgIdList, item.Nodes);
                        }
                    }
                    btnSelected.Tag = string.Join(",", _selectedOrgs.ToList().ConvertAll(a => a.officeName));
                }));
            });
        }
    }

    public class MultiSelOrgTreeItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MultiSelOrgTreeItemModel()
        {
            _nodes = new ObservableCollection<MultiSelOrgTreeItemModel>();
            _parent = null;
        }

        private ObservableCollection<MultiSelOrgTreeItemModel> _nodes;
        public ObservableCollection<MultiSelOrgTreeItemModel> Nodes
        {
            get { return _nodes; }
            set
            {
                this._nodes = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Nodes"));
                }
            }
        }

        private MultiSelOrgTreeItemModel _parent;
        public MultiSelOrgTreeItemModel Parent
        {
            get { return _parent; }
            set
            {
                this._parent = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Parent"));
                }
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                this._name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        /// <summary>
        /// 是否是叶子
        /// </summary>
        public bool _isLeaf;
        public bool IsLeaf
        {
            get { return _isLeaf; }
            set
            {
                this._isLeaf = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLeaf"));
                }
                if (value)
                {
                    CheckVisiable = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// 选择框是否可见
        /// </summary>
        public Visibility _checkVisiable = Visibility.Visible;
        public Visibility CheckVisiable
        {
            get { return _checkVisiable; }
            set
            {
                this._checkVisiable = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CheckVisiable"));
                }
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                this._isChecked = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        public DepartmentModel Info;
    }
}
