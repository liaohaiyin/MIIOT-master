using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class RFIDPrinter : DriverObserver
    {
        public RFIDPrinter() : base()
        {

        }
        private List<string> ReStrCut(string item)
        {
            List<string> list = new List<string>();
            if (item.Length > 5)
            {
                list.Add(item.Substring(0, 5));
                var temp = ReStrCut(item.Substring(5, item.Length - 5));
                list.AddRange(temp);
                return list;
            }
            else
            {
                list.Add(item);
                return list;
            }
        }
        public string GetErrMsg(int errdata)
        {
            int a = 0;
            byte[] MsgData = new byte[1024];
            a = PrintLab.PTK_GetErrorInfo(errdata, MsgData, 1024);//打开

            string msg = Encoding.GetEncoding("GBK").GetString(MsgData).TrimEnd('\0');

            return msg;
        }
        public int Printer(List<string> datas, out string code)
        {
            code = "";
            int a = 0;
            try
            {
                char para = 'N';
                byte[] RFIDdata = new byte[1024];
                int len = 1024;
                a = PrintLab.PTK_OpenUSBPort(255);//打开
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_OpenUSBPort{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_ClearBuffer();//清缓存
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_ClearBuffer{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_SetLabelHeight(600, 120, 0, true); //设置标签为高度为300dots的连续纸
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_SetLabelHeight{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_SetLabelWidth(600);//设置宽度
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_SetLabelWidth{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_SetDarkness(13);
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_SetDarkness{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_ReadHFLabeUID(para, RFIDdata, len);//读取RFID
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_ReadHFLabeUID{a}", new Exception());
                    return a;
                }
                code = Encoding.GetEncoding("GBK").GetString(RFIDdata).TrimEnd('\0');
                for (int i = 0; i < datas.Count; i++)
                {
                    a = PrintLab.PTK_DrawText_TrueType(20, (uint)(20 + (i * 50)), 35, 0, "黑体", 1, 400, 0, 0, 0, datas[i]);
                    if (a > 0)
                    {
                        PrintLab.PTK_CloseUSBPort();
                        Log.Fatal($"打印机异常PTK_DrawText_TrueType{a}", new Exception());
                        return a;
                    }
                }
                a = PrintLab.PTK_DrawText_TrueType(20, (uint)(30 + (datas.Count * 50)), 35, 0, "黑体", 1, 400, 0, 0, 0, code);
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_DrawText_TrueType{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_DrawBar2D_QR(380, 380, 0, 0, 0, 8, 0, 0, 8, code);//打印二维码
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_DrawBar2D_QR{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_PrintLabel(1, 1);//打印一张标签
                if (a > 0)
                {
                    PrintLab.PTK_CloseUSBPort();
                    Log.Fatal($"打印机异常PTK_PrintLabel{a}", new Exception());
                    return a;
                }
                a = PrintLab.PTK_CloseUSBPort();
            }
            catch (Exception ex)
            {
                a = PrintLab.PTK_CloseUSBPort();//关闭
                Log.Error("打印异常", ex);
            }

            return a;
        }
    }
    public enum errCode
    {
        [Description("语法错误")]
        SYNTAX = 1,
        [Description("碳带探测出错")]
        CERR = 82,
        [Description("标签探测出错")]
        LABLEERR = 83,
        [Description("语法错误")]
        HEADER = 87,
        [Description("打印头未关闭")]
        STOP = 88,
        [Description("暂停状态")]
        WRITERERR = 110,
        [Description("标签校准失败")]
        CALIRATION = 111,
        [Description("语法错误")]
        READERR = 116,
        [Description("读取RFID标签数据失败")]
        CONNECTERR = 3002,
        [Description("连接错误")]
        CONNECtIONERR = 3003

    }

    public class PrintLab
    {


        [DllImport("CDFPSK.dll")]
        public static extern int PTK_OpenUSBPort(uint px);
        [DllImport("CDFPSK.dll")]
        public static extern int SetPCComPort(System.UInt32 BaudRate, bool HandShake);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_SetPrintSpeed(uint px);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_SetDarkness(uint id);
        [DllImport("CDFPSK.dll")]
        public static extern int ClosePort();
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_FormDel(string pid);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_FormDownload(string pid);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_FormEnd();
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_ExecForm(string pid);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_Download();
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DownloadInitVar(string pstr);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_PrintLabel(uint number, uint cpnumber);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DefineCounter(uint id, uint maxNum, short ptext, string pstr, string pMsg);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawTextTrueTypeW
                                            (int x, int y, int FHeight,
                                            int FWidth, string FType,
                                            int Fspin, int FWeight,
                                            bool FItalic, bool FUnline,
                                            bool FStrikeOut,
                                            string id_name,
                                            string data);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawBarcode(uint px,
                                        uint py,
                                        uint pdirec,
                                        string pCode,
                                        uint pHorizontal,
                                        uint pVertical,
                                        uint pbright,
                                        char ptext,
                                        string pstr);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_SetLabelHeight(uint lheight, uint gapH, int gapOffset, bool bFlag);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_SetLabelWidth(uint lwidth);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_ClearBuffer();
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawRectangle(uint px, uint py, uint thickness, uint pEx, uint pEy);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawLineOr(uint px, uint py, uint pLength, uint pH);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawBar2D_QR(uint x, uint y, uint w, uint v, uint o, uint r, uint m, uint g, uint s, string pstr);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawBar2D_QREx(uint x, uint y, uint o, uint r, uint g, uint s, uint v, string id_name, string pstr);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawBar2D_Pdf417(uint x, uint y, uint w, uint v, uint s, uint c, uint px, uint py, uint r, uint l, uint t, uint o, string pstr);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_PcxGraphicsDel(string pid);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_PcxGraphicsDownload(string pcxname, string pcxpath);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawPcxGraphics(uint px, uint py, string gname);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_ReadRFIDLabelData(uint nDataBlock, uint nRFPower, uint bFeed, byte[] data, int dataSize);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawText(uint px, uint py, uint pdirec, uint pFont, uint pHorizontal, uint pVertical, char ptext, string pstr);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawTextEx(uint px, uint py, uint pdirec, uint pFont, uint pHorizontal, uint pVertical, char ptext, string pstr, bool varible);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawText_TrueTypeEx(uint px, uint py, uint fHeight, uint fWidth, string fType, uint fSpin, uint fWeight,
            uint fitalic, uint funline, uint fstrikeOut, uint lineMaxWidth, uint lineMaxNum, int lineGapH, uint middleSwitch, string pstr);

        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawText_TrueType(uint px, uint py, uint fHeight, uint fWidth, string fType, uint fSpin, uint fWeight,
            uint fitalic, uint funline, uint fstrikeOut, string pstr);

        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawBar2D_DATAMATRIX(uint x, uint y, uint w, uint h, uint o, uint m, string pstr);//DataMatrix二维条码
                                                                                                                       //[DllImport("CDFPSK.dll")]
                                                                                                                       //public static extern int PTK_DrawBar2D_Pdf417(uint x, uint y, uint w, uint v, uint s, uint c, uint px, uint py, uint r, uint l, uint t, uint o, string pstr);  //PDF417二维条码
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_DrawBar2D_HANXIN(uint x, uint y, uint w, uint v, uint o, uint r, uint m, uint g, uint s, string pstr);//汉信码二维条码  

        [DllImport("CDFPSK.dll")]
        public static extern int PTK_GetErrorInfo(int error_n, byte[] errorInfo, int infoSize);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_ReadRFTagDataUSB(uint usbPort, uint nDataBlock, uint nRFPower, bool bFeed, string data);

        [DllImport("CDFPSK.dll")]
        public static extern int PTK_ReadHFLabeUID(char usbPort, byte[] data, int infoSize);
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_ReadHFTagUIDPrintAuto();
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_CloseUSBPort();
        [DllImport("CDFPSK.dll")]
        public static extern int PTK_SetUtilityInfoProc(byte[] _G1Info, uint infoNum, string info);

    }
    enum PrinterSetting
    {
        POSLanguage = 1, //打印机语言 中文/英文
        POSThermal = 2, //打印方式 热敏/热转印
        POSSensorMode = 3, //纸张探测方式 穿透/下反射/上反射
        POSDark = 4, //打印黑度 0-20
        POSSpeed = 5, //打印速度 1-8
        POSDirect = 6, //打印方向
        POSCMDType = 9, //打印机命令类型
        POSCUTMode = 11, //切刀模式
        POSCUTPage = 12, //切纸频率
        POSCUTPos = 13,  //刀片位置
        POSPEELMode = 14, //剥纸模式
        POSCOMSpeed = 15, //串口速率
        POSIP = 18, //打印机IP
        POSIPMask = 19,  //打印机掩码
        POSGateWay = 20, //打印机网关
        POSPort = 21, //打印机Port
        POSOffsetX = 22, //打印机水平偏移
        POSOffsetY = 23, //垂直偏移
        POSOffsetPaneel = 24,//撕纸偏移
        POSOffsetTph = 25, //定位偏移
        POSOffsetCut = 26, //切纸偏移
        POSOffsetPeel = 27, //剥纸偏移
        POSDate = 28, //打印机当前时间
        POSPTearMode = 30, //撕纸模式
    };
    enum ReadErr
    {
        Err = 0003,//无法读取标签
        Other = 0005,//读取写过的标签
        MulQty = 0006,//读取到至少2个标签
    }
}
