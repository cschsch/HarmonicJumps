using SQLite;

namespace Database
{
    public class RekordboxDatabase
    {
        public SQLiteConnectionString ConnectionString { get; }

        public RekordboxDatabase(string path)
        {
            ConnectionString = new SQLiteConnectionString(path,
                openFlags: SQLiteOpenFlags.ReadOnly | SQLiteOpenFlags.SharedCache,
                storeDateTimeAsTicks: true,
                key: "402fd482c38817c35ffa8ffb8c7d93143b749e7d315df7a81732a1ff43608497");
        }
    }
}
