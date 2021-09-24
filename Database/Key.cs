using SQLite;

namespace Database
{
    [Table("djmdKey")]
    public class Key : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string ScaleName { get; set; }
        public int? Seq { get; set; }

        public override string ToString() => ScaleName;
    }
}