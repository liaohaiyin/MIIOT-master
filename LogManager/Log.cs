using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Log
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Log()
        {

        }
        public static void Debug(string msg)
        {
            log.Debug(msg);
        }
        public static void Info(string msg)
        {
            log.Info(msg);
        }
        public static void Fatal(string msg, Exception ex)
        {
            log.Fatal(msg, ex);
        }
        public static void Error(string msg, Exception ex)
        {
            log.Error(msg, ex);
        }
    }

