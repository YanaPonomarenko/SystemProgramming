using DungeonSimulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonSimulator.Services;

public class QuestExecutor
{
    private static readonly Random _random = new();

    public async Task<bool> RunQuestAsync(Hero hero, Quest quest, CancellationToken ct)
    {
        Console.WriteLine($"{hero.Name} вирушив у \"{quest.Title}\"");

        try
        {
            await Task.Delay(quest.Duration, ct);

            bool success = CalculateSuccess(hero, quest);

            if (success)
            {
                Console.WriteLine($"{hero.Name} завершив \"{quest.Title}\" — Перемога (+{quest.Bonus} золота)");
            }
            else
            {
                Console.WriteLine($"{hero.Name} завершив \"{quest.Title}\" — Поразка (0 золота)");
            }

            return success;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"{hero.Name} відступив з \"{quest.Title}\" (тайм-аут)");
            return false;
        }
    }

    private bool CalculateSuccess(Hero hero, Quest quest)
    {
        int chance = (hero.Power * 100) / quest.DifficultyLevel;
        return chance >= 50;
    }
}
