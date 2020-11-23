using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Trical.Common
{
    public class CamanerHelper
    {
        const string DllName = @".\HCamera\DoccameraDll.dll";
        [DllImport(DllName)]
        extern static int bStartPlayRotateEx(int hWnd, int sRotate);

        [DllImport(DllName)]
        extern static int bStartPlay2Ex(int hwnd, int sRotate);

        [DllImport(DllName)]
        extern static int bStopPlay();
        [DllImport(DllName)]
        extern static int bSaveJPG(string filePath, string fileName);
        [DllImport(DllName)]
        extern static string sGetBarCodeEx(int BarcodeType, string imagePath);
        public CamanerHelper()
        {
          
        }

        public void Start(int handle)
        {
            bStartPlayRotateEx(handle, 0);
        }
    }
}
