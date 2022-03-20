using NUnit.Framework;
using Osiris.Utilities.Extensions;
using UnityEngine;

namespace Osiris.Utilities.Tests
{
    public class Vector2ExtensionsTests
    {
        [Test]
        public void ToVector3_ZCoordinateShouldBeZero()
        {
            Vector3 expectedVector = new Vector3(1, 2, 0);
            Vector2 inputVector = new Vector2(1, 2);
            Vector3 resultVector = inputVector.ToVector3();
            Assert.AreEqual(expectedVector, resultVector);
        }

        [Test]
        public void ToVector3_ZCoordinateShouldMatchInput()
        {
            Vector3 expectedVector = new Vector3(1, 2, 9);
            Vector2 inputVector = new Vector2(1, 2);
            Vector3 resultVector = inputVector.ToVector3(9);
            Assert.AreEqual(expectedVector, resultVector);
        }
    }
}