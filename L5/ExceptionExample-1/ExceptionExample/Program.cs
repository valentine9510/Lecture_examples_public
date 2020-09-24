using System;

namespace LearningExceptions
{
    
    class Program
    {
        static void Main(string[] args)
        {

            //*****
            //Example 1 the possible exception is not handled 
            //string s = null;
            //int myStringLength = s.Length;

            //*****
            //Example 2: the generic exception handles is used
            //try
            //{

            //    string s = null;
            //    int mystringLength = s.Length;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);

            //}


            //*****
            //Example 3: the generic exception cutsom throw
            try
            {
                string s = null;

                if (s == null)
                    throw new Exception("Throw your own exception with custom message!");
                var l = s.Length;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("The program ends");
        }
    }
}
