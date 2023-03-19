using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ThreadPoolTest
{
    internal class Program
    {
        static int totalUrls;
        static void Main(string[] args)
        {
            List<string> urls = new List<string> 
            { 
            "http://onet.pl/",
            "http://awas-serwis.pl/",
            "http://alx.pl/",
            "http://pudelek.pl/"
            };

            totalUrls = urls.Count; 
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(15, 15);
            foreach (var url in urls)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DownloadData), url);
            }

            Console.ReadKey();
        }

        static void DownloadData(object urlObject)
        {
            string url = (string)urlObject;
            Console.WriteLine($"Pobieram dane z {url}");
            WebClient webCient = new WebClient();
            string data = webCient.DownloadString(url);
            Console.WriteLine($"Zakończono pobieranie {url}, liczba danych: {data.Length}");
            Interlocked.Decrement(ref totalUrls);
        }

        public static void CustomMethod(object obj)
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"Thread ID: {thread.ManagedThreadId}");
            Thread.Sleep(1000);
        }
    }
}
