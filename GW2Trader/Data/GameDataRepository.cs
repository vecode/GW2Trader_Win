using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using System.Collections.ObjectModel;
using GW2TPApiWrapper.Wrapper;
using System.Data.Entity;
using GW2TPApiWrapper.Entities;

namespace GW2Trader.Data
{
    public class GameDataRepository : IGameDataRepository
    {
        private IGameDataContext _context;
        private Dictionary<int, GameItemModel> _gameItems;
        private ObservableCollection<InvestmentWatchlistModel> _investmentLists;
        private ObservableCollection<ItemWatchlistModel> _itemWatchlists;

        public GameDataRepository(IGameDataContext context)
        {
            _context = context;
            _investmentLists = new ObservableCollection<InvestmentWatchlistModel>(_context.InvestmentWatchlists);
            _itemWatchlists = new ObservableCollection<ItemWatchlistModel>();

            BuildGameItemDictionary();
            BuildWatchedItemsCollection();
        }

        #region retrieving data
        public ObservableCollection<InvestmentWatchlistModel> InvestmentLists
        {
            get
            {
                return _investmentLists;
            }
        }

        public GameItemModel GameItemById(int id)
        {
            GameItemModel item;
            _gameItems.TryGetValue(id, out item);
            return item;
        }
        #endregion

        public IEnumerable<GameItemModel> GetAllGameItems()
        {
            return _context.GameItems;
        }

        public IEnumerable<GameItemModel> GameItemsById(int[] ids)
        {
            List<GameItemModel> items = new List<GameItemModel>();

            Array.ForEach(ids, id => items.Add(GameItemById(id)));
            return items;
        }

        public void DeleteWatchlist<T>(WatchlistModel<T> watchlist) where T : WatchlistModel<T>
        {
            DbSet dbset = _context.Set<T>();
            dbset.Remove(watchlist);
            _context.Save();
        }

        public void UpdateWatchlist<T>(WatchlistModel<T> watchlist)
        {
            _context.Save();
        }

        public void UpdateWatchlistItem<T>(WatchlistModel<T> watchlist, T item)
        {
            _context.Save();
        }

        public void RebuiltGameItemDatabase(ITradingPostApiWrapper tpApiWrapper)
        {
            foreach (var entity in _context.GameItems)
                _context.GameItems.Remove(entity);

            int[] itemIdsFromApi = tpApiWrapper.ItemIds();

            GameItemModel convertedItemModel;
            ItemDetails itemDetailsFromApi;

            foreach (int id in itemIdsFromApi)
            {
                itemDetailsFromApi = tpApiWrapper.ItemDetails(id);
                convertedItemModel = ConvertToGameItem(itemDetailsFromApi);
                _context.GameItems.Add(convertedItemModel);
            }
            _context.Save();
            BuildGameItemDictionary();
        }

        public void DeleteItemFromWatchlist<T>(WatchlistModel<T> watchlist, T item)
        {
            watchlist.Items.Remove(item);
        }

        private static GameItemModel ConvertToGameItem(ItemDetails item)
        {
            GameItemModel itemModel = new GameItemModel
            {
                Id = item.ID,
                IconUrl = item.IconUrl,
                Name = item.Name,
                Rarity = item.Rarity,
                RestrictionLevel = item.Level,
                Type = item.Type,
                CommerceDataLastUpdated = DateTime.Now
            };
            return itemModel;
        }

        private void BuildGameItemDictionary()
        {
            if (_context.GameItems != null)
            {
                _gameItems = _context.GameItems.ToDictionary(item => item.Id, item => item);
            }
        }

        public void AddWatchlist(ItemWatchlistModel watchlist)
        {
            _context.ItemIdWatchlists.Add(
                new ItemIdWatchlistModel
                {
                    Id = watchlist.Id,
                    Description = watchlist.Description,
                    Name = watchlist.Name
                });
            _context.Save();
            _itemWatchlists.Add(watchlist);
        }

        public void AddWatchlist(InvestmentWatchlistModel watchlist)
        {
            _context.InvestmentWatchlists.Add(watchlist);
            _context.Save();
            _investmentLists.Add(watchlist);
        }


        public ObservableCollection<ItemWatchlistModel> ItemWatchlists
        {
            get { return _itemWatchlists; }
        }

        public void AddItemToWatchlist(ItemWatchlistModel watchlist, GameItemModel item)
        {
            watchlist.Items.Add(item);
            _context.Save();
        }

        public void AddInvestmentToWatchlist(InvestmentWatchlistModel watchlist, InvestmentModel investment)
        {
            watchlist.Items.Add(investment);
            _context.Save();
        }

        private void BuildWatchedItemsCollection()
        {
            if (_itemWatchlists == null)
                _itemWatchlists = new ObservableCollection<ItemWatchlistModel>();

            List<GameItemModel> watchedItems;
            foreach (ItemIdWatchlistModel idWatchlist in _context.ItemIdWatchlists)
            {
                watchedItems = new List<GameItemModel>();
                foreach (int id in idWatchlist.Items)
                {
                    watchedItems.Add(GameItemById(id));
                }

                ItemWatchlistModel itemWatchlist = new ItemWatchlistModel();
                itemWatchlist.Items = watchedItems;
                _itemWatchlists.Add(itemWatchlist);
            }
        }
    }
}
