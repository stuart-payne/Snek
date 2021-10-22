using UnityEngine;
using System;
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
            Debug.Log(position);
            return Grid[position.x, position.y].transform.position;
        }
        


        public void AddGridItemAtGridPosition(IGridItem gridItem, Vector2Int position)
        {
            Grid[position.x, position.y].AddGridItem(gridItem);
        }

        public GridPiece GetGridPieceAtGridPosition(Vector2Int position) => Grid[position.x, position.y];
    }
}