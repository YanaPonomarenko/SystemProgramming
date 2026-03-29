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
 


//class Currency
//{
//    public int r030 { get; set; }
//    public string? txt { get; set; }
//    public decimal rate { get; set; }

//    public override string ToString()
//    {
//        return $"Currency: {txt} Rate: {rate}";
//    }
//}

//private static readonly string _url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

    //static async Task<List<Currency>?> GetCurrency()
    //{
    //    using (HttpClient client = new HttpClient())
    //    {
    //        try
    //        {
    //            var response = await client.GetStringAsync(_url);
    //            var obj = JsonSerializer.Deserialize<List<Currency>>(response);
    //            if (obj != null)
    //            {
    //                return obj;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //        }
    //        return null;
    //    }
    //}

    //static async Task Main(string[] args)
    //{

    //    var data = await GetCurrency();

    //    if (data != null)
    //    {
    //        Console.WriteLine("Курси валют:");
    //        foreach (var item in data)
    //        {
    //            Console.WriteLine(item);
    //        }

    //        decimal usdRate = 0;
    //        foreach (var item in data)
    //        {
    //            if (item.txt == "Долар США")
    //            {
    //                usdRate = item.rate;
    //                break;
    //            }
    //        }

    //        if (usdRate > 0)
    //        {
    //            Console.WriteLine($"\nКонвертація гривні в долари");
    //            Console.WriteLine($"Поточний курс: 1 USD = {usdRate} грн\n");

    //            Console.Write("Введіть кількість гривень: ");
    //            string? input = Console.ReadLine();

    //            if (decimal.TryParse(input, out decimal uahAmount))
    //            {
    //                decimal usdAmount = uahAmount / usdRate;
    //                Console.WriteLine($"\n{uahAmount} грн = {usdAmount:F2} USD");
    //            }
    //            else
    //            {
    //                Console.WriteLine("Помилка: введіть коректне число");
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine("Курс долара не знайдено");
    //        }
    //    }
    //    Console.ReadLine();
    //}


//public class Document
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public override string ToString()
//    {
//        return $"Документ #{Id}: {Name}";
//    }
//}

//class Program
//{
//    static async Task<Document[]> LoadDocument()
//    {
//        Console.WriteLine("Завантаження.. Починаємо завантаження документів");
//        await Task.Delay(500);

//        var documents = new Document[]
//        {
//            new Document { Id = 1, Name = "Документ1" },
//            new Document { Id = 2, Name = "Документ2" },
//            new Document { Id = 3, Name = "Документ3" },
//            new Document { Id = 4, Name = "Документ4" },
//            new Document { Id = 5, Name = "Документ5" }
//        };

//        for (int i = 0; i < documents.Length; i++)
//        {
//            Console.WriteLine($"Завантаження... Завантажуємо {documents[i].Name}");
//            await Task.Delay(800);
//        }

//        Console.WriteLine("Завантаження... Всі документи завантажено");
//        return documents;
//    }

//    static void Main(string[] args)
//    {
//        Console.WriteLine("Робимо запит на завантаження документів");
//        var documentsTask = LoadDocument();

//        for (int i = 0; i < 10; i++)
//        {
//            Console.WriteLine("Головний потік працює....");
//            Thread.Sleep(500);
//        }
//        documentsTask.Wait();

//        Console.WriteLine("\nОтримали документи:");
//        foreach (var doc in documentsTask.Result)
//        {
//            Console.WriteLine(doc);
//        }
//    }
//}
//    class Order
//    {
//        public int Id { get; set; }
//        public string ProductName { get; set; }
//    }

//    private static ConcurrentQueue<Order> _orderQueue = new ConcurrentQueue<Order>();

//    private static bool _isWorking = true;

//    private static object _consoleLocker = new object();

//    static void Main(string[] args)
//    {
//        Console.WriteLine("Додайте замовлення (введіть назву товару, або 'exit' для завершення):");

//        int orderId = 1;
//        string productName;

//        while (true)
//        {
//            Console.Write("Введіть назву товару: ");
//            productName = Console.ReadLine();

//            if (productName.ToLower() == "exit")
//                break;

//            Order order = new Order
//            {
//                Id = orderId++,
//                ProductName = productName
//            };

//            _orderQueue.Enqueue(order);
//            Console.WriteLine($"Замовлення #{order.Id} для товару {order.ProductName} додано до черги");
//        }

//        const int WORKERS_COUNT = 3;
//        Thread[] workers = new Thread[WORKERS_COUNT];

//        for (int i = 0; i < WORKERS_COUNT; i++)
//        {
//            workers[i] = new Thread(ProcessOrders);
//            workers[i].Start();
//        }

//        while (_orderQueue.Count > 0)
//        {
//            Thread.Sleep(100);
//        }

//        _isWorking = false;

//        foreach (Thread worker in workers)
//        {
//            worker.Join();
//        }

//        Console.WriteLine("\nВсі замовлення оброблено");
//    }

//    static void ProcessOrders()
//    {
//        Random random = new Random();

//        while (_isWorking)
//        {
//            if (_orderQueue.TryDequeue(out Order order))
//            {
//                lock (_consoleLocker)
//                {
//                    Console.WriteLine($"Працівник {Thread.CurrentThread.ManagedThreadId} обробляє замовлення #{order.Id}: {order.ProductName}");
//                }
//                Thread.Sleep(random.Next(500, 1500));

//                lock (_consoleLocker)
//                {
//                    Console.WriteLine($"Працівник {Thread.CurrentThread.ManagedThreadId} завершив замовлення #{order.Id}: {order.ProductName}");
//                }
//            }
//            else
//            {
//                Thread.Sleep(100);
//            }
//        }
//    }
//}
