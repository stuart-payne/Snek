using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snek.Core
{
    public class RockSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_RockPrefab;
        [SerializeField] private Board m_Board;

        [HideInInspector] public List<Rock> Rocks;

        private void Awake()
        {
            Rocks = new List<Rock>();
        }

        public void SpawnRock(Vector2Int snekHeadPosition)
        {
            // we dont want to spawn a rock right in front of the player
            var validGridPiece = m_Board.GetEmptyGridPiecesWithExclusionZone(snekHeadPosition, 2.0f);
            var selectedGridPiece = validGridPiece[Random.Range(0, validGridPiece.Count)];
            var rock = Instantiate(
                m_RockPrefab,
                selectedGridPiece.transform.position,
                Quaternion.identity).GetComponent<Rock>(); 
            Rocks.Add(rock);
            selectedGridPiece.AddGridItem(rock);
            RockSpawned?.Invoke();
        }

        public event Action RockSpawned;
    }
}
