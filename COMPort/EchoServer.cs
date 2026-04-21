using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPort
{
    public class EchoServer
    {
            private SerialPort port;

            public EchoServer(string portName, int baudRate)
            {
                port = new SerialPort(portName, baudRate);
                port.DataReceived += OnDataReceived;
            }

            private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                try
                {
                    Thread.Sleep(50);

                    string data = port.ReadExisting();

                    if (!string.IsNullOrEmpty(data))
                    {
                        Console.WriteLine($"[RECEIVED] {data.Trim()}");
                        port.Write(data);
                        Console.WriteLine($"[SENT] {data.Trim()}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            public void Start()
            {
                port.Open();
                Console.WriteLine($"Echo server started on {port.PortName}");
                Console.WriteLine("Press Enter to exit...");
            }

            public void Stop()
            {
                if (port.IsOpen)
                    port.Close();
            }
        }
    }


