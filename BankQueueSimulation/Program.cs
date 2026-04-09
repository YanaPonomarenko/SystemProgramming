using System;
using System.Threading;
using BankQueueSimulation.Services;

namespace BankQueueSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Черга у банку\n");

            const int totalClients = 15;
            const int producerThreadsCount = 3;

            Bank bank = new Bank(totalClients);
            Thread[] producerThreads = new Thread[producerThreadsCount];

            int clientCounter = 0;
            object lockObj = new object();

            for (int i = 0; i < producerThreadsCount; i++)
            {
                int threadId = i + 1;
                ClientGenerator generator = new ClientGenerator(bank, threadId);

                producerThreads[i] = new Thread(() =>
                    generator.GenerateClients(totalClients, ref clientCounter, lockObj));

                producerThreads[i].Start();
            }
            Thread serverThread = new Thread(bank.ServeClients);
            serverThread.Start();

            foreach (var thread in producerThreads)
            {
                thread.Join();
            }

            serverThread.Join();

            Console.WriteLine("Робочий день завершено");
            Console.WriteLine($"Всього обслуговано клієнтів: {bank.ServedClientsCount}");
            Console.ReadKey();
        }
    }
}
