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


        public void UpdateItemInformation(GameItemModel item)
        {
            ItemDetails updatedInformation = _tpApiWrapper.ItemDetails(item.Id);
            item.IconUrl = updatedInformation.IconUrl;
            item.LastUpdated = DateTime.Now;
            item.Name = updatedInformation.Name;
            item.Rarity = updatedInformation.Rarity;
            item.RestrictionLevel = updatedInformation.Level;
            item.Type = updatedInformation.Type;
        }

        public void UpdateCommerceData(GameItemModel item)
        {
            ItemListing updatedData = _tpApiWrapper.Listings(item.Id);
            item.Listing.Buys = updatedData.Buys;
            item.Listing.Sells = updatedData.Sells;
        }

    }
}
