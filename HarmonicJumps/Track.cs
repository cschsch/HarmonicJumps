using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using HarmonicJumps;
using SQLite;

namespace HarmonicJumps
{
    public class Track
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string[] GenreTags { get; set; }
        public decimal BPM { get; set; }
        public TimeSpan Length { get; set; }
        public int TrackNo { get; set; }
        public int Rating { get; set; }
        public string Label { get; set; }
        public HarmonicJumps.Key Key { get; set; }
        public int DJPlayCount { get; set; }
        public string ImagePath { get; set; }
        public string[] Tags { get; set; }
        public DateTime DateCreated { get; set; }
        public string[][] Playlists { get; set; }

        public static Track FromID(SQLiteConnection db, string id, string sharePath = "")
        {
            var content = db.Find<Content>(id);
            var artist = db.Find<Artist>(content.ArtistID);
            var album = db.Find<Album>(content.AlbumID);
            var genre = db.Find<Genre>(content.GenreID);
            var label = db.Find<Label>(content.LabelID);
            var key = db.Find<Database.Key>(content.KeyID);
            var myTagIds = db.Table<SongMyTag>().Where(smt => smt.ContentID == content.ID).Select(smt => smt.MyTagID);
            var myTags = db.Table<MyTag>().Where(mt => myTagIds.Contains(mt.ID));
            var playlistIds = db.Table<SongPlaylist>().Where(sp => sp.ContentID == content.ID).Select(sp => sp.PlaylistID);
            var playlistTable = db.Table<Playlist>();
            var innermostPlaylists = playlistTable.Where(p => playlistIds.Contains(p.ID));

            Func<Playlist, string[]> getPlaylistPath = playlist =>
            {
                var path = new List<string>();
                while (playlist != null)
                {
                    path.Add(playlist.Name);
                    playlist = playlistTable.SingleOrDefault(p => p.ID == playlist.ParentID);
                }
                path.Reverse();
                return path.ToArray();
            };

            return new Track
            {
                Title = content.Title,
                Artist = artist.Name,
                Album = album?.Name,
                Genre = genre.Name,
                GenreTags = myTags.Where(mt => mt.ParentID == "1").Select(mt => mt.Name).ToArray(),
                BPM = (content.BPM ?? 0) / 100,
                Length = TimeSpan.FromSeconds(content.Length ?? 0),
                TrackNo = content.TrackNo ?? 0,
                Rating = content.Rating ?? 0,
                Label = label?.Name,
                Key = HarmonicJumps.Key.FromScaleName(key.ScaleName),
                DJPlayCount = content.DJPlayCount ?? 0,
                ImagePath = Path.Combine(sharePath, content.ImagePath),
                Tags = myTags.Where(mt => mt.ParentID != "1").Select(mt => mt.Name).ToArray(),
                DateCreated = DateTime.Parse(content.DateCreated),
                Playlists = innermostPlaylists.Select(getPlaylistPath).ToArray()
            };
        }
    }

}
