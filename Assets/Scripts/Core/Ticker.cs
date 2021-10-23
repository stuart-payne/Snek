using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class Ticker : MonoBehaviour
    {
        [Range(0.1f, 2.0f)]
        public float TickRate;

        private IEnumerator m_TickCoroutine;
        private List<ITickable> m_Tickables;

        private void Awake()
        {
            m_Tickables = new List<ITickable>();
        }

        public void StartTick()
        {
            m_TickCoroutine = Tick(TickRate);
            StartCoroutine(m_TickCoroutine);
        }

        public void StopTick()
        {
            StopCoroutine(m_TickCoroutine);
        }

        public void AddTickable(ITickable tickable) => m_Tickables.Add(tickable);
        public void RemoveTickable(ITickable tickable) => m_Tickables.Remove(tickable);

        IEnumerator Tick(float secondsPerTick)
        {
            while (true)
            {
                yield return new WaitForSeconds(secondsPerTick);
                foreach (var tickable in m_Tickables)
                {
                    tickable.Tick();
                }
            }
        }
    }
}
