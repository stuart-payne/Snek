using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snek.Core
{
    public class Snek : MonoBehaviour
    {
        [SerializeField] private int m_StartingLength;
        [SerializeField] private Board m_Board;
        [SerializeField] private GameObject m_SnakePiecePrefab;

        private Queue<SnakePiece> m_Body;
        private Vector2Int m_HeadPosition;
        private Direction m_LastDirectionMoved;
        private int m_CurrentLength;

        private void Awake()
        {
            m_Body = new Queue<SnakePiece>();
            m_CurrentLength = m_StartingLength;
        }
        
        private void Start()
        {
            SpawnStartingBody();
        }

        private void SpawnStartingBody()
        {
            Vector2Int position = m_Board.GetRandomPosition();
            var dir = Utils.GetRandomDirection();
            var dirVec = Utils.DirectionToVector2Int(dir);
            for (var i = 0; i < m_StartingLength; i++)
            {
                
                var piecePosition = m_Board.PositionToGridPosition((position + (dirVec * i)));
                var snakePiece = InstantiateSnakePieceAtGridPosition(piecePosition);
                AddSnakePiece(snakePiece,piecePosition);
            }

            m_LastDirectionMoved = dir;
            FinishedSpawningEvent?.Invoke();
        }

        private SnakePiece InstantiateSnakePieceAtGridPosition(Vector2Int position)
        {
            var snakePiece = Instantiate(
                m_SnakePiecePrefab, 
                m_Board.GridPositionToWorldPosition(position), 
                Quaternion.identity).GetComponent<SnakePiece>();
            snakePiece.GridPosition = position;
            return snakePiece;
        }

        public void MoveSnek(Direction dir)
        {
            var dirToMove = Utils.IsOppositeDirection(dir, m_LastDirectionMoved) ? m_LastDirectionMoved : dir;
            var newPosition = m_Board.PositionToGridPosition(m_HeadPosition + Utils.DirectionToVector2Int(dirToMove));
            m_LastDirectionMoved = dirToMove;
            var gridPiece = m_Board.GetGridPieceAtGridPosition(newPosition);
            var gridItem = gridPiece.Contains();

            switch (gridItem)
            {
                case GridItem.Apple:
                    AddSnakePiece(InstantiateSnakePieceAtGridPosition(newPosition), gridPiece);
                    AppleEatenEvent?.Invoke();
                    break;
                case GridItem.Rock:
                    DeathEvent?.Invoke();
                    break;
                case GridItem.Snake:
                    DeathEvent?.Invoke();
                    break;
                case GridItem.None:
                    var snakePiece = RemoveLastSnakePiece();
                    AddSnakePiece(snakePiece, newPosition);
                    ChangeSnakePieceWorldPosition(snakePiece);
                    break;
            }
        }

        private void AddSnakePiece(SnakePiece snakePiece, Vector2Int gridPosition)
        {
            m_Body.Enqueue(snakePiece);
            m_Board.Grid[gridPosition.x, gridPosition.y].AddGridItem(snakePiece);
            snakePiece.GridPosition = gridPosition;
            m_HeadPosition = gridPosition;
        }


        private void AddSnakePiece(SnakePiece snakePiece, GridPiece gridPiece)
        {
            m_Body.Enqueue(snakePiece);
            gridPiece.AddGridItem(snakePiece);
            m_HeadPosition = snakePiece.GridPosition;
        }

        private void ChangeSnakePieceWorldPosition(SnakePiece snakePiece)
        {
            snakePiece.transform.position = m_Board.GridPositionToWorldPosition(snakePiece.GridPosition);
        }

        private SnakePiece RemoveLastSnakePiece()
        {
            var snakePiece = m_Body.Dequeue();
            m_Board.Grid[snakePiece.GridPosition.x, snakePiece.GridPosition.y].RemoveItem();
            return snakePiece;
        }

        public event Action DeathEvent;
        public event Action AppleEatenEvent;
        public event Action FinishedSpawningEvent;
    }
}