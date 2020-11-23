using MIIOT.DiagManager.Model;
using MIIOT.UI.Core;
using System;

namespace MIIOT.DiagManager.Core
{
    public class XamlResourceItem
    {
        public XamlResourceItem(XamlResourceInfo res)
        {
            if (res == null)
                throw new ArgumentNullException("res");
            this.XamlResourceInfo = res;
        }
        public XamlResourceInfo XamlResourceInfo { get; set; }

        public enumModuleStatus Status { get; private set; } = enumModuleStatus.Unloaded;

        public void Load()
        {
            try
            {
                ResourceManager.Load(XamlResourceInfo.Path);
                Status = enumModuleStatus.Loaded;
            }
            catch
            {
                Status = enumModuleStatus.LoadError;
            }
        }
    }
}
