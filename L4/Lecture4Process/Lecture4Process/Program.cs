﻿//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=netcore-3.1

using System.Diagnostics;
namespace MyProcess
{
    class MyProcess
    {
        public static void Main()
        {
            using Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "/Users/ali/github/csharp/Lecture_examples/L4/hello_world.sh";
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
        }
    }
}

