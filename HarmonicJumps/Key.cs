using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace HarmonicJumps
{
    public class Key : IEquatable<Key>
    {
        public static IReadOnlyDictionary<string, int> KeyValueMappings { get; }
        static Key()
        {
            KeyValueMappings = new Dictionary<string, int> 
            {
                { "Abm", 1 }, { "B", 1 },
                { "Ebm", 2 }, { "F#", 2 },
                { "Bbm", 3 }, { "Db", 3 },
                { "Fm", 4 }, { "Ab", 4 },
                { "Cm", 5 }, { "Eb", 5 },
                { "Gm", 6 }, { "Bb", 6 },
                { "Dm", 7 }, { "F", 7 },
                { "Am", 8 }, { "C", 8 },
                { "Em", 9 }, { "G", 9 },
                { "Bm", 10 }, { "D", 10 },
                { "F#m", 11 }, { "A", 11 },
                { "Dbm", 12 }, { "E", 12 }
            };
        }

        private static ConcurrentDictionary<(int, Signature), Key> FlyweightFactory { get; } = new ConcurrentDictionary<(int, Signature), Key>();
        public int Value { get; }
        public Signature Signature { get; }

        public static Key Create(int value, Signature signature)
        {
            var exists = FlyweightFactory.TryGetValue((value, signature), out var key);
            if (exists) return key;
            var newKey = new Key(value, signature);
            FlyweightFactory[(value, signature)] = newKey;
            return newKey;
        }

        private Key(int value, Signature signature)
        {
            if (!Enumerable.Range(1, 12).Contains(value))
                throw new ArgumentOutOfRangeException(nameof(value), $"Value of key must be between 1 and 12, but was {value}.");
            if (signature == Signature.Default)
                throw new ArgumentException($"Signature must be either minor or major, but was {signature}", nameof(signature));

            Value = value;
            Signature = signature;
        }

        public static Key FromScaleName(string scaleName)
        {
            var value = 0;
            var signature = Signature.Default;

            var first = scaleName.First();
            var last = scaleName.Last();

            if (Enumerable.Range(1, 9).Contains(first - 48))
            {
                value = int.Parse(scaleName.Substring(0, scaleName.Length - 1));
                signature = last == 'A' ? Signature.Minor : Signature.Major;
            } else
            {
                value = KeyValueMappings[scaleName];
                signature = last == 'm' ? Signature.Minor : Signature.Major;
            }

            return Create(value, signature);
        }

        public static Key operator +(Key a, int x)
        {
            if (x < 0) return a - -x;
            var value = (a.Value + x) % 12;

            if (value == 0)
                return Create(12, a.Signature);

            return Create(value, a.Signature);
        }

        public static Key operator -(Key a, int x)
        {
            if (x < 0) return a + -x;
            var value = (a.Value - x) % 12;

            if (value <= 0)
                return Create(value + 12, a.Signature);

            return Create(value, a.Signature);
        }

        public static Key operator -(Key a) => a.Signature == Signature.Minor ? Create(a.Value, Signature.Major) : Create(a.Value, Signature.Minor);

        public bool Equals([AllowNull] Key other)
        {
            if (other is null) return this is null;
            return Value == other.Value && Signature == other.Signature;
        }

        public static bool operator ==(Key a, Key b) => a.Equals(b);
        public static bool operator !=(Key a, Key b) => !a.Equals(b);

        public override bool Equals(object obj)
        {
            return Equals(obj as Key);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Value.GetHashCode();
                hash = hash * 23 + Signature.GetHashCode();
                return hash;
            }
        }

        public override string ToString() => $"{Value}{(Signature == Signature.Minor ? "A" : "B")}";
    }
}
