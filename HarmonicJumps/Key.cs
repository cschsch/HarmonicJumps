using System;
using System.Linq;

namespace HarmonicJumps
{
    public class Key
    {
        public int Value { get; }
        public Signature Signature { get; }

        public Key(int value, Signature signature)
        {
            if (!Enumerable.Range(1, 12).Contains(value))
                throw new ArgumentOutOfRangeException(nameof(value), $"Value of key must be between 1 and 12, but was {value}.");
            if (signature == Signature.Default)
                throw new ArgumentException($"Signature must be either minor or major, but was {signature}", nameof(signature));

            Value = value;
            Signature = signature;
        }

        public static Key operator +(Key a, int x)
        {
            if (x < 0) return a - -x;
            var value = (a.Value + x) % 12;

            if (value == 0)
                return new Key(12, a.Signature);

            return new Key(value, a.Signature);
        }

        public static Key operator -(Key a, int x)
        {
            if (x < 0) return a + -x;
            var value = (a.Value - x) % 12;

            if (value <= 0)
                return new Key(value + 12, a.Signature);

            return new Key(value, a.Signature);
        }

        public static Key operator -(Key a) => a.Signature == Signature.Minor ? new Key(a.Value, Signature.Major) : new Key(a.Value, Signature.Minor);
    }
}
