using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using GW2Trader.Command;
using GW2Trader.Data;
using GW2Trader.Model;

namespace GW2Trader.ViewModel
{
    public class WatchlistViewModel : BaseViewModel
    {
        private IApiDataUpdater _apiDataUpdater;
        private readonly IGameDataContextProvider _contextProvider;
        private IList<GameItemModel> _items;
        private ItemWatchlistModel _selectedWatchlist;
        private ObservableCollection<ItemWatchlistModel> _watchlists;

        public WatchlistViewModel(IGameDataContextProvider contextProvider, IList<GameItemModel> items,
            IApiDataUpdater apiDataUpdater)
        {
            ViewModelName = "Watchlists";
            _contextProvider = contextProvider;
            _apiDataUpdater = apiDataUpdater;
            _items = items;

            using (var context = contextProvider.GetContext())
            {

                Watchlists = new ObservableCollection<ItemWatchlistModel>(BuildWatchlists(context.ItemIdWatchlists.ToList(), items));
            }

            if (Watchlists.Count != 0)
            {
                SelectedWatchlist = Watchlists[0];
            }
        }

        public ObservableCollection<ItemWatchlistModel> Watchlists
        {
            get { return _watchlists; }
            set
            {
                _watchlists = value;
                RaisePropertyChanged("WatchLists");
            }
        }

        public ItemWatchlistModel SelectedWatchlist
        {
            get { return _selectedWatchlist; }
            set
            {
                _selectedWatchlist = value;
                RaisePropertyChanged("SelectedWatchlist");
            }
        }

        public void AddWatchlist(ItemWatchlistModel watchlist)
        {
            using (var context = _contextProvider.GetContext())
            {
                context.ItemIdWatchlists.Add(new ItemIdWatchlistModel
                {
                    Name = watchlist.Name,
                    Items = watchlist.Items.Select(i => i.ItemId).ToList()
                });
                context.Save();
                watchlist.Id = context.ItemIdWatchlists.ToList().Last().Id;
            }
            _watchlists.Add(watchlist);
        }

        public void RemoveWatchlist(ItemWatchlistModel watchlist)
        {
            _watchlists.Remove(watchlist);
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToRemove = context.ItemIdWatchlists.Single(wl => wl.Id == watchlist.Id);
                context.ItemIdWatchlists.Remove(watchlistToRemove);
                context.Save();
            }
        }

        public void UpdateWatchlistName(ItemWatchlistModel watchlist)
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToUpdate = context.ItemIdWatchlists.Single(wl => wl.Id == watchlist.Id);
                watchlistToUpdate.Name = watchlist.Name;
                context.Save();
            }
        }


        private IList<ItemWatchlistModel> BuildWatchlists(IList<ItemIdWatchlistModel> itemIdWatchlists,
                                                        IList<GameItemModel> items)
        {
            return itemIdWatchlists.Select(itemIdWatchlist => BuildWatchlist(itemIdWatchlist, items)).ToList();
        }

        private ItemWatchlistModel BuildWatchlist(ItemIdWatchlistModel itemIdWatchlist, IList<GameItemModel> items)
        {
            var itemWatchlist = new ItemWatchlistModel
            {
                Id = itemIdWatchlist.Id,
                Name = itemIdWatchlist.Name,
                Items = new List<GameItemModel>()
            };

            if (itemIdWatchlist.Items == null) return itemWatchlist;

            foreach (var id in itemIdWatchlist.Items)
            {
                itemWatchlist.Items.Add(items.Single(i => i.ItemId == id));
            }
            return itemWatchlist;
        }

        #region commands

        private RelayCommand _addWatchlistCommand;

        public RelayCommand AddWatchlistCommand
        {
            get { return _addWatchlistCommand ?? (_addWatchlistCommand = new AddWatchlistCommand()); }
        }

        private RelayCommand _deleteWatchlistCommand;
        public RelayCommand DeleteWatchlistCommand
        {
            get { return _deleteWatchlistCommand ?? (_deleteWatchlistCommand = new DeleteWatchlistCommand()); }
        }

        private RelayCommand _updateWatchlistNameCommand;

        public RelayCommand UpdateWatchlistNameCommand
        {
            get { return _updateWatchlistNameCommand ?? (_updateWatchlistNameCommand = new UpdateWatchlistNameCommand()); }
        }

        #endregion
    }
}