using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonSimulator.Models;

public record Quest(string Title, int DifficultyLevel, int Bonus, TimeSpan Duration);
