using ParallelHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallOnceExample
{
    class Program
    {
        static OnceFlag _flag = new OnceFlag();
        static void Main(string[] args)
        {
            var t1 = new Thread(() => DoOnce(1));
            var t2 = new Thread(() => DoOnce(2));
            var t3 = new Thread(() => DoOnce(3));
            var t4 = new Thread(() => DoOnce(4));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
        }

        private static void DoOnce(int index)
        {
            Call.Once(_flag, () => Console.WriteLine("Callled once (" + index + ")"));
        }
    }
}
