using MIIOT.Data;
using MIIOT.Data.Entity;
using MIIOT.DiagManager.Core;
using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Pages;
using MIIOT.UI;
using MIIOT.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MIIOT.DiagManager.ViewModels
{
    public class FallbackCargoPageVm : NbPageVm
    {
        #region 条件绑定
        public int ScanMaxLength { get; private set; } = 20;

        #endregion

        #region 字段绑定显示
        protected string _supply_name;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string supply_name
        {
            get { return _supply_name; }
            set { _supply_name = value; OnPropertyChanged(nameof(supply_name)); }
        }
        protected string _storehouse_name;
        /// <summary>
        /// 退货库房
        /// </summary>
        public virtual string storehouse_name
        {
            get { return _storehouse_name; }
            set { _storehouse_name = value; OnPropertyChanged(nameof(storehouse_name)); }
        }

        protected string _refund_person_name;
        /// <summary>
        /// 退货人
        /// </summary>
        public virtual string refund_person_name
        {
            get { return _refund_person_name; }
            set { _refund_person_name = value; OnPropertyChanged(nameof(refund_person_name)); }
        }
        #endregion

        #region 表格绑定属性
        private ObservableCollection<pub_refund_dtl> _items = new ObservableCollection<pub_refund_dtl>();

        public ObservableCollection<pub_refund_dtl> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        #endregion

        #region 翻页相关属性
        protected int _pageNo = 1;

        public virtual int PageNo
        {
            get { return _pageNo; }
            set { _pageNo = value; OnPropertyChanged(nameof(PageNo)); }
        }

        protected int _pageSize = 10;

        public virtual int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; OnPropertyChanged(nameof(PageSize)); }
        }

        protected int _totalPages = 0;

        public virtual int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; OnPropertyChanged(nameof(TotalPages)); }
        }

        protected int _totalItems = 0;

        public virtual int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; OnPropertyChanged(nameof(TotalItems)); }
        }
        #endregion

        #region 搜索条件       

        protected string _searchOrderNo;

        public virtual string SearchOrderNo
        {
            get { return _searchOrderNo; }
            set { _searchOrderNo = value; OnPropertyChanged(nameof(SearchOrderNo)); }
        }

        protected string _searchPlaceholder;

        public virtual string SearchPlaceholder
        {
            get { return _searchPlaceholder; }
            set { _searchPlaceholder = value; OnPropertyChanged(nameof(SearchPlaceholder)); }
        }

        /// <summary>
        /// 搜索无结果提示
        /// </summary>
        protected string _searchNoHint;

        public virtual string SearchNoHint
        {
            get { return _searchNoHint; }
            set { _searchNoHint = value; OnPropertyChanged(nameof(SearchNoHint)); }
        }
        #endregion


        #region 动态添加退库单
        protected bool _AppendNavVisibility;

        public virtual bool AppendNavVisibility
        {
            get { return _AppendNavVisibility; }
            set { _AppendNavVisibility = value; OnPropertyChanged(nameof(AppendNavVisibility)); }
        }
        #endregion

        #region 初始化页面
        public FallBackCargoPage Vmpage { get; set; }

        public FallbackCargoPageVm(FallBackCargoPage page)
        {
            this.Vmpage = page;
        }
        #endregion

        /// <summary>
        /// 初始化数据
        /// </summary>
        public override void Init()
        {
            using (var db = AppRuntime.DbService.CreateDb())
            {
                LoadNavItem(db);
            }
        }

        public void LoadNavItem()
        {
            using (var db = AppRuntime.DbService.CreateDb())
                LoadNavItem(db);
        }

        public void LoadNavItem(Db db)
        {
            Task.Run(() =>
            {
                IsWaiting = true;

                var query = db.Query<pub_refund>();
                if (!string.IsNullOrWhiteSpace(this.SearchOrderNo))
                    query.And(p => p.source_no.Like($"%{this.SearchOrderNo}%"));

                var qRet = query.OrderBy(p => p.ID, Data.OrderByType.Asc).ToList();
                var list = new List<pub_refund>(qRet.Data ?? new List<pub_refund>());
                LoadToolMenu(list);
                IsWaiting = false;
                Thread.Sleep(new Random().Next(100, 1000));
            });
        }

        /// <summary>
        /// 显示商品数据
        /// </summary>
        /// <param name="db"></param>
        public void LoadItems(Db db, Int64 BillsNo)
        {
            Task.Run(() =>
            {
                IsWaiting = true;

                var query = db.Query<pub_refund_dtl>();
                query.And(p => p.refund_id == BillsNo);

                var qRet = query.OrderBy(p => p.ID, Data.OrderByType.Desc).Page(this.PageNo, this.PageSize);
                this.TotalPages = (int)Math.Ceiling((double)qRet.Total / (double)this.PageSize);
                this.TotalItems = qRet.Total;
                this.Items = new ObservableCollection<pub_refund_dtl>(qRet.Data ?? new List<pub_refund_dtl>());

                IsWaiting = false;
                Thread.Sleep(new Random().Next(100, 500));
            });
        }

        #region 加载单据按钮
        /// <summary>
        /// 加载单号按钮
        /// </summary>
        /// <param name="list"></param>
        public void LoadToolMenu(List<pub_refund> list)
        {
            Task.Run(() =>
            {
                IsWaiting = true;
                RenderNavMenuItems(list);
                IsWaiting = false;
            }).ContinueWith(m =>
            {
                AppRuntime.ExecuteOnUI(() =>
                {
                    if (this.Vmpage.ToolNavMenu.Children?.Count > 0)
                        (this.Vmpage.ToolNavMenu.Children[0] as NbNavMenuItem).IsChecked = true;
                });
            });
        }

        private void RenderNavMenuItems(List<pub_refund> btn)
        {
            AppRuntime.ExecuteOnUI(() =>
            {
                this.Vmpage.ToolNavMenu.Children.Clear();
                if (btn?.Count <= 0 && !string.IsNullOrWhiteSpace(this.SearchOrderNo))
                {
                    this.SearchOrderNo = null;
                    this.SearchNoHint = "无结果，请重试";
                }
                else
                {
                    foreach (var mi in btn)
                    {
                        this.Vmpage.ToolNavMenu.Children.Add(CreateNbNavMenuItem(mi));
                    }
                    this.SearchNoHint = null;
                }
            });
        }

        private NbNavMenuItem CreateNbNavMenuItem(pub_refund mi)
        {
            var item = new NbNavMenuItem();
            item.Margin = new Thickness(15, 0, 0, 5);
            item.Style = (Style)item.FindResource("ToolNavMenuItem");
            item.Content = mi.source_no;
            item.Tag = mi;
            item.Checked += NbNavMenuItem_Click;
            return item;
        }

        private void NbNavMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AppRuntime.ExecuteOnUI(() =>
            {
                var menuItem = sender as NbNavMenuItem;
                using (var db = AppRuntime.DbService.CreateDb())
                {
                    string selectText = menuItem.Content.ToString();
                    var qRet = AppRuntime.DbService.QueryFirstOrDefault<pub_refund>(p => p.source_no == selectText).Data;
                    if (qRet != null)
                    {
                        this.supply_name = qRet.supply_name;
                        this.storehouse_name = qRet.storehouse_name;
                        this.refund_person_name = qRet.refund_person_name;
                        LoadItems(db, qRet.ID);
                    }
                }
            });
        }
        #endregion

        #region 翻页
        public ICommand GoPage
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    using (var db = AppRuntime.DbService.CreateDb())
                        LoadNavItem(db);
                });
            }
        }
        #endregion

        #region 扫描单号回车
        public ICommand SearchCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    using (var db = AppRuntime.DbService.CreateDb())
                        LoadNavItem(db);
                });
            }
        }
        #endregion

        #region 动态显示退库信息
        public ICommand AppendNavMenuItem
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    this.AppendNavVisibility = true;
                });
            }
        }

        public ICommand CanncelAppendNavItem
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    this.AppendNavVisibility = false;
                });
            }
        }
        #endregion

    }
}
