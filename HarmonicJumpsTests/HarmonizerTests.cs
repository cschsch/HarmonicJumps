using System;
using System.Collections.Generic;
using System.Text;
using HarmonicJumps;
using NUnit.Framework;

namespace HarmonicJumpsTests
{
    public class HarmonizerTests
    {
        [Test]
        public void Next_1B_AllHarmonicJumps()
        {
            var key = new Key(1, Signature.Major);

            var expected = new[]
            {
                new Key(6, Signature.Major),
                new Key(10, Signature.Minor),
                new Key(11, Signature.Major),
                new Key(12, Signature.Major),
                new Key(1, Signature.Major),
                new Key(1, Signature.Minor),
                new Key(2, Signature.Major),
                new Key(2, Signature.Minor),
                new Key(3, Signature.Major),
                new Key(5, Signature.Minor),
                new Key(8, Signature.Major)
            };

            var result = Harmonizer.Next(key);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Next_1A_AllHarmonicJumps()
        {
            var key = new Key(1, Signature.Minor);

            var expected = new[]
            {
                new Key(6, Signature.Minor),
                new Key(9, Signature.Major),
                new Key(11, Signature.Minor),
                new Key(12, Signature.Minor),
                new Key(12, Signature.Major),
                new Key(1, Signature.Minor),
                new Key(1, Signature.Major),
                new Key(2, Signature.Minor),
                new Key(3, Signature.Minor),
                new Key(4, Signature.Major),
                new Key(8, Signature.Minor)
            };

            var result = Harmonizer.Next(key);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Next_1A_FindOptionsDefault_DoesNotContain1A()
        {
            var key = new Key(1, Signature.Minor);

            var expected = new[]
            {
                new Key(6, Signature.Minor),
                new Key(9, Signature.Major),
                new Key(11, Signature.Minor),
                new Key(12, Signature.Minor),
                new Key(12, Signature.Major),
                new Key(1, Signature.Major),
                new Key(2, Signature.Minor),
                new Key(3, Signature.Minor),
                new Key(4, Signature.Major),
                new Key(8, Signature.Minor)
            };

            var result = Harmonizer.Next(key, FindOptions.Default);

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
