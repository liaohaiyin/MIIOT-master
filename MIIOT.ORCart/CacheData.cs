using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.Common;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.ORCart
{
    public class CacheData
    {
        private static CacheData _ins;
        public static object obj = new object();
        public static CacheData Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                    {
                        _ins = new CacheData();
                    }
                }
                return _ins;
            }
        }
        public CacheData()
        {
            SnowId = new IdWorker(3);
            JsonUserCache = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\userCache.json");
            JsonConfig = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\config.json");
            CartNo = JsonConfig["CartNo"];
            string Host = JsonConfig["Host"];
            dbUtil = new DapperUtil(JsonConfig["DBStr"]);
            sMHttpRequester = new SMHttpRequester(Host);
            List<pub_surgery> pub_Surgeries = dbUtil.Query<pub_surgery>(
                "SELECT b.opsubtitle surgery_name,customname patient from cart_clear a INNER JOIN spd_surgeryinfo b on a.origin_id=b.surgeryid ORDER BY editdate DESC LIMIT 1", null);
            if (pub_Surgeries != null && pub_Surgeries.Count > 0)
            {
                PreReceive = pub_Surgeries[0].surgery_name + "   " + pub_Surgeries[0].patient;
            }
            var custock = dbUtil.Query<spd_stockhouse>("SELECT b.* from sys_param a INNER JOIN spd_stockhouse b on a.paramkey=b.storehouseid and a.paramtype='CurrStock'", null);
            if (custock != null && custock.Count > 0)
                currStock = custock[0];

        }
        public MainWindow mainWindow { get; set; }
        public DapperUtil dbUtil { get; set; }
        public JsonConfigHelper JsonConfig { get; set; }
        public JsonConfigHelper JsonUserCache { get; set; }
        public List<sys_menu> CurrMenus = new List<sys_menu>();
        public string CartNo { get; set; }
        public sys_user User { get; set; } = new sys_user();
        public IdWorker SnowId;
        public bool isReplenishEdit { get; set; }
        public string SelectedMenu { get; set; }

        public sys_menu RootMenu = new sys_menu();
        public string PreReceive { get; set; }
        public SMHttpRequester sMHttpRequester;
        public spd_stockhouse currStock { get; set; }
        public bool isAutoSync { get; set; } = true;

#if Release

        public BaiduIDFaceUtil faceUtil;
#endif
    }
}
