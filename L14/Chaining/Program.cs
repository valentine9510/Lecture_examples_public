using System;
using System.Threading.Tasks;

namespace TaskChaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> antecedent = Task.Run(() =>
            {
                Task.Delay(2000);
                return DateTime.Today.ToShortDateString();
            });

            Task<string> continuation = antecedent.ContinueWith(x =>
            {
                return "Today is " + antecedent.Result;
            });

            Task<int> continuation2 = continuation.ContinueWith(x => printFunction());

            Console.WriteLine("I added this, " + continuation.Result);
            Console.WriteLine(continuation2.Result);
        }

        static int printFunction()
        {
            Console.WriteLine(" Testing Void function ");

            return 1000;
        }
    }
}