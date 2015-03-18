using System;
using System.Linq;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Model;

namespace GW2Trader.Data
{
    public class DbBuilder
    {
        private readonly IGameDataContextProvider _contextProvider;
        private readonly ITradingPostApiWrapper _wrapper;

        public DbBuilder(ITradingPostApiWrapper wrapper, IGameDataContextProvider contextProvider)
        {
            _wrapper = wrapper;
            _contextProvider = contextProvider;
        }

        public void BuildDatabase(bool dropOldDb = false)
        {
            using (var context = _contextProvider.GetContext())
            {
                if (!context.GameItems.Any() || dropOldDb)
                {
                    var ids = _wrapper.ItemIds().ToArray();
                    var items = _wrapper.ItemDetails(ids).ToList();
                    var convertedItems = items.Select(ConvertToGameItem).ToList();

                    context.BulkInsert(convertedItems);
                }
            }
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
                SubType = item.Details != null? item.Details.Type : null,
                CommerceDataLastUpdated = DateTime.Now
            };
            return itemModel;
        }
    }
}