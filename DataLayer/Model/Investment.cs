using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DataLayer.Model
{
    [Table("Investment")]
    public class Investment : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public static readonly float SalesCommission = 0.15f;

        public bool IsSold { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int BuyPrice { get; set; }

        public int SellPrice { get; set; }

        public int Count { get; set; }

        public int TargetSellPrice { get; set; }

        [OneToMany]
        public List<Item> Items { get; set; }
    }
}
