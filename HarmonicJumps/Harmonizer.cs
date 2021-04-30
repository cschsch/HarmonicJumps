using System.Collections.Generic;

namespace HarmonicJumps
{
    public class Harmonizer
    {
        public static IEnumerable<Key> Next(Key key)
        {
            var keys = new List<Key>
            {
                key - 7,
                key - 2,
                key - 1,
                key,
                key + 1,
                key + 2,
                key + 7,
                -key
            };

            if(key.Signature == Signature.Minor)
            {
                keys.Add(-key - 1);
                keys.Add(-key + 3);
                keys.Add(-key - 4);
            }
            else if(key.Signature == Signature.Major)
            {
                keys.Add(-key + 1);
                keys.Add(-key - 3);
                keys.Add(-key + 4);
            }

            return keys;
        }
    }
}
