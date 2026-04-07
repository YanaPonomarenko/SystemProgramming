namespace BankAccount
{
     class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Обробка замовлень\n");

            int totalOrders = 10;
            CountdownEvent countdownEvent = new CountdownEvent(totalOrders);

            for (int i = 1; i <= totalOrders; i++)
            {
                int orderId = i;
                ThreadPool.QueueUserWorkItem(ProcessOrder, new object[] { orderId, countdownEvent });
            }

            countdownEvent.Wait();
            Console.WriteLine("\nВсі замовлення оброблені");
            countdownEvent.Dispose();
            Console.ReadKey();
        }

        static void ProcessOrder(object state)
        {
            object[] data = (object[])state;
            int orderId = (int)data[0];
            CountdownEvent countdownEvent = (CountdownEvent)data[1];

            int threadId = Thread.CurrentThread.ManagedThreadId;
            Random rand = new Random(orderId);
            Thread.Sleep(rand.Next(500, 1500));

            Console.WriteLine($"Замовлення {orderId} оброблено на потоці {threadId}");
            countdownEvent.Signal();
        }




        //    private static BankAccount account;

        //    static void Main(string[] args)
        //    {
        //        account = new BankAccount(1000);

        //        Thread[] threads = new Thread[3];

        //        for (int i = 0; i < 3; i++)
        //        {
        //            threads[i] = new Thread(DoWork);
        //            threads[i].Name = $"Потік-{i + 1}";
        //            threads[i].Start();
        //        }

        //        foreach (Thread t in threads)
        //        {
        //            t.Join();
        //        }

        //        Console.WriteLine($"\nФінальний баланс: {account.GetBalance()}");
        //        Console.ReadKey();
        //    }

        //    static void DoWork()
        //    {
        //        Random rand = new Random();

        //        for (int j = 0; j < 10; j++)
        //        {
        //            int operation = rand.Next(0, 3);
        //            decimal amount = rand.Next(50, 500);

        //            switch (operation)
        //            {
        //                case 0:
        //                    account.Deposit(amount);
        //                    break;
        //                case 1:
        //                    account.Withdraw(amount);
        //                    break;
        //                case 2:
        //                    if (rand.Next(0, 2) == 0)
        //                        account.Block();
        //                    else
        //                        account.Unblock();
        //                    break;
        //            }

        //            Thread.Sleep(rand.Next(100, 500));
        //        }
        //    }

    }
}

