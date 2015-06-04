using System;
using SQLite.Net.Attributes;

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

        [MaxLength(120)]
        public string IconUrl { get; set; }

        public int BuyPrice { get; set; }

        public int SellPrice { get; set; }

        public int Demand { get; set; }

        public int Supply { get; set; }

        public int PreviousBuyPrice { get; set; }

        public int PreviousSellPrice { get; set; }

        public int PreviousDemand { get; set; }

        public int PreviousSupply { get; set; }

        public DateTime CommerceDataLastUpdated { get; set; }
    }
}
