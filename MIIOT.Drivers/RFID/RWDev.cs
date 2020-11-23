using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace MIIOT.Drivers
{

    public static class RWDev
    {

        

        private const string DLLNAME = @"HFReader.dll";

       
        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenNetPort(int Port,
                                             string IPaddr,
                                             ref byte ComAddr,
                                             ref int PortHandle);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseNetPort(int PortHandle);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenComPort(int port,
                                             ref byte comAddr,
                                             byte baud,
                                             ref int frmComPortIndex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseComPort();

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseSpecComPort(int frmComPortIndex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int AutoOpenComPort(ref int port,
                                                 ref byte comAddr,
                                                 byte baud,
                                                 ref int frmComPortIndex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int Inventory_Collection(ref byte comAddr,
                                           byte Option,
                                           byte[] Target_Ant,
                                           byte[] DsfidAndUID,
                                           ref int CardNum,
                                           int frmComPortIndex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int Inventory(ref byte comAddr,
                                           ref byte State,
                                           ref byte Afi,
                                           byte[] DsfidAndUID,
                                           ref int CardNum,
                                           int frmComPortIndex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int ResetToReady(ref byte comAddr,
                                               ref byte state,
                                               byte[] UID,
                                               ref byte errorCode,
                                               int frmportindex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseRf(ref byte comAddr, int frmComPortIndex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenRf(ref byte comAddr, int frmComPortIndex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetReaderInformation(ref byte comAddr,                                                      
                                                      byte[] versionInfo,
                                                      ref byte readerType,                                                   
                                                      byte[] trType,
                                                      ref byte InventoryScanTime,
                                                      int frmComPortIndex);



        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetActiveANT( ref byte comAddr,  
                                               ref byte _ANT_Status ,
                                               int frmportindex ) ; 

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetANTStatus( ref byte comAddr,  
                                               ref byte Get_ANT_Status ,
                                               int frmportindex );


        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetParseMode( ref byte comAddr,  
                                               ref byte _ParseMode, 
                                               int frmportindex );
  
        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetParseMode( ref byte comAddr,  
                                               ref byte  Get_ParseMode ,
                                               int frmportindex ) ;

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPwr(ref byte comAddr,
                                               ref byte _Pwr,
                                               int frmportindex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPwr(ref byte comAddr,
                                        ref byte _Pwr,
                                        ref byte _PwrVal,
                                        int frmportindex);

    
        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetReaderRunStatus(ref byte comAddr,
                                            ref byte PAvolt, ref byte PAcur, ref byte pwrForward, ref byte pwrReverse, ref byte Temperature,
                                            int frmportindex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetAntennaSWR(ref byte comAddr,
                                            ref byte SWR_int, ref byte SWR_dec,
                                            int frmportindex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetSafeThreshold(ref byte comAddr,
                                            byte Temp, byte SWR_int,byte SWR_dec,
                                            int frmportindex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetSafeThreshold(ref byte comAddr,
                                            ref byte Temp, ref byte SWR_int,ref byte SWR_dec,
                                            int frmportindex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetNoiseMeasurement(ref byte comAddr,
                                            ref byte _Noise,
                                            int frmportindex);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetTagSNR(ref byte comAddr,
                                            byte[] UIDIn,
                                            byte[] UIDOut,
                                            ref byte SNR,
                                            ref byte _Noise,
                                            int frmportindex);


    }
}
