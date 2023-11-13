
using SQLite;

namespace Mytaskapp.Models
{
    internal class Product
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Items { get; set; }


    }
}
