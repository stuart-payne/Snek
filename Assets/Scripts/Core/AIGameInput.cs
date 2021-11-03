using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class AIGameInput : MonoBehaviour, IGameInput, ITickable
    {
        [SerializeField] private Board m_Board;
        [SerializeField] private Snek m_Snek;
        [SerializeField] private RockSpawner m_RockSpawner;
        [SerializeField] private AppleSpawner m_AppleSpawner;

        private int m_PathPosition = 0;
        private GridPiece[] m_CurrentPath;
        private Direction m_DirectionToMove;
        private bool m_PathIsDirty;

        public Direction DirectionToMove {
            get
            {
                return m_DirectionToMove;
            }
        }

        public void Tick()
        {

            if (m_PathPosition > 0)
                m_PathPosition--;
            if (m_PathIsDirty)
            {
                FindPathToApple();
                m_PathIsDirty = false;
            }
            SetDirection();
        }

        private void Start()
        {
            m_Snek.FinishedSpawningEvent += Reevaluate;
            m_Snek.AppleEatenEvent += Reevaluate;
            m_RockSpawner.RockSpawned += Reevaluate;
            FindPathToApple();
        }

        private void FindPathToApple()
        {
            var start= m_Board.GetGridPiece(m_Snek.HeadPosition);
            var goal = m_Board.GetGridPiece(m_AppleSpawner.CurrentApplePosition);

            m_CurrentPath = Pathfinding.BreadthFillSearch(start, goal);
            m_PathPosition = m_CurrentPath.Length - 1;
            SetDirection();
        }

        private void SetDirection()
        {
            if (m_CurrentPath == null || m_CurrentPath.Length == 0)
                return; ;
            var current = m_CurrentPath[m_PathPosition];
            var next = m_CurrentPath[m_PathPosition - 1];
            var dir = next.GridPosition - current.GridPosition; 
            m_DirectionToMove = Utils.Vector2IntToDirection(dir);
        }

        private void Reevaluate()
        {
            m_PathIsDirty = true;
        }
    }
}
