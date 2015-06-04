using System;
using DataLayer.Model;

namespace DataLayerTest
{
    internal static class TestDataProvider
    {
        public static Item GetItem()
        {
            return new Item
            {
                Name = "Beginner Sword",
                Rarity = "Rare",
                Type = "Weapon",
                SubType = "Greatsword",
                Level = 0,
                Id = 1
            };
        }

        public static Investment GetInvestment()
        {
            return new Investment
            {
                Item = GetItem(),
                BuyPrice = 100,
                SellPrice = 200,
                Count = 5,
                PurchaseDate = DateTime.Today,
                IsSold = false
            };
        }
    }
}
