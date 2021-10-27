using System.Collections;
using System.Collections.Generic;
using Snek.Core;
using UnityEngine;

namespace Snek
{
    public class StaticDifficulties : IDifficultyProvider
    {
        public Difficulty[] GetDifficulties()
        {
            return new[]
            {
                new Difficulty
                {
                    Name = "Easy",
                    SecondsPerTick = 0.6f,
                    TicksPerStoneSpawn = 0,
                    GridXSize = 10,
                    GridYSize = 10,
                },
                new Difficulty
                {
                    Name = "Normal",
                    SecondsPerTick = 0.3f,
                    TicksPerStoneSpawn = 30,
                    GridXSize = 12,
                    GridYSize = 12,
                },
                new Difficulty
                {
                    Name = "Stupid",
                    SecondsPerTick = 0.15f,
                    TicksPerStoneSpawn = 10,
                    GridXSize = 16,
                    GridYSize = 16,
                }
            };
        }
    }
}
