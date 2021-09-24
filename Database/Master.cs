using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Database
{
    [Table("sqlite_master")]
    public class Master
    {
        public string type { get; set; }
        public string name { get; set; }
        public string tbl_name { get; set; }
        public int rootpage { get; set; }
        public string sql { get; set; }

        public override string ToString() => $"{tbl_name} - {name}";
    }
}
