using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using System.Collections.ObjectModel;
using GW2TPApiWrapper.Entities;
using System.Data.Entity;

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

            _gameItems = new Dictionary<int, GameItemModel>();
            if (_context.GameItems != null)
            {
                _gameItems = _context.GameItems.ToDictionary(item => item.Id, item => item);
            }

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
            throw new NotImplementedException();
        }

        public void AddWatchlist<T>(WatchlistModel<T> watchlist)
        {
            throw new NotImplementedException();
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

        public bool RebuiltGameItemDatabase(ITradingPostApiWrapper tpApiWrapper)
        {
            throw new NotImplementedException();
        }

        public void DeleteItemFromWatchlist<T>(WatchlistModel<T> watchlist, T item)
        {
            throw new NotImplementedException();
        }
    }
}
