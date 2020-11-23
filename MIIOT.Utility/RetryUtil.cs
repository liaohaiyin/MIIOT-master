using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    public class RetryUtil
    {

        //RetryHelper.Retry<int, Exception>(3, nTimes =>
        //{
        //    var content = new { Carton_No = barcode, Carton_Weight };
        //    List<object> detail = new List<object>();
        //    detail.Add(content);
        //    var data = new { whgid = "slhk.wh1", token, detail };
        //    string dataJson = JsonConvert.SerializeObject(data);
        //    string datastr = Http.Post(Host + UpdateWeightUrl, dataJson);
        //    return 0;
        //});


        public static int retry(int time, Func<int> trything)
        {
            for (int i = 1; i < time; i++)
            {
                int ttt = trything();
                if (ttt > time)
                {
                    return 0;
                }
            }
            return 0;
        }
        public static  T Retry<T, TException>(int timesToRetry, Func<int, T> thingToTry) where TException : Exception
        {
            // Start at 1 instead of 0 to allow for final attempt
            int i;
            for (i = 1; i < timesToRetry; i++)
            {
                try
                {
                    return  thingToTry(i);
                }
                catch (TException)
                {
                    // Maybe: Trace.WriteLine("Failed attempt...");
                }
            }

            return thingToTry(i); // Final attempt, let exception bubble up
        }

        //这里我增加了个异步版本 
        public static async Task<T> RetryAsync<T, TException>(int timesToRetry, Func<int, Task<T>> thingToTry) where TException : Exception
        {
            // Start at 1 instead of 0 to allow for final attempt
            int i;
            for (i = 1; i < timesToRetry; i++)
            {
                try
                {
                    return await thingToTry(i);
                }
                catch (TException)
                {
                    Thread.Sleep(5000);
                    // Maybe: Trace.WriteLine("Failed attempt...");
                }
            }

            return await thingToTry(i); // Final attempt, let exception bubble up
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TS">超时等待时长ms</param>
        /// <param name="OnAction">执行任务</param>
        /// <param name="TimeAction">超时处理任务</param>
        public static void TimeOutAction(int TS, Action OnAction, Action TimeAction)
        {
            CancellationTokenSource cts = new CancellationTokenSource(TS);
            cts.Token.Register(() =>
            {//等待设置时间后执行
                TimeAction();
            });
            Task.Factory.StartNew(() =>
            {//立即执行
                while (!cts.Token.IsCancellationRequested)
                {
                    OnAction();
                    Task.Delay(TS).Wait();
                }
            }, cts.Token);
        }
    }
}
