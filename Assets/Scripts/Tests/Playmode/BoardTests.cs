using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Snek.Core;
using UnityEditor;
using UnityEditor.SceneTemplate;

namespace Snek.Tests
{
    public class BoardTests
    {
        private Board m_CachedBoard;
        [UnityTest]
        public IEnumerator PositionToGridPositionWrapsValuesOverBounds()
        {
            var testValues = new[]
            {
                (new Vector2Int(-1, 7), new Vector2Int(3, 3)),
                (new Vector2Int(4, 4), new Vector2Int(0, 0)),
                (new Vector2Int(-10, 10), new Vector2Int(2, 2)),
                (new Vector2Int(-7, 7), new Vector2Int(1, 3))
            };
            var board = GetTestBoard();
            yield return null;
            foreach (var testValue in testValues)
            {
                Assert.AreEqual(testValue.Item2, board.PositionToGridPosition(testValue.Item1));
            }
        }

        [UnityTest]
        public IEnumerator DoNotWrapValidValues()
        {
            var board = GetTestBoard();
            yield return null;
            var testVector = new Vector2Int(2, 2);
            var expectedVector = new Vector2Int(2, 2);
            Assert.AreEqual(board.PositionToGridPosition(testVector), expectedVector);
        }

        [UnityTest]
        public IEnumerator RandomPositionReturnsValidValues()
        {
            var board = GetTestBoard();
            yield return null;
            var iterationNumber = 100;
            for (var i = 0; i < iterationNumber; i++)
            {
                Vector2Int randomPosition = board.GetRandomPosition();
                Assert.AreEqual(randomPosition.x < board.Width  && randomPosition.x >= 0, true);
                Assert.AreEqual(randomPosition.y < board.Height && randomPosition.y >= 0, true);
            }
        }

        [UnityTest]
        public IEnumerator AsManySquaresAsWidthAndHeight()
        {
            var board = GetTestBoard();
            yield return null;
            Assert.NotNull(board.Grid);
            Assert.AreEqual(board.Grid.GetLength(0) == board.Width, true);
            Assert.AreEqual(board.Grid.GetLength(1) == board.Height, true);
        }

        private Board GetTestBoard()
        {
            if (m_CachedBoard)
                return m_CachedBoard;
            var boardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TestGrid.prefab");
            m_CachedBoard = Object.Instantiate(boardPrefab).GetComponent<Board>();
            return m_CachedBoard;
        }
        
    }
}
