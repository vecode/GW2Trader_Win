using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using System.Collections.ObjectModel;
using GW2TPApiWrapper.Wrapper;
using System.Data.Entity;
using GW2TPApiWrapper.Entities;
using System.Net;
using System.Windows.Threading;

namespace GW2Trader.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly IGameDataContextProvider _contextProvider;

        private Dictionary<int, GameItemModel> _gameItemDictionary;
        private List<GameItemModel> _gameItems;

        public ItemRepository(IGameDataContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
            RebuildItemDictionary();
            RebuildItemList();
        }

        public GameItemModel ItemById(int id)
        {
            GameItemModel item;
            _gameItemDictionary.TryGetValue(id, out item);
            return item;
        }

        public IEnumerable<GameItemModel> Items()
        {
            return _gameItems;
        }

        public IEnumerable<GameItemModel> ItemsById(int[] ids)
        {
            List<GameItemModel> items = new List<GameItemModel>();

            Array.ForEach(ids, id => items.Add(ItemById(id)));
            return items;
        }

        public void RebuildItemDictionary()
        {
            using (var context = _contextProvider.GetContext())
            {
                _gameItemDictionary = context.GameItems.ToDictionary(item => item.ItemId, item => item);
            }
        }

        public void RebuildItemList()
        {
            _gameItems = _gameItemDictionary.Select(i => i.Value).ToList();
        }

        public void Add(GameItemModel item)
        {
            using (var context = _contextProvider.GetContext())
            {
                context.GameItems.Add(item);
                context.Save();
                RebuildItemDictionary();
            }
        }

        public void Add(IEnumerable<GameItemModel> items)
        {
            using (var context = _contextProvider.GetContext())
            {
                context.GameItems.AddRange(items);
                context.Save();
                RebuildItemDictionary();
            }
        }

        public void Update(GameItemModel item)
        {
            using (var context = _contextProvider.GetContext())
            {
                context.GameItems.Attach(item);
                var entry = context.Entry(item);
                entry.State = EntityState.Modified;
                context.Save();
            }
        }
    }
}
