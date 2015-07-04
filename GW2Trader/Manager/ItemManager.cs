using System;
using System.Collections.Generic;
using System.Linq;

using DataLayer.Repository;
using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader.Model;
using GW2Trader.ApiWrapper.Entities;

namespace GW2Trader.Manager
{
    public class ItemManager : IItemManager
    {
        private ItemRepository _repository;
        private ITradingPostApiWrapper _apiWrapper;


        public ItemManager(
            ItemRepository repository,
            ITradingPostApiWrapper apiWrapper)
        {
            _repository = repository;
            _apiWrapper = apiWrapper;
        }

        public List<Model.Item> Search(
            string keyword, string rarity = null, string type = null,
            string subType = null, int minLevel = 0, int maxLevel = 80,
            int minMargin = 0, int maxMargin = 0, int minRoi = 0,
            int maxRoi = 0, int pageSize = 10, int page = 0)
        {
            var items = _repository.Search(keyword, rarity, type, subType, minLevel, maxLevel, minMargin, maxMargin, minRoi, maxRoi, pageSize, page);
            return items.Select(x => new Item(x)).ToList();
        }

        public void UpdatePrices(Item item)
        {
            var updatedPrices = _apiWrapper.Prices(new[] { item.Id }).Single();

            item.BuyPrice = updatedPrices.Buys.UnitPrice;
            item.SellPrice = updatedPrices.Sells.UnitPrice;
            item.Demand = updatedPrices.Buys.Quantity;
            item.Supply = updatedPrices.Sells.Quantity;
            item.CommerceDataLastUpdated = DateTime.Now;

            _repository.Save(ConvertToBase(item));
        }

        public void UpdatePrices(List<Model.Item> items)
        {
            throw new NotImplementedException();
        }

        public void BuildItemDb()
        {
            List<int> missingItemIds = MissingItemIds().ToList();

            //List<Item> missingItems = _apiWrapper.ItemDetails(missingItemids).Select(ConvertToItem).ToList();

            int subsetSize = 200;

            foreach (int subsetIdx in Enumerable.Range(0, (int)Math.Ceiling(missingItemIds.Count / 200 * 1.0)))
            {
                List<int> itemIdSubset = missingItemIds.Skip(subsetIdx * subsetSize).Take(subsetSize).ToList();
                List<Item> missingItemSubset = _apiWrapper.ItemDetails(itemIdSubset).Select(ConvertToItem).ToList();
                _repository.Save(missingItemSubset);
            }
        }

        public List<int> MissingItemIds()
        {
            List<int> currentItemIds = _apiWrapper.ItemIds().ToList();
            var localItemIds = new HashSet<int>(_repository.GetAll().Select(x => x.Id));

            currentItemIds.RemoveAll(id => localItemIds.Contains(id));
            return currentItemIds;
        }

        private static Item ConvertToItem(ApiItem item)
        {
            return new Item
            {
                Id = item.Id,
                IconUrl = item.IconUrl,
                Name = item.Name,
                Rarity = item.Rarity,
                Level = item.Level,
                Type = item.Type,
                SubType = item.Details != null ? item.Details.Type : null
            };
        }

        private static DataLayer.Model.Item ConvertToBase(Item item)
        {
            return new DataLayer.Model.Item
            {
                BuyPrice = item.BuyPrice,
                CommerceDataLastUpdated = item.CommerceDataLastUpdated,
                Demand = item.Demand,
                IconUrl = item.IconUrl,
                Id = item.Id,
                Level = item.Level,
                Name = item.Name,
                PreviousBuyPrice = item.PreviousBuyPrice,
                PreviousDemand = item.PreviousDemand,
                PreviousSellPrice = item.PreviousSellPrice,
                PreviousSupply = item.PreviousSupply,
                Rarity = item.Rarity,
                SellPrice = item.SellPrice,
                SubType = item.SubType,
                Supply = item.Supply,
                Type = item.Type
            };
        }

        public Item GetItem(int id)
        {
            return new Item(_repository.Get(id));
        }


        public void UpdatePriceListings(Item item)
        {
            var updateListings = _apiWrapper.Listings(item.Id);
            item.SellOrders = updateListings.Sells.Select(x => new PriceListing { Price = x.UnitPrice, Quantity = x.Quantity }).ToList();

            item.BuyOrders = updateListings.Buys.Select(x => new PriceListing { Price = x.UnitPrice, Quantity = x.Quantity }).ToList();
        }

        public void UpdatePriceListings(List<Item> items)
        {
            throw new NotImplementedException();
        }
    }
}
