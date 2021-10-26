using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class AppleSpawner : MonoBehaviour
    {
        [SerializeField] private Board m_Board;
        [SerializeField] private GameObject m_ApplePrefab;
        private Apple m_Apple;

        public void SpawnNewApple()
        {
            var validPieces = m_Board.GetEmptyGridPieces();
            var gridPiece = validPieces[Random.Range(0, validPieces.Count)];
            if (!m_Apple)
                m_Apple = Instantiate(m_ApplePrefab, gridPiece.transform.position, Quaternion.identity).GetComponent<Apple>();
            else
                m_Apple.transform.position = gridPiece.transform.position;
            gridPiece.AddGridItem(m_Apple);
        }
    }
}
