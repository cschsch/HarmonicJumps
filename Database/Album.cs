using SQLite;

namespace Database
{
    [Table("djmdAlbum")]
    public class Album : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Name { get; set; }
        public string AlbumArtistID { get; set; }
        public string ImagePath { get; set; }
        public int? Compilation { get; set; }
        public string SearchStr { get; set; }

        public override string ToString() => Name;
    }
}