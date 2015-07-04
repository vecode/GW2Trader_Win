using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader_Windows.Model;
using GW2Trader.ApiWrapper.Entities;

namespace GW2Trader_Windows.Data
{
    public class ApiDataUpdater : IApiDataUpdater
    {
        private const int ItemsToProcessPerTask = 200;
        private readonly ITradingPostApiWrapper _tpApiWrapper;

        public ApiDataUpdater(ITradingPostApiWrapper wrapper)
        {
            _tpApiWrapper = wrapper;
        }

        public void UpdateItemData(GameItemModel item)
        {
            var updatedData = _tpApiWrapper.ItemDetails(item.ItemId);
            item.IconUrl = updatedData.IconUrl;
            item.Name = updatedData.Name;
            item.Rarity = updatedData.Rarity;
            item.RestrictionLevel = updatedData.Level;
            item.Type = updatedData.Type;
        }

        public void UpdatePrices(GameItemModel item)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrices(IList<GameItemModel> items)
        {
            int[] ids = items.Select(i => i.ItemId).ToArray();
            var updatedPrices = _tpApiWrapper.Prices(ids).ToDictionary(item => item.Id, item => item);

            foreach (GameItemModel item in items)
            {                
                ApiItemPrice price;
                if (updatedPrices.TryGetValue(item.ItemId, out price))
                {
                    item.SellPrice = price.Sells.UnitPrice;                    
                    item.SellListingQuantity = price.Sells.Quantity;
                    item.BuyPrice = price.Buys.UnitPrice;
                    item.BuyOrderQuantity = price.Buys.Quantity;
                    item.CommerceDataLastUpdated = DateTime.Now;
                }
            }
        }

        public void UpdateCommerceDataParallel(GameItemModel item)
        {
            Task.Run(() => UpdatePrices(item));
            Task.Run(() => UpdateListings(item));
        }

        public void UpdateCommerceDataParallel(IList<GameItemModel> items)
        {
            int neededTasks = (int)Math.Ceiling(items.Count / (ItemsToProcessPerTask * 1.0f));

            foreach (int taskIndex in Enumerable.Range(0, neededTasks))
            {
                // divide items in smaller subsets and process each subset in parallel
                var itemSubset = items.Skip(taskIndex * ItemsToProcessPerTask)
                    .Take(ItemsToProcessPerTask).ToList();
                Task.Run(() => UpdatePrices(itemSubset));
                Task.Run(() => UpdateListings(itemSubset));
            }
        }

        public void UpdateListings(GameItemModel item)
        {
            var updatedData = _tpApiWrapper.Listings(item.ItemId);
            item.Listing = new ApiItemListing
            {
                Id = item.ItemId,
                Buys = updatedData.Buys,
                Sells = updatedData.Sells
            };
            item.CommerceDataLastUpdated = DateTime.Now;
        }

        public void UpdateListings(IList<GameItemModel> items)
        {
            var ids = items.Select(i => i.ItemId).ToArray();
            var updatedListings = _tpApiWrapper.Listings(ids).ToList();

            foreach (var item in items)
            {
                var respectiveListing = updatedListings.SingleOrDefault(i => i.Id == item.ItemId);
                if (respectiveListing != null)
                {
                    item.Listing = respectiveListing;
                }
            }
        }

        public void UpdateListingsParallel(GameItemModel item)
        {
            UpdateListingsParallel(new List<GameItemModel> { item });
        }

        public void UpdatePricesParallel(IList<GameItemModel> items)
        {
            int neededTasks = (int)Math.Ceiling(items.Count / (ItemsToProcessPerTask * 1.0f));

            foreach (int taskIndex in Enumerable.Range(0, neededTasks))
            {
                // divide items in smaller subsets and process each subset in parallel
                var itemSubset = items.Skip(taskIndex * ItemsToProcessPerTask)
                    .Take(ItemsToProcessPerTask).ToList();
                Task.Run(() => UpdatePrices(itemSubset));
            }
        }

        public void UpdateListingsParallel(IList<GameItemModel> items)
        {
            int neededTasks = (int)Math.Ceiling(items.Count / (ItemsToProcessPerTask * 1.0f));

            foreach (int taskIndex in Enumerable.Range(0, neededTasks))
            {
                // divide items in smaller subsets and process each subset in parallel
                var itemSubset = items.Skip(taskIndex * ItemsToProcessPerTask)
                    .Take(ItemsToProcessPerTask).ToList();
                Task.Run(() => UpdateListings(itemSubset));
            }
        }
    }
}