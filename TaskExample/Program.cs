using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task task1 = new Task(TestTask);
            //task1.Start();
            
            //Task task2 = Task.Factory.StartNew(TestTask);

            //Task task3 = Task.Run(() =>
            //{
            //    TestTask();
            //});

            //Task.WaitAll(new Task[] {task1, task2, task3});
            //Task.WaitAny(new Task[] {task1, task2, task3});

            Task<int> task4 = Task.Run(() => Add(10, 20));
            Task.WaitAll(new Task[] { task4 });
            task4.ContinueWith(t1 =>
            {
                var task5 = Task.Run(() => Avarage(task4.Result, 2));
                task5.ContinueWith(t2 =>
                {
                    Console.WriteLine($"Average: {t2.Result}");
                });
            });

            //Console.WriteLine($"Trwa wykonywanie zadania #1");
            Console.ReadKey();
        }

        static void TestTask()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] count value = {i}");
                Thread.Sleep(500);
            }
        }

        static int Add(int a, int b)
        {
            Console.WriteLine($"Liczenie sumy dla {a} oraz {b}");
            Thread.Sleep(500);
            return a + b;
        }

        static int Avarage(int sum, int n)
        {
            Console.WriteLine($"Liczenie średniej dla sumy {sum} oraz {n} liczb");
            Thread.Sleep(1000);
            return sum / n;
        }
    }
}
