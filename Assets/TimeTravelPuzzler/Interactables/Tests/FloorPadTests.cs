using NSubstitute;
using NUnit.Framework;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Tests
{
    public class FloorPadTests
    {
        private const string _testLogPrefix = "FloorPadTests";

        private IFloorPad _floorPadBehaviourSub;
        private FloorPad _floorPad;
        private UnityConsoleLogger _logger;

        [SetUp]
        public void Initialise()
        {
            _floorPadBehaviourSub = Substitute.For<IFloorPad>();
            _floorPadBehaviourSub.RequiredPressWeight.Returns(50);

            _logger = (UnityConsoleLogger)ScriptableObject.CreateInstance(typeof(UnityConsoleLogger));
            _logger.DisplayLogging = false;

            _floorPad = new FloorPad(_floorPadBehaviourSub, _logger, _testLogPrefix);
        }

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
            _logger.Log("Add insufficient weight.", _testLogPrefix);
            Assert.IsFalse(_floorPad.CanPress(49));
            _logger.Log("Add sufficient additional weight.", _testLogPrefix);
            Assert.IsTrue(_floorPad.CanPress(1));
        } 
        #endregion

        [Test]
        public void Press_ShouldChangePressStatusIfNotPressed()
        {
            _floorPad.Press();
            Assert.IsTrue(_floorPad.IsPressed);
        }

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

        [Test]
        public void Release_ShouldChangePressStatusIfNotPressed()
        {
            _floorPad.Press();
            Assert.IsTrue(_floorPad.IsPressed);
            _floorPad.Release();
            Assert.IsFalse(_floorPad.IsPressed);
        }
    }
}
