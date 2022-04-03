using NSubstitute;
using NUnit.Framework;
using O = Osiris.Utilities.Logging;
using UnityEngine;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using Osiris.TimeTravelPuzzler.Core.Commands;

namespace Osiris.TimeTravelPuzzler.Interactables.Tests
{
    public class OneWayFloorPadTests
    {
        private const string _testLogPrefix = "OneWayFloorPadTests";

        private IFloorPad _floorPadBehaviourSub;
        private FloorPad _floorPad;
        private IEventChannelSO _pressedChannelSub;
        private IEventChannelSO _releasedChannelSub;
        private IEventChannelSO<IRewindableCommand> _recordableEventChannelSub;
        private O::UnityConsoleLogger _logger;

        [SetUp]
        public void Initialise()
        {
            _floorPadBehaviourSub = Substitute.For<IFloorPad>();
            _floorPadBehaviourSub.RequiredPressWeight.Returns(50);

            _pressedChannelSub = Substitute.For<IEventChannelSO>();
            _releasedChannelSub = Substitute.For<IEventChannelSO>();

            _recordableEventChannelSub = Substitute.For<IEventChannelSO<IRewindableCommand>>();

            _logger = (O::UnityConsoleLogger)ScriptableObject.CreateInstance(typeof(O::UnityConsoleLogger));
            _logger.DisplayLogging = false;

            _floorPad = new OneWayFloorPad(_floorPadBehaviourSub, _logger, _testLogPrefix, _pressedChannelSub,
                                           _releasedChannelSub, _recordableEventChannelSub);
        }

        [Test]
        public void CanRelease_ShouldReturnFalseAfterBeingPressed()
        {
            _floorPad.Press();
            Assert.False(_floorPad.CanRelease(-_floorPad.CurrentPressWeight));
        }

        [Test]
        public void Release_ShouldLogErrorToUnityConsole()
        {
            O::ILogger loggerSub = Substitute.For<O::ILogger>();
            OneWayFloorPad floorPad = new OneWayFloorPad(_floorPadBehaviourSub, loggerSub, _testLogPrefix,
                                                         _pressedChannelSub, _releasedChannelSub, _recordableEventChannelSub);
            floorPad.Press();
            floorPad.Release();

            loggerSub.Received().Log(Arg.Is<string>(s => s == "One-way floor pads may not be released."),
                Arg.Any<string>(), Arg.Is<O::LogLevel>(ll => ll == O::LogLevel.Error));

        }
    }
}
