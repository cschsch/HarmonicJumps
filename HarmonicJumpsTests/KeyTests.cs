using System;
using HarmonicJumps;
using NUnit.Framework;

namespace HarmonicJumpsTests
{
    public class KeyTests
    {
        [Test]
        public void Key_ValueNotInRange_ThrowsArgumentException()
        {
            var values = new[] { -13, 0, 14 };
            
            foreach(var value in values)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Key(value, Signature.Major));
            }
        }

        [Test]
        public void Key_SignatureDefault_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Key(1, default));
        }

        [TestCase(1, 1, 2)]
        [TestCase(10, 3, 1)]
        [TestCase(4, 12, 4)]
        [TestCase(12, 0, 12)]
        [TestCase(12, 1, 1)]
        [TestCase(8, 34, 6)]
        [TestCase(2, -1, 1)]
        [TestCase(1, -3, 10)]
        [TestCase(4, -12, 4)]
        [TestCase(12, -0, 12)]
        [TestCase(1, -1, 12)]
        [TestCase(8, -34, 10)]
        public void Add(int valueOfKey, int valueToAdd, int resultValue)
        {
            var key = new Key(valueOfKey, Signature.Minor);

            var result = key + valueToAdd;

            Assert.AreEqual(resultValue, result.Value);
            Assert.AreEqual(Signature.Minor, result.Signature);
        }

        [TestCase(2, 1, 1)]
        [TestCase(3, 3, 12)]
        [TestCase(4, 12, 4)]
        [TestCase(12, 0, 12)]
        [TestCase(1, 1, 12)]
        [TestCase(8, 34, 10)]
        [TestCase(2, -1, 3)]
        [TestCase(12, -3, 3)]
        [TestCase(4, -12, 4)]
        [TestCase(12, -0, 12)]
        [TestCase(12, -1, 1)]
        [TestCase(8, -34, 6)]
        public void Substract(int valueOfKey, int valueToAdd, int resultValue)
        {
            var key = new Key(valueOfKey, Signature.Minor);

            var result = key - valueToAdd;

            Assert.AreEqual(resultValue, result.Value);
            Assert.AreEqual(Signature.Minor, result.Signature);
        }

        [TestCase(Signature.Minor, Signature.Major)]
        [TestCase(Signature.Major, Signature.Minor)]
        public void Negate(Signature signatureOfKey, Signature resultSignature)
        {
            var key = new Key(12, signatureOfKey);

            var result = -key;

            Assert.AreEqual(resultSignature, result.Signature);
            Assert.AreEqual(12, key.Value);
        }
    }
}