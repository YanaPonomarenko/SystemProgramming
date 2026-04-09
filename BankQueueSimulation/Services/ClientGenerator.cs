using BankQueueSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueueSimulation.Services;

public class ClientGenerator
{
    private readonly Bank _bank;
    private readonly int _threadId;
    private readonly string[] _purposes = {
            "Відкриття рахунку",
            "Отримання кредиту",
            "Поповнення картки",
            "Консультація",
            "Закриття рахунку"
        };

    private readonly string[] _names = {
            "Олександр", "Марія", "Іван", "Олена", "Петро", "Наталія", "Сергій", "Анна", "Дмитро", "Тетяна",
            "Андрій", "Ірина", "Володимир", "Катерина", "Юрій"
        };

    private readonly Random _random = new Random();

    public ClientGenerator(Bank bank, int threadId)
    {
        _bank = bank;
        _threadId = threadId;
    }

    public void GenerateClients(int totalClients, ref int clientCounter, object lockObj)
    {
        while (true)
        {
            int currentClientNumber;
            lock (lockObj)
            {
                if (clientCounter >= totalClients)
                    break;
                currentClientNumber = ++clientCounter;
            }

            string name = _names[_random.Next(_names.Length)];
            string purpose = _purposes[_random.Next(_purposes.Length)];
            Client client = new Client(name, purpose);

            _bank.AddClient(client);
            Thread.Sleep(_random.Next(500, 1500));
        }

        Console.WriteLine($"Потік-генератор {_threadId} закінчив роботу.");
    }
}
