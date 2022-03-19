using NUnit.Framework;
using Osiris.Utilities.Extensions;
using UnityEngine;

namespace Osiris.Utilities.Tests
{
    public class Vector3ExtensionsTests
    {
        [Test]
        public void ToVector3_ShouldProduceValidVector2()
        {
            Vector3 inputVector = new Vector3(1, 2, 3);
            Vector2 expectedVector = new Vector2(1, 2);
            Vector2 resultVector = inputVector.ToVector2();
            
            Assert.AreEqual(resultVector, expectedVector);
        }
    }
}