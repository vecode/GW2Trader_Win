using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Entities;
using GW2Trader.Model;

namespace GW2Trader.Data
{
    public class ApiDataUpdater : IApiDataUpdater
    {
        ITradingPostApiWrapper _tpApiWrapper;

        public ApiDataUpdater(ITradingPostApiWrapper wrapper)
        {
            _tpApiWrapper = wrapper;
        }

        public void UpdateItemData(GameItemModel item)
        {
            Item updatedData = _tpApiWrapper.ItemDetails(item.ItemId);
            item.IconUrl = updatedData.IconUrl;
            item.Name = updatedData.Name;
            item.Rarity = updatedData.Rarity;
            item.RestrictionLevel = updatedData.Level;
            item.Type = updatedData.Type;
        }

        public void UpdateCommerceData(GameItemModel item)
        {
            ItemListing updatedData = _tpApiWrapper.Listings(item.ItemId);
            item.Listing.Buys = updatedData.Buys;
            item.Listing.Sells = updatedData.Sells;
            item.CommerceDataLastUpdated = DateTime.Now;
        }

    }
}
