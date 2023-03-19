using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PararellTest
{
    internal class PararellOper
    {
        Random random = new Random();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public void LoopParallelCancel()
        {
            new Thread(() =>
            {
                Thread.Sleep(1_000);
                cancellationTokenSource.Cancel();

            }).Start();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 4;
            parallelOptions.CancellationToken = cancellationTokenSource.Token;
            try
            {

                Parallel.For(0, 10, parallelOptions, i =>
                {
                    long total = LongOperation();
                    Console.WriteLine($"{i} - {total}");
                    Thread.Sleep(500);
                });
            } catch (OperationCanceledException exc)
            {
                Console.WriteLine(exc.Message);
            }
            sw.Stop();
            Console.WriteLine($"LoopParallelCancel - {sw.Elapsed.TotalMilliseconds}");
        }
        public void LoopNoPararell()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                long total = LongOperation();
                Console.WriteLine($"{i} - {total}");
            }
            sw.Stop();
            Console.WriteLine($"LoopNoPararell - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopWithPararell()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 8;
            Parallel.For(0, 10, parallelOptions, i =>
            {
                long total = LongOperation();
                Console.WriteLine($"{i} - {total}");
            });
            sw.Stop();
            Console.WriteLine($"LoopWithPararell - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopWithPararellBrakeStop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 8;
            Parallel.For(0, 10, parallelOptions, (i, loopState) =>
            {
                long total = LongOperation();
                if (i >= 5)
                {
                    loopState.Stop();
                }
                Console.WriteLine($"{i} - {total}");
            });
            sw.Stop();
            Console.WriteLine($"LoopWithPararellBrakeStop - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopWithPararellForeach()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 8;
            List<int> integersList = Enumerable.Range(0, 10).ToList();
            Parallel.ForEach( integersList, parallelOptions, i =>
            {
                long total = LongOperation();
                Console.WriteLine($"{i} - {total}");
            });
            sw.Stop();
            Console.WriteLine($"LoopWithPararellForeach - {sw.Elapsed.TotalMilliseconds}");
        }

        public void ParallelInvoke()
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4
            };
            Parallel.Invoke( options,
                () => TestTask(1),
                () => TestTask(2),
                () => TestTask(3),
                () => TestTask(4)
                );
        }

        private long LongOperation()
        {
            long total = 0;
            for (int i = 0; i < 100_000_000; i++)
            {
                total += i;
            }
            return total;
        }

        private void TestTask(int nr)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"Start zadania [{nr}]");
            sw.Start();
            Thread.Sleep(random.Next(500, 1200));
            sw.Stop();
            Console.WriteLine($"Koniec zadania [{nr}] - czas trwania {sw.Elapsed.TotalMilliseconds}");
        }
    }
}
