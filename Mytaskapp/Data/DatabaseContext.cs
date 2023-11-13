
using CoreGraphics;
using SQLite;
using System.Linq.Expressions;

namespace Mytaskapp.Data
{
    public class DatabaseContext
    {
        private const string DbName = "Mydata.db";
        private static string Dbpath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(Dbpath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

        
        // Create table if not exists = CTINE
        private async Task CTINE<TTable>() where TTable : class, new() 
        {
            await Database.CreateTableAsync<TTable>();
        }

        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CTINE<TTable>();
            return Database.Table<TTable>();
        }



        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new() 
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        
        }

        public async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
            return await Database.Table<TTable>().Where(predicate).ToListAsync();
        }




        //public async Task Try()
        //{
        //    await Database.CreateTableAsync<Product>();
        //    Database.Table<Product>().ToListAsync();

        //}
    }
}
