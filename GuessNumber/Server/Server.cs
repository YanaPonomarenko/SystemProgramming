using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber.Server;

class Server
{
    public static void Run()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();

        Console.WriteLine("Сервер запущено...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Клієнт підключився.");

            Session session = new Session(client);

            Thread thread = new Thread(session.Start);
            thread.Start();
        }
    }
}
