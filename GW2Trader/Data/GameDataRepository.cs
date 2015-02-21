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
        private ObservableCollection<ItemIdWatchlistModel> _gameItemWatchlists;

        public GameDataRepository(IGameDataContext context)
        {
            _context = context;

            BuildGameItemDictionary();

            if (_context.InvestmentWatchlists != null)
            {
                _investmentLists = new ObservableCollection<InvestmentWatchlistModel>(_context.InvestmentWatchlists);
            }
            else
            {
                _investmentLists = new ObservableCollection<InvestmentWatchlistModel>();
            }

            if (_context.ItemIdWatchlists != null)
            {
                _gameItemWatchlists = new ObservableCollection<ItemIdWatchlistModel>(_context.ItemIdWatchlists);
            }
            else
            {
                _gameItemWatchlists = new ObservableCollection<ItemIdWatchlistModel>();
            }
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

        public ObservableCollection<ItemIdWatchlistModel> ItemWatchlists
        {
            get { return _gameItemWatchlists; }
        }

        public IEnumerable<GameItemModel> GameItemsById(int[] ids)
        {
            List<GameItemModel> items = new List<GameItemModel>();

            Array.ForEach(ids, id => items.Add(GameItemById(id)));
            return items;
        }

        public void AddWatchlist<T>(WatchlistModel<T> watchlist)
        {
            _context.Set<WatchlistModel<T>>().Add(watchlist);
            _context.Save();
        }

        public void AddItemToWatchlist<T>(WatchlistModel<T> watchlist, T item)
        {
            watchlist.Items.Add(item);
            _context.Save();
        }

        public void DeleteWatchlist<T>(WatchlistModel<T> watchlist) where T : WatchlistModel<T>
        {
            DbSet dbset = _context.Set<T>();
            dbset.Remove(watchlist);
            _context.Save();
        }

        public void UpdateWatchlist<T>(WatchlistModel<T> watchlist)
        {
            throw new NotImplementedException();
        }

        public void UpdateWatchlistItem<T>(WatchlistModel<T> watchlist, T item)
        {
            throw new NotImplementedException();
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
                LastUpdated = DateTime.Now                                
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
    }
}
