using System;
using System.Collections;
using System.Collections.Generic;
using Snek.Core;
using UnityEngine;

namespace Snek.Core
{
    public class GameManager : MonoBehaviour, ITickable
    {
        [SerializeField] private Snek m_Snek;
        [SerializeField] private GameInput m_GameInput;
        [SerializeField] private Ticker m_Ticker;
        [SerializeField] private AppleSpawner m_AppleSpawner;
        [SerializeField] private RockSpawner m_RockSpawner;
        [SerializeField] private int m_RockSpawnRate = 30;

        private int m_Ticks = 0;
        
        public Difficulty[] Difficulties { get; private set; }

        private void Awake()
        {
            var difficultyParser = new DifficultyParser();
            Difficulties = difficultyParser.GetDifficultiesFromFile().Difficulties;
        }

        private void Start()
        {
            m_Ticker.AddTickable(this);
            m_Ticker.StartTick();
            m_Snek.FinishedSpawningEvent += m_AppleSpawner.SpawnNewApple;
            m_Snek.AppleEatenEvent += m_AppleSpawner.SpawnNewApple;
            m_Snek.DeathEvent += () => Debug.Log("DEAD SNEK");
        }

        public void Tick()
        {
            m_Snek.MoveSnek(m_GameInput.DirectionToMove);
            m_Ticks++;
            if(m_Ticks % m_RockSpawnRate == 0)
                m_RockSpawner.SpawnRock(m_Snek.HeadPosition);
        }
    }
}
