
using SQLite;

namespace Mytaskapp.Data
{
    public class DatabaseContext
    {
        private const string DbName = "Mydata.db";
        private static string Dbpath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(Dbpath, SQLiteOpenFlags.Create| SQLiteOpenFlags.ReadWrite|SQLiteOpenFlags.SharedCache));
    }
}
