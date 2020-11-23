using LiteDB;
using MIIOT.Drivers;
using MIIOT.Model.TricalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/7/23 18:33:01
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Trical.Common
{
    public class ScreenManager
    {
        private static object obj = new object();
        private static ScreenManager _ins;

        public static ScreenManager Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                        _ins = new ScreenManager();
                }
                return _ins;
            }
        }

        public ScreenManager()
        {

        }
        public void init(string Remote, string Target)
        {
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                var temp = LiteDBHelper.Ins.GetCollection<ScreenDataModel>().FindAll(a => a.Target == Target);
                foreach (var item in temp)
                {
                    SendMsg(Remote, item.Target, item.CMD, item.SourceType, item.dataMsg);
                    
                }
            });

        }
        public void SendMsg(string Remote, string Target, string CMD, string SourceType, string dataMsg)
        {
            CacheData.Ins.TcpServer.SendByJson(Remote, Target, CMD, SourceType, dataMsg);
            CacheScreenData(Target, CMD, SourceType, dataMsg);
        }
        public void CacheScreenData(string Target, string CMD, string SourceType, string dataMsg)
        {
            string dataMD5 = $"{Target}{SourceType}{dataMsg}".MD5Encrypt();
            if (CMD == "ADD")
            {
                var temp = LiteDBHelper.Ins.GetCollection<ScreenDataModel>().FirstOrDefault(a => a.dataMD5 == dataMD5);
                if (temp != null) return;
                ScreenDataModel screenData = new ScreenDataModel()
                {
                    Target = Target,
                    CMD = CMD,
                    SourceType = SourceType,
                    dataMsg = dataMsg,
                    dataMD5 = dataMD5
                };
                LiteDBHelper.Ins.Insert<ScreenDataModel>(screenData);
            }
            else
            {
                LiteDBHelper.Ins.Delete<ScreenDataModel>(Query.EQ("dataMD5", dataMD5));
            }
        }
    }
}
