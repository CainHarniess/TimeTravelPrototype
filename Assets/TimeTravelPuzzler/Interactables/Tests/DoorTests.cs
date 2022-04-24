using NSubstitute;
using NUnit.Framework;
using Osiris.Testing.Abstractions;
using Osiris.TimeTravelPuzzler.Interactables.Doors;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.Tests
{
    public class DoorTests
    {
        private OUL.ILogger _loggerSub;
        private IBehaviourProxy _colliderSub;

        private Door _door;

        [SetUp]
        public void Initialise()
        {
            _loggerSub = Substitute.For<OUL.ILogger>();
            _colliderSub = Substitute.For<IBehaviourProxy>();

            _door = new Door(string.Empty, _loggerSub, _colliderSub, false);
        }

        [Test]
        public void CanOpen_ShouldReturnTrueWhenClosed()
        {
            Assert.True(_door.CanOpen());
        }

        [Test]
        public void CanOpen_ShouldReturnFalseWhenOpen()
        {
            _door.Open();
            Assert.False(_door.CanOpen());
        }

        [Test]
        public void Open_ShouldDeactivateRenderer()
        {
            _door.Open();
        }

        [Test]
        public void Open_RendererShouldBeDisabled()
        {
            _door.Open();
        }

        [Test]
        public void Open_ShouldDeactivateBoxCollider2D()
        {
            _door.Open();
            _colliderSub.Received().Enabled = false;
        }

        [Test]
        public void Open_ColliderShouldBeDisabled()
        {
            _door.Open();
            Assert.False(_colliderSub.Enabled);
        }

        [Test]
        public void CanClose_ShouldReturnFalseWhenClosed()
        {
            Assert.False(_door.CanClose());
        }

        [Test]
        public void CanClose_ShouldReturnTrueWhenOpen()
        {
            _door.Open();
            Assert.True(_door.CanClose());
        }

        [Test]
        public void Close_ShouldActivateRenderer()
        {
            _door.Close();
        }

        [Test]
        public void Close_RendererShouldBeEnabled()
        {
            _door.Open();
            _door.Close();
        }

        [Test]
        public void Close_ShouldActivateBoxCollider2D()
        {
            _door.Close();
            _colliderSub.Received().Enabled = true;
        }

        [Test]
        public void Close_ColliderShouldBeEnabled()
        {
            _door.Open();
            _door.Close();
            Assert.True(_colliderSub.Enabled);
        }
    }
}
