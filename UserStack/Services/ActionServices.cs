using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStack.Models;

namespace UserStack.Services;

public class ActionServices
{
    private readonly ConcurrentStack<User> _actionStack = new ConcurrentStack<User>();
    private int _actionCounter = 0;

    public void AddAction(string actionName, string userName = "Користувач")
    {
        var action = new User(Interlocked.Increment(ref _actionCounter), actionName, userName);
        _actionStack.Push(action);
        Console.WriteLine($"[Додано] {action}");
        Console.WriteLine($"Розмір стеку: {_actionStack.Count}");
    }

    public bool UndoLastAction()
    {
        if (_actionStack.TryPop(out User action))
        {
            Console.WriteLine($"[Скасовано] {action}");
            Console.WriteLine($"Розмір стеку: {_actionStack.Count}");
            return true;
        }
        else
        {
            Console.WriteLine("[Помилка] Немає дій для скасування!");
            return false;
        }
    }

    public User PeekLastAction()
    {
        _actionStack.TryPeek(out User action);
        return action;
    }

    public void ProcessAllActions()
    {
        Console.WriteLine("\nОбробка\n");
        int processedCount = 0;

        while (!_actionStack.IsEmpty)
        {
            if (_actionStack.TryPop(out User task))
            {
                processedCount++;
                Console.WriteLine($"Обробляється: {task}");
                Thread.Sleep(300);
            }
        }

        Console.WriteLine($"\nВсього оброблено дій: {processedCount}");
    }

    public int Count => _actionStack.Count;
    public bool IsEmpty => _actionStack.IsEmpty;

    public void Clear()
    {
        int count = _actionStack.Count;
        _actionStack.Clear();
        Console.WriteLine($"[Очищено] Видалено {count} дій");
    }
}
