using System;
using System.Collections;
using System.Collections.Generic;
using Snek.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snek.Core
{
    public class GameManager : MonoBehaviour, ITickable
    {
        [SerializeField] private Snek m_Snek;
        [SerializeField] private GameInput m_GameInput;
        [SerializeField] private Ticker m_Ticker;
        [SerializeField] private AppleSpawner m_AppleSpawner;
        [SerializeField] private RockSpawner m_RockSpawner;
        [SerializeField] private Board m_Board;
        [SerializeField] private GameoverUIManager m_GameoverUIManager;
        
        private int m_RockSpawnRate = 30;
        private int m_Ticks = 0;
        private event Action TickEvent;
        
        public Difficulty[] Difficulties { get; private set; }

        private void Awake()
        {
#if UNITY_WEBGL
            IDifficultyProvider difficultyParser = new StaticDifficulties();
#else
            IDifficultyProvider difficultyParser = new DifficultyParser();
#endif
            Difficulties = difficultyParser.GetDifficulties();
        }

        public void SetupGame(Difficulty difficulty)
        {
            BuildBoardWithDifficulty(difficulty.GridXSize, difficulty.GridYSize);
            m_Ticker.SetTickRate(difficulty.SecondsPerTick);
            m_Snek.FinishedSpawningEvent += m_AppleSpawner.SpawnNewApple;
            m_Snek.SpawnStartingBody();
            m_Snek.AppleEatenEvent += m_AppleSpawner.SpawnNewApple;
            m_Snek.DeathEvent += () => Debug.Log("DEAD SNEK");
            m_Snek.DeathEvent += OnSnekDeath;
            SetRockSpawnRate(difficulty.TicksPerStoneSpawn);
            TickEvent += MoveSnek;
            m_Ticker.AddTickable(this);
            m_Ticker.StartTick();
        }

        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void SetRockSpawnRate(int ticksPerStoneSpawn)
        {
            if (ticksPerStoneSpawn == 0) return;
            m_RockSpawnRate = ticksPerStoneSpawn;
            TickEvent += TickRockSpawner;
        }

        private void BuildBoardWithDifficulty(int width, int height)
        {
            m_Board.GenerateGrid(width, height);
        }

        private void OnSnekDeath()
        {
            m_Ticker.StopTick();
            m_GameoverUIManager.gameObject.SetActive(true);
        }

        private void TickRockSpawner()
        {
            m_Ticks++;
            if(m_Ticks % m_RockSpawnRate == 0)
                m_RockSpawner.SpawnRock(m_Snek.HeadPosition);
        }
        
        private void MoveSnek() => m_Snek.MoveSnek(m_GameInput.DirectionToMove);
        
        public void Tick()
        {
            TickEvent?.Invoke();
        }
    }
}
