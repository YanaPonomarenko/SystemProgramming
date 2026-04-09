using DungeonSimulator.Models;
using DungeonSimulator.Services;

namespace DungeonSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Hero bob = new Hero("Bob", 100, 80);
            Hero alice = new Hero("Alice", 120, 95);

            Quest quest1 = new Quest("Печера дракона", 90, 200, TimeSpan.FromSeconds(2.5));
            Quest quest2 = new Quest("Вежа мага", 70, 150, TimeSpan.FromSeconds(1.8));

            var executor = new QuestExecutor();

            using var cts1 = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            using var cts2 = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            Task<bool>[] tasks = new[]
            {
            executor.RunQuestAsync(bob, quest1, cts1.Token),
            executor.RunQuestAsync(alice, quest2, cts2.Token)
        };

            var remainingTasks = tasks.ToList();
            while (remainingTasks.Any())
            {
                var completedTask = await Task.WhenAny(remainingTasks);
                remainingTasks.Remove(completedTask);
                await completedTask;
            }

            Console.WriteLine("\nВсі квести завершено");
            Console.ReadKey();
        }
    }
}
