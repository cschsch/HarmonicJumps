using System;
using System.Linq;
using System.Linq.Expressions;
using Database;
using GUI.Model;
using HarmonicJumps;
using Microsoft.Extensions.Configuration;
using SQLite;

namespace CommandLine
{
    //TODO currently only used for debugging purposes
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddCommandLine(Environment.GetCommandLineArgs())
                .Build()
                .Get<Configuration>();

            var rekordboxDatabase = new RekordboxDatabase(configuration.DatabasePath);
            using var db = new SQLiteConnection(rekordboxDatabase.ConnectionString);
            var tracks = db.Table<Content>().AsParallel().Select(c => Track.FromID(db, c.ID)).ToArray();
            var harmonizer = new Harmonizer(configuration.HarmonizerDepth);
            var trackFinder = new TrackFinder(harmonizer, tracks);

            var path = trackFinder.Find(
                tracks.First(track => track.Key == HarmonicJumps.Key.Create(4, Signature.Major)),
                tracks.First(track => track.Key == HarmonicJumps.Key.Create(12, Signature.Minor)))
                .Take(5)
                .ToArray();
        }
    }
}
