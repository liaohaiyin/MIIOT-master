using MIIOT.Data;
using MIIOT.Data.Entity;
using MIIOT.Data.Service.Helper;
using MIIOT.DiagManager.Core;
using MIIOT.DiagManager.Helper;
using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Model.Validation;
using MIIOT.DiagManager.Pages;
using MIIOT.UI;
using MIIOT.UI.ControlEx;
using MIIOT.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MIIOT.DiagManager.ViewModels
{
    public class WorkAcceptInfoPageVm : NbPageVm
    {
        #region 常量
        public static readonly pub_accept AllBillsTypeInfo = new pub_accept() { ID = -1, source_no = "全部" };
        #endregion

        public bool IsCache { get; private set; }

        #region 缓存数据
        public List<pub_accept> BillsTypeInfosCache { get; } = new List<pub_accept>();
        #endregion

        #region 条件绑定
        public int ScanMaxLength { get; private set; } = 20;

        #endregion

        #region 字段绑定显示
        protected string _organ_name;
        /// <summary>
        /// 医院名称
        /// </summary>
        public virtual string organ_name
        {
            get { return _organ_name; }
            set { _organ_name = value; OnPropertyChanged(nameof(organ_name)); }
        }

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
        /// 库房名称
        /// </summary>
        public virtual string storehouse_name
        {
            get { return _storehouse_name; }
            set { _storehouse_name = value; OnPropertyChanged(nameof(storehouse_name)); }
        }

        protected string _accept_person_name;
        /// <summary>
        /// 验收人
        /// </summary>
        public virtual string accept_person_name
        {
            get { return _accept_person_name; }
            set { _accept_person_name = value; OnPropertyChanged(nameof(accept_person_name)); }
        }
        #endregion

        #region 初始化页面
        public WorkAcceptInfoPage Vmpage { get; set; }

        public string PrintNum { get; } = GetSetting("PrintNum");

        public WorkAcceptInfoPageVm(NbPage page)
        {
            this.Vmpage = page as WorkAcceptInfoPage;
            this.Items.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    this._hasCollectItem = true;
                }
            };
            this.Items.Add(new pub_accept_dtl { ID = 1273155039877791799, organ_id = 1, accept_id = 1273155039877791799, source_type = 0, source_id = 1273155039877791799, goods_name = "纤维连接蛋白检测试剂盒1" });
            
        }

        #endregion

        #region 表格绑定属性
        private ObservableCollection<pub_accept_dtl> _items = new ObservableCollection<pub_accept_dtl>();

        public ObservableCollection<pub_accept_dtl> Items
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
        protected pub_accept _searchBillsTypeInfo;

        public virtual pub_accept SearchBillsTypeInfo
        {
            get { return _searchBillsTypeInfo; }
            set { _searchBillsTypeInfo = value; OnPropertyChanged(nameof(SearchBillsTypeInfo)); }
        }

        protected ObservableCollection<pub_accept> _searchBillsTypeInfoList;

        public virtual ObservableCollection<pub_accept> SearchBillsTypeInfoList
        {
            get { return _searchBillsTypeInfoList; }
            set { _searchBillsTypeInfoList = value; OnPropertyChanged(nameof(SearchBillsTypeInfoList)); }
        }

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

        #region 属性
        protected bool _hasVerify = false;

        public virtual bool HasVerify
        {
            get { return this._hasVerify; }
            set { _hasVerify = value; OnPropertyChanged(nameof(HasVerify)); }
        }

        protected bool _hasOverReceipt = false;

        public virtual bool HasOverReceipt
        {
            get { return this._hasOverReceipt; }
            set { _hasOverReceipt = value; OnPropertyChanged(nameof(HasOverReceipt)); }
        }

        protected bool _hasCollectItem = false;

        public virtual bool HasCollectItem
        {
            get { return this._hasCollectItem; }
            set { _hasCollectItem = value; OnPropertyChanged(nameof(HasCollectItem)); }
        }
        #endregion

        /// <summary>
        /// 初始化数据
        /// </summary>
        public override void Init()
        {
            using (var db = AppRuntime.DbService.CreateDb())
            {
                //LoadCache(db);
                LoadSearch();
                LoadNavItem(db);

                this._hasVerify = true;
                this._hasOverReceipt = false;
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

                var query = db.Query<pub_accept>();
                if (!string.IsNullOrWhiteSpace(this.SearchOrderNo))
                    query.And(p => p.source_no.Like($"%{this.SearchOrderNo}%"));

                var qRet = query.OrderBy(p => p.ID, Data.OrderByType.Asc).ToList();
                var list = new List<pub_accept>(qRet.Data ?? new List<pub_accept>());
                LoadToolMenu(list);
                IsWaiting = false;
                Thread.Sleep(new Random().Next(100, 1000));
            });
        }

        /// <summary>
        /// 显示商品数据
        /// </summary>
        /// <param name="db"></param>
        public void LoadItems(Db db, Int64 ID)
        {
            Task.Run(() =>
            {
                IsWaiting = true;

                var query = db.Query<pub_accept_dtl>();
                query.And(p => p.accept_id == ID);

                var qRet = query.OrderBy(p => p.ID, Data.OrderByType.Desc).Page(this.PageNo, this.PageSize);
                this.TotalPages = (int)Math.Ceiling((double)qRet.Total / (double)this.PageSize);
                this.TotalItems = qRet.Total;
                this.Items = new ObservableCollection<pub_accept_dtl>(qRet.Data ?? new List<pub_accept_dtl>());

                IsWaiting = false;
                Thread.Sleep(new Random().Next(100, 500));
            });
        }

        /// <summary>
        /// 数据缓存
        /// </summary>
        /// <param name="db"></param>
        public void LoadCache(Db db)
        {
            IsCache = true;
            IsCache = IsCache & LoadCacheItem(BillsTypeInfosCache, db);
        }

        private bool LoadCacheItem<T>(List<T> cache, Db db)
        {
            var ret = db.Query<T>().ToList();
            if (ret.Code == DbResultCode.Error)
                return false;

            cache.Clear();
            if (ret.Code == DbResultCode.Nothing)
                return true;
            cache.AddRange(ret.Data);
            return true;
        }
        /// <summary>
        /// 加载搜索条件中的列表
        /// 保持原来搜索条件不变
        /// </summary>
        public void LoadSearch(bool original = true)
        {
            if (this.SearchBillsTypeInfoList == null) this.SearchBillsTypeInfoList = new ObservableCollection<pub_accept>();
            var oldBillsID = AllBillsTypeInfo.ID;
            if (original)
            {
                oldBillsID = this.SearchBillsTypeInfo?.ID ?? AllBillsTypeInfo.ID;
            }
            AppRuntime.ExecuteOnUI(() =>
            {
                this.SearchBillsTypeInfoList.Clear();
                this.SearchBillsTypeInfoList.Add(AllBillsTypeInfo);
                this.BillsTypeInfosCache.ForEach(item => this.SearchBillsTypeInfoList.Add(item));
            });
            this.SearchBillsTypeInfo = this.SearchBillsTypeInfoList.FirstOrDefault(p => p.ID == oldBillsID) ?? AllBillsTypeInfo;
        }

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

        #region 加载单据按钮
        /// <summary>
        /// 加载单号按钮
        /// </summary>
        /// <param name="list"></param>
        public void LoadToolMenu(List<pub_accept> list)
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

        private void RenderNavMenuItems(List<pub_accept> btn)
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

        private NbNavMenuItem CreateNbNavMenuItem(pub_accept mi)
        {
            var item = new NbNavMenuItem();
            item.Margin = new Thickness(15,0,0,5);
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
                    var qRet = AppRuntime.DbService.QueryFirstOrDefault<view_pub_accept>(p => p.source_no == selectText).Data;
                    if (qRet != null)
                    {
                        this.organ_name = qRet.organ_name;
                        this.supply_name = qRet.supply_name;
                        this.storehouse_name = qRet.storehouse_name;
                        this.accept_person_name = qRet.accept_person_name;
                        LoadItems(db, qRet.ID);
                    }
                }
            });
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

        #region 验收确认
        public ICommand VerifyResult
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

        
        #region 打印标签
        public ICommand PrintLabel
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (this.Items == null)
                        return ;
                    Task.Run(() =>
                    {
                        IsWaiting = true;
                        foreach (var item in this.Items)
                        {
                            int? printNum = Convert.ToInt32(GetSetting("PrintNum"));
                            var canPrintNum = (item?.delivery_qty ?? 0) - (printNum ?? 0);
                            if (canPrintNum == 0)
                                return;
                            for (int i = 0; i < canPrintNum; i++)
                            {
                                string labelText = null;
                                int res = PrintHelper.Init(item, out labelText);
                                if (res != 0 || labelText == null)
                                {
                                    string ErrMsg = AppRuntime.printer.GetErrMsg(res);
                                    NbMessageBox.Error($"打印机异常：{res}:" + ErrMsg);
                                    IsWaiting = false;
                                    return;
                                }
                                AppRuntime.ExecuteOnUI(() =>
                                {
                                    SetSetting("PrintNum", printNum.ToString());
                                });
                                printNum += 1;
                            }
                        }
                        IsWaiting = false;
                    });
                });
            }
        }
        #endregion

        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private void SetSetting(string key, string value)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
