using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LockPerformance lockPerformance = new LockPerformance();
            lockPerformance.ClassicLock();
            Console.WriteLine($"Classic Lock = {lockPerformance.ClassicLock()}");
            Console.WriteLine($"Semaphore Lock = {lockPerformance.Semaphore()}");
            Console.WriteLine($"Monitor Lock = {lockPerformance.MonitorLock()}");
            Console.ReadKey();
        }
    }
}
