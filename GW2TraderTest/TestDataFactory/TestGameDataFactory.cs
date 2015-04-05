using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using GW2Trader.Data;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Enum;
using GW2TPApiWrapper.Entities;
using System.Collections.ObjectModel;

namespace GW2TraderTest
{
    public class TestGameDataFactory
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

        //public IEnumerable<ItemIdWatchlistModel> GetTestItemIdWatchlists()
        //{
        //    List<ItemIdWatchlistModel> watchlists = new List<ItemIdWatchlistModel>()
        //    {
        //        new ItemIdWatchlistModel()
        //        { 
        //            Id = 0,
        //            Description = "some really special items", 
        //            Name = "some items",
        //            Items = new List<int> { 2, 3, 123456 }
        //        },
        //        new ItemIdWatchlistModel()
        //        { 
        //            Id = 1,
        //            Description = "some basic items", 
        //            Name = "basic items",
        //            Items = new List<int> { 2, 3 }
        //        },
        //        new ItemIdWatchlistModel()
        //        { 
        //            Id = 2,
        //            Description = "some really special items", 
        //            Name = "some items",
        //            Items = new List<int>()
        //        }
        //    };
        //    return watchlists;
        //}

        public IEnumerable<InvestmentWatchlistModel> GetTestInvestmentWatchlists()
        {
            List<InvestmentWatchlistModel> watchlists = new List<InvestmentWatchlistModel>()
            {
                new InvestmentWatchlistModel()
                {
                    Id = 0,
                    Description = "some investments in rare weapons",
                    Name = "rare weapons",
                    Items = new ObservableCollection<InvestmentModel>
                    {
                        new InvestmentModel 
                        {
                            Count = 5,
                            PurchasePrice = 12345,
                            DesiredSellPrice = 99999,
                            IsSold = false, 
                            SoldFor = null
                        },
                        new InvestmentModel 
                        {
                            Count = 8,
                            PurchasePrice = 1254,
                            DesiredSellPrice = 7188,
                            SoldFor = 7500,
                            IsSold = true, 
                        }
                    }
                },
                new InvestmentWatchlistModel()
                {
                    Id = 0,
                    Description = "some investments in rare weapons",
                    Name = "rare weapons",
                    Items = new ObservableCollection<InvestmentModel>()
                }
            };
            return watchlists;
        }

    }
}
