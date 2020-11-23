using MIIOT.DiagManager.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Core
{
    public class ModuleItem
    {

        public ModuleItem(ModuleInfo module)
        {
            if (module == null)
                throw new ArgumentNullException("module");
            this.ModuleInfo = module;
        }
        public ModuleInfo ModuleInfo { get; set; }
        public string ModuleDir
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", ModuleInfo.AssemblyString);
            }
        }
        public string ModuleFullPath
        {
            get
            {
                return Path.Combine(ModuleDir, $"{ModuleInfo.AssemblyString}.dll");
            }
        }
        public string Version { get; private set; }

        public void Load()
        {
            try
            {
                var dlls = Directory.GetFiles(ModuleDir, "*.dll");
                if (dlls.Length == 0)
                    return;
                foreach (var dll in dlls)
                {
                    var ass = Assembly.LoadFrom(dll);

                    if(dll == ModuleFullPath && ass != null)
                    {
                        Version = ass.GetName().Version.ToString();
                    }
                }
            }
            catch
            {
            }
        }
    }
}
