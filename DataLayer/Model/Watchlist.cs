using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DataLayer.Model
{
    [Table("Watchlist")]
    public class Watchlist : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [OneToMany]
        public List<Item> Items { get; set; }
    }
}
