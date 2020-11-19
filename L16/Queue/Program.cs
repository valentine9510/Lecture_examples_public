using System;
using System.Collections.Generic;
public class SamplesQueue
{

    public static void Main()
    {

        // Creates and initializes a new Queue.
        Queue<string> myQ = new Queue<string>();
        myQ.Enqueue("Hello");
        myQ.Enqueue("World");
        myQ.Enqueue("!");


        // Displays the properties and values of the Queue.
        Console.WriteLine("Just print myQ values:");
        PrintValues(myQ);

        
        Console.WriteLine("Peek and Dequeue myQ values:");
        Console.WriteLine("count:{0}", myQ.Count);
        while (myQ.Count>0)
        {
            Console.WriteLine("Peek: {0}", myQ.Peek());
            Console.WriteLine("Dequeue: {0}", myQ.Dequeue());
            
        }




    }

    public static void PrintValues(IEnumerable<string> myCollection)
    {
        foreach (Object obj in myCollection)
            Console.Write("    {0}", obj);
        Console.WriteLine();
    }


}
