using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using GW2Trader.Data;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Enums;

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
                    Id = 1,
                    IconUrl = @"http://icon_file_1.png",
                    Name = "Test Item 1",
                    Rarity = Item.ItemRarity.Basic,
                    RestrictionLevel = 10,
                    Type = Item.ItemType.Weapon,
                    Price = new ItemPrice 
                        {
                            Id = 1, 
                            Sells = new Listing 
                            {
                                Quantity = 20, 
                                UnitPrice = 100
                            }, 
                            Buys = new Listing 
                            {
                                Quantity = 123, 
                                UnitPrice = 87
                            }},            
                },
                new GameItemModel 
                {
                    Id = 2,
                    IconUrl = @"http://icon_file_2.png",
                    Name = "Test Item 2",
                    Rarity = Item.ItemRarity.Fine,
                    RestrictionLevel = 20,
                    Type = Item.ItemType.Armor,
                    Price = new ItemPrice 
                        {
                            Id = 2, 
                            Sells = new Listing 
                            {
                                Quantity = 40, 
                                UnitPrice = 200
                            }, 
                            Buys = new Listing 
                            {
                                Quantity = 246, 
                                UnitPrice = 174
                            }},            
                },
                                new GameItemModel 
                {
                    Id = 3,
                    IconUrl = @"http://icon_file_3.png",
                    Name = "Test Item 3",
                    Rarity = Item.ItemRarity.Fine,
                    RestrictionLevel = 30,
                    Type = Item.ItemType.Armor,
                    Price = new ItemPrice 
                        {
                            Id = 3, 
                            Sells = new Listing 
                            {
                                Quantity = 60, 
                                UnitPrice = 300
                            }, 
                            Buys = new Listing 
                            {
                                Quantity = 369, 
                                UnitPrice = 261
                            }},            
                }
            };
            return items;
        }

        public IEnumerable<Watchlist<int>> GetTestWatchlistsWithIds()
        {
            List<Watchlist<int>> watchlists = new List<Watchlist<int>>()
            {
                new Watchlist<int>()
                { 
                    Id = 0,
                    Description = "some really special items", 
                    Name = "some items",
                    Items = new List<int> { 1, 2, 3, 4 }
                },
                new Watchlist<int>()
                { 
                    Id = 1,
                    Description = "some basic items", 
                    Name = "basic items",
                    Items = new List<int> { 99, 4, 7 }
                },
                new Watchlist<int>()
                { 
                    Id = 0,
                    Description = "some really special items", 
                    Name = "some items",
                    Items = new List<int>()
                }
            };
            return watchlists;
        }

        public IEnumerable<Watchlist<InvestmentModel>> GetTestWatchlistsWithInvestments()
        {
            List<Watchlist<InvestmentModel>> watchlists = new List<Watchlist<InvestmentModel>>()
            {
                new Watchlist<InvestmentModel>()
                {
                    Id = 0,
                    Description = "some investments in rare weapons",
                    Name = "rare weapons",
                    Items = new List<InvestmentModel>
                    {
                        new InvestmentModel 
                        {
                            ItemId = 100,
                            Count = 5,
                            PurchasePrice = 12345,
                            DesiredSellPrice = 99999,
                            IsSold = false, 
                            SoldFor = null
                        },
                        new InvestmentModel 
                        {
                            ItemId = 111,
                            Count = 8,
                            PurchasePrice = 1254,
                            DesiredSellPrice = 7188,
                            SoldFor = 7500,
                            IsSold = true, 
                        }
                    }
                },
                new Watchlist<InvestmentModel>()
                {
                    Id = 0,
                    Description = "some investments in rare weapons",
                    Name = "rare weapons",
                    Items = new List<InvestmentModel>()
                }
            };
            return watchlists;
        }
    }
}
