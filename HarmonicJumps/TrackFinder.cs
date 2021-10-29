using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace HarmonicJumps
{
    public class TrackFinder
    {
        public Harmonizer Harmonizer { get; }
        public ICollection<Track> Tracks { get; }
        public IDictionary<Key, Track[]> KeyTrackMap { get; }

        public TrackFinder(Harmonizer harmonizer, ICollection<Track> tracks)
        {
            Harmonizer = harmonizer;
            Tracks = tracks;
            KeyTrackMap = tracks.GroupBy(track => track.Key).ToDictionary(group => group.Key, group => group.ToArray());
        }

        public IEnumerable<Track[][]> Find(Track start, Track end, FilterOptions options = FilterOptions.Default) => Harmonizer
                .Find(start.Key, end.Key, options)
                .AsParallel()
                .Select(keyPath => keyPath.Select(key => KeyTrackMap[key]).ToArray());
    }
}
