using System;

namespace HarmonicJumps
{
    [Flags]
    public enum FilterOptions
    {
        Default = 0,
        RepeatSameKey = 1,
        SameGenre = 1 << 1,
        SameTag = 1 << 2
    }
}
