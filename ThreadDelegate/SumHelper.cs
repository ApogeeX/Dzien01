using System.Threading;

namespace ThreadDelegate
{
    public delegate void ResultCallbackDelegate(int result);

    internal class SumHelper
    {
        private int number;
        private ResultCallbackDelegate resultCallback;

        public SumHelper(int number, ResultCallbackDelegate resultCallback)
        {
            this.number = number;
            this.resultCallback = resultCallback;
        }

        public void CalculateSum()
        {
            int result = 0;
            for (int i = 1; i <= number; i++)
            {
                result += i;
                Thread.Sleep(10);
            }

            if (resultCallback != null) // sprawdzic czy jest podpieta metoda do delegata
            {
                resultCallback(result);
            }
            
        }
    }
}
