using System;
using NUnit.Framework;
using Snek.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Snek.Tests
{
    public class UtilsTests
    {
        [Test]
        public void IsOppositeDirectionTests()
        {
            var testCases = new[]
            {
                (Direction.Up, Direction.Down, true),
                (Direction.Left, Direction.Right, true),
                (Direction.Right, Direction.Left, true),
                (Direction.Down, Direction.Up, true),
                (Direction.Down, Direction.Right, false),
                (Direction.Up, Direction.Right, false)
            };

            foreach (var (dir1, dir2, expected) in testCases)
            {
                Assert.AreEqual(Utils.IsOppositeDirection(dir1, dir2), expected);
            }
        }

        [Test]
        public void DirectionToVector2IntTests()
        {
            var testCases = new[]
            {
                (Direction.Up, new Vector2Int(0, 1)),
                (Direction.Down, new Vector2Int(0, -1)),
                (Direction.Left, new Vector2Int(-1, 0)),
                (Direction.Right, new Vector2Int(1, 0))
            };

            foreach (var (direction, expectedVector) in testCases)
            {
                Assert.AreEqual(Utils.DirectionToVector2Int(direction), expectedVector);
            }
        }

        
        [Test]
        public void GetRandomDirectionTests()
        {
            const int testIterations = 100;
            for (var i = 0; i < testIterations; i++)
            {
                Assert.IsTrue(Enum.IsDefined(typeof(Direction), Utils.GetRandomDirection()));
            }
        }
    }
}
