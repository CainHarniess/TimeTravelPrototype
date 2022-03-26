using NSubstitute;
using NUnit.Framework;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Tests
{
    public class FloorPadReleaseTriggerInteractorTests
    {
        private const string _testLogPrefix = "FloorPadPressTriggerInteractorTests";
        private UnityConsoleLogger _logger;
        private IFloorPad _floorPadSub;

        private FloorPadReleaseTriggerInteractor _interactor;

        [SetUp]
        public void Initialise()
        {
            _logger = (UnityConsoleLogger)ScriptableObject.CreateInstance(typeof(UnityConsoleLogger));
            _logger.DisplayLogging = false;

            _floorPadSub = Substitute.For<IFloorPad>();
            _interactor = new FloorPadReleaseTriggerInteractor(_logger, _testLogPrefix);
        }

        [Test]
        public void Interact_ShouldCallReleaseWhenCanReleaseReturnsTrue()
        {
            _floorPadSub.CanRelease(Arg.Any<int>()).Returns(true);
            _interactor.Interact(_floorPadSub, 50);

            _floorPadSub.Received().Release();
        }

        [Test]
        public void Interact_ShouldCallCanReleaseWithCorrectParameter()
        {
            _floorPadSub.CanRelease(Arg.Any<int>()).Returns(true);
            _interactor.Interact(_floorPadSub, 50);

            _floorPadSub.DidNotReceive().CanRelease(Arg.Is<int>(x => x != 50));
            _floorPadSub.Received().CanRelease(Arg.Is<int>(50));
        }

        [Test]
        public void Interact_ShouldNotCallReleaseWhenCanReleaseReturnsFalse()
        {
            _floorPadSub.CanRelease(Arg.Any<int>()).Returns(false);
            _interactor.Interact(_floorPadSub, 50);

            _floorPadSub.DidNotReceive().Release();
        }
    }
}
