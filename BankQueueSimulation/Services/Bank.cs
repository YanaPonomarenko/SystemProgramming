using BankQueueSimulation.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueueSimulation.Services;

public class Bank
{
    private readonly ConcurrentQueue<Client> _queue = new ConcurrentQueue<Client>();
    private readonly Random _random = new Random();
    private int _servedClientsCount = 0;
    private readonly int _totalClientsToGenerate;

    public Bank(int totalClientsToGenerate)
    {
        _totalClientsToGenerate = totalClientsToGenerate;
    }
    public void AddClient(Client client)
    {
        _queue.Enqueue(client);
        Console.WriteLine($"[Додано] {client} у чергу. Розмір черги: {_queue.Count}");
    }

    public void ServeClients()
    {
        Console.WriteLine("Оператор почав роботу. Очікуємо клієнтів...\n");

        while (_servedClientsCount < _totalClientsToGenerate)
        {
            if (_queue.TryDequeue(out Client client))
            {
                Interlocked.Increment(ref _servedClientsCount);
                Console.WriteLine($"[Обслуговою] {client}...");

                int serviceTime = _random.Next(1000, 3000);
                Thread.Sleep(serviceTime);

                Console.WriteLine($"[Готово] {client} обслужений за {serviceTime} мс. Обслуговано клієнтів: {_servedClientsCount}");
            }
            else
            {
                if (_servedClientsCount < _totalClientsToGenerate)
                {
                    Thread.Sleep(100);
                }
            }
        }

        Console.WriteLine($"\nОператор закінчив роботу. Всього обслуговано: {_servedClientsCount} клієнтів.");
    }

    public int ServedClientsCount => _servedClientsCount;
}
