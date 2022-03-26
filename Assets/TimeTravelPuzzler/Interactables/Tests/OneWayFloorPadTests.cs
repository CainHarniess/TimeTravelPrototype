using NSubstitute;
using NUnit.Framework;
using O = Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Tests
{
    public class OneWayFloorPadTests
    {
        private const string _testLogPrefix = "OneWayFloorPadTests";

        private IFloorPad _floorPadBehaviourSub;
        private FloorPad _floorPad;
        private O::UnityConsoleLogger _logger;

        [SetUp]
        public void Initialise()
        {
            _floorPadBehaviourSub = Substitute.For<IFloorPad>();
            _floorPadBehaviourSub.RequiredPressWeight.Returns(50);

            _logger = (O::UnityConsoleLogger)ScriptableObject.CreateInstance(typeof(O::UnityConsoleLogger));
            _logger.DisplayLogging = false;

            _floorPad = new OneWayFloorPad(_floorPadBehaviourSub, _logger, _testLogPrefix);
        }

        [Test]
        public void CanRelease_ShouldReturnFalseAfterBeingPressed()
        {
            _floorPad.Press();
            Assert.IsFalse(_floorPad.CanRelease(-_floorPad.CurrentPressWeight));
        }

        [Test]
        public void Release_ShouldLogErrorToUnityConsole()
        {
            O::ILogger loggerSub = Substitute.For<O::ILogger>();
            OneWayFloorPad floorPad = new OneWayFloorPad(_floorPadBehaviourSub, loggerSub, _testLogPrefix);
            floorPad.Press();
            floorPad.Release();

            loggerSub.Received().Log(Arg.Is<string>(s => s == "One-way floor pads may not be released."),
                Arg.Any<string>(), Arg.Is<O::LogLevel>(ll => ll == O::LogLevel.Error));

        }
    }
}
