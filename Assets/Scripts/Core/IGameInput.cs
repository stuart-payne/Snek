using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public interface IGameInput
    {
        public Direction DirectionToMove { get; }
    }
}
