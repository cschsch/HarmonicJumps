using SQLite;

namespace Database
{
    [Table("djmdPlaylist")]
    public class Playlist : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public int? Seq { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int? Attribute { get; set; }
        public string ParentID { get; set; }
        public string SmartList { get; set; }

        public override string ToString() => Name;
    }
}