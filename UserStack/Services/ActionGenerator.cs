using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStack.Services;

public class ActionGenerator
{
    private readonly ActionServices _actionService;
    private readonly int _threadId;
    private readonly string[] _possibleActions = 
        {
            "відкрив документ","зберегти документ","закрив документ","скопіював текст",
            "вставив текст","вирізав текст","змінив шрифт","додав зображення","видалив зображення"
        };

    private readonly Random _random = new Random();

    public ActionGenerator(ActionServices actionService, int threadId)
    {
        _actionService = actionService;
        _threadId = threadId;
    }

    public void GenerateActions(int actionsCount)
    {
        for (int i = 0; i < actionsCount; i++)
        {
            string action = _possibleActions[_random.Next(_possibleActions.Length)];
            _actionService.AddAction(action, $"Потік-{_threadId}");
            Thread.Sleep(_random.Next(200, 800));
        }

        Console.WriteLine($"Потік-{_threadId} завершив генерацію {actionsCount} дій.");
    }
}

