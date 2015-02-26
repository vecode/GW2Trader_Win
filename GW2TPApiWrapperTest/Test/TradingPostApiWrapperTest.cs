using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Entities;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapperTest.Mock;

namespace GW2TPApiWrapperTest.Test
{
    [TestClass]
    public class TradingPostApiWrapperTest
    {
        private TradingPostApiWrapper _tpApiWrapper = new TradingPostApiWrapper(new ApiAccessorMock());
        private int _invalidId = -1;

        // valid id from https://api.guildwars2.com/v2/commerce/listings
        private int _validId = 30689;

        [TestMethod]
        public void ItemIdsShouldBeValid()
        {
            int[] ids = _tpApiWrapper.ItemIds();
            // MockApiAccessor returns 3 ids
            Assert.AreEqual(ids.Length, 3);

            Assert.AreEqual(ids[0], 1);
            Assert.AreEqual(ids[1], 2);
            Assert.AreEqual(ids[2], 11);
        }

        [TestMethod]
        public void SingleItemRetrievalShouldWork()
        {
            // check for invalid item id
            //Item item = _tpApiWrapper.ItemDetails(_invalidId);
            //Assert.IsNull(item);

            // details from https://api.guildwars2.com/v2/items/30689
            var item = _tpApiWrapper.ItemDetails(30689);
            Assert.IsNotNull(item);
            Assert.AreEqual(30689, item.Id);
            Assert.AreEqual(item.Name, "Eternity");
            Assert.AreEqual(item.Type.ToString(), "Weapon");
            Assert.AreEqual(item.Rarity.ToString(), "Legendary");
        }

        [TestMethod]
        public void ItemListingsShouldBeValid()
        {
            // check for invalid item id
            ItemListing listing = _tpApiWrapper.Listings(_invalidId);
            Assert.IsNull(listing);

            // check for valid id
            listing = _tpApiWrapper.Listings(_validId);
            Assert.AreEqual(listing.Buys.Length, 2);
            Assert.AreEqual(listing.Buys[0].Quantity, 49);
            Assert.AreEqual(listing.Buys[0].UnitPrice, 4);
            Assert.AreEqual(listing.Sells.Length, 2);
        }
    }
}
