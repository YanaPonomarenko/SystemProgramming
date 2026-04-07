using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore
{
    class Program
    {
        private static StoreApiService _storeApiService;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            _storeApiService = new StoreApiService();

            while (true)
            {  
                Console.WriteLine("1. Переглянути всі товари");
                Console.WriteLine("2. Переглянути товар за ID");
                Console.WriteLine("3. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                if (choice == "3")
                {
                    Console.WriteLine("До побачення!");
                    break;
                }

                if (choice == "1")
                {
                    await ShowAllProducts();
                }
                else if (choice == "2")
                {
                    await ShowProductById();
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Натисніть будь-яку клавішу...");
                    Console.ReadKey();
                }
            }
        }
        static async Task ShowAllProducts()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var animationTask = ShowAnimation(cancellationTokenSource.Token);

            try
            {
                Product[] products = await _storeApiService.GetAllProductsAsync();
                cancellationTokenSource.Cancel();
                await animationTask;

                Console.Clear();
                Console.WriteLine($"Отримано {products.Length} товарів");
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                await animationTask;
                Console.Clear();
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        static async Task ShowProductById()
        {
            Console.Write("Введіть ID товару (1-20): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id < 1 || id > 20)
            {
                Console.WriteLine("Невірний ID");
                Console.ReadKey();
                return;
            }

            var cancellationTokenSource = new CancellationTokenSource();
            var animationTask = ShowAnimation(cancellationTokenSource.Token);

            try
            {
                Product product = await _storeApiService.GetProductByIdAsync(id);
                cancellationTokenSource.Cancel();
                await animationTask;

                Console.Clear();
                Console.WriteLine($"Товар з ID {id} отримано");
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                await animationTask;
                Console.Clear();
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        static async Task ShowAnimation(CancellationToken cancellationToken)
        {
            Task task = new Task(Animation, cancellationToken);
            task.Start();
            await task;
        }

        static void Animation(object state)
        {
            CancellationToken cancellationToken = (CancellationToken)state;
            int dots = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write("\rЗавантаження " + new string('*', dots % 4 + 1));
                dots++;
                Thread.Sleep(500);
            }
            Console.Write("\r" + new string(' ', 20) + "\r");
        }
    }
}
