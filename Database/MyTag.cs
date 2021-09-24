using SQLite;

namespace Database
{
    [Table("djmdMyTag")]
    public class MyTag : CommonTable
    {
        [PrimaryKey]
        public string ID { get; set; }
        public int? Seq { get; set; }
        public string Name { get; set; }
        public int? Attribute { get; set; }
        public string ParentID { get; set; }
    }
}