using System;
using System.Collections.Generic;
using System.Linq;
using GW2Trader.ApiWrapper.Entities;
using GW2Trader.ApiWrapper.Wrapper;

namespace GW2Trader.Android.Util.OfflineTest
{
    public class OfflineTPWrapperMock : ITradingPostApiWrapper
    {
        public IEnumerable<int> ItemIds()
        {
            return Enumerable.Range(1, 10);
        }

        public ApiItem ItemDetails(int id)
        {
            return new ApiItem
            {
                Id = id,
                Description = "Test Item " + id,
                Level = 12,
                Name = "ItemName " + id,
                Rarity = "Exotic",
                Type = "Weapon"
            };
        }

        public IEnumerable<ApiItem> ItemDetails(IEnumerable<int> ids)
        {
            return ids.Select(ItemDetails);
        }

        public ApiItemListing Listings(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiItemListing> Listings(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiItemPrice> Prices(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}