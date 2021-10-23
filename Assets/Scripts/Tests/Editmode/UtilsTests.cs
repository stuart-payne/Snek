using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Snek.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Snek
{
    public class UtilsTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void IsOppositeDirection()
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
    }
}
