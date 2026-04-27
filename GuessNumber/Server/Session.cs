using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber.Server;

public class Session
{
    private readonly TcpClient _client;

    public Session(TcpClient client)
    {
        _client = client;
    }

    public void Start()
    {
        using var stream = _client.GetStream();
        using var reader = new StreamReader(stream);
        using var writer = new StreamWriter(stream) { AutoFlush = true };

        Random rnd = new Random();
        int secret = rnd.Next(1, 51);

        int attempts = 0;
        Stopwatch timer = Stopwatch.StartNew();

        writer.WriteLine("Гра почалась! Вгадай число від 1 до 50.");

        while (true)
        {
            string? input = reader.ReadLine();
            if (input == null) break;

            if (!int.TryParse(input, out int guess))
            {
                writer.WriteLine("Будь ласка, введи ціле число.");
                continue;
            }

            attempts++;

            if (guess < secret)
                writer.WriteLine("Більше");
            else if (guess > secret)
                writer.WriteLine("Менше");
            else
            {
                timer.Stop();
                writer.WriteLine("Вірно!");
                writer.WriteLine($"Тривалість гри: {timer.Elapsed.TotalSeconds:F1} секунд.");
                writer.WriteLine($"Невдалих спроб: {attempts - 1}");
                break;
            }
        }
    }
}
