using System;

namespace Snek.Core
{
    [Serializable]
    public class Difficulty
    {
        public string Name;
        public float SecondsPerTick;
        public int TicksPerStoneSpawn;
        public int GridXSize;
        public int GridYSize;
    }

    [Serializable]
    public class DifficultyArray
    {
        public Difficulty[] Difficulties;
    }
}
