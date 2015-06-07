using System;
using System.Collections.Generic;
using DataLayer.Repository;
using System.Linq;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Model;
using GW2TPApiWrapper.Entities;

namespace GW2Trader.Manager
{
    public class ItemManager : IItemManager
    {
        private ItemRepository _repository;
        private ITradingPostApiWrapper _apiWrapper;

        public ItemManager(ItemRepository repository, ITradingPostApiWrapper apiWrapper)
        {
            _repository = repository;
            _apiWrapper = apiWrapper;
        }

        public List<Model.Item> Search(
            string keyword, string rarity = null, string type = null, 
            string subType = null, int minLevel = 0, int maxLevel = 100, 
            int minMargin = 0, int maxMargin = 0, int minRoi = 0, 
            int maxRoi = 0, int pageSize = 10, int page = 0)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrices(List<Model.Item> items)
        {
            throw new NotImplementedException();
        }

        public void BuildItemDb()
        {
            List<int> missingItemids = MissingItemIds();

            List<Item> missingItems = _apiWrapper.ItemDetails(missingItemids).Select(ConvertToItem).ToList();

            _repository.Save(missingItems);
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
    }
}
