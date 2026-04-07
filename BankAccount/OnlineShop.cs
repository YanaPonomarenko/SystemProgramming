using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount;

public class OnlineShop
{
        public static void ProcessOrders(int totalOrders = 10)
    {

        CountdownEvent countdownEvent = new CountdownEvent(totalOrders);

        for (int i = 1; i <= totalOrders; i++)
        {
            int orderId = i;
            ThreadPool.QueueUserWorkItem(ProcessOrder, new object[] { orderId, countdownEvent });
        }

        countdownEvent.Wait();
        countdownEvent.Dispose();
    }

    static void ProcessOrder(object state)
    {
        object[] data = (object[])state;
        int orderId = (int)data[0];
        CountdownEvent countdownEvent = (CountdownEvent)data[1];

        int threadId = Thread.CurrentThread.ManagedThreadId;
        Random rand = new Random(orderId);
        Thread.Sleep(rand.Next(500, 1500));

        countdownEvent.Signal();
    }
}

