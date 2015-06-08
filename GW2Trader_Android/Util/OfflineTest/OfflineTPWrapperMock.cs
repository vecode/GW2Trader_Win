using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GW2TPApiWrapper.Wrapper;

namespace GW2Trader_Android.Util.OfflineTest
{
    public class OfflineTPWrapperMock : ITradingPostApiWrapper
    {
        public IEnumerable<int> ItemIds()
        {
            return Enumerable.Range(1, 10);
        }

        public GW2TPApiWrapper.Entities.ApiItem ItemDetails(int id)
        {
            return new GW2TPApiWrapper.Entities.ApiItem
            {
                Id = id,
                Description ="Test Item " + id,
                Level = 12,
                Name = "ItemName " + id,
                Rarity = "Exotic",
                Type = "Weapon"
            };
        }

        public IEnumerable<GW2TPApiWrapper.Entities.ApiItem> ItemDetails(IEnumerable<int> ids)
        {
            return ids.Select(ItemDetails);
        }

        public GW2TPApiWrapper.Entities.ApiItemListing Listings(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GW2TPApiWrapper.Entities.ApiItemListing> Listings(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GW2TPApiWrapper.Entities.ApiItemPrice> Prices(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}