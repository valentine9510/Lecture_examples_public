using System;
using System.Diagnostics;

namespace ChildProcessInTheSameConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = ".\\..\\..\\..\\..\\..\\hello_world.sh",
            };

            Console.WriteLine("Starting a process...");
            using (var process = Process.Start(processInfo))
            {
                process.WaitForExit();
            }
            Console.WriteLine("Main Process Exiting ...");
        }
    }
}
