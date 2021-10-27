using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Snek.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Snek.Tests
{
    public class TickerTests
    {

        [UnityTest]
        public IEnumerator WillTickOnceAfterTickRatePassed()
        {
            var ticker = SetupTicker(1.0f);
            var testTickable = new TestTickable();
            ticker.AddTickable(testTickable);
            ticker.StartTick();
            yield return new WaitForSeconds(1.1f);
            Assert.IsTrue(testTickable.TickCount == 1);
        }

        [UnityTest]
        public IEnumerator WIllTickOnceThenStopTickingAfter()
        {
            var ticker = SetupTicker(0.2f);
            var testTickable = new TestTickable();
            ticker.AddTickable(testTickable);
            ticker.StartTick();
            yield return new WaitForSeconds(0.3f);
            Assert.IsTrue(testTickable.TickCount == 1);
            ticker.StopTick();
            yield return new WaitForSeconds(0.3f);
            Assert.IsTrue(testTickable.TickCount == 1);
        }

        [UnityTest]
        public IEnumerator WillStopTickingTickableAfterRemoval()
        {
            var ticker = SetupTicker(0.2f);
            var tickableToStay = new TestTickable();
            var tickableToBeRemoved = new TestTickable();
            
            ticker.AddTickable(tickableToStay);
            ticker.AddTickable(tickableToBeRemoved);
            ticker.StartTick();

            yield return new WaitForSeconds(0.3f);
            ticker.RemoveTickable(tickableToBeRemoved);
            yield return new WaitForSeconds(0.3f);
            
            Assert.IsTrue(tickableToStay.TickCount == 2);
            Assert.IsTrue(tickableToBeRemoved.TickCount == 1);
        }

        private class TestTickable : ITickable
        {
            public int TickCount = 0;
            public void Tick() => TickCount++;
        }

        private Ticker SetupTicker(float tickRate)
        {
            var tickerObj = new GameObject();
            var ticker = tickerObj.AddComponent<Ticker>();
            ticker.SetTickRate(tickRate);
            return ticker;
        }
    }
}
