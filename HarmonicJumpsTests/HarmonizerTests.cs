using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HarmonicJumps;
using NUnit.Framework;

namespace HarmonicJumpsTests
{
    public class HarmonizerTests
    {
        [Test]
        public void Next_1B_AllHarmonicJumps()
        {
            var key = Key.Create(1, Signature.Major);

            var expected = new[]
            {
                Key.Create(6, Signature.Major),
                Key.Create(10, Signature.Minor),
                Key.Create(11, Signature.Major),
                Key.Create(12, Signature.Major),
                Key.Create(1, Signature.Major),
                Key.Create(1, Signature.Minor),
                Key.Create(2, Signature.Major),
                Key.Create(2, Signature.Minor),
                Key.Create(3, Signature.Major),
                Key.Create(5, Signature.Minor),
                Key.Create(8, Signature.Major)
            };

            var result = Harmonizer.Next(key);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Next_1A_AllHarmonicJumps()
        {
            var key = Key.Create(1, Signature.Minor);

            var expected = new[]
            {
                Key.Create(6, Signature.Minor),
                Key.Create(9, Signature.Major),
                Key.Create(11, Signature.Minor),
                Key.Create(12, Signature.Minor),
                Key.Create(12, Signature.Major),
                Key.Create(1, Signature.Minor),
                Key.Create(1, Signature.Major),
                Key.Create(2, Signature.Minor),
                Key.Create(3, Signature.Minor),
                Key.Create(4, Signature.Major),
                Key.Create(8, Signature.Minor)
            };

            var result = Harmonizer.Next(key);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Next_1A_FindOptionsDefault_DoesNotContain1A()
        {
            var key = Key.Create(1, Signature.Minor);

            var expected = new[]
            {
                Key.Create(6, Signature.Minor),
                Key.Create(9, Signature.Major),
                Key.Create(11, Signature.Minor),
                Key.Create(12, Signature.Minor),
                Key.Create(12, Signature.Major),
                Key.Create(1, Signature.Major),
                Key.Create(2, Signature.Minor),
                Key.Create(3, Signature.Minor),
                Key.Create(4, Signature.Major),
                Key.Create(8, Signature.Minor)
            };

            var result = Harmonizer.Next(key, FilterOptions.Default);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Find_SameKey_ThrowsArgumentException()
        {
            var harmonizer = new Harmonizer(3);
            var key = Key.Create(1, Signature.Minor);

            Assert.Throws<ArgumentException>(() => harmonizer.Find(key, key));
        }

        [Test]
        public void Find_1A_6B_DoesNotRepeatSameKeyTwice()
        {
            var harmonizer = new Harmonizer(4);
            var start = Key.Create(1, Signature.Minor);
            var end = Key.Create(6, Signature.Major);

            var results = harmonizer.Find(start, end).ToArray();

            foreach(var result in results)
            {
                CollectionAssert.AreEqual(result, result.Distinct());
            }
        }
    }
}
