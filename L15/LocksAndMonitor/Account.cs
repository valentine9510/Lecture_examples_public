using System;
using System.Threading;

namespace LocksAndMonitor
{
    internal class Account
    {
        Object LockA = new Object();
        int balance;
        Random random = new Random();
        public Account(int initialBalance)
        {
            balance = initialBalance;
        }

        int Withdraw(int amount)
        {
            if (balance < 0)
            {
                throw new Exception("Not enough balance");
            }

            Monitor.Enter(LockA);
            try
            {
                
                if (balance >= amount)
                {
                    Console.WriteLine("Amount drawn: " + amount);
                    balance = balance - amount;

                    return balance;
                }
            }
            finally
            {
                Monitor.Exit(LockA);
            }


            //lock (LockA)
            //{
            //    if (balance >= amount)
            //    {
            //        Console.WriteLine("Amount drawn: " + amount);
            //        balance = balance - amount;

            //        return balance;
            //    }
            //}

            return 0;
           
        }

        public void WithdrawRandomly()
        {
            for (int i = 0; i < 100; i++)
            {
                var balance = Withdraw(random.Next(2000, 5000));
                if (balance > 0)
                {
                    Console.WriteLine("Balance left" + balance);
                }
                else
                {
                    Console.WriteLine("Balance left" + balance);
                }
            }
        }
    }
}
