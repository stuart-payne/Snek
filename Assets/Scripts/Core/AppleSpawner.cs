using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Snek.Core
{
    public class AppleSpawner : MonoBehaviour
    {
        [SerializeField] private Board m_Board;
        [SerializeField] private GameObject m_ApplePrefab;
        private Apple m_Apple;

        private readonly Vector2Int[] m_ReachableChecks = {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right,
        };
        
        public Vector2Int CurrentApplePosition { get; set; }

        public void SpawnNewApple()
        {
            var validPieces = m_Board.GetEmptyGridPieces();
            var gridPiece = validPieces[Random.Range(0, validPieces.Count)];
            while (!CheckIfPossibleForPlayer(gridPiece))
            {
                validPieces.Remove(gridPiece);
                gridPiece = validPieces[Random.Range(0, validPieces.Count)];
            }
            if (!m_Apple)
                m_Apple = Instantiate(m_ApplePrefab, gridPiece.transform.position, Quaternion.identity).GetComponent<Apple>();
            else
                m_Apple.transform.position = gridPiece.transform.position;
            gridPiece.AddGridItem(m_Apple);
            CurrentApplePosition = gridPiece.GridPosition;
        }

        private bool CheckIfPossibleForPlayer(GridPiece gridPiece)
        {
            var piecesToCheck = m_ReachableChecks
                .Select(dir => m_Board.GetGridPiece(m_Board.PositionToGridPosition(gridPiece.GridPosition + dir)))
                .ToList();
            var rockCount = piecesToCheck.Count(piece => piece.Contains() == GridItem.Rock);
            return rockCount < 3;
        }
    }
}
