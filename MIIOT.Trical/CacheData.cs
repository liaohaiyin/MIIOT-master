using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Trical;
using MIIOT.Model.TricalModel;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MIIOT.Trical
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
            JsonUserCache = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\userCache.json");
            JsonConfig = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\config.json");
            MasterTCP = JsonConfig["MasterName"];
            SlaveTCP = JsonConfig["SlaveName"];
            long.TryParse(JsonConfig["WarehouseID"], out WarehouseID);
            WarehouseName = JsonConfig["WarehouseName"];
            TcpServer =new SuperSoccketMrg();
            Screens.Add(MasterTCP, "");
            Screens.Add(SlaveTCP, "");
        }

        public string Host { get; set; }
        public JsonConfigHelper JsonConfig { get; set; }
        public JsonConfigHelper JsonUserCache { get; set; }
        public string Token { get; set; }
        public SolidColorBrush solid;
        public SolidColorBrush ForegroundSolid;
        public user_info _user_info { get; set; }
        public MainWindow mainWindow { get; set; }
        public SuperSoccketMrg TcpServer { get; set; }
        public string MasterTCP;
        public string SlaveTCP;
        public long WarehouseID;
        public string WarehouseName;
        public List<Stock> Stocks;
        public WSocketClient WSClient { get; set; }
        public Dictionary<string, string> Screens { get; set; } = new Dictionary<string, string>();
        public string Organ = "";
    }
}
