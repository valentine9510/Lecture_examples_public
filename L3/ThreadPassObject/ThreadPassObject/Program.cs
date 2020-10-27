// C# program to illustrate the 
// use of Thread(ParameterizedThreadStart) 
// constructor with non-static method 
using System; 
using System.Threading; 
  
public class MyThread
{

    // Non-static method 
    public void Job()
    {
        for (int z = 0; z < 3; z++)
        {

            Console.WriteLine("My thread is " +
                          "in progress...!!");
        }
    }
}

// Driver Class 
public class GFG
{

    // Main Method 
    public static void Main()
    {
        // Creating object of MYTHREAD class 
        MyThread obj = new MyThread();

        // Creating a thread which 
        // calls a parameterized instance method 
        Thread thr = new Thread(obj.Job);
        thr.Start();
    }
}