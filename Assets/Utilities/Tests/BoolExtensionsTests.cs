using NUnit.Framework;
using Osiris.Utilities.Extensions;

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
            Assert.AreEqual(false, valueToChange);
        }
    }
}