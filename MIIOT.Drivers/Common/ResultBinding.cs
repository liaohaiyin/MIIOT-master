using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers.Common
{
    public class ResultBinding
    {
        double MiniTimeSpan = 2000;
        double MaxTimeSpan = 10000;
        public ResultBinding(double miniTimeSpan, double maxTimeSpan)
        {
            MiniTimeSpan = miniTimeSpan;
            MaxTimeSpan = maxTimeSpan;
        }
        private Queue SeqQueue = Queue.Synchronized(new Queue());
        public void addSeqNo(string sign)
        {
            SeqNos seq = new SeqNos() { Sign = sign, dateTime = DateTime.Now };
            SeqQueue.Enqueue(seq);
        }
        public string CheckCodeEx(string Sign, string SignEx="")
        {
            if (Sign == SignEx) return Sign;
            else if (SignEx != "")
                SeqQueue.Enqueue(new SeqNos() { Sign = SignEx, dateTime = DateTime.Now });
            SeqNos seq = SeqQueue.Count > 0 ? (SeqNos)SeqQueue.Dequeue() : null;
            if (seq == null)
            {
                return "";
            }
            else
            {

                return CheckCodeEx(Sign, seq.Sign);
            }

        }
        public bool CheckCode(string Sign, DateTime dateTime)
        {
            Thread.Sleep(5);
            SeqNos seq = SeqQueue.Count > 0 ? (SeqNos)SeqQueue.Dequeue() : null;
            if (seq == null)
                return false;
            else
            {
                //double delayTime = (DateTime.Now - seq.dateTime).TotalMilliseconds;
                //if (delayTime > MiniTimeSpan && delayTime < MaxTimeSpan)
                //{
                if (seq.Sign == Sign)
                {
                    return true;
                }
                else
                {
                    double sDelayTime = (DateTime.Now - dateTime).TotalMilliseconds;
                    if (sDelayTime < MaxTimeSpan)
                    { SeqQueue.Enqueue(seq); }

                    return CheckCode(Sign, dateTime);
                }
                //}
                //else
                //{
                //    return CheckCode(Sign, dateTime);
                //}
            }
        }
        public string bindSeqNo(string sign)
        {
            string Sign = "";
            Queue que = SeqQueue;
            SeqNos seq = que.Count > 0 ? (SeqNos)que.Peek() : null;
            if (seq == null)
                return Sign;
            else
            {
                double delayTime = (DateTime.Now - seq.dateTime).TotalMilliseconds;
                Console.WriteLine("delay" + delayTime);
                if (delayTime > MiniTimeSpan && delayTime < MaxTimeSpan)
                {
                    if (seq.Sign == sign)
                    {
                        que.Dequeue();
                        return sign;
                    }
                    else
                    {
                        int len = que.Count;
                        for (int i = 0; i < len; i++)
                        {
                            SeqNos seqtemp = que.Count > 0 ? (SeqNos)que.Peek() : null;
                            if (seqtemp == null)
                            {
                                break;
                            }
                            else
                            {
                                double delayTime1 = (DateTime.Now - seq.dateTime).TotalMilliseconds;
                                Console.WriteLine("delay1" + delayTime);
                                if (delayTime > MiniTimeSpan && delayTime < MaxTimeSpan)
                                {
                                    que.Dequeue();
                                    if (seq.Sign == sign)
                                    {
                                        return sign;
                                    }
                                    else
                                    {
                                        que.Enqueue(seqtemp);
                                    }
                                }
                                else
                                    que.Dequeue();

                            }
                        }
                    }
                }
                else
                    que.Dequeue();
            }
            return Sign;
        }

    }
    public class SeqNos
    {
        public string Sign { get; set; }
        public DateTime dateTime { get; set; }
    }
}
