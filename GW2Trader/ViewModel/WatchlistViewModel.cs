﻿using System.Collections.Generic;
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
                WatchlistName = _selectedWatchlist == null ? null : _selectedWatchlist.Name;
           }
        }

        private string _watchlistName;
        public string WatchlistName
        {
            get { return _watchlistName; }
            set
            {
                _watchlistName = value;
                RaisePropertyChanged("WatchlistName");
            }
        }

        public void AddWatchlist()
        {
            ItemWatchlistModel newWatchlist = new ItemWatchlistModel { Name = WatchlistName };
            using (var context = _contextProvider.GetContext())
            {                
                context.ItemWatchlists.Add(newWatchlist);
                context.Save();
                newWatchlist.Id = context.ItemWatchlists.ToList().Last().Id;
            }
            Watchlists.Add(newWatchlist);
        }

        public void RemoveWatchlist(ItemWatchlistModel watchlist)
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToRemove = context.ItemWatchlists.Single(wl => wl.Id == watchlist.Id);
                context.ItemWatchlists.Remove(watchlistToRemove);
                context.Save();
            }
            _watchlists.Remove(watchlist);
            if (Watchlists.Any())
            {
                SelectedWatchlist = Watchlists.Last();
            }
        }

        public void UpdateWatchlistName()
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToUpdate = context.ItemWatchlists.Single(wl => wl.Id == SelectedWatchlist.Id);
                watchlistToUpdate.Name = WatchlistName;
                context.Save();
            }
            SelectedWatchlist.Name = WatchlistName;
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

        public void DeleteWatchlistItem(GameItemModel item)
        {
            using (var context = _contextProvider.GetContext())
            {
                ItemWatchlistModel contextWatchlist = context.ItemWatchlists.Single(wl => wl.Id == SelectedWatchlist.Id);
                var contextItem = context.GameItems.Single(i => i.ItemId == item.ItemId);
                contextWatchlist.Items.Remove(contextItem);
                context.Save();
            }
            SelectedWatchlist.Items.Remove(item);
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

        private RelayCommand _deleteWatchlistItemCommand;
        public RelayCommand DeleteWatchlistItemCommand
        {
            get { return _deleteWatchlistItemCommand ?? (_deleteWatchlistItemCommand = new DeleteWatchlistItemCommand()); }
        }

        #endregion
    }
}