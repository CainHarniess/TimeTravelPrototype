using NSubstitute;
using NUnit.Framework;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using OUL = Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Tests
{
    public class FloorPadTests
    {
        private const string _testLogPrefix = "FloorPadTests";

        private IFloorPad _floorPadBehaviourSub;
        private FloorPad _floorPad;
        private IEventChannelSO _pressedChannelSub;
        private IEventChannelSO _releasedChannelSub;
        private OUL.ILogger _loggerSub;

        [SetUp]
        public void Initialise()
        {
            _floorPadBehaviourSub = Substitute.For<IFloorPad>();
            _floorPadBehaviourSub.RequiredPressWeight.Returns(50);

            _pressedChannelSub = Substitute.For<IEventChannelSO>();
            _releasedChannelSub = Substitute.For<IEventChannelSO>();

            _loggerSub = Substitute.For<OUL.ILogger>();

            _floorPad = new FloorPad(_floorPadBehaviourSub, _loggerSub, _testLogPrefix, _pressedChannelSub,
                                     _releasedChannelSub);
        }

        #region RequiredPressWeight
        [Test]
        public void RequiredPressWeight_AccessesBehaviourValue()
        {
            int requiredPressWeight = _floorPad.RequiredPressWeight;
            int behaviourPressWeight = _floorPadBehaviourSub.Received().RequiredPressWeight;
        }

        [Test]
        public void RequiredPressWeight_ShouldEqualBehaviorValue()
        {
            int requiredPressWeight = _floorPad.RequiredPressWeight;
            int behaviourPressWeight = _floorPadBehaviourSub.RequiredPressWeight;
            Assert.AreEqual(behaviourPressWeight, requiredPressWeight);
        } 
        #endregion

        #region CanPress
        [Test]
        public void CanPress_ShouldReturnFalseIfAlreadyPressed()
        {
            _floorPad.Press();
            Assert.IsFalse(_floorPad.CanPress(_floorPadBehaviourSub.RequiredPressWeight));
        }

        [Test]
        public void CanPress_ShouldReturnTrueIfNotAlreadyPressed()
        {
            int requiredWeight = _floorPadBehaviourSub.RequiredPressWeight;
            Assert.IsTrue(_floorPad.CanPress(requiredWeight));
        }

        [Test]
        public void CanPress_ShouldReturnTrueWithSufficientWeight()
        {
            Assert.IsTrue(_floorPad.CanPress(50));
        }

        [Test]
        public void CanPress_ShouldReturnFalseWithInsufficientWeight()
        {
            Assert.IsFalse(_floorPad.CanPress(49));
        }

        [Test]
        public void CanPress_ShouldReturnTrueWithSufficientCombinedWeight()
        {
            _loggerSub.Log("Add insufficient weight.", _testLogPrefix);
            Assert.IsFalse(_floorPad.CanPress(49));
            _loggerSub.Log("Add sufficient additional weight.", _testLogPrefix);
            Assert.IsTrue(_floorPad.CanPress(1));
        }

        [Test]
        public void CanPress_ShouldAccessBehaviourRequiredWeight()
        {
            _floorPad.CanPress(50);
            int behaviourRequiredWeight = _floorPadBehaviourSub.Received().RequiredPressWeight;
        }
        #endregion

        #region Press
        [Test]
        public void Press_ShouldChangePressStatusIfNotPressed()
        {
            _floorPad.Press();
            Assert.IsTrue(_floorPad.IsPressed);
        }

        [Test]
        public void Press_ShouldBroadcastOnPressChannel()
        {
            _floorPad.Press();
            _pressedChannelSub.Received().Raise();
            _releasedChannelSub.DidNotReceive().Raise();
        }

        [Test]
        public void Press_ShouldBroadcastOnPressChannelOnceIfNotReleased()
        {
            _floorPad.Press();
            _floorPad.Press();
            _pressedChannelSub.Received(1).Raise();
        }

        [Test]
        public void Press_ShouldBroadcastOnPressChannelTwiceReleased()
        {
            _floorPad.Press();
            _floorPad.Release();
            _floorPad.Press();
            _pressedChannelSub.Received(2).Raise();
        } 
        #endregion

        #region CanRelease
        [Test]
        public void CanRelease_ShouldReturnTrueIfAlreadyPressed()
        {
            _floorPad.Press();
            Assert.IsTrue(_floorPad.CanRelease(1));
        }

        [Test]
        public void CanRelease_ShouldReturnFalseIfNotAlreadyPressed()
        {
            int requiredWeight = _floorPadBehaviourSub.RequiredPressWeight;
            Assert.False(_floorPad.CanRelease(requiredWeight));
        }

        [Test]
        public void CanRelease_ShouldReturnFalseIfPressNotCalled()
        {
            _floorPad.CanPress(75);
            Assert.IsFalse(_floorPad.CanRelease(50));
        }

        [Test]
        public void CanRelease_ShouldReturnTrueWithEnoughWeightRemoved()
        {
            _floorPad.CanPress(75);
            _floorPad.Press();
            Assert.IsTrue(_floorPad.CanRelease(50));
        }

        [Test]
        public void CanRelease_ShouldReturnFalseWithInsufficientWeight()
        {
            _floorPad.CanPress(75);
            _floorPad.Press();
            Assert.IsFalse(_floorPad.CanRelease(25));
        }

        [Test]
        public void CanRelease_ShouldReturnTrueWithSufficientCombinedWeightRemoved()
        {
            _floorPad.CanPress(75);
            _floorPad.Press();
            Assert.IsFalse(_floorPad.CanRelease(25));
            Assert.IsTrue(_floorPad.CanRelease(1));
        }
        #endregion

        #region Release
        [Test]
        public void Release_ShouldChangePressStatusIfNotPressed()
        {
            _floorPad.Press();
            Assert.IsTrue(_floorPad.IsPressed);
            _floorPad.Release();
            Assert.IsFalse(_floorPad.IsPressed);
        }

        [Test]
        public void Release_ShouldBroadcastOnReleaseChannel()
        {
            _floorPad.Press();
            _floorPad.Release();
            _releasedChannelSub.Received().Raise();
        }

        [Test]
        public void Release_ShouldBroadcastOnReleaseChannelOnceIfNotPressedAgain()
        {
            _floorPad.Press();
            _floorPad.Release();
            _floorPad.Release();
            _releasedChannelSub.Received(1).Raise();
        }

        [Test]
        public void Release_ShouldBroadcastOnReleaseChannelOnceIfPressedAgain()
        {
            _floorPad.Press();
            _floorPad.Release();
            _floorPad.Press();
            _floorPad.Release();
            _releasedChannelSub.Received(2).Raise();
        } 
        #endregion
    }
}
