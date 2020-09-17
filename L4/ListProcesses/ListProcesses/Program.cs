using System;
using System.Diagnostics;


namespace ListProcesses
{
    class Program
    {
        static void Main(string[] args)
        {
            // Show all processes on the local computer.
            Process[] processes = Process.GetProcesses();
            // Display count.
            // Console.WriteLine("Count: {0}", processes.Length);
            // Loop over processes.
            foreach (Process process in processes)
            {
                Console.WriteLine("Process Name: {0}, Process Id:{1}", process.ProcessName, process.Id );
            }
        }
    }
}
