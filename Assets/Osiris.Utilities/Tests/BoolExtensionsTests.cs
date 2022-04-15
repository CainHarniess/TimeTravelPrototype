using NUnit.Framework;
using Osiris.Utilities.Extensions;
using System;

namespace Osiris.Utilities.Tests
{
    public class BoolExtensionsTests
    {
        [Test]
        public void ChangeStatus_ShouldChangeStatus()
        {
            bool valueToChange = true;
            valueToChange.ChangeStatus(false);
            Assert.AreEqual(false, valueToChange);
        }

        [Test]
        public void ChangeStatus_ShouldNotChangeStatus()
        {
            bool valueToChange = false;
            valueToChange.ChangeStatus(false);
        }

        [Test]
        public void ChangeStatusWithException_ShouldChangeStatus()
        {
            bool valueToChange = true;
            valueToChange.ChangeStatusWithException(false);
            Assert.AreEqual(false, valueToChange);
        }

        [Test]
        public void ChangeStatusWithException_ShouldThrowException()
        {
            bool valueToChange = false;
            Assert.Throws<ArgumentException>(() => valueToChange.ChangeStatusWithException(false));
        }
    }
}