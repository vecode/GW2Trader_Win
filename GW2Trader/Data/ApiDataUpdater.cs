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

        public void UpdateCommerceData(IList<GameItemModel> items)
        {
            int[] ids = items.Select(i => i.ItemId).ToArray();
            List<ItemPrice> updatedPrices = _tpApiWrapper.Price(ids).ToList();

            foreach (GameItemModel gameItemModel in items)
            {
                ItemPrice respectivePrice = updatedPrices.Find(p => p.Id == gameItemModel.ItemId);
                gameItemModel.SellListing = respectivePrice.Sells.UnitPrice;
                gameItemModel.SellListingQuantity = respectivePrice.Sells.Quantity;
                gameItemModel.BuyOrder = respectivePrice.Buys.UnitPrice;
                gameItemModel.BuyOrderQuantity = respectivePrice.Buys.Quantity;
                gameItemModel.CommerceDataLastUpdated = DateTime.Now;
            }
        }

    }
}
