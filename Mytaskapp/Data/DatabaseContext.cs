using System;
using System.Runtime;
using Mytaskapp.Models;
using SQLite;

namespace Mytaskapp.Data
{
    public class DatabaseContext
    {
        private const string DbName = "Mydata.db";
        private static string Dbpath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(Dbpath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

        


        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new() 
        {
         await Database.CreateTableAsync<TTable>();
         return await Database.Table<TTable>().ToListAsync();
        
        }

        public async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Exception<Func<TTable, bool>> predicate) where TTable : class, new()
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
