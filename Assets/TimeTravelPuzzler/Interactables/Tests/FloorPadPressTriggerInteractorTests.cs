//using System.Collections;
//using System.Collections.Generic;
//using NSubstitute;
//using NUnit.Framework;
//using Osiris.TimeTravelPuzzler.Interactables.Core;
//using Osiris.Utilities.Logging;
//using UnityEngine;
//using UnityEngine.TestTools;

//namespace Osiris.TimeTravelPuzzler.Interactables.Tests
//{
//    public class FloorPadPressTriggerInteractorTests
//    {
//        private const string _testLogPrefix = "FloorPadPressTriggerInteractorTests";
//        private UnityConsoleLogger _logger;
//        private IFloorPad _floorPadSub;

//        private FloorPadPressTriggerInteractor _interactor;

//        [SetUp]
//        public void Initialise()
//        {
//            _logger = (UnityConsoleLogger)ScriptableObject.CreateInstance(typeof(UnityConsoleLogger));
//            _logger.DisplayLogging = false;
//            _floorPadSub = Substitute.For<IFloorPad>();
//            _interactor = new FloorPadPressTriggerInteractor(_logger, _testLogPrefix);
//        }

//        [Test]
//        public void Interact_ShouldCallPressWhenCanPressReturnsTrue()
//        {
//            _floorPadSub.CanPress(Arg.Any<int>()).Returns(true);
//            _interactor.Interact(_floorPadSub, 50);

//            _floorPadSub.Received().Press();
//        }

//        [Test]
//        public void Interact_ShouldCallCanPressWithCorrectParameter()
//        {
//            _floorPadSub.CanPress(Arg.Any<int>()).Returns(true);
//            _interactor.Interact(_floorPadSub, 50);

//            _floorPadSub.DidNotReceive().CanPress(Arg.Is<int>(x => x != 50));
//            _floorPadSub.Received().CanPress(Arg.Is<int>(50));
//        }

//        [Test]
//        public void Interact_ShouldNotCallPressWhenCanPressReturnsFalse()
//        {
//            _floorPadSub.CanPress(Arg.Any<int>()).Returns(false);
//            _interactor.Interact(_floorPadSub, 50);

//            _floorPadSub.DidNotReceive().Press();
//        }
//    }
//}
