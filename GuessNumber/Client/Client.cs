using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber.Client;

class Client
{
    public static void Run()
    {
        try
        {
            using TcpClient client = new TcpClient("127.0.0.1", 5000);
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream);
            using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

            Console.WriteLine(reader.ReadLine());

            while (true)
            {
                Console.Write("Твоя спроба: ");
                string guess = Console.ReadLine()!;
                writer.WriteLine(guess);

                string response = reader.ReadLine()!;
                Console.WriteLine(response);

                if (response == "Вірно!")
                {
                    Console.WriteLine(reader.ReadLine());
                    Console.WriteLine(reader.ReadLine());
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}
