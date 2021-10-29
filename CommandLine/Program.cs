using System;
using System.Linq;
using System.Linq.Expressions;
using GUI.Model;
using HarmonicJumps;
using SQLite;

namespace CommandLine
{
    public class Program
    {
        static void Main(string[] args)
        {
            var rekordboxDatabase = new Database.RekordboxDatabase("C:\\Users\\cschwarzbeck.HOME\\Desktop\\rekordbox_bak_20210916\\master.db");
            using var db = new SQLiteConnection(rekordboxDatabase.ConnectionString);
            var tracks = db.Table<Database.Content>().AsParallel().Select(c => Track.FromID(db, c.ID)).ToArray();
            var harmonizer = new Harmonizer(4);
            var trackFinder = new TrackFinder(harmonizer, tracks);

            var path = trackFinder.Find(
                tracks.First(track => track.Key == Key.Create(4, Signature.Major)),
                tracks.First(track => track.Key == Key.Create(12, Signature.Minor)))
                .Take(5)
                .ToArray();
        }
    }
}
