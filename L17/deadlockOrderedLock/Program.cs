//this example is worj in progress
// this example needs some work to make it an ordered lock avoidence 

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeadlockAvoidanceAllLocj
{
    class Program
    {
        public static void Main()
        {
            object lock1 = new object();
            Console.WriteLine("Starting...");
            Account A =  new Account(1,100);
            Account B = new Account(2,200);


            var task1 = Task.Run(() => transfer(100, ref A, ref B));
            //var task2 = Task.Run(() => transfer(400, ref B, ref A));

            //Task.WaitAll(task1, task2);
            Console.WriteLine("Account A and B balances:{0} and {1}, respectively.",A._balance, B._balance);
            Console.WriteLine("Finished...");
        }
        public static void transfer(double amount, ref Account a, ref Account b)
        {
            Account first = a;
            Account second = b;
            if (a._id > b._id)
            {
                first = b;
                second = a;
            }
            lock (first) {
                lock (second) {
                    first._balance -= amount;
                    second._balance += amount;
                }
            }
        }
    }
    public class Account
    {
        public double _balance;
        public int _id;
        public Account(int id, double balance) {
            this._balance = balance;
            this._id = id;

        }
    }

}
