using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GW2Trader.Model;
using GW2TPApiWrapper.Enum;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Data;
using GW2TPApiWrapperTest;

namespace GW2TraderTest.Test
{
    [TestClass]
    public class ApiDataUpdaterTest
    {
        private GameItemModel ValidGameItem()
        {
            return new GameItemModel
            {
                ItemId = 1,
                IconUrl = "oldUrl",
                Name = "oldName",
                RestrictionLevel = 0,
                Type = ItemType.Type.Armor,
                Rarity = ItemRarity.Rarity.Basic,
                Listing = new ItemListing
                {
                    Id = 1,
                    Buys = new Listing[] 
                    {
                        new Listing{ Quantity = 1, UnitPrice = 1},
                        new Listing{ Quantity = 2, UnitPrice = 2}
                    },
                    Sells = new Listing[] 
                    { 
                        new Listing{ Quantity = 10, UnitPrice = 10},
                        new Listing{ Quantity = 20, UnitPrice = 20}
                    }
                }
            };
        }

        [TestMethod]
        public void ItemDataShouldBeUpdated()
        {
            ApiTestDataFactory testDataFactory = new ApiTestDataFactory();
            ITradingPostApiWrapper wrapper = new TradingPostApiWrapperMock(testDataFactory);
            IApiDataUpdater updater = new ApiDataUpdater(wrapper);

            GameItemModel item = ValidGameItem();
            updater.UpdateItemData(item);

            Item updatedItem = testDataFactory.Items.Find(i => i.Id == item.ItemId);

            Assert.AreEqual(updatedItem.IconUrl, item.IconUrl);
            Assert.AreEqual(updatedItem.Level, item.RestrictionLevel);
            Assert.AreEqual(updatedItem.Name, item.Name);
            Assert.AreEqual(updatedItem.Rarity, item.Rarity);
            Assert.AreEqual(updatedItem.Type, item.Type);
        }

        [TestMethod]
        public void CommerceDataShouldBeUpdated()
        {
            ApiTestDataFactory testDataFactory = new ApiTestDataFactory();
            ITradingPostApiWrapper wrapper = new TradingPostApiWrapperMock(testDataFactory);
            IApiDataUpdater updater = new ApiDataUpdater(wrapper);

            GameItemModel item = ValidGameItem();
            updater.UpdatePrices(item);

            ItemListing updatedCommerceData = testDataFactory.ItemListings.Find(i => i.Id == item.ItemId);

            Listing[] buys = updatedCommerceData.Buys;
            
            CollectionAssert.AreEqual(updatedCommerceData.Buys, item.Listing.Buys);
            CollectionAssert.AreEqual(updatedCommerceData.Sells, item.Listing.Sells);
        }
    }
}
