using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Core.Helper
{
    public delegate void NotifyEventHandler(object sender, string macType, string cabientNo, bool IsConnected);
    public class DriverObserver
    {
        public NotifyEventHandler NotifyEvent;

        public DriverObserver()
        {
        }

        #region 新增对订阅号列表的维护操作
        public void AddObserver(NotifyEventHandler ob)
        {
            NotifyEvent += ob;
        }
        public void RemoveObserver(NotifyEventHandler ob)
        {
            NotifyEvent -= ob;
        }

        #endregion

        public virtual void Start()
        {

        }
        public void Notify(string macType, string cabientNo, bool isConnected)
        {
            if (NotifyEvent != null)
            {
                NotifyEvent(this, macType, cabientNo, isConnected);
            }
        }
    }
}
