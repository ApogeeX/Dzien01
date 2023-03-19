using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PararellTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PararellOper pararellOper = new PararellOper();
            //pararellOper.LoopNoPararell();

            //pararellOper.LoopWithPararell();

            //pararellOper.LoopWithPararellForeach();
            //pararellOper.LoopWithPararellBrakeStop();
            //pararellOper.ParallelInvoke();
            pararellOper.LoopParallelCancel();
            Console.ReadKey();
        }
    }
}
