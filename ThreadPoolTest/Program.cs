using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(15, 15);
            for (int i = 0; i < 10 ; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CustomMethod));
            }
            Console.ReadKey();
        }

        public static void CustomMethod(object obj)
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"Thread ID: {thread.ManagedThreadId}");
            Thread.Sleep(1000);
        }
    }
}
