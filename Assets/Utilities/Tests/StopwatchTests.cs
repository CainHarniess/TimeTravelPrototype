using NUnit.Framework;
using Osiris.Utilities.Timing;
using System;

namespace Osiris.Utilities.Tests
{
    public class StopwatchTests
    {
        private Stopwatch _stopwatch;

        [SetUp]
        public void Initialise()
        {
            _stopwatch = new Stopwatch();
        }

        [Test]
        public void Start_ShouldThrowExceptionIfAlreadyRunning()
        {
            _stopwatch.Start();
            Assert.Throws<InvalidOperationException>(() => _stopwatch.Start());
        }

        [Test]
        public void Stop_ShouldThrowExceptionIfNotRunning()
        {
            Assert.Throws<InvalidOperationException>(() => _stopwatch.Stop());
        }
    }
}
