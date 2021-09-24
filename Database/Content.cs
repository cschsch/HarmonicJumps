using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Database
{
    [Table("djmdContent")]
    public class Content : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string FolderPath { get; set; }
        public string FileNameL { get; set; }
        public string FileNameS { get; set; }
        public string Title { get; set; }
        public string ArtistID { get; set; }
        public string AlbumID { get; set; }
        public string GenreID { get; set; }
        public int? BPM { get; set; }
        public int? Length { get; set; }
        public int? TrackNo { get; set; }
        public int? BitRate { get; set; }
        public int? BitDepth { get; set; }
        public string Commnt { get; set; }
        public int? FileType { get; set; }
        public int? Rating { get; set; }
        public int? ReleaseYear { get; set; }
        public string RemixerID { get; set; }
        public string LabelID { get; set; }
        public string OrgArtistID { get; set; }
        public string KeyID { get; set; }
        public string StockDate { get; set; }
        public string ColorID { get; set; }
        public int? DJPlayCount { get; set; }
        public string ImagePath { get; set; }
        public string MasterDBID { get; set; }
        public string MasterSongID { get; set; }
        public string AnalysisDataPath { get; set; }
        public string SearchStr { get; set; }
        public int? FileSize { get; set; }
        public int? DiscNo { get; set; }
        public string ComposerID { get; set; }
        public string Subtitle { get; set; }
        public int? SampleRate { get; set; }
        public int? DisableQuantize { get; set; }
        public int? Analysed { get; set; }
        public string ReleaseDate { get; set; }
        public string DateCreated { get; set; }
        public int? ContentLink { get; set; }
        public string Tag { get; set; }
        public string ModifiedByRBM { get; set; }
        public string HotCueAutoLoad { get; set; }
        public string DeliveryControl { get; set; }
        public string DeliveryComment { get; set; }
        public string CueUpdated { get; set; }
        public string AnalysisUpdated { get; set; }
        public string TrackInfoUpdated { get; set; }
        public string Lyricist { get; set; }
        public string ISRC { get; set; }
        public int? SamplerTrackInfo { get; set; }
        public int? SamplerPlayOffset { get; set; }
        public float? SamplerGain { get; set; }
        public string VideoAssociate { get; set; }
        public int? LyricStatus { get; set; }
        public int? ServiceID { get; set; }
        public string OrgFolderPath { get; set; }
        public string Reserved1 { get; set; }
        public string Reserved2 { get; set; }
        public string Reserved3 { get; set; }
        public string Reserved4 { get; set; }
        public string ExtInfo { get; set; }
        public string rb_file_id { get; set; }
        public string DeviceID { get; set; }
        public string rb_LocalFolderPath { get; set; }
        public string SrcID { get; set; }
        public string SrcTitle { get; set; }
        public string SrcArtistName { get; set; }
        public string SrcAlbumName { get; set; }
        public int? SrcLength { get; set; }

        public override string ToString() => Title;
    }
}