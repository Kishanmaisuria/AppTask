using SQLite;
using System.Linq.Expressions;

namespace Mytaskapp.Data
{
    public class DatabaseContext
    {
        private const string DbName = "Mydata.db3";
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
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();

        }

        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CTINE<TTable>();
            return await Database.InsertAsync(item) > 0;
        }
        //GetItemByID = GIBID
        public async Task<TTable> GIBIDAsync<TTable>(object PrimaryKey) where TTable : class, new()
        {
            await CTINE<TTable>();
            return await Database.GetAsync<TTable>(PrimaryKey);
        }

        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CTINE<TTable>();
            return await Database.UpdateAsync(item) > 0;
        }

        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CTINE<TTable>();
            return await Database.DeleteAsync(item) > 0;

        }


        //DeleteItemByID = DIBID
        public async Task<bool> DIBIDAsync<TTable>(object PrimaryKey) where TTable : class, new()
        {
            await CTINE<TTable>();
            return await Database.DeleteAsync<TTable>(PrimaryKey) > 0;
        }

        public async ValueTask DisposeAsync() => await _connection?.CloseAsync();









        //public async Task Try()
        //{
        //    await Database.CreateTableAsync<Product>();
        //    Database.Table<Product>().ToListAsync();

        //}
    }
}
