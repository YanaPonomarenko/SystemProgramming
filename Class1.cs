using System;
using System.Threading;

public class Class1
{
    private static readonly Semaphore semaphore = new Semaphore(1, 1);

    private static void Work(object? id)
    {
        Console.WriteLine($"Thread {id} чекає...");
        //Додаємо змінну для відстеження стану бо не знаемо чи отримали семафор
        bool entered = false;

        try
        {
            //Зберігаємо результат WaitOne
            entered = semaphore.WaitOne(10000);

            if (!entered)
            {
                Console.WriteLine($"Thread {id} не дочекався семафора за 10 секунд");
                return;
            }

            // Критчна секція виконується тільки  якщо entered == true
            Console.WriteLine($"Thread {id} зайшов");
            Thread.Sleep(2000);
        }
        finally
        {
            //Викликаємо Release тільки якщо отримали семафор,в оригінальному коді semaphore.Release() викликався завжди і це була причина exception
            if (entered)
            {
                Console.WriteLine($"Thread {id} виходить");
                semaphore.Release();
            }
        }
    }

    public static void Run()
    {
        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(Work);
            threads[i].Start(i + 1);
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }
    }
}
