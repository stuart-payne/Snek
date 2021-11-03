using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class Ticker : MonoBehaviour
    {
        private float m_TickRate;
        private IEnumerator m_TickCoroutine;
        private List<ITickable> m_Tickables;
        private List<ITickable> m_LateTickables;

        private void Awake()
        {
            m_Tickables = new List<ITickable>();
            m_LateTickables = new List<ITickable>();
        }


        public void SetTickRate(float tickRate) => m_TickRate = tickRate;

        public void StartTick()
        {
            m_TickCoroutine = Tick(m_TickRate);
            StartCoroutine(m_TickCoroutine);
        }

        public void StopTick()
        {
            StopCoroutine(m_TickCoroutine);
        }

        public void AddTickable(ITickable tickable) => m_Tickables.Add(tickable);
        public void RemoveTickable(ITickable tickable) => m_Tickables.Remove(tickable);
        public void AddLateTickable(ITickable tickable) => m_LateTickables.Add(tickable);
        public void RemoveLateTickable(ITickable tickable) => m_LateTickables.Remove(tickable);

        IEnumerator Tick(float secondsPerTick)
        {
            while (true)
            {
                yield return new WaitForSeconds(secondsPerTick);
                foreach (var tickable in m_Tickables)
                {
                    tickable.Tick();
                }

                foreach (var lateTickable in m_LateTickables)
                {
                    lateTickable.Tick();
                }
            }
        }
    }
}
