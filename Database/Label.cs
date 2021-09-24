using SQLite;

namespace Database
{
    [Table("djmdLabel")]
    public class Label : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}