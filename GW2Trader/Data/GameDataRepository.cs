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
    public class GameDataRepository : IGameDataRepository
    {
        private readonly IGameDataContext _context;        

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
            return _context.GameItems.ToList();
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

        public void DeleteItemFromWatchlist<T>(WatchlistModel<T> watchlist, T item)
        {
            watchlist.Items.Remove(item);
        }

        private void BuildGameItemDictionary()
        {

            if (_context.GameItems != null)
            {
                _gameItems = _context.GameItems.ToDictionary(item => item.ItemId, item => item);
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

        private byte[] LoadImage(string url)
        {
            byte[] image;
            Uri uri;

            // validate url
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
            {
                using (var webClient = new WebClient())
                {
                    image = webClient.DownloadData(uri);
                }
            }
            else image = null;
            return image;
        }

        public ObservableCollection<GameItemModel> ItemCollection
        {
            get { return _context.GameItems.Local; }
        }


        public void AddToDb(GameItemModel item)
        {
            _context.GameItems.Add(item);
            _context.Save();
            BuildGameItemDictionary();
        }

        public void AddToDb(IEnumerable<GameItemModel> items)
        {
            _context.GameItems.AddRange(items);
            _context.Save();
            BuildGameItemDictionary();
        }
    }
}
