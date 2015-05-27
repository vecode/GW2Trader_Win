using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DataLayer.Model
{
    [Table("Item")]
    public class Item : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [MaxLength(75)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Rarity { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(20)]
        public string SubType { get; set; }

        public int Level { get; set; }

        [ForeignKey(typeof(Icon))]
        public int IconId { get; set; }

        [ManyToOne]
        public Icon Icon { get; set; }

        public int BuyPrice { get; set; }

        public int SellPrice { get; set; }

        public int Demand { get; set; }

        public int Supply { get; set; }

        public DateTime CommerceDataLastUpdated { get; set; }
    }
}
