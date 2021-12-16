using System;
using System.Threading.Tasks;

namespace TasksIo
{
    class Program
    {
        static void Main(string[] args)
        {

            //Task<string> task = new Task<string>(() => GetPosts("https://jsonplaceholder.typicode.com/posts"));
            //task.Start();
            Task<string> task = Task.Factory.StartNew<string>
                (() => GetPosts("https://jsonplaceholder.typicode.com/users"));

            SomethingElse();

            try
            {
                task.Wait();
                Console.WriteLine(task.Result);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void SomethingElse()
        {
            //Implementation of a function that does something completely unrelated to the other Task 
            Console.WriteLine(" Something else");
        }

        private static string GetPosts(string url)
        {
            //throw null;
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
