using NUnit.Framework;
using Osiris.Utilities.Timing;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Osiris.Utilities.PlayTests
{
    public class StopwatchPlayTests
    {
        private Stopwatch _stopwatch;

        [SetUp]
        public void Initialise()
        {
            _stopwatch = new Stopwatch();
        }

        [UnityTest]
        public IEnumerator Stop_DeltaTimeShouldBeDifferenceBetweenStartAndStop()
        {
            float waitTime = 0.1f;
            _stopwatch.Start();

            yield return new WaitForSeconds(waitTime);

            _stopwatch.Stop();

            Assert.That(waitTime, Is.EqualTo(_stopwatch.DeltaTime).Within(0.025f));
        }
    }
}
