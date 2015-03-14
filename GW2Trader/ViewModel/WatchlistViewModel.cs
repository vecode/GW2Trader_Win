using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using GW2Trader.Command;
using GW2Trader.Data;
using GW2Trader.Model;

namespace GW2Trader.ViewModel
{
    public class WatchlistViewModel : BaseViewModel
    {
        private IApiDataUpdater _apiDataUpdater;
        private readonly IGameDataContextProvider _contextProvider;
        private ItemWatchlistModel _selectedWatchlist;
        private ObservableCollection<ItemWatchlistModel> _watchlists;

        public WatchlistViewModel(IGameDataContextProvider contextProvider,
            IApiDataUpdater apiDataUpdater)
        {
            ViewModelName = "Watchlists";
            _contextProvider = contextProvider;
            _apiDataUpdater = apiDataUpdater;

            using (var context = contextProvider.GetContext())
            {             
                var watchlists = context.ItemWatchlists.Include(wl => wl.Items).ToList();
                Watchlists = new ObservableCollection<ItemWatchlistModel>(watchlists);
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
                context.ItemWatchlists.Add(new ItemWatchlistModel
                {
                    Name = watchlist.Name,
                    Items = watchlist.Items == null ? new ObservableCollection<GameItemModel>() : new ObservableCollection<GameItemModel>(watchlist.Items.ToList())
                });
                context.Save();
                watchlist.Id = context.ItemWatchlists.ToList().Last().Id;
            }
            _watchlists.Add(watchlist);
        }

        public void RemoveWatchlist(ItemWatchlistModel watchlist)
        {
            _watchlists.Remove(watchlist);
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToRemove = context.ItemWatchlists.Single(wl => wl.Id == watchlist.Id);
                context.ItemWatchlists.Remove(watchlistToRemove);
                context.Save();
            }
        }

        public void UpdateWatchlistName(ItemWatchlistModel watchlist)
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToUpdate = context.ItemWatchlists.Single(wl => wl.Id == watchlist.Id);
                watchlistToUpdate.Name = watchlist.Name;
                context.Save();
            }
        }

        public void AddItemsToWatchlist(List<GameItemModel> itemsToAdd, ItemWatchlistModel watchlist)
        {
            using (var context = _contextProvider.GetContext())
            {
                ItemWatchlistModel contextWatchlist = context.ItemWatchlists.Single(wl => wl.Id == watchlist.Id);

                foreach (GameItemModel item in itemsToAdd)
                {
                    if (contextWatchlist.Items.All(i => i.ItemId != item.ItemId))
                    {
                        var contextItem = context.GameItems.Single(i => i.ItemId == item.ItemId);
                        contextWatchlist.Items.Add(contextItem);

                        watchlist.Items.Add(item);
                    }
                }
                context.Save();
            }
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