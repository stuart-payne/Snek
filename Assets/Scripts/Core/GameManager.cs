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

        private void Start()
        {
            m_Ticker.AddTickable(this);
            m_Ticker.StartTick();
        }

        public void Tick()
        {
            m_Snek.MoveSnek(m_GameInput.DirectionToMove);
        }
    }
}
