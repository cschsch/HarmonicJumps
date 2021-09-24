using SQLite;

namespace Database
{
    [Table("djmdGenre")]
    public class Genre : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}