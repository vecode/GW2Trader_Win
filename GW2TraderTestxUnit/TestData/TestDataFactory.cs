using System.Collections.Generic;
using GW2Trader.Model;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Enum;

namespace GW2TraderTest.TestData
{
    internal class TestDataFactory : ITestDataFactory
    {
        public IEnumerable<GameItemModel> GetTestGameItems()
        {
            List<GameItemModel> items = new List<GameItemModel>()
            {
                new GameItemModel 
                {
                    ItemId = 123456,
                    IconUrl = @"http://icon_file_1.png",
                    Name = "Test Item 1",
                    Rarity = ItemRarity.Rarity.Basic.ToString(),
                    RestrictionLevel = 10,
                    Type = ItemType.Type.Weapon.ToString(),
                    Listing = new ItemListing 
                    {
                        Id = 3,
                        Buys = new Listing[] 
                        {
                            new Listing { Quantity = 50, UnitPrice = 200 },
                            new Listing { Quantity = 55, UnitPrice = 202 }
                        },
                        Sells = new Listing[]
                        {
                            new Listing { Quantity = 10, UnitPrice = 500 },
                            new Listing { Quantity = 20, UnitPrice = 501 },
                            new Listing { Quantity = 30, UnitPrice = 502 }
                        }                        
                    }        
                },
                new GameItemModel 
                {
                    ItemId = 2,
                    IconUrl = @"http://icon_file_2.png",
                    Name = "Test Item 2",
                    Rarity = ItemRarity.Rarity.Fine.ToString(),
                    RestrictionLevel = 20,
                    Type = ItemType.Type.Armor.ToString(),
                    Listing = new ItemListing 
                    {
                        Id = 3,
                        Buys = new Listing[] 
                        {
                            new Listing { Quantity = 50, UnitPrice = 200 },
                            new Listing { Quantity = 55, UnitPrice = 202 }
                        },
                        Sells = new Listing[]
                        {
                            new Listing { Quantity = 10, UnitPrice = 500 },
                            new Listing { Quantity = 20, UnitPrice = 501 },
                            new Listing { Quantity = 30, UnitPrice = 502 }
                        }                        
                    }          
                },
                                new GameItemModel 
                {
                    ItemId = 3,
                    IconUrl = @"http://icon_file_3.png",
                    Name = "Test Item 3",
                    Rarity = ItemRarity.Rarity.Fine.ToString(),
                    RestrictionLevel = 30,
                    Type = ItemType.Type.Armor.ToString(),
                    Listing = new ItemListing 
                    {
                        Id = 3,
                        Buys = new Listing[] 
                        {
                            new Listing { Quantity = 50, UnitPrice = 200 },
                            new Listing { Quantity = 55, UnitPrice = 202 }
                        },
                        Sells = new Listing[]
                        {
                            new Listing { Quantity = 10, UnitPrice = 500 },
                            new Listing { Quantity = 20, UnitPrice = 501 },
                            new Listing { Quantity = 30, UnitPrice = 502 }
                        }                        
                    }
                }
            };
            return items;
        }
    }
}
