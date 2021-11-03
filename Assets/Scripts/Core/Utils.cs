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

        public static Direction Vector2IntToDirection(Vector2Int dir)
        {
            if (dir.magnitude > 1)
                return WrapDirectionHack(dir);
            if (dir == Vector2Int.up)
                return Direction.Up;
            
            if(dir == Vector2Int.right)
                return Direction.Right;
            
            if (dir == Vector2Int.down)
                return Direction.Down;
            
            if (dir == Vector2Int.left)
                return Direction.Left;
            throw new ArgumentException("Vector is not a normalized Vector or is diagonal");
        }

        public static Direction WrapDirectionHack(Vector2Int dir)
        {
            Debug.Log("WRAPHACK CALLED");
            if (dir.x == 0)
            {
                return dir.y > 0 ? Direction.Down : Direction.Up;
            }
            return dir.x > 0 ? Direction.Left : Direction.Right;
        }

        public static bool IsOppositeDirection(Direction dir1, Direction dir2)
        {
            return Math.Abs(dir1 - dir2) == 2;
        }

        public static float ToroidalDistance(Vector2Int pos1, Vector2Int pos2)
        {
            float dx = Mathf.Abs(pos2.x - pos1.x);
            float dy = Mathf.Abs(pos2.y - pos1.y);

            if (dx > 0.5f)
                dx = 1.0f - dx;
            if (dy > 0.5f)
                dy = 1.0f - dy;

            return Mathf.Sqrt(dx * dx + dy * dy);
        }
    }
}
