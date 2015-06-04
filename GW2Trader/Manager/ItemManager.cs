using System;
using System.Collections.Generic;
using DataLayer.Repository;

namespace GW2Trader.Manager
{
    public class ItemManager : IItemManager
    {
        private ItemRepository _repository;

        public ItemManager(ItemRepository repository)
        {
            _repository = repository;
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
    }
}
