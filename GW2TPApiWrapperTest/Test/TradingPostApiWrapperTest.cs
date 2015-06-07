using System;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Entities;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapperTest.Mock;
using Xunit;

namespace GW2TPApiWrapperTest.Test
{
    public class TradingPostApiWrapperTest
    {
        private readonly TradingPostApiWrapper _tpApiWrapper = new TradingPostApiWrapper(new ApiAccessorMock());

        // valid id from https://api.guildwars2.com/v2/commerce/listings
        private const int ValidId = 30689;

        [Fact]
        public void ItemIdsShouldBeValid()
        {
            int[] ids = _tpApiWrapper.ItemIds().ToArray();
            // MockApiAccessor returns 3 ids
            Assert.Equal(ids.Length, 3);

            Assert.Equal(ids[0], 1);
            Assert.Equal(ids[1], 2);
            Assert.Equal(ids[2], 11);
        }

        [Fact]
        public void SingleItemRetrievalShouldWork()
        {
            // details from https://api.guildwars2.com/v2/items/30689
            var item = _tpApiWrapper.ItemDetails(30689);
            Assert.NotNull(item);
            Assert.Equal(30689, item.Id);
            Assert.Equal(item.Name, "Eternity");
            Assert.Equal(item.Type, "Weapon");
            Assert.Equal(item.Rarity, "Legendary");
        }

        [Fact]
        public void ItemListingsShouldBeValid()
        {
            // check for invalid item id
            ApiItemListing listing = _tpApiWrapper.Listings(-1);
            Assert.Null(listing);

            // check for valid id
            listing = _tpApiWrapper.Listings(ValidId);
            Assert.Equal(listing.Buys.Length, 2);
            Assert.Equal(listing.Buys[0].Quantity, 49);
            Assert.Equal(listing.Buys[0].UnitPrice, 4);
            Assert.Equal(listing.Sells.Length, 2);
        }

        [Fact]
        public void RetrievalOfMultiplePricesShouldWork()
        {
            int[] ids = { 24, 68, 69 };

            List<ApiItemPrice> prices = _tpApiWrapper.Prices(ids).ToList();

            Assert.Equal(24, prices[0].Id);
            Assert.Equal(68, prices[1].Id);
            Assert.Equal(69, prices[2].Id);
        }
    }
}
