using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public interface IDifficultyProvider
    {
        public Difficulty[] GetDifficulties();
    }
}
