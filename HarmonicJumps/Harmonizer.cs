using System.Collections.Generic;

namespace HarmonicJumps
{
    public class Harmonizer
    {
        public int MaxDepth { get; }

        public Harmonizer(int maxDepth)
        {
            MaxDepth = maxDepth;
        }

        public static IEnumerable<Key> Next(Key key, FindOptions options = FindOptions.RepeatSameKey)
        {
            yield return key - 7;
            yield return key - 2;
            yield return key - 1;
            if(options.HasFlag(FindOptions.RepeatSameKey)) yield return key;
            yield return -key;
            yield return key + 1;
            yield return key + 2;
            yield return key + 7;

            if(key.Signature == Signature.Minor)
            {
                yield return -key - 1;
                yield return -key + 3;
                yield return -key - 4;
            }
            else if(key.Signature == Signature.Major)
            {
                yield return -key + 1;
                yield return -key - 3;
                yield return -key + 4;
            }
        }

        }
    }
}
