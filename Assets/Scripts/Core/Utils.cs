using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snek.Core
{
    public static class Utils
    {
        public static Direction GetRandomDirection() => (Direction) Random.Range(0, 3);

        public static Vector2Int DirectionToVector2Int(Direction dir)
        {
            return dir switch
            {
                Direction.Up => Vector2Int.up,
                Direction.Right => Vector2Int.right,
                Direction.Down => Vector2Int.down,
                Direction.Left => Vector2Int.left,
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }

        public static bool IsOppositeDirection(Direction dir1, Direction dir2)
        {
            return Math.Abs(dir1 - dir2) == 2;
        }
    }
}
