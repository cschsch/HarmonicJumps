using SQLite;

namespace Database
{
    [Table("djmdArtist")]
    public class Artist : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Name { get; set; }
        public string SearchStr { get; set; }

        public override string ToString() => Name;
    }
}