using System;
using System.Collections.Generic;
using System.Linq;
using GW2Trader.ApiWrapper.Entities;
using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader_Windows.Model;
using GW2Trader.ApiWrapper.Entities;

namespace GW2Trader_Windows.Data
{
    public class DbBuilder : IDbBuilder
    {
        private readonly IGameDataContextProvider _contextProvider;
        private readonly ITradingPostApiWrapper _wrapper;

        public DbBuilder(ITradingPostApiWrapper wrapper, IGameDataContextProvider contextProvider)
        {
            _wrapper = wrapper;
            _contextProvider = contextProvider;
        }

        public void BuildDatabase()
        {
            using (var context = _contextProvider.GetContext())
            {
                if (!context.GameItems.Any())
                {
                    var ids = _wrapper.ItemIds().ToArray();
                    var items = _wrapper.ItemDetails(ids).ToList();
                    var convertedItems = items.Select(ConvertToGameItem).ToList();

                    context.BulkInsert(convertedItems);
                }
            }
        }

        private static GameItemModel ConvertToGameItem(ApiItem item)
        {
            var itemModel = new GameItemModel
            {
                ItemId = item.Id,
                IconUrl = item.IconUrl,
                Name = item.Name,
                Rarity = item.Rarity,
                RestrictionLevel = item.Level,
                Type = item.Type,
                SubType = item.Details != null ? item.Details.Type : null,
                CommerceDataLastUpdated = DateTime.Now
            };
            return itemModel;
        }


        public void RebuildDatabase()
        {
            throw new NotImplementedException();
        }

        public void UpdateDatabase()
        {
            int[] currentIds = _wrapper.ItemIds().ToArray();

            using (var context = _contextProvider.GetContext())
            {
                HashSet<int> localIds = new HashSet<int>(context.GameItems.Select(item => item.ItemId));

                List<int> missingIds = currentIds.Where(id => !localIds.Contains(id)).ToList();

                List<GameItemModel> missingItems =
                    _wrapper.ItemDetails(missingIds).Select(ConvertToGameItem).ToList();

                context.BulkInsert(missingItems);
            }
        }
    }
}