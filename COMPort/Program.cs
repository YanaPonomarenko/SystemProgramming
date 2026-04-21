using System;
using System.IO.Ports;

namespace COMPort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string portName = "COM8";
            int baudRate = 9600;

            if (args.Length > 0) portName = args[0];
            if (args.Length > 1) int.TryParse(args[1], out baudRate);

            EchoServer server = new EchoServer(portName, baudRate);
            server.Start();

            Console.WriteLine($"Echo server started on {portName}");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            server.Stop();
        }
    }
}

