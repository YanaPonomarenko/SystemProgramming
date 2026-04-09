using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using System.Threading;
namespace SystemProgramming;




//class Program
//{
//    private static Mutex logMutex;

//    static void Main(string[] args)
//    {
//        logMutex = new Mutex(false, "Global\\MyLogFileMutex");

//        int processId = Environment.ProcessId;
//        string logFilePath = "log.txt";

//        Console.WriteLine($"Процес {processId} запущено. Натисніть будь яку клавішу для запису...");
//        Console.ReadKey();

//        for (int i = 0; i < 5; i++)
//        {
//            WriteToLogFile(logFilePath, processId, i + 1);
//            Thread.Sleep(100);
//        }

//        Console.WriteLine($"Процес {processId} завершив запис. Натисніть будь яку клавішу для виходу...");
//        Console.ReadKey();
//    }

//    static void WriteToLogFile(string filePath, int processId, int lineNumber)
//    {
//        logMutex.WaitOne();

//        try
//        {
//            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | Процес {processId} | Рядок {lineNumber}";

//            File.AppendAllText(filePath, logEntry + Environment.NewLine);
//            Thread.Sleep(50);

//            Console.WriteLine($" Процес {processId} записав рядок {lineNumber}");
//        }
//        finally
//        {
//            logMutex.ReleaseMutex();
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string[] urls = new string[]
//        {
//            "https://picsum.photos/id/1/800/600",
//            "https://picsum.photos/id/2/800/600",
//            "https://picsum.photos/id/3/800/600",
//            "https://picsum.photos/id/4/800/600",
//            "https://picsum.photos/id/5/800/600",
//            "https://picsum.photos/id/6/800/600",
//            "https://picsum.photos/id/7/800/600",
//            "https://picsum.photos/id/8/800/600",
//            "https://picsum.photos/id/9/800/600",
//            "https://picsum.photos/id/10/800/600"
//        };

//        using HttpClient client = new HttpClient();
//        Stopwatch stopwatch = Stopwatch.StartNew();

//        Parallel.ForEach(urls, (url) =>
//        {
//            try
//            {
//                byte[] data = client.GetByteArrayAsync(url).Result;
//                string fileName = $"image_{DateTime.Now.Ticks}.jpg";
//                File.WriteAllBytes(fileName, data);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Помилка: {ex.Message}");
//            }
//        });

//        stopwatch.Stop();
//        Console.WriteLine($"Загальний час: {stopwatch.Elapsed.TotalSeconds:F2} секунд");
//    }
//}











//class Program
//{
//    static async Task Main()
//    {
//        string[] urls = new string[]
//        {
//             "https://picsum.photos/id/1/800/600",
//             "https://picsum.photos/id/2/800/600",
//            "https://picsum.photos/id/3/800/600",
//             "https://picsum.photos/id/4/800/600",
//            "https://picsum.photos/id/5/800/600",
//            "https://picsum.photos/id/6/800/600",
//            "https://picsum.photos/id/7/800/600",
//            "https://picsum.photos/id/8/800/600",
//            "https://picsum.photos/id/9/800/600",
//            "https://picsum.photos/id/10/800/600"
//        };

//        using HttpClient client = new HttpClient();

//        Stopwatch stopwatch = Stopwatch.StartNew();

//        Task[] tasks = new Task[urls.Length];

//        for (int i = 0; i < urls.Length; i++)
//        {
//            int index = i;

//            tasks[index] = Task.Run(async () =>
//            {
//                try
//                {
//                    Console.WriteLine($"Почато завантаження {index + 1}/{urls.Length}...");
//                    byte[] data = await client.GetByteArrayAsync(urls[index]);
//                    string fileName = $"image_{index + 1}.jpg";
//                    await File.WriteAllBytesAsync(fileName, data);
//                    Console.WriteLine($"Збережено: {fileName}");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Помилка завантаження {urls[index]}: {ex.Message}");
//                }
//            });
//        }
//        await Task.WhenAll(tasks);

//        stopwatch.Stop();
//        Console.WriteLine($"Загальний час: {stopwatch.Elapsed.TotalSeconds:F2} секунд");
//    }
//}


//    while (true)
//    {
//        Console.WriteLine("\n1 - Показати процеси");
//        Console.WriteLine("2 - Відкрити dou.ua");
//        Console.WriteLine("3 - Зберегти процеси у файл");
//        Console.WriteLine("4 - Вийти");
//        Console.Write("Виберіть: ");

//        string choice = Console.ReadLine();

//        if (choice == "1")
//            ShowAllProcesses();
//        else if (choice == "2")
//            OpenDouUa();
//        else if (choice == "3")
//            SaveProcessToFile();
//        else if (choice == "4")
//            break;
//    }
//}

//static void ShowAllProcesses()
//{
//    Process[] processes = Process.GetProcesses();
//    foreach (var process in processes)
//    {
//        try
//        {
//            Console.WriteLine($"{process.ProcessName} PID: {process.Id}");
//        }
//        catch (Exception)
//        {
//            Console.WriteLine("Unknown process");
//        }
//    }
//    Console.WriteLine($"\nВсього: {processes.Length} процесів");
//}

//static void OpenDouUa()
//{
//    Process.Start(new ProcessStartInfo("https://dou.ua") { UseShellExecute = true });
//    Console.WriteLine("Відкрито dou.ua");
//}

//static void SaveProcessToFile()
//{
//    string fileName = $"processes_{DateTime.Now.Ticks}.txt";

//    Process[] processes = Process.GetProcesses();
//    string result = "";

//    foreach (var process in processes)
//    {
//        try
//        {
//            result += $"{process.ProcessName} PID: {process.Id}\n";
//        }
//        catch (Exception)
//        {
//            result += "Unknown process\n";
//        }
//    }

//    File.WriteAllText(fileName, result);
//    Console.WriteLine($"Збережено у файл: {fileName}");



