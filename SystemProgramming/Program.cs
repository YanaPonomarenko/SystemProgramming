using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using System.Threading;
namespace SystemProgramming;


class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n1 - Показати процеси");
            Console.WriteLine("2 - Відкрити dou.ua");
            Console.WriteLine("3 - Зберегти процеси у файл");
            Console.WriteLine("4 - Вийти");
            Console.Write("Виберіть: ");

            string choice = Console.ReadLine();

            if (choice == "1")
                ShowAllProcesses();
            else if (choice == "2")
                OpenDouUa();
            else if (choice == "3")
                SaveProcessToFile();
            else if (choice == "4")
                break;
        }
    }

    static void ShowAllProcesses()
    {
        Process[] processes = Process.GetProcesses();
        foreach (var process in processes)
        {
            try
            {
                Console.WriteLine($"{process.ProcessName} PID: {process.Id}");
            }
            catch (Exception)
            {
                Console.WriteLine("Unknown process");
            }
        }
        Console.WriteLine($"\nВсього: {processes.Length} процесів");
    }

    static void OpenDouUa()
    {
        Process.Start(new ProcessStartInfo("https://dou.ua") { UseShellExecute = true });
        Console.WriteLine("Відкрито dou.ua");
    }

    static void SaveProcessToFile()
    {
        string fileName = $"processes_{DateTime.Now.Ticks}.txt";

        Process[] processes = Process.GetProcesses();
        string result = "";

        foreach (var process in processes)
        {
            try
            {
                result += $"{process.ProcessName} PID: {process.Id}\n";
            }
            catch (Exception)
            {
                result += "Unknown process\n";
            }
        }

        File.WriteAllText(fileName, result);
        Console.WriteLine($"Збережено у файл: {fileName}");
    }
}
 
