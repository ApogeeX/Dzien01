using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadConsoleExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // LongOperation(); wywołanie synchroniczne

            Thread thread1 = new Thread(new ThreadStart(LongOperation));
            thread1.Priority = ThreadPriority.Normal;
            thread1.Start();

            Thread thread2 = new Thread(() => LongOperation());
            thread2.Start();

            Thread thread3 = new Thread(() => LongOperationWithParamas(5));
            thread3.Start();

            Thread thread4 = new Thread(new ParameterizedThreadStart(LongOperationWithParamas));
            thread4.Start(5);

            Thread thread5 = new Thread(() => LongOperationWithParamas2(12, 600));
            thread5.Start();

            thread1.Abort();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Koniec pracy...");
            Console.ReadKey();

            if (thread1.IsAlive)
                thread1.Abort();
            if (thread2.IsAlive)
                thread2.Abort();
            if (thread3.IsAlive)
                thread3.Abort();
            if (thread4.IsAlive)
                thread4.Abort();
            if (thread5.IsAlive)
                thread5.Abort();
        }

        static void LongOperation()
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"{threadID} couter : {i}");
            }
        }

        static void LongOperationWithParamas(object counter)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < (int)counter; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{threadID} couter : {i}");
            }
        }

        static void LongOperationWithParamas2(int counter, int delay)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < counter; i++)
            {
                Thread.Sleep(delay);
                Console.WriteLine($"{threadID} couter : {i}");
            }
        }
    }
}
