using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapperTest
{
    public class ApiTestDataFactory
    {
        private List<Item> _items = new List<Item>
        {
            new Item 
            {
                Id = 1,
                Description = "this is item 1",
                Level = 10,
                Rarity = ItemRarity.Rarity.Basic.ToString(),
                Type = ItemType.Type.Armor.ToString(),
                VendorValue = 3, 
                Name = "item 1",
                IconUrl = "http://item_icon.png"
            },
            new Item 
            {
                Id = 2,
                Description = "this is item 2",
                Level = 20,
                Rarity = ItemRarity.Rarity.Basic.ToString(),
                Type = ItemType.Type.Armor.ToString(),
                VendorValue = 6, 
                Name = "item 2",
                IconUrl = "http://item_icon.png"
            },
            new Item 
            {
                Id = 3,
                Description = "this is item 3",
                Level = 30,
                Rarity = ItemRarity.Rarity.Basic.ToString(),
                Type = ItemType.Type.Armor.ToString(),
                VendorValue = 9, 
                Name = "item 3",
                IconUrl = "http://item_icon.png"
            }
        };

        // TODO obsolete
        //public List<ItemPrice> Prices
        //{
        //    get
        //    {
        //        return new List<ItemPrice>
        //        { 
        //            new ItemPrice
        //            {
        //                Id = 1,
        //                Buys = new Listing { Quantity = 10, UnitPrice = 100 },
        //                Sells = new Listing { Quantity = 50, UnitPrice = 500 }
        //            },
        //            new ItemPrice
        //            {
        //                Id = 2,
        //                Buys = new Listing { Quantity = 20, UnitPrice = 200 },
        //                Sells = new Listing { Quantity = 100, UnitPrice = 1000 }
        //            },
        //            new ItemPrice
        //            {
        //                Id = 3,
        //                Buys = new Listing { Quantity = 30, UnitPrice = 300 },
        //                Sells = new Listing { Quantity = 150, UnitPrice = 1500 }
        //            }
        //        };
        //    }
        //}

        private List<ItemListing> _itemListings = new List<ItemListing>()
        {
            new ItemListing
            {
                Id = 1,
                Buys = new Listing[] 
                { 
                    new Listing{ Quantity = 10, UnitPrice = 100},
                    new Listing{ Quantity = 24, UnitPrice = 102}
                },
                Sells = new Listing[]
                {
                    new Listing{ Quantity = 123, UnitPrice = 123},
                    new Listing{ Quantity = 130, UnitPrice = 124}
                }
            },
            new ItemListing
            {
                Id = 2,
                Buys = new Listing[] 
                { 
                    new Listing{ Quantity = 10, UnitPrice = 100},
                    new Listing{ Quantity = 24, UnitPrice = 102}
                },
                Sells = new Listing[]
                {
                    new Listing{ Quantity = 123, UnitPrice = 123},
                    new Listing{ Quantity = 130, UnitPrice = 124}
                }
            },
            new ItemListing
            {
                Id = 1,
                Buys = new Listing[] 
                { 
                    new Listing{ Quantity = 10, UnitPrice = 100},
                    new Listing{ Quantity = 24, UnitPrice = 102}
                },
                Sells = new Listing[]
                {
                    new Listing{ Quantity = 123, UnitPrice = 123},
                    new Listing{ Quantity = 130, UnitPrice = 124}
                }
            }        
        };

        public List<ItemListing> ItemListings
        {
            get
            {
                return _itemListings;
            }
        }

        public List<Item> Items
        {
            get
            {
                return _items;
            }
        }

        public int[] AllIds()
        {
            return _items.Select(item => item.Id).Distinct().ToArray();
        }
    }
}
