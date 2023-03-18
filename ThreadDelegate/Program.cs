using System;
using System.Threading;

namespace ThreadDelegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tworzenie wątku delegatem w celu notyfikacji zdarzenia zakonczenia pracy watku
            ResultCallbackDelegate resultCallback = new ResultCallbackDelegate(ResultCallbackMethod);

            SumHelper sumHelper = new SumHelper(50, resultCallback);
            Thread thread = new Thread(sumHelper.CalculateSum);
            thread.Start();
            Console.WriteLine("Liczę.....");
            thread.Join();
            Console.ReadKey();
        }

        private static void ResultCallbackMethod(int result)
        {
            Console.WriteLine($"Wynik operacji = {result}");
        }
    }
}
