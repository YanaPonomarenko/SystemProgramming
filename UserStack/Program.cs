using UserStack.Services;

namespace UserStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Стек дій\n");

            var actionService = new ActionServices();

            const int threadsCount = 3;
            const int actionsPerThread = 5;

            Thread[] threads = new Thread[threadsCount];

            for (int i = 0; i < threadsCount; i++)
            {
                int threadId = i + 1;
                var generator = new ActionGenerator(actionService, threadId);
                threads[i] = new Thread(() => generator.GenerateActions(actionsPerThread));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"\nВсього дій у стеку: {actionService.Count}");


            for (int i = 0; i < 3; i++)
            {
                if (!actionService.UndoLastAction())
                    break;
                Thread.Sleep(500);
            }

            Console.WriteLine($"\nДій після скасування: {actionService.Count}");

            actionService.ProcessAllActions();

            Console.WriteLine($"\nСтек порожній? {actionService.IsEmpty}");


            actionService.AddAction("відкрив новий документ", "Admin");
            actionService.AddAction("додав коментар", "Admin");
            actionService.AddAction("зберегти як PDF", "Admin");

            Console.WriteLine($"\nДій у стеку: {actionService.Count}");

            actionService.UndoLastAction();

            Console.WriteLine($"\nДій після скасування: {actionService.Count}");

            actionService.Clear();

            Console.WriteLine("Програма завершила роботу.");
            Console.ReadKey();
        }
    }
}
