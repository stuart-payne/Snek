using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Random = UnityEngine.Random;


namespace Snek.Core
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private GameObject m_Square;
        [SerializeField] private int m_Width;
        [SerializeField] private int m_Height;
        
        public int Height => m_Height;

        public int Width => m_Width;

        public GridPiece[,] Grid;

        private void Awake()
        {
            Grid = new GridPiece[m_Width, m_Height];
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            for (var x = 0; x < m_Width; x++)
            {
                for (var y = 0; y < m_Height; y++)
                {
                    Grid[x, y] = Instantiate(
                        m_Square,
                        new Vector3(x - m_Width / 2, y - m_Height / 2, 0),
                        Quaternion.identity,
                        transform).GetComponent<GridPiece>();
                }
            }
        }

        private int WrapValue(int value, int bound)
        {
            if (value >= 0 && value < bound)
                return value;
            if (value < 0)
            {
                value *= -1;
                value %= bound;
                return bound - value;
            }

            return value % bound;
        }

        public GridPiece GetGridPiece(Vector2Int position) => Grid[position.x, position.y];

        public void AddGridItem(Vector2Int position, IGridItem gridItem) =>
            Grid[position.x, position.y].AddGridItem(gridItem);

        public void RemoveGridItem(Vector2Int position) => Grid[position.x, position.y].RemoveItem();

        public List<GridPiece> GetEmptyGridPieces()
        {
            var emptyGridPieces = new List<GridPiece>();
            foreach (var gridPiece in Grid)
            {
                if(gridPiece.Contains() == GridItem.None)
                    emptyGridPieces.Add(gridPiece);
            }

            return emptyGridPieces;
        }


        public List<GridPiece> GetEmptyGridPiecesWithExclusionZone(Vector2Int exclusionPoint, float radius)
        {
            // Used by RockSpawner, we do not want to spawn rocks on edge so we skip 0 and end both for x and y
            var validGridPieces = new List<GridPiece>();
            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                if(x == 0 || x == Grid.GetLength(0) - 1)
                    continue;
                for (var y = 0; y < Grid.GetLength(1); y++)
                {
                    if(y == 0 || y == Grid.GetLength(1) - 1)
                        continue;
                    var position = new Vector2Int(x, y);
                    if(Vector2Int.Distance(position, exclusionPoint) < radius)
                        continue;
                    var gridPiece = Grid[position.x, position.y];
                    if(gridPiece.Contains() == GridItem.None)
                        validGridPieces.Add(gridPiece);
                }
            }
            return validGridPieces;
        }

        public Vector2Int GetRandomPosition()
        {
            return new Vector2Int(
                Random.Range(0, m_Width - 1),
                Random.Range(0, m_Height - 1));
        }

        public Vector2Int PositionToGridPosition(Vector2Int position)
        {
            return new Vector2Int(
                WrapValue(position.x, m_Width),
                WrapValue(position.y, m_Height)
            );
        }

        public Vector3 GridPositionToWorldPosition(Vector2Int position)
        {
            return Grid[position.x, position.y].transform.position;
        }
        


        public void AddGridItemAtGridPosition(IGridItem gridItem, Vector2Int position)
        {
            Grid[position.x, position.y].AddGridItem(gridItem);
        }

        public GridPiece GetGridPieceAtGridPosition(Vector2Int position) => Grid[position.x, position.y];
    }
}