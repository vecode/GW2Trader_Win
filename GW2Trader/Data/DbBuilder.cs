using System;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Model;

namespace GW2Trader.Data
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

        public void UpdateDatabase()
        {
            using (var context = _contextProvider.GetContext())
            {
                List<int> currentIds = _wrapper.ItemIds().ToList();
                HashSet<int> localIds = new HashSet<int>(context.GameItems.Select(item => item.ItemId));

                List<int> missingIds = new List<int>(currentIds.Count - localIds.Count);
                missingIds.AddRange(currentIds.Where(id => !localIds.Contains(id)));

                if (!missingIds.Any())
                {
                    // database is up-to-date
                    return;
                }

                List<GameItemModel> missingItems = 
                    _wrapper.ItemDetails(missingIds).Select(ConvertToGameItem).ToList();

                context.BulkInsert(missingItems);
            }
        }

        public void RebuildDatabase()
        {
            throw new NotImplementedException();
        }

        private static GameItemModel ConvertToGameItem(Item item)
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
    }
}